Imports System.Data
Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class frmRelatorioEstoque

    ' Evita tremulação visual ao renderizar a interface
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    ' Evento disparado ao carregar o formulário
    Private Sub FrmRelatorioEstoque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()

        ' Maximiza a janela e remove bordas
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

        ' Define imagem de fundo
        Me.BackgroundImage = My.Resources.frmRelatórioEstoque
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Configura layout visual e carrega dados
        ConfigurarLayout()
        ConfigurarEstiloGrid()
        CarregarRelatorioEstoque()

        Me.ResumeLayout()
    End Sub

    ' Cria e posiciona os botões e ajusta o grid na tela
    Private Sub ConfigurarLayout()
        ' Painel principal com layout vertical
        Dim painelPrincipal As New TableLayoutPanel With {
        .Dock = DockStyle.Fill,
        .RowCount = 2,
        .ColumnCount = 1,
        .BackColor = Color.Transparent
    }
        painelPrincipal.RowStyles.Add(New RowStyle(SizeType.Percent, 90)) ' Área principal
        painelPrincipal.RowStyles.Add(New RowStyle(SizeType.Percent, 15)) ' Botões

        ' Painel para posicionar o DataGridView com margens laterais
        Dim painelGridResponsivo As New Panel With {
        .Dock = DockStyle.Fill,
        .BackColor = Color.Transparent,
        .Padding = New Padding(40, 10, 40, 10) ' margem esquerda, topo, direita, inferior
    }

        dgvEstoque.Height = 300 ' altura reduzida
        dgvEstoque.Dock = DockStyle.Bottom
        dgvEstoque.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEstoque.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvEstoque.BackgroundColor = Color.White

        painelGridResponsivo.Controls.Add(dgvEstoque)

        ' Painel para os botões distribuídos
        Dim painelBotoesInferior As New TableLayoutPanel With {
        .Dock = DockStyle.Fill,
        .ColumnCount = 3,
        .RowCount = 1,
        .BackColor = Color.Transparent,
        .Padding = New Padding(40, 5, 40, 5)
    }
        painelBotoesInferior.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33))
        painelBotoesInferior.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33))
        painelBotoesInferior.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 34))

        ' Botões compactos
        Dim btnVoltar As New Button With {
        .Text = "Voltar",
        .Dock = DockStyle.Fill,
        .Height = 28,
        .Font = New System.Drawing.Font("Segoe UI", 12, FontStyle.Bold),
        .BackColor = Color.Gray,
        .ForeColor = Color.White,
        .Margin = New Padding(5)
    }
        AddHandler btnVoltar.Click, AddressOf btnVoltar_Click

        Dim btnImprimir As New Button With {
        .Text = "Imprimir",
        .Dock = DockStyle.Fill,
        .Height = 28,
        .Font = New System.Drawing.Font("Segoe UI", 12, FontStyle.Bold),
        .BackColor = Color.Blue,
        .ForeColor = Color.White,
        .Margin = New Padding(5)
    }
        AddHandler btnImprimir.Click, AddressOf BtnImprimir_Click
        Dim btnPDF As New Button With {
        .Text = "Gerar PDF",
        .Dock = DockStyle.Fill,
        .Height = 28,
        .Font = New System.Drawing.Font("Segoe UI", 12, FontStyle.Bold),
        .BackColor = Color.Blue,
        .ForeColor = Color.White,
        .Margin = New Padding(5)
    }
        AddHandler btnPDF.Click, AddressOf BtnPDF_Click

        ' Adiciona os botões às colunas
        painelBotoesInferior.Controls.Add(btnVoltar, 0, 0)
        painelBotoesInferior.Controls.Add(btnImprimir, 1, 0)
        painelBotoesInferior.Controls.Add(btnPDF, 2, 0)

        ' Adiciona os painéis ao painel principal
        painelPrincipal.Controls.Add(painelGridResponsivo, 0, 0)
        painelPrincipal.Controls.Add(painelBotoesInferior, 0, 1)

        ' Adiciona ao formulário
        Me.Controls.Add(painelPrincipal)
    End Sub

    ' Estiliza o DataGridView para aparência profissional
    Private Sub ConfigurarEstiloGrid()
        With dgvEstoque
            .Font = New System.Drawing.Font("Segoe UI", 12)
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.BackColor = Color.DarkRed
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .EnableHeadersVisualStyles = False
            .DefaultCellStyle.BackColor = Color.White
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.SelectionBackColor = Color.LightGray
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight ' Alinhamento à direita
            .BorderStyle = BorderStyle.FixedSingle
            .RowHeadersVisible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub


    ' Carrega os dados do estoque no grid
    Private Sub CarregarRelatorioEstoque()
        Try
            ' Consulta SQL com colunas reorganizadas conforme solicitado
            Dim sql As String = "
SELECT 
    p.codigo_barras AS Codigo_Barras,
    e.lote AS Lote,
    p.nome AS Produto,
    e.data_validade AS Data_Validade,
    e.quantidade AS Quantidade,
    p.fornecedor_nome AS Fornecedor
