Imports MercadinhoNewApp.Models


Public Class frmCadastroProduto
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub frmCadastroProduto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Carregar categorias
        Dim dtCat = DBHelper.Consultar("SELECT categoria_id, nome FROM Categorias")
        CbtCategoria.DataSource = dtCat
        CbtCategoria.DisplayMember = "nome"
        CbtCategoria.ValueMember = "categoria_id"


        ' Carregar fornecedores
        Dim dtForn = DBHelper.Consultar("SELECT fornecedor_id, nome FROM Fornecedores")
        cbbFornecedor.DataSource = dtForn
        cbbFornecedor.DisplayMember = "nome"
        cbbFornecedor.ValueMember = "fornecedor_id"

    End Sub


    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Dim novoLote As New Lote()
        novoLote.CodigoLote = txtCodigoLote.Text
        novoLote.Quantidade = Convert.ToInt32(txtQuantidade.Text)
        novoLote.DataValidade = dtpValidade.Value



        Dim novoProduto As New Produto()

        novoProduto.Nome = txtNome.Text
        novoProduto.CodigoBarras = txtCodigoBarras.Text
        novoProduto.CodigoInterno = txtCodigoBarras.Text
        novoProduto.Marca = txtMarca.Text
        novoProduto.UnidadeMedida = txtUnidadeMedida.Text
        novoProduto.DataValidade = dtpValidade.Value



        Try
            ' Validações básicas
            If txtNome.Text.Trim() = "" Or txtCodigoBarras.Text.Trim() = "" Or txtDescricao.Text.Trim() = "" Then
                MessageBox.Show("Preencha todos os campos obrigatórios.")
                Exit Sub
            End If

            If CbtCategoria.SelectedIndex = -1 Then
                MessageBox.Show("Selecione uma categoria.")
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

            ' Validando e convertendo quantidade
            Dim quantidade As Integer
            If Not Integer.TryParse(txtQuantidade.Text, quantidade) Or quantidade < 0 Then
                MessageBox.Show("Informe uma quantidade válida.")
                Exit Sub
            End If

            ' Inserir produto
            Dim sqlProduto As String = "INSERT INTO Produtos (nome, codigo_barras, descricao, categoria, unidade_medida) VALUES (@nome, @codigo, @desc, @cat, @unidade)"
            Dim parametrosProduto As New Dictionary(Of String, Object) From {
            {"@nome", txtNome.Text},
            {"@codigo", txtCodigoBarras.Text},
            {"@desc", txtDescricao.Text},
            {"@cat", CbtCategoria.SelectedItem.ToString()},
            {"@unidade", txtUnidadeMedida.Text}
        }
            DBHelper.ExecutarComando(sqlProduto, parametrosProduto)


            ' Recuperar ID do produto recém-cadastrado
            Dim produtoId As Integer = Convert.ToInt32(DBHelper.ConsultarScalar("SELECT last_insert_rowid()"))

            ' Inserir lote com quantidade
            Dim sqlLote As String = "INSERT INTO Lotes (produto_id, codigo_lote, quantidade, data_entrada, data_validade) VALUES (@prod, @lote, @qtd, @entrada, @validade)"
            Dim parametrosLote As New Dictionary(Of String, Object) From {
            {"@prod", produtoId},
            {"@lote", txtCodigoLote.Text},
            {"@qtd", quantidade},
            {"@entrada", DateTime.Now.ToString("yyyy-MM-dd")},
            {"@validade", dtpValidade.Value.ToString("yyyy-MM-dd")}
        }
            DBHelper.ExecutarComando(sqlLote, parametrosLote)

            MessageBox.Show("Produto cadastrado com sucesso!")
            LimparCampos()

        Catch ex As Exception
            MessageBox.Show("Erro ao salvar: " & ex.Message)
        End Try
    End Sub





    Private Sub LimparCampos()
        txtNome.Clear()
        txtCodigoBarras.Clear()
        txtDescricao.Clear()
        CbtCategoria.SelectedIndex = -1
        txtUnidadeMedida.Clear()
        txtCodigoLote.Clear()
        txtQuantidade.Clear()
        dtpValidade.Value = DateTime.Today
    End Sub



    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Close()
    End Sub

End Class