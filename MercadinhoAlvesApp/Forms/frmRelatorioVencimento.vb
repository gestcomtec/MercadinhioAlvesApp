Imports System.Data
Imports System.Data.SQLite

Public Class frmRelatorioVencimento

    ' Evento de carregamento do formulário
    Private Sub frmRelatorioVencimento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarRelatório("emdia")
    End Sub

    ' Método público acessível por outros formulários ou botões
    Public Sub CarregarRelatório(filtro As String)
        Try
            Dim sql As String = "SELECT nome AS produto, codigo_barras, data_validade, julianday(data_validade) - julianday('now') AS dias_restantes FROM Produtos"
            Dim dt As DataTable = DBHelper.Consultar(sql)
            Dim dtFiltrado As DataTable = dt.Clone()

            For Each row As DataRow In dt.Rows
                Dim diasRestantes As Integer = 0
                If Not IsDBNull(row("dias_restantes")) Then
                    diasRestantes = Convert.ToInt32(row("dias_restantes"))
                End If

                Select Case filtro
                    Case "vencidos"
                        If diasRestantes < 0 Then dtFiltrado.ImportRow(row)
                    Case "sete"
                        If diasRestantes >= 0 AndAlso diasRestantes <= 7 Then dtFiltrado.ImportRow(row)
                    Case "quinze"
                        If diasRestantes > 7 AndAlso diasRestantes <= 15 Then dtFiltrado.ImportRow(row)
                    Case "emdia"
                        If diasRestantes > 15 Then dtFiltrado.ImportRow(row)
                End Select
            Next

            dgvRelatorio.DataSource = dtFiltrado
            AplicarCores()

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar relatório (" & filtro & "): " & ex.Message)
        End Try
    End Sub

    ' Botão: Vencidos
    Private Sub btnVencidos_Click(sender As Object, e As EventArgs) Handles btnVencidos.Click
        CarregarRelatório("vencidos")
    End Sub

    ' Botão: Até 7 dias
    Private Sub btnSeteDias_Click(sender As Object, e As EventArgs) Handles btnSeteDias.Click
        CarregarRelatório("sete")
    End Sub

    ' Botão: Até 15 dias
    Private Sub btnQuinzeDias_Click(sender As Object, e As EventArgs) Handles btnQuinzeDias.Click
        CarregarRelatório("quinze")
    End Sub

    ' Botão: Em dia
    Private Sub btnEmDia_Click(sender As Object, e As EventArgs) Handles btnEmDia.Click
        CarregarRelatório("emdia")
    End Sub

    ' Botão: Voltar
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Close()
    End Sub

    ' Aplica cores às linhas do DataGridView com base nos dias restantes
    Private Sub AplicarCores()
        For Each row As DataGridViewRow In dgvRelatorio.Rows
            If row.Cells("dias_restantes").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("dias_restantes").Value) Then
                Dim diasRestantes As Integer = Convert.ToInt32(row.Cells("dias_restantes").Value)

                If diasRestantes < 0 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                ElseIf diasRestantes <= 7 Then
                    row.DefaultCellStyle.BackColor = Color.OrangeRed
                ElseIf diasRestantes <= 15 Then
                    row.DefaultCellStyle.BackColor = Color.Gold
                Else
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                End If
            End If
        Next
    End Sub

End Class
