Imports System.Data.SQLite

Public Class frmLogin
    Private Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        If DBHelper.ValidarLogin(txtUsername.Text, txtSenha.Text) Then
            Dim dashboard As New frmDashboard
            dashboard.Show()
            Me.Hide()

        Else
            MessageBox.Show("Usuário ou senha inválidos.")
        End If
    End Sub


    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

