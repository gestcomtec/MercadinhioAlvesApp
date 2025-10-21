Public Class frmDashboard

    Private Sub btnCadastroProduto_Click(sender As Object, e As EventArgs) Handles btnCadastroProduto.Click
        Dim frm As New frmCadastroProduto
        frm.ShowDialog()
    End Sub

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicialização opcional
    End Sub

    Private Sub btnRelatorioVencimento_Click(sender As Object, e As EventArgs) Handles btnRelatorioVencimento.Click
        Dim relatorioForm As New frmRelatorioVencimento()
        relatorioForm.CarregarRelatorio("vencidos") ' ✅ certo
        relatorioForm.ShowDialog()

    End Sub

    Private Sub btnMovimentacao_Click(sender As Object, e As EventArgs) Handles btnMovimentacao.Click
        Dim frm As New frmMovimentacao
        frm.ShowDialog()
    End Sub

    Private Sub btnFecharApp_Click(sender As Object, e As EventArgs) Handles btnFecharApp.Click
        Application.Exit()
    End Sub

    Private Sub btnRelatorioEstoque_Click(sender As Object, e As EventArgs) Handles btnRelatorioEstoque.Click
        Dim frm As New frmRelatorioEstoque
        frm.ShowDialog()
    End Sub

End Class
