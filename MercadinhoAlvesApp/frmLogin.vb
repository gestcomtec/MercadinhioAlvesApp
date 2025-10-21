Imports System.Data.SQLite
Imports System.Text
Imports System.Diagnostics

Public Class frmLogin

    ' Evento disparado ao carregar o formulário
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Aplica a imagem como fundo em tela cheia
        Me.BackgroundImage = My.Resources.MercadinhoAlves
        Me.BackgroundImageLayout = ImageLayout.Zoom ' Preenche toda a tela
    End Sub


    ' Evento disparado ao clicar no botão "Entrar"
    Private Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        ' ✅ Valida o login usando método da classe DBHelper
        If DBHelper.ValidarLogin(txtUsername.Text, txtSenha.Text) Then
            ' ✅ Se válido, abre o dashboard e oculta o formulário de login
            Dim dashboard As New frmDashboard
            dashboard.Show()
            Me.Hide()
        Else
            ' ✅ Se inválido, exibe mensagem de erro
            MessageBox.Show("Usuário ou senha inválidos.")
        End If
    End Sub

End Class
