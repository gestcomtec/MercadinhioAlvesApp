Imports System.Data
Imports System.Data.SQLite

Public Class frmRelatorioEstoque

    Private Sub frmRelatorioEstoque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarRelatorio()
    End Sub

    Private Sub CarregarRelatorio()
        Try
            Dim sql As String = "
    SELECT 
        p.nome AS produto,
        p.codigo_barras,
        e.quantidade,
        e.lote,
        e.data_validade,
        f.nome AS fornecedor
    FROM Estoque e
    INNER JOIN Produtos p ON e.produto_id = p.produto_id
    LEFT JOIN Fornecedores f ON p.fornecedor_id = f.fornecedor_id
"


            Dim dt As DataTable = DBHelper.Consultar(sql)
            dgvRelatorioEstoque.DataSource = dt

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar relatório de estoque: " & ex.Message)
        End Try
    End Sub

    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Dim resposta = MessageBox.Show("Deseja voltar ao menu principal?", "Confirmar saída", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If resposta = DialogResult.Yes Then
            For i As Double = 1 To 0 Step -0.1
                Me.Opacity = i
                Threading.Thread.Sleep(30)
            Next
            Me.Close()
        End If
    End Sub


End Class
