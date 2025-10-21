Imports System.Data.SQLite
Imports MercadinhoNewApp.Models

Public Class frmCadastroProduto

    ' Evento disparado ao carregar o formulário
    Private Sub frmCadastroProduto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Carrega categorias no ComboBox
        Dim dtCat = DBHelper.Consultar("SELECT categoria_id, nome FROM Categorias")
        CbtCategoria.DataSource = dtCat
        CbtCategoria.DisplayMember = "nome"           ' Exibe o nome da categoria
        CbtCategoria.ValueMember = "categoria_id"     ' Usa o ID da categoria como valor

        ' ❌ Removido: carregamento de fornecedores via ComboBox
    End Sub

    ' Evento disparado ao clicar no botão "Salvar"
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            ' Validações obrigatórias
            If txtNome.Text.Trim() = "" Or txtCodigoBarras.Text.Trim() = "" Or txtDescricao.Text.Trim() = "" Then
                MessageBox.Show("Preencha todos os campos obrigatórios.")
                Exit Sub
            End If

            If CbtCategoria.SelectedIndex = -1 Then
                MessageBox.Show("Selecione uma categoria.")
                Exit Sub
            End If

            If txtFornecedor.Text.Trim() = "" Then
                MessageBox.Show("Informe o nome do fornecedor.")
                Exit Sub
            End If

            If txtUnidadeMedida.Text.Trim() = "" Then
                MessageBox.Show("Informe a unidade de medida.")
                Exit Sub
            End If

            If txtCodigoLote.Text.Trim() = "" Then
                MessageBox.Show("Informe o código do lote.")
                Exit Sub
            End If

            ' Validação da quantidade
            Dim quantidade As Integer
            If Not Integer.TryParse(txtQuantidade.Text, quantidade) Or quantidade < 0 Then
                MessageBox.Show("Informe uma quantidade válida.")
                Exit Sub
            End If

            ' Abre conexão única com o banco
            Dim conn As New SQLiteConnection("Data Source=MercadinhoAlves.db;Version=3;")
            conn.Open()

            ' Comando SQL para inserir produto na tabela Produtos
            Dim sqlProduto As String = "
                INSERT INTO Produtos 
                (nome, codigo_barras, descricao, categoria_id, fornecedor_nome, unidade_medida, quantidade, data_validade) 
                VALUES (@nome, @codigo, @desc, @cat, @forn, @unidade, @qtd, @validade)"

            Dim cmdProduto As New SQLiteCommand(sqlProduto, conn)
            cmdProduto.Parameters.AddWithValue("@nome", txtNome.Text)
            cmdProduto.Parameters.AddWithValue("@codigo", txtCodigoBarras.Text)
            cmdProduto.Parameters.AddWithValue("@desc", txtDescricao.Text)
            cmdProduto.Parameters.AddWithValue("@cat", CbtCategoria.SelectedValue)     ' ✅ categoria_id correto
            cmdProduto.Parameters.AddWithValue("@forn", txtFornecedor.Text)            ' ✅ nome do fornecedor manual
            cmdProduto.Parameters.AddWithValue("@unidade", txtUnidadeMedida.Text)
            cmdProduto.Parameters.AddWithValue("@qtd", quantidade)
            cmdProduto.Parameters.AddWithValue("@validade", dtpValidade.Value.ToString("yyyy-MM-dd"))
            cmdProduto.ExecuteNonQuery()

            ' Recupera o ID do produto recém-cadastrado
            Dim cmdId As New SQLiteCommand("SELECT last_insert_rowid()", conn)
            Dim produtoId As Integer = Convert.ToInt32(cmdId.ExecuteScalar())

            ' Comando SQL para inserir lote na tabela Lotes
            Dim sqlLote As String = "
                INSERT INTO Lotes 
                (produto_id, codigo_lote, quantidade, data_entrada, data_validade) 
                VALUES (@prod, @lote, @qtd, @entrada, @validade)"

            Dim cmdLote As New SQLiteCommand(sqlLote, conn)
            cmdLote.Parameters.AddWithValue("@prod", produtoId)
            cmdLote.Parameters.AddWithValue("@lote", txtCodigoLote.Text)
            cmdLote.Parameters.AddWithValue("@qtd", quantidade)
            cmdLote.Parameters.AddWithValue("@entrada", DateTime.Now.ToString("yyyy-MM-dd"))
            cmdLote.Parameters.AddWithValue("@validade", dtpValidade.Value.ToString("yyyy-MM-dd"))
            cmdLote.ExecuteNonQuery()

            ' Comando SQL para inserir estoque na tabela Estoque
            Dim sqlEstoque As String = "
                INSERT INTO Estoque 
                (produto_id, quantidade, lote, data_validade) 
                VALUES (@prod, @qtd, @lote, @validade)"

            Dim cmdEstoque As New SQLiteCommand(sqlEstoque, conn)
            cmdEstoque.Parameters.AddWithValue("@prod", produtoId)
            cmdEstoque.Parameters.AddWithValue("@qtd", quantidade)
            cmdEstoque.Parameters.AddWithValue("@lote", txtCodigoLote.Text)
            cmdEstoque.Parameters.AddWithValue("@validade", dtpValidade.Value.ToString("yyyy-MM-dd"))
            cmdEstoque.ExecuteNonQuery()

            ' Fecha conexão
            conn.Close()

            ' Exibe mensagem final de sucesso
            MessageBox.Show("Produto cadastrado com sucesso!")
            LimparCampos()

        Catch ex As Exception
            ' Exibe mensagem de erro em caso de falha
            MessageBox.Show("Erro ao salvar: " & ex.Message)
        End Try
    End Sub

    ' Limpa os campos do formulário após o cadastro
    Private Sub LimparCampos()
        txtNome.Clear()
        txtCodigoBarras.Clear()
        txtDescricao.Clear()
        CbtCategoria.SelectedIndex = -1
        txtFornecedor.Clear()                          ' ✅ limpa o novo campo fornecedor
        txtUnidadeMedida.Clear()
        txtCodigoLote.Clear()
        txtQuantidade.Clear()
        dtpValidade.Value = DateTime.Today
    End Sub

    ' Fecha o formulário ao clicar em "Voltar"
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Close()
    End Sub

End Class

