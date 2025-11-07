Imports System.Data
Imports System.Data.SQLite
Imports System.Diagnostics
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class frmRelatorioVencimento
    Inherits Form

    ' Controles principais
    Private dgvRelatorio As DataGridView
    Private btnExportarExcel, btnExportarPDF, btnImprimir, btnVoltar As Button
    Private btnEmDia, btnVencidos, btnSeteDias, btnQuinzeDias As Button

    ' Evita flickering visual
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    ' Inicialização do formulário
    Public Sub New()
        Me.Text = "Relatório de Vencimentos"
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.BackgroundImage = My.Resources.frmRelatórioVencimento

        InicializarComponentes()
        CarregarRelatorio("emdia")
    End Sub

    Private Sub InicializarComponentes()
        ' Criação do DataGridView com altura reduzida para liberar espaço inferior
        Dim alturaGrid As Integer = CInt(Me.ClientSize.Height * 0.9) ' 90% da altura da janela
        Dim margemLateral As Integer = 40
        Dim margemInferiorGrid As Integer = 90 ' espaço acima dos botões

        dgvRelatorio = New DataGridView With {
    .Location = New Point(margemLateral, Me.ClientSize.Height - alturaGrid - margemInferiorGrid),
    .Size = New Size(Me.ClientSize.Width - (2 * margemLateral), alturaGrid),
    .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right,
    .ReadOnly = True,
    .AllowUserToAddRows = False,
    .AllowUserToDeleteRows = False,
    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
}
        Me.Controls.Add(dgvRelatorio)

        ' Estilização dos cabeçalhos do grid
        With dgvRelatorio.ColumnHeadersDefaultCellStyle
            .BackColor = Color.RoyalBlue          ' Fundo azul
            .ForeColor = Color.White              ' Texto branco
            .Font = New System.Drawing.Font("Segoe UI", 10, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter ' Centraliza o texto
        End With

        dgvRelatorio.EnableHeadersVisualStyles = False


        ' Lista de botões e seus eventos (com "Voltar" por último)
        Dim botoes As New List(Of Tuple(Of String, EventHandler)) From {
        New Tuple(Of String, EventHandler)("Exportar PDF", AddressOf BtnExportarPDF_Click),
        New Tuple(Of String, EventHandler)("Exportar Excel", AddressOf BtnExportarExcel_Click),
        New Tuple(Of String, EventHandler)("Imprimir", AddressOf BtnImprimir_Click),
        New Tuple(Of String, EventHandler)("Em Dias", AddressOf BtnEmDia_Click),
        New Tuple(Of String, EventHandler)("Vencidos", AddressOf BtnVencidos_Click),
        New Tuple(Of String, EventHandler)("7 dias", AddressOf BtnSeteDias_Click),
        New Tuple(Of String, EventHandler)("15 dias", AddressOf BtnQuinzeDias_Click),
        New Tuple(Of String, EventHandler)("Voltar", AddressOf BtnVoltar_Click) ' último botão
    }

        ' Estilo visual dos botões
        Dim fonteBotao As New System.Drawing.Font("Segoe UI", 12, FontStyle.Bold)
        Dim corFundoPadrao As Color = Color.RoyalBlue
        Dim corTextoPadrao As Color = Color.White
        Dim corFundoVoltar As Color = Color.Gray

        ' Cálculo de posicionamento inferior
        Dim margemInferior As Integer = 20
        Dim espacamento As Integer = 12
        Dim larguraBotao As Integer = 140
        Dim alturaBotao As Integer = 50
        Dim totalBotoes As Integer = botoes.Count
        Dim larguraTotal As Integer = (larguraBotao * totalBotoes) + (espacamento * (totalBotoes - 1))
        Dim xInicial As Integer = (Me.ClientSize.Width - larguraTotal) \ 2
        Dim yPosicao As Integer = Me.ClientSize.Height - alturaBotao - margemInferior

        ' Criação dos botões com estilo e posicionamento
        Dim xPosicao As Integer = xInicial
        For Each item In botoes
            Dim corFundo As Color = If(item.Item1 = "Voltar", corFundoVoltar, corFundoPadrao)
            Dim btn As New Button With {
            .Text = item.Item1,
            .Size = New Size(larguraBotao, alturaBotao),
            .Location = New Point(xPosicao, yPosicao),
            .Anchor = AnchorStyles.Bottom,
            .BackColor = corFundo,
            .ForeColor = corTextoPadrao,
            .Font = fonteBotao,
            .FlatStyle = FlatStyle.Flat
        }
            btn.FlatAppearance.BorderSize = 0

            ' Efeito hover (exceto para botão Voltar)
            If item.Item1 <> "Voltar" Then
                AddHandler btn.MouseEnter, Sub(s, e) CType(s, Button).BackColor = Color.DodgerBlue
                AddHandler btn.MouseLeave, Sub(s, e) CType(s, Button).BackColor = corFundoPadrao
            End If

            AddHandler btn.Click, item.Item2
            Me.Controls.Add(btn)
            xPosicao += larguraBotao + espacamento
        Next
    End Sub

    ' Método auxiliar para criar botões
    Private Function CriarBotao(texto As String, posicao As Point, eventoClick As EventHandler) As Button
        Dim btn As New Button With {
            .Text = texto,
            .Size = New Size(120, 30),
            .Location = posicao
        }
        AddHandler btn.Click, eventoClick
        Me.Controls.Add(btn)
        Return btn
    End Function

    ' Carrega os dados filtrados no grid
    Public Sub CarregarRelatorio(filtro As String)
        Try
            ' Consulta SQL original
            Dim sql As String = "
            SELECT 
                p.nome AS produto, 
                p.codigo_barras, 
                l.codigo_lote,
                l.data_validade, 
                l.quantidade, 
                julianday(date(l.data_validade)) - julianday(date('now')) AS dias_restantes 
            FROM Produtos p
            INNER JOIN Lotes l ON p.produto_id = l.produto_id
            WHERE l.data_validade IS NOT NULL
              AND l.quantidade > 0"

            Dim dt As DataTable = DBHelper.Consultar(sql)

            ' Criação do DataTable filtrado
            Dim dtFiltrado As New DataTable()
            dtFiltrado.Columns.Add("produto", GetType(String))
            dtFiltrado.Columns.Add("codigo_barras", GetType(String))
            dtFiltrado.Columns.Add("codigo_lote", GetType(String))
            dtFiltrado.Columns.Add("data_validade", GetType(Date))
            dtFiltrado.Columns.Add("quantidade", GetType(Integer))
            dtFiltrado.Columns.Add("dias_restantes", GetType(Integer))
            dtFiltrado.Columns.Add("alerta", GetType(String))

            ' Preenchimento do DataTable filtrado com base no filtro
            For Each row As DataRow In dt.Rows
                If IsDBNull(row("data_validade")) OrElse IsDBNull(row("dias_restantes")) Then Continue For

                Dim diasRestantesRaw As Double
                If Double.TryParse(row("dias_restantes").ToString(), diasRestantesRaw) Then
                    Dim diasRestantesInt As Integer = Math.Floor(diasRestantesRaw)
                    Dim incluir As Boolean = False

                    Select Case filtro
                        Case "vencidos"
                            incluir = diasRestantesInt < 0
                        Case "sete"
                            incluir = diasRestantesInt >= 0 AndAlso diasRestantesInt <= 7
                        Case "quinze"
                            incluir = diasRestantesInt > 7 AndAlso diasRestantesInt <= 15
                        Case "emdia"
                            incluir = diasRestantesInt > 15
                    End Select

                    Dim alertaTexto As String = ""
                    Select Case diasRestantesInt
                        Case < 0 : alertaTexto = "Vencido"
                        Case 0 To 7 : alertaTexto = "Até 7 dias"
                        Case 8 To 15 : alertaTexto = "Até 15 dias"
                        Case Else : alertaTexto = "Em dia"
                    End Select

                    If incluir Then
                        dtFiltrado.Rows.Add(
                        row("produto").ToString(),
                        row("codigo_barras").ToString(),
                        row("codigo_lote").ToString(),
                        Convert.ToDateTime(row("data_validade")),
                        Convert.ToInt32(row("quantidade")),
                        diasRestantesInt,
                        alertaTexto
                    )
                    End If
                End If
            Next

            ' Ordena os dados pela coluna "data_validade" em ordem crescente
            Dim dtOrdenado As DataView = dtFiltrado.DefaultView
            dtOrdenado.Sort = "data_validade ASC"
            dgvRelatorio.DataSource = dtOrdenado.ToTable()

            ' Formatação e ordenação das colunas
            dgvRelatorio.Columns("dias_restantes").DefaultCellStyle.Format = "N0"
            dgvRelatorio.Columns("data_validade").DefaultCellStyle.Format = "dd/MM/yyyy"

            With dgvRelatorio
                .Columns("codigo_barras").DisplayIndex = 0
                .Columns("codigo_lote").DisplayIndex = 1
                .Columns("produto").DisplayIndex = 2
                .Columns("data_validade").DisplayIndex = 3
                .Columns("quantidade").DisplayIndex = 4
                .Columns("dias_restantes").DisplayIndex = 5
                .Columns("alerta").DisplayIndex = 6

                .Columns("codigo_barras").HeaderText = "Código de Barras"
                .Columns("codigo_lote").HeaderText = "Lote"
                .Columns("produto").HeaderText = "Produto"
                .Columns("data_validade").HeaderText = "Validade"
                .Columns("quantidade").HeaderText = "Qtd em Estoque"
                .Columns("dias_restantes").HeaderText = "Dias Restantes"
                .Columns("alerta").HeaderText = "Status"
            End With

            For Each col As DataGridViewColumn In dgvRelatorio.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Next

            For Each col As DataGridViewColumn In dgvRelatorio.Columns
                col.HeaderText = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(col.HeaderText.ToLower())
            Next

            AplicarCores()
        Catch ex As Exception
            MessageBox.Show("Erro ao carregar relatório: " & ex.Message)
        End Try
    End Sub


    ' Aplica cores às linhas com base nos dias restantes
    Private Sub AplicarCores()
        For Each row As DataGridViewRow In dgvRelatorio.Rows
            Try
                '  Verifica se a coluna "dias_restantes" existe
                If dgvRelatorio.Columns.Contains("dias_restantes") AndAlso row.Cells("dias_restantes").Value IsNot Nothing Then
                    Dim diasRestantes As Integer = Convert.ToInt32(row.Cells("dias_restantes").Value)
                    Dim corBase As Color

                    '  Define cor de fundo com base na validade
                    Select Case diasRestantes
                        Case < 0
                            corBase = Color.Red
                            row.DefaultCellStyle.ForeColor = Color.White '  Texto branco para vencidos

                        Case <= 7
                            corBase = Color.Orange

                        Case <= 15
                            corBase = Color.Yellow

                        Case Else
                            corBase = Color.LightGreen
                    End Select

                    row.DefaultCellStyle.BackColor = corBase

                    '  Aplica negrito em todas as células da linha, independentemente da faixa de validade
                    For Each cell As DataGridViewCell In row.Cells
                        Try
                            cell.Style.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
                        Catch ex As Exception
                            cell.Style.Font = New System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
                        End Try
                    Next

                End If

                '  Verifica se a coluna "quantidade" existe
                If dgvRelatorio.Columns.Contains("quantidade") AndAlso row.Cells("quantidade").Value IsNot Nothing Then
                    Dim qtd As Integer = Convert.ToInt32(row.Cells("quantidade").Value)

                    '  Destaque para baixo estoque (≤ 5 unidades)
                    If qtd <= 5 Then
                        For Each cell As DataGridViewCell In row.Cells
                            cell.Style.Font = New System.Drawing.Font("Segoe UI", 10, FontStyle.Bold)
                            cell.Style.ForeColor = Color.DarkRed
                        Next
                    End If
                End If

            Catch ex As Exception
                '  Ignora erros silenciosamente para evitar travamentos
            End Try
        Next
    End Sub

    ' Eventos dos botões de filtro
    Private Sub BtnEmDia_Click(sender As Object, e As EventArgs)
        CarregarRelatorio("emdia")
    End Sub

    Private Sub BtnVencidos_Click(sender As Object, e As EventArgs)
        CarregarRelatorio("vencidos")
    End Sub

    Private Sub BtnSeteDias_Click(sender As Object, e As EventArgs)
        CarregarRelatorio("sete")
    End Sub

    Private Sub BtnQuinzeDias_Click(sender As Object, e As EventArgs)
        CarregarRelatorio("quinze")
    End Sub

    ' Botão: Voltar
    Private Sub BtnVoltar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    ' Botão: Exportar Excel (CSV)
    Private Sub BtnExportarExcel_Click(sender As Object, e As EventArgs)
        Try
            Dim caminho As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RelatorioVencimentos.csv")
            Using sw As New StreamWriter(caminho, False, Encoding.UTF8)

                ' Cabeçalhos incluindo 'codigo_lote' (mantém ordem do grid)
                For Each col As DataGridViewColumn In dgvRelatorio.Columns.Cast(Of DataGridViewColumn).OrderBy(Function(c) c.DisplayIndex)
                    sw.Write(col.HeaderText & ";")
                Next
                sw.WriteLine()

                ' Ordena as linhas pela coluna "data_validade" antes de exportar
                Dim linhasOrdenadas = dgvRelatorio.Rows.Cast(Of DataGridViewRow) _
                .Where(Function(r) Not r.IsNewRow) _
                .OrderBy(Function(r) Convert.ToDateTime(r.Cells("data_validade").Value))

                ' Dados incluindo 'codigo_lote' (mantém ordem do grid)
                For Each row As DataGridViewRow In linhasOrdenadas
                    For Each col As DataGridViewColumn In dgvRelatorio.Columns.Cast(Of DataGridViewColumn).OrderBy(Function(c) c.DisplayIndex)
                        sw.Write(row.Cells(col.Name).Value?.ToString() & ";")
                    Next
                    sw.WriteLine()
                Next
            End Using

            ' Abre o arquivo automaticamente
            Process.Start("explorer.exe", caminho)
        Catch ex As Exception
            MessageBox.Show("Erro ao exportar: " & ex.Message)
        End Try
    End Sub

    ' Botão: Exportar PDF (usando iTextSharp)
    Private Sub BtnExportarPDF_Click(sender As Object, e As EventArgs)
        Try
            Dim caminho As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RelatorioVencimentos.pdf")

            Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            PdfWriter.GetInstance(doc, New FileStream(caminho, FileMode.Create))
            doc.Open()

            '  Título
            Dim titulo As New iTextSharp.text.Paragraph("Relatório de Vencimentos") With {
            .Alignment = Element.ALIGN_CENTER
        }
            doc.Add(titulo)
            doc.Add(New iTextSharp.text.Paragraph(" "))

            '  Tabela com nova coluna 'codigo_lote'
            Dim tabela As New iTextSharp.text.pdf.PdfPTable(dgvRelatorio.Columns.Count)
            tabela.WidthPercentage = 100

            ' Cabeçalhos
            For Each col As DataGridViewColumn In dgvRelatorio.Columns.Cast(Of DataGridViewColumn).OrderBy(Function(c) c.DisplayIndex)
                Dim cell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(col.HeaderText)) With {
                .BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY
            }
                tabela.AddCell(cell)
            Next

            'Ordena as linhas pela coluna "data_validade" antes de exportar
            Dim linhasOrdenadas = dgvRelatorio.Rows.Cast(Of DataGridViewRow) _
            .Where(Function(r) Not r.IsNewRow) _
            .OrderBy(Function(r) Convert.ToDateTime(r.Cells("data_validade").Value))

            ' Linhas
            For Each row As DataGridViewRow In linhasOrdenadas
                For Each col As DataGridViewColumn In dgvRelatorio.Columns.Cast(Of DataGridViewColumn).OrderBy(Function(c) c.DisplayIndex)
                    Dim texto As String = row.Cells(col.Name).Value?.ToString()
                    Dim fonteCelula As iTextSharp.text.Font = If(col.Name = "quantidade",
                    iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD),
                    iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL))
                    tabela.AddCell(New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(texto, fonteCelula)))
                Next
            Next

            doc.Add(tabela)
            doc.Close()

            '  Abre o PDF automaticamente
            Process.Start("explorer.exe", caminho)
        Catch ex As Exception
            MessageBox.Show("Erro ao exportar PDF: " & ex.Message)
        End Try
    End Sub

    ' Botão: Imprimir
    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs)
        Try
            Dim pd As New PrintDocument()
            AddHandler pd.PrintPage, AddressOf PrintPageHandler

            Dim dlg As New PrintDialog With {
                .Document = pd
            }

            If dlg.ShowDialog() = DialogResult.OK Then
                pd.Print()
            End If
        Catch ex As Exception
            MessageBox.Show("Erro ao imprimir: " & ex.Message)
        End Try
    End Sub
    ' Renderiza o conteúdo do DataGridView para impressão
    Private currentRowIndex As Integer = 0 ' Controla a linha atual entre páginas
    Private currentPageNumber As Integer = 1 ' Controla o número da página
    Private linhasOrdenadasParaImpressao As List(Of DataGridViewRow) '  Armazena as linhas ordenadas para impressão

    ' Método para impressão
    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim fontePadrao As New System.Drawing.Font("Arial", 10)
        Dim fonteCabecalho As New System.Drawing.Font("Arial", 12, FontStyle.Bold)
        Dim fonteRodape As New System.Drawing.Font("Arial", 9, FontStyle.Italic)
        Dim fonteNegrito As New System.Drawing.Font("Arial", 10, FontStyle.Bold) ' ✅ Fonte em negrito para quantidade

        Dim margemEsquerda As Integer = e.MarginBounds.Left
        Dim margemTopo As Integer = e.MarginBounds.Top
        Dim larguraDisponivel As Integer = e.MarginBounds.Width
        Dim alturaLinha As Integer = 30
        Dim yPos As Integer = margemTopo
        Dim xPos As Integer = margemEsquerda

        ' Cabeçalho com data e título
        Dim dataAtual As String = DateTime.Now.ToString("dd/MM/yyyy")
        e.Graphics.DrawString("Relatório de Vencimentos", fonteCabecalho, Brushes.Black, margemEsquerda, yPos)
        e.Graphics.DrawString("Data: " & dataAtual, fontePadrao, Brushes.Black, margemEsquerda + 400, yPos)
        yPos += alturaLinha

        ' Calcula largura proporcional para cada coluna (mantendo ordem do grid)
        Dim colunasOrdenadas = dgvRelatorio.Columns.Cast(Of DataGridViewColumn) _
            .OrderBy(Function(c) c.DisplayIndex).ToList()
        Dim totalColunas As Integer = colunasOrdenadas.Count
        Dim larguraColuna As Integer = larguraDisponivel \ totalColunas

        ' Cabeçalhos das colunas (mantendo ordem do grid)
        For Each col As DataGridViewColumn In colunasOrdenadas
            e.Graphics.DrawString(col.HeaderText, fontePadrao, Brushes.Black, xPos, yPos)
            xPos += larguraColuna
        Next
        yPos += alturaLinha
        xPos = margemEsquerda

        ' Inicializa e ordena as linhas apenas na primeira página
        If linhasOrdenadasParaImpressao Is Nothing Then
            linhasOrdenadasParaImpressao = dgvRelatorio.Rows.Cast(Of DataGridViewRow) _
                .Where(Function(r) Not r.IsNewRow) _
                .OrderBy(Function(r) Convert.ToDateTime(r.Cells("data_validade").Value)) _
                .ToList()
        End If

        ' Linhas com quebra de página
        While currentRowIndex < linhasOrdenadasParaImpressao.Count
            Dim row As DataGridViewRow = linhasOrdenadasParaImpressao(currentRowIndex)

            If yPos + alturaLinha > e.MarginBounds.Bottom - 40 Then
                e.HasMorePages = True
                currentPageNumber += 1
                Return
            End If

            ' Dados das células (mantendo ordem do grid)
            For Each col As DataGridViewColumn In colunasOrdenadas
                Dim texto As String = row.Cells(col.Name).Value?.ToString()

                ' Se for a coluna "quantidade", imprime em negrito
                If col.Name = "quantidade" Then
                    e.Graphics.DrawString(texto, fonteNegrito, Brushes.Black, xPos, yPos)
                Else
                    e.Graphics.DrawString(texto, fontePadrao, Brushes.Black, xPos, yPos)
                End If

                xPos += larguraColuna
            Next

            yPos += alturaLinha
            xPos = margemEsquerda
            currentRowIndex += 1
        End While

        ' Rodapé com número da página
        Dim rodapeY As Integer = e.MarginBounds.Bottom + 10
        e.Graphics.DrawString("Página " & currentPageNumber.ToString(), fonteRodape, Brushes.Black, margemEsquerda, rodapeY)

        ' Finaliza impressão
        e.HasMorePages = False
        currentRowIndex = 0
        currentPageNumber = 1
        linhasOrdenadasParaImpressao = Nothing '  Limpa para próxima impressão
    End Sub

End Class