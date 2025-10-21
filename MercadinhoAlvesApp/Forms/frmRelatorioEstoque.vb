Imports System.Data
Imports System.Data.SQLite

Public Class frmRelatorioEstoque

    ' Evento disparado ao carregar o formulário
    Private Sub frmRelatorioEstoque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarRelatorioEstoque()
    End Sub

    ' Método que carrega os dados do estoque com nome do fornecedor
    Private Sub CarregarRelatorioEstoque()
        Try
            ' Consulta SQL com INNER JOIN para trazer dados do produto e fornecedor
            Dim sql As String = "
                SELECT 
                    p.nome AS produto,
                    p.codigo_barras,
                    e.quantidade,
                    e.lote,
                    e.data_validade,
                    p.fornecedor_nome AS fornecedor
                FROM Estoque e
                INNER JOIN Produtos p ON e.produto_id = p.produto_id
                WHERE e.data_validade IS NOT NULL"

            ' Executa a consulta e carrega os dados no DataGridView
            Dim dt As DataTable = DBHelper.Consultar(sql)
            dgvEstoque.DataSource = dt

            ' Formata colunas
            dgvEstoque.Columns("quantidade").DefaultCellStyle.Format = "N0"
            dgvEstoque.Columns("data_validade").DefaultCellStyle.Format = "dd/MM/yyyy"

        Catch ex As Exception
            ' Exibe mensagem de erro em caso de falha
            MessageBox.Show("Erro ao carregar relatório de estoque: " & ex.Message)
        End Try
    End Sub

    ' Botão: Voltar (fecha o formulário diretamente, sem mensagem)
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Close()
    End Sub

End Class

