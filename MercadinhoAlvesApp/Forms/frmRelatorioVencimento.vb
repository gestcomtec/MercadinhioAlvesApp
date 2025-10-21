Imports System.Data
Imports System.Data.SQLite
Imports System.IO
Imports System.Text
Imports System.Diagnostics

Public Class frmRelatorioVencimento

    ' Evento disparado ao carregar o formulário
    Private Sub frmRelatorioVencimento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Carrega o relatório padrão com filtro "emdia"
        CarregarRelatorio("emdia")
    End Sub

    ' Método principal que carrega o relatório com base no filtro selecionado
    Public Sub CarregarRelatorio(filtro As String)
        Try
            ' Consulta SQL que calcula os dias restantes usando apenas a data (sem horário)
            Dim sql As String = "
                SELECT 
                    nome AS produto, 
                    codigo_barras, 
                    data_validade, 
                    julianday(date(data_validade)) - julianday(date('now')) AS dias_restantes 
                FROM Produtos
                WHERE data_validade IS NOT NULL"

            ' Executa a consulta e armazena os resultados em um DataTable
            Dim dt As DataTable = DBHelper.Consultar(sql)

            ' Cria nova tabela com colunas adicionais para alerta visual
            Dim dtFiltrado As New DataTable()
            dtFiltrado.Columns.Add("produto", GetType(String))
            dtFiltrado.Columns.Add("codigo_barras", GetType(String))
            dtFiltrado.Columns.Add("data_validade", GetType(Date))
            dtFiltrado.Columns.Add("dias_restantes", GetType(Integer))
            dtFiltrado.Columns.Add("alerta", GetType(String)) ' ✅ nova coluna

            ' Percorre cada linha da tabela original
            For Each row As DataRow In dt.Rows
                If IsDBNull(row("data_validade")) OrElse IsDBNull(row("dias_restantes")) Then Continue For

                Dim diasRestantesRaw As Double
                If Double.TryParse(row("dias_restantes").ToString(), diasRestantesRaw) Then
                    Dim diasRestantesInt As Integer = Math.Floor(diasRestantesRaw)

                    ' Aplica o filtro conforme o tipo selecionado
                    Dim incluir As Boolean = False
                    Select Case filtro
                        Case "vencidos"
                            incluir = diasRestantesInt < 0
                        Case "sete"
                            incluir = diasRestantesInt >= 0 AndAlso diasRestantesInt <= 7
                        Case "quinze"
                            incluir = diasRestantesInt > 7 AndAlso diasRestantesInt <= 15
                        Case "emdia"
                            incluir = diasRestantesInt > 15
                    End Select

                    ' Define alerta visual
                    Dim alertaTexto As String = ""
                    Select Case diasRestantesInt
                        Case < 0
                            alertaTexto = "🔴 Vencido"
                        Case 0 To 7
                            alertaTexto = "🟠 Até 7 dias"
                        Case 8 To 15
                            alertaTexto = "🟡 Até 15 dias"
                        Case Else
                            alertaTexto = "🟢 Em dia"
                    End Select

                    ' Adiciona à nova tabela
                    If incluir Then
                        dtFiltrado.Rows.Add(
                            row("produto").ToString(),
                            row("codigo_barras").ToString(),
                            Convert.ToDateTime(row("data_validade")),
                            diasRestantesInt,
                            alertaTexto
                        )
                    End If
                End If
            Next

            ' Exibe os dados filtrados no DataGridView
            dgvRelatorio.DataSource = dtFiltrado

            ' Formata colunas
            dgvRelatorio.Columns("dias_restantes").DefaultCellStyle.Format = "N0"
            dgvRelatorio.Columns("data_validade").DefaultCellStyle.Format = "dd/MM/yyyy"

            ' Aplica cores às linhas com base nos dias restantes
            AplicarCores()

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar relatório (" & filtro & "): " & ex.Message)
        End Try
    End Sub

    ' Botões de filtro
    Private Sub btnVencidos_Click(sender As Object, e As EventArgs) Handles btnVencidos.Click
        CarregarRelatorio("vencidos")
    End Sub

    Private Sub btnSeteDias_Click(sender As Object, e As EventArgs) Handles btnSeteDias.Click
        CarregarRelatorio("sete")
    End Sub

    Private Sub btnQuinzeDias_Click(sender As Object, e As EventArgs) Handles btnQuinzeDias.Click
        CarregarRelatorio("quinze")
    End Sub

    Private Sub btnEmDia_Click(sender As Object, e As EventArgs) Handles btnEmDia.Click
        CarregarRelatorio("emdia")
    End Sub

    ' Botão: Voltar
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Me.Close()
    End Sub

    ' Botão: Exportar para Excel (CSV)
    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Try
            ' Define o caminho do arquivo na área de trabalho
            Dim caminho As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RelatorioVencimentos.csv")

            ' Cria o arquivo CSV com codificação UTF-8
            Using sw As New StreamWriter(caminho, False, Encoding.UTF8)
                ' Cabeçalhos
                For Each col As DataGridViewColumn In dgvRelatorio.Columns
                    sw.Write(col.HeaderText & ";")
                Next
                sw.WriteLine()

                ' Linhas
                For Each row As DataGridViewRow In dgvRelatorio.Rows
                    For Each cell As DataGridViewCell In row.Cells
                        sw.Write(cell.Value?.ToString() & ";")
                    Next
                    sw.WriteLine()
                Next
            End Using

            ' Abre o arquivo automaticamente no Excel
            Process.Start("explorer.exe", caminho)

        Catch ex As Exception
            MessageBox.Show("Erro ao exportar: " & ex.Message)
        End Try
    End Sub

    ' Aplica cores às linhas do DataGridView com base nos dias restantes
    Private Sub AplicarCores()
        For Each row As DataGridViewRow In dgvRelatorio.Rows
            If row.Cells("dias_restantes").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("dias_restantes").Value) Then
                Dim diasRestantes As Integer = Convert.ToInt32(row.Cells("dias_restantes").Value)

                If diasRestantes < 0 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                ElseIf diasRestantes <= 7 Then
                    row.DefaultCellStyle.BackColor = Color.Orange
                ElseIf diasRestantes <= 15 Then
                    row.DefaultCellStyle.BackColor = Color.Yellow
                Else
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                End If
            End If
        Next
    End Sub

End Class
