Imports System.Data.SQLite
Imports MercadinhoNewApp.Models

Public Class frmCadastroProduto

    ' Controles declarados manualmente
    Private txtNomeProduto, txtCodigoBarrasProduto, txtDescricaoProduto As TextBox
    Private txtUnidadeMedidaProduto, txtCodigoLoteProduto, txtMarcaProduto, txtFornecedorProduto As TextBox
    Private txtQuantidadeProduto As TextBox
    Private comboCategoriaProduto As ComboBox
    Private dtpValidadeProduto As DateTimePicker
    Private btnSalvarProduto, btnVoltarCadastro As Button

    ' Evita flickering na renderização
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    ' Evento de carregamento do formulário
    Private Sub frmCadastroProduto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()

        ' Tela cheia e imagem de fundo
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackgroundImage = My.Resources.frmCadastroProdutos
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Painel centralizado para os campos
        Dim painelCampos As New Panel With {
            .Size = New Size(800, 350),
            .BackColor = Color.FromArgb(200, Color.White),
            .Location = New Point((Me.ClientSize.Width - 800) \ 2, Me.ClientSize.Height - 350),
            .Anchor = AnchorStyles.Bottom
        }
        Me.Controls.Add(painelCampos)

        ' Instanciação dos controles
        txtNomeProduto = New TextBox()
        txtCodigoBarrasProduto = New TextBox()
        txtDescricaoProduto = New TextBox()
        txtUnidadeMedidaProduto = New TextBox()
        txtCodigoLoteProduto = New TextBox()
        txtMarcaProduto = New TextBox()
        txtFornecedorProduto = New TextBox()
        txtQuantidadeProduto = New TextBox()
        comboCategoriaProduto = New ComboBox()
        dtpValidadeProduto = New DateTimePicker()
        btnSalvarProduto = New Button()
        btnVoltarCadastro = New Button()

        ' Adiciona campos com rótulos
        AdicionarCampoComLabel(painelCampos, "Nome do Produto:", txtNomeProduto, 50, 30, 330)
        AdicionarCampoComLabel(painelCampos, "Código de Barras:", txtCodigoBarrasProduto, 420, 30, 330)
        AdicionarCampoComLabel(painelCampos, "Descrição:", txtDescricaoProduto, 50, 90, 700)
        AdicionarCampoComLabel(painelCampos, "Unidade de Medida:", txtUnidadeMedidaProduto, 50, 160, 160)
        AdicionarCampoComLabel(painelCampos, "Código do Lote:", txtCodigoLoteProduto, 230, 160, 160)
        AdicionarCampoComLabel(painelCampos, "Marca:", txtMarcaProduto, 410, 160, 160)
        AdicionarCampoComLabel(painelCampos, "Fornecedor:", txtFornecedorProduto, 590, 160, 160)
        AdicionarCampoComLabel(painelCampos, "Categoria:", comboCategoriaProduto, 100, 230, 220)
        AdicionarCampoComLabel(painelCampos, "Quantidade:", txtQuantidadeProduto, 340, 230, 160)
        AdicionarCampoComLabel(painelCampos, "Data de Vencimento:", dtpValidadeProduto, 520, 230, 230)

        ' Botão: Salvar
        btnSalvarProduto.Text = "Salvar"
        btnSalvarProduto.Location = New Point(200, 280)
        btnSalvarProduto.Size = New Size(180, 40)
        btnSalvarProduto.BackColor = Color.FromArgb(0, 120, 215)
        btnSalvarProduto.ForeColor = Color.White
        btnSalvarProduto.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnSalvarProduto.FlatStyle = FlatStyle.Flat
        btnSalvarProduto.FlatAppearance.BorderSize = 0
        AddHandler btnSalvarProduto.Click, AddressOf btnSalvarProduto_Click
        painelCampos.Controls.Add(btnSalvarProduto)

        ' Botão: Voltar
        btnVoltarCadastro.Text = "Voltar"
        btnVoltarCadastro.Location = New Point(420, 280)
        btnVoltarCadastro.Size = New Size(180, 40)
        btnVoltarCadastro.BackColor = Color.Gray
        btnVoltarCadastro.ForeColor = Color.White
        btnVoltarCadastro.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnVoltarCadastro.FlatStyle = FlatStyle.Flat
        btnVoltarCadastro.FlatAppearance.BorderSize = 0
        AddHandler btnVoltarCadastro.Click, AddressOf btnVoltarCadastro_Click
        painelCampos.Controls.Add(btnVoltarCadastro)

        ' Carrega categorias no ComboBox
        Dim dtCat = DBHelper.Consultar("SELECT categoria_id, nome FROM Categorias")
        comboCategoriaProduto.DataSource = dtCat
        comboCategoriaProduto.DisplayMember = "nome"
        comboCategoriaProduto.ValueMember = "categoria_id"
    End Sub

    ' Função auxiliar para adicionar rótulo e campo
    Private Sub AdicionarCampoComLabel(painel As Panel, textoLabel As String, controle As Control, x As Integer, y As Integer, largura As Integer)
        Dim lbl As New Label With {
            .Text = textoLabel,
            .Location = New Point(x, y - 22),
            .Size = New Size(largura, 20),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        painel.Controls.Add(lbl)

        controle.Location = New Point(x, y)
        controle.Size = New Size(largura, 40)
        controle.Font = New Font("Segoe UI", 12)
        painel.Controls.Add(controle)
    End Sub

    ' Botão: Voltar
    Private Sub btnVoltarCadastro_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    ' Botão: Salvar
    Private Sub btnSalvarProduto_Click(sender As Object, e As EventArgs)
        Try
            ' Validação dos campos obrigatórios
            If txtNomeProduto.Text.Trim() = "" Or txtCodigoBarrasProduto.Text.Trim() = "" Or txtDescricaoProduto.Text.Trim() = "" Then
                MessageBox.Show("Preencha todos os campos obrigatórios.")
                Exit Sub
            End If

            If comboCategoriaProduto.SelectedIndex = -1 Then
                MessageBox.Show("Selecione uma categoria.")
                Exit Sub
            End If

            If txtFornecedorProduto.Text.Trim() = "" Or txtUnidadeMedidaProduto.Text.Trim() = "" Or txtCodigoLoteProduto.Text.Trim() = "" Or txtMarcaProduto.Text.Trim() = "" Then
                MessageBox.Show("Preencha os campos técnicos.")
                Exit Sub
            End If

            Dim quantidade As Integer
            If Not Integer.TryParse(txtQuantidadeProduto.Text, quantidade) OrElse quantidade < 0 Then
                MessageBox.Show("Informe uma quantidade válida.")
                Exit Sub
            End If

            ' Conexão com banco de dados
            Using conn As New SQLiteConnection("Data Source=MercadinhoAlves.db;Version=3;")
                conn.Open()

                ' Inserção do Produto
                Dim sqlProduto As String = "
                    INSERT INTO Produtos 
                    (nome, codigo_barras, descricao, categoria_id, fornecedor_nome, unidade_medida, quantidade, data_validade)
                    VALUES (@nome, @codigo, @desc, @cat, @forn, @unidade, @qtd, @validade)"
                Using cmdProduto As New SQLiteCommand(sqlProduto, conn)
                    cmdProduto.Parameters.AddWithValue("@nome", txtNomeProduto.Text)
                    cmdProduto.Parameters.AddWithValue("@codigo", txtCodigoBarrasProduto.Text)
                    cmdProduto.Parameters.AddWithValue("@desc", txtDescricaoProduto.Text)
                    cmdProduto.Parameters.AddWithValue("@cat", comboCategoriaProduto.SelectedValue)
                    cmdProduto.Parameters.AddWithValue("@forn", txtFornecedorProduto.Text)
                    cmdProduto.Parameters.AddWithValue("@unidade", txtUnidadeMedidaProduto.Text)
                    cmdProduto.Parameters.AddWithValue("@qtd", quantidade)
                    cmdProduto.Parameters.AddWithValue("@validade", dtpValidadeProduto.Value.ToString("yyyy-MM-dd"))
                    cmdProduto.ExecuteNonQuery()
                End Using

                ' Recupera o ID do produto recém-cadastrado
                Dim produtoId As Integer
                Using cmdId As New SQLiteCommand("SELECT last_insert_rowid()", conn)
                    produtoId = Convert.ToInt32(cmdId.ExecuteScalar())
                End Using

                ' Inserção do Lote
                Dim sqlLote As String = "
                    INSERT INTO Lotes 
                    (produto_id, codigo_lote, quantidade, data_entrada, data_validade)
                    VALUES (@prod, @lote, @qtd, @entrada, @validade)"
                Using cmdLote As New SQLiteCommand(sqlLote, conn)
                    cmdLote.Parameters.AddWithValue("@prod", produtoId)
                    cmdLote.Parameters.AddWithValue("@lote", txtCodigoLoteProduto.Text)
                    cmdLote.Parameters.AddWithValue("@qtd", quantidade)
                    cmdLote.Parameters.AddWithValue("@entrada", DateTime.Now.ToString("yyyy-MM-dd"))
                    cmdLote.Parameters.AddWithValue("@validade", dtpValidadeProduto.Value.ToString("yyyy-MM-dd"))
                    cmdLote.ExecuteNonQuery()
                End Using

                ' Inserção no Estoque
                Dim sqlEstoque As String = "
                    INSERT INTO Estoque 
                    (produto_id, quantidade, lote, data_validade)
                    VALUES (@prod, @qtd, @lote, @validade)"
                Using cmdEstoque As New SQLiteCommand(sqlEstoque, conn)
                    cmdEstoque.Parameters.AddWithValue("@prod", produtoId)
                    cmdEstoque.Parameters.AddWithValue("@qtd", quantidade)
                    cmdEstoque.Parameters.AddWithValue("@lote", txtCodigoLoteProduto.Text)
                    cmdEstoque.Parameters.AddWithValue("@validade", dtpValidadeProduto.Value.ToString("yyyy-MM-dd"))
                    cmdEstoque.ExecuteNonQuery()
                End Using
            End Using

            ' Cadastro finalizado
            MessageBox.Show("Produto cadastrado com sucesso!")
            LimparCampos()

        Catch ex As Exception
            MessageBox.Show("Erro ao salvar: " & ex.Message)
        End Try
    End Sub
    ' Limpa os campos do formulário
    Private Sub LimparCampos()
        txtNomeProduto.Clear()
        txtCodigoBarrasProduto.Clear()
        txtDescricaoProduto.Clear()
        txtUnidadeMedidaProduto.Clear()
        txtCodigoLoteProduto.Clear()
        txtMarcaProduto.Clear()
        txtFornecedorProduto.Clear()
        txtQuantidadeProduto.Clear()
        comboCategoriaProduto.SelectedIndex = -1
        dtpValidadeProduto.Value = DateTime.Today
    End Sub
End Class