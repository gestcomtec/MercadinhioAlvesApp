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
    Private Sub frmRelatorioEstoque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        AddHandler btnImprimir.Click, AddressOf btnImprimir_Click

        Dim btnPDF As New Button With {
        .Text = "Gerar PDF",
        .Dock = DockStyle.Fill,
        .Height = 28,
        .Font = New System.Drawing.Font("Segoe UI", 12, FontStyle.Bold),
        .BackColor = Color.Blue,
        .ForeColor = Color.White,
        .Margin = New Padding(5)
    }
        AddHandler btnPDF.Click, AddressOf btnPDF_Click

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
            .BorderStyle = BorderStyle.FixedSingle
            .RowHeadersVisible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    ' Carrega os dados do estoque no grid
    Private Sub CarregarRelatorioEstoque()
        Try
            Dim sql As String = "
SELECT 
    p.nome AS Produto,
    p.codigo_barras AS Codigo_Barras,
    e.quantidade AS Quantidade,
    e.lote AS Lote,
    e.data_validade AS Data_Validade,
    p.fornecedor_nome AS Fornecedor
    FROM Estoque e
    INNER JOIN Produtos p ON e.produto_id = p.produto_id
    WHERE e.data_validade IS NOT NULL
    ORDER BY e.rowid DESC"


            Dim dt As DataTable = DBHelper.Consultar(sql)
            dgvEstoque.DataSource = dt

            ' Formata colunas específicas
            dgvEstoque.Columns("quantidade").DefaultCellStyle.Format = "N0"
            dgvEstoque.Columns("data_validade").DefaultCellStyle.Format = "dd/MM/yyyy"

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar relatório de estoque: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Fecha o formulário
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    ' Gera PDF com os dados do DataGridView
    Private Sub btnPDF_Click(sender As Object, e As EventArgs)
        Try
            Dim doc As New Document(PageSize.A4)
            Dim caminhoPDF As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RelatorioEstoque.pdf")
            PdfWriter.GetInstance(doc, New FileStream(caminhoPDF, FileMode.Create))
            doc.Open()

            Dim tabela As New PdfPTable(dgvEstoque.Columns.Count)
            tabela.WidthPercentage = 100

            ' Cabeçalhos
            For Each coluna As DataGridViewColumn In dgvEstoque.Columns
                tabela.AddCell(New Phrase(coluna.HeaderText, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
            Next

            ' Dados
            For Each linha As DataGridViewRow In dgvEstoque.Rows
                If Not linha.IsNewRow Then
                    For Each celula As DataGridViewCell In linha.Cells
                        tabela.AddCell(New Phrase(celula.Value?.ToString(), FontFactory.GetFont("Arial", 10)))
                    Next
                End If
            Next

            doc.Add(tabela)
            doc.Close()

            MessageBox.Show("PDF gerado com sucesso na área de trabalho!", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Erro ao gerar PDF: " & ex.Message)
        End Try
    End Sub

    ' Imprime os dados do DataGridView
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs)
        Try
            Dim printDoc As New PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintPageHandler
            Dim printDialog As New PrintDialog With {.Document = printDoc}

            If printDialog.ShowDialog() = DialogResult.OK Then
                printDoc.Print()
            End If
        Catch ex As Exception
            MessageBox.Show("Erro ao imprimir: " & ex.Message)
        End Try
    End Sub

    ' Evento que desenha a página para impressão
    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim fonte As New System.Drawing.Font("Segoe UI", 10)
        Dim y As Integer = 100

        ' Cabeçalhos
        For Each coluna As DataGridViewColumn In dgvEstoque.Columns
            e.Graphics.DrawString(coluna.HeaderText, fonte, Brushes.Black, 50, y)
            y += 20
        Next

        y += 20

        ' Dados
        For Each linha As DataGridViewRow In dgvEstoque.Rows
            If Not linha.IsNewRow Then
                For Each celula As DataGridViewCell In linha.Cells
                    e.Graphics.DrawString(celula.Value?.ToString(), fonte, Brushes.Black, 50, y)
                    y += 20
                Next
                y += 10
            End If
        Next
    End Sub

    ' Controle do grid já existente no Designer
    ' Certifique-se de que o nome seja exatamente "dgvEstoque"
    ' e que não esteja duplicado no código

End Class
