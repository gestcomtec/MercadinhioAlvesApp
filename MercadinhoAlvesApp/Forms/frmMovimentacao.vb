Imports System.Data.SQLite

Public Class frmMovimentacao

    ' Evento disparado ao carregar o formulário
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

    ' Evento disparado ao clicar no botão "Registrar"
    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        ' Validação dos campos obrigatórios
        If cbbLote.SelectedIndex = -1 Or cbbTipo.SelectedIndex = -1 Or txtQuantidade.Text = "" Then
            MessageBox.Show("Preencha os campos obrigatórios.")
            Exit Sub
        End If

        ' Validação da quantidade
        Dim quantidade As Integer
        If Not Integer.TryParse(txtQuantidade.Text, quantidade) Or quantidade <= 0 Then
            MessageBox.Show("Quantidade inválida.")
            Exit Sub
        End If

        ' Captura os dados selecionados
        Dim loteId As Integer = Convert.ToInt32(cbbLote.SelectedValue)
        Dim tipoMov As String = cbbTipo.Text

        Try
            ' Se for saída, verificar se há estoque suficiente
            If tipoMov = "saida" Then
                Dim dtEstoque As New DataTable()
                Using conn As New SQLiteConnection("Data Source=MercadinhoAlves.db;Version=3;")
                    conn.Open()
                    Dim sqlConsultaEstoque As String = "
                        SELECT quantidade 
                        FROM Estoque 
                        WHERE lote = (SELECT codigo_lote FROM Lotes WHERE lote_id = @lote)"
                    Using cmd As New SQLiteCommand(sqlConsultaEstoque, conn)
                        cmd.Parameters.AddWithValue("@lote", loteId)
                        Using da As New SQLiteDataAdapter(cmd)
                            da.Fill(dtEstoque)
                        End Using
                    End Using
                End Using

                ' Verifica se o lote existe no estoque
                If dtEstoque.Rows.Count = 0 Then
                    MessageBox.Show("Lote não encontrado no estoque.")
                    Exit Sub
                End If

                ' Verifica se há quantidade suficiente
                Dim quantidadeAtual As Integer = Convert.ToInt32(dtEstoque.Rows(0)("quantidade"))
                If quantidade > quantidadeAtual Then
                    MessageBox.Show("Quantidade solicitada excede o estoque disponível.")
                    Exit Sub
                End If
            End If

            ' Registrar movimentação na tabela Movimentacoes
            Dim sqlMov As String = "
                INSERT INTO Movimentacoes 
                (lote_id, tipo, quantidade, usuario, observacao) 
                VALUES (@lote, @tipo, @qtd, @user, @obs)"

            Dim parametrosMov As New Dictionary(Of String, Object) From {
                {"@lote", loteId},
                {"@tipo", tipoMov},
                {"@qtd", quantidade},
                {"@user", Environment.UserName},
                {"@obs", txtObservacao.Text}
            }
            DBHelper.ExecutarComando(sqlMov, parametrosMov)

            ' Atualizar quantidade na tabela Lotes
            Dim operador As String = If(tipoMov = "entrada", "+", "-")
            Dim sqlUpdateLote As String = $"UPDATE Lotes SET quantidade = quantidade {operador} @qtd WHERE lote_id = @lote"
            Dim parametrosUpdateLote As New Dictionary(Of String, Object) From {
                {"@qtd", quantidade},
                {"@lote", loteId}
            }
            DBHelper.ExecutarComando(sqlUpdateLote, parametrosUpdateLote)

            ' Atualizar quantidade na tabela Estoque
            Dim sqlUpdateEstoque As String = $"UPDATE Estoque SET quantidade = quantidade {operador} @qtd WHERE lote = (SELECT codigo_lote FROM Lotes WHERE lote_id = @lote)"
            Dim parametrosUpdateEstoque As New Dictionary(Of String, Object) From {
                {"@qtd", quantidade},
                {"@lote", loteId}
            }
            DBHelper.ExecutarComando(sqlUpdateEstoque, parametrosUpdateEstoque)

            ' Exibe confirmação e limpa os campos
            MessageBox.Show("Movimentação registrada e estoque atualizado!")
            LimparCampos()

        Catch ex As Exception
            ' Exibe mensagem de erro em caso de falha
            MessageBox.Show("Erro ao registrar movimentação: " & ex.Message)
        End Try
    End Sub

    ' Limpa os campos do formulário após o registro
    Private Sub LimparCampos()
        cbbLote.SelectedIndex = -1
        cbbTipo.SelectedIndex = 0
        txtQuantidade.Clear()
        txtObservacao.Clear()
    End Sub

    ' Evento disparado ao alterar o tipo de movimentação (não utilizado no momento)
    Private Sub cbbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbTipo.SelectedIndexChanged
        ' Pode ser usado para lógica futura (ex: habilitar/desabilitar campos)
    End Sub

    ' Botão: Voltar para o dashboard
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Hide()
        Dim dashboard As New frmDashboard
        dashboard.Show()
    End Sub

End Class