FROM Estoque e
INNER JOIN Produtos p ON e.produto_id = p.produto_id
WHERE e.data_validade IS NOT NULL
ORDER BY e.rowid DESC"

            ' Executa consulta e vincula ao grid
            Dim dt As DataTable = DBHelper.Consultar(sql)
            dgvEstoque.DataSource = dt

            ' Formata colunas específicas
            dgvEstoque.Columns("Quantidade").DefaultCellStyle.Format = "N0"
            dgvEstoque.Columns("Data_Validade").DefaultCellStyle.Format = "dd/MM/yyyy"

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar relatório de estoque: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' Fecha o formulário
    Private Sub BtnVoltar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    ' Gera PDF com os dados do DataGridView e exibe diretamente na tela
    Private Sub BtnPDF_Click(sender As Object, e As EventArgs)
        Try
            Dim caminho As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RelatorioVencimentos.pdf")

            ' Criação do documento PDF
            Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            PdfWriter.GetInstance(doc, New FileStream(caminho, FileMode.Create))
            doc.Open()

            ' Título
            Dim titulo As New iTextSharp.text.Paragraph("Relatório de Vencimentos") With {
            .Alignment = Element.ALIGN_CENTER
        }
            doc.Add(titulo)
            doc.Add(New iTextSharp.text.Paragraph(" "))

            ' Tabela com colunas reorganizadas manualmente
            Dim tabela As New iTextSharp.text.pdf.PdfPTable(6)
            tabela.WidthPercentage = 100

            ' Cabeçalhos na ordem desejada
            Dim headers As String() = {"Código de Barras", "Lote", "Produto", "Data de Validade", "Quantidade", "Fornecedor"}
            For Each header As String In headers
                Dim cell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(header)) With {
                .BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY
            }
                tabela.AddCell(cell)
            Next

            ' Linhas com dados na mesma ordem
            For Each row As DataGridViewRow In dgvEstoque.Rows
                If Not row.IsNewRow Then
                    tabela.AddCell(row.Cells("Codigo_Barras").Value?.ToString())
                    tabela.AddCell(row.Cells("Lote").Value?.ToString())
                    tabela.AddCell(row.Cells("Produto").Value?.ToString())
                    tabela.AddCell(Convert.ToDateTime(row.Cells("Data_Validade").Value).ToString("dd/MM/yyyy"))
                    tabela.AddCell(row.Cells("Quantidade").Value?.ToString())
                    tabela.AddCell(row.Cells("Fornecedor").Value?.ToString())
                End If
            Next

            doc.Add(tabela)
            doc.Close()

            ' Abre o arquivo PDF automaticamente
            Process.Start("explorer.exe", caminho)
        Catch ex As Exception
            MessageBox.Show("Erro ao exportar PDF: " & ex.Message)
        End Try
    End Sub




    ' Evento que desenha a página para impressão
    Private Sub BtnImprimir_Click(sender As Object, e As PrintPageEventArgs)
        Dim fonte As New System.Drawing.Font("Segoe UI", 10)
        Dim y As Integer = 100
        Dim x As Integer = 50

        ' Cabeçalhos na ordem desejada
        Dim headers As String() = {"Código de Barras", "Lote", "Produto", "Data de Validade", "Quantidade", "Fornecedor"}
        For Each header As String In headers
            e.Graphics.DrawString(header, fonte, Brushes.Black, x, y)
            x += 150 ' espaçamento horizontal entre colunas
        Next

        y += 30 ' espaço abaixo dos cabeçalhos

        ' Dados na mesma ordem
        For Each linha As DataGridViewRow In dgvEstoque.Rows
            If Not linha.IsNewRow Then
                x = 50
                e.Graphics.DrawString(linha.Cells("Codigo_Barras").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Lote").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Produto").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(Convert.ToDateTime(linha.Cells("Data_Validade").Value).ToString("dd/MM/yyyy"), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Quantidade").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Fornecedor").Value?.ToString(), fonte, Brushes.Black, x, y)

                y += 25 ' espaço entre linhas
            End If
        Next
    End Sub


    ' Evento que desenha a página para impressão
    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim fonte As New System.Drawing.Font("Segoe UI", 10)
        Dim y As Integer = 100
        Dim x As Integer = 50

        ' Cabeçalhos na ordem desejada
        Dim headers As String() = {"Código de Barras", "Lote", "Produto", "Data de Validade", "Quantidade", "Fornecedor"}
        For Each header As String In headers
            e.Graphics.DrawString(header, fonte, Brushes.Black, x, y)
            x += 150
        Next

        y += 30

        ' Dados na mesma ordem
        For Each linha As DataGridViewRow In dgvEstoque.Rows
            If Not linha.IsNewRow Then
                x = 50
                e.Graphics.DrawString(linha.Cells("Codigo_Barras").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Lote").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Produto").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(Convert.ToDateTime(linha.Cells("Data_Validade").Value).ToString("dd/MM/yyyy"), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Quantidade").Value?.ToString(), fonte, Brushes.Black, x, y)
                x += 150
                e.Graphics.DrawString(linha.Cells("Fornecedor").Value?.ToString(), fonte, Brushes.Black, x, y)

                y += 25
            End If
        Next
    End Sub


End Class
