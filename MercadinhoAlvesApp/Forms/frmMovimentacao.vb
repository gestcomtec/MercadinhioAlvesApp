Public Class frmMovimentacao
    Private Sub frmMovimentacao_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Carregar lotes ativos
        Dim dtLotes = DBHelper.Consultar("SELECT lote_id, codigo_lote FROM Lotes WHERE ativo = 1")
        cbbLote.DataSource = dtLotes
        cbbLote.DisplayMember = "codigo_lote"
        cbbLote.ValueMember = "lote_id"


        ' Preencher tipos de movimentação
        cbbTipo.Items.Clear()
        cbbTipo.Items.Add("entrada")
        cbbTipo.Items.Add("saida")
        cbbTipo.SelectedIndex = 0
    End Sub

    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        If cbbLote.SelectedIndex = -1 Or cbbTipo.SelectedIndex = -1 Or txtQuantidade.Text = "" Then
            MessageBox.Show("Preencha os campos obrigatórios.")
            Exit Sub
        End If

        Dim quantidade As Integer
        If Not Integer.TryParse(txtQuantidade.Text, quantidade) Or quantidade <= 0 Then
            MessageBox.Show("Quantidade inválida.")
            Exit Sub
        End If

        Dim loteId As Integer = Convert.ToInt32(cbbLote.SelectedValue)
        Dim tipoMov As String = cbbTipo.Text

        Try
            ' Registrar movimentação
            Dim sqlMov As String = "INSERT INTO Movimentacoes (lote_id, tipo, quantidade, usuario, observacao) VALUES (@lote, @tipo, @qtd, @user, @obs)"
            Dim parametrosMov As New Dictionary(Of String, Object) From {
            {"@lote", loteId},
            {"@tipo", tipoMov},
            {"@qtd", quantidade},
            {"@user", Environment.UserName},
            {"@obs", txtObservacao.Text}
        }
            DBHelper.ExecutarComando(sqlMov, parametrosMov)

            ' Atualizar quantidade do lote
            Dim operador As String = If(tipoMov = "entrada", "+", "-")
            Dim sqlUpdate As String = $"UPDATE Lotes SET quantidade = quantidade {operador} @qtd WHERE lote_id = @lote"
            Dim parametrosUpdate As New Dictionary(Of String, Object) From {
            {"@qtd", quantidade},
            {"@lote", loteId}
        }
            DBHelper.ExecutarComando(sqlUpdate, parametrosUpdate)

            MessageBox.Show("Movimentação registrada e estoque atualizado!")
            LimparCampos()
        Catch ex As Exception
            MessageBox.Show("Erro ao registrar movimentação: " & ex.Message)
        End Try
    End Sub


    Private Sub LimparCampos()
        cbbLote.SelectedIndex = -1
        cbbTipo.SelectedIndex = 0
        txtQuantidade.Clear()
        txtObservacao.Clear()
    End Sub

    Private Sub cbbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbTipo.SelectedIndexChanged

    End Sub

    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Hide()
        Dim dashboard As New frmDashboard
        dashboard.Show()
    End Sub

End Class