Imports System.Data.SQLite
Imports System.Text
Imports System.Diagnostics

Public Class frmLogin



    ' Ativa dupla renderização para evitar piscar na inicialização
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    ' Timer para efeito de fade-in
    Private WithEvents fadeInTimer As New Timer()

    ' Intensidade da transição de opacidade
    Private fadeStep As Double = 0.15

    ' Controles criados via código
    Private txtUsuarioLogin As TextBox
    Private txtSenhaLogin As TextBox
    Private btnEntrarLogin As Button
    Private btnSairLogin As Button
    Private iconVerSenha As PictureBox
    Private senhaVisivel As Boolean = False

    ' Evento de carregamento do formulário
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()

        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackgroundImage = My.Resources.LoginDesign
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.Opacity = 0

        fadeInTimer.Interval = 50
        fadeInTimer.Start()

        ' Painel centralizado para os campos
        Dim painelLogin As New Panel With {
            .Size = New Size(400, 250),
            .BackColor = Color.FromArgb(220, Color.White),
            .Location = New Point((Me.ClientSize.Width - 410) \ 2, (Me.ClientSize.Height - 2) \ 2)
        }
        Me.Controls.Add(painelLogin)

        Dim fonteCampo As New Font("Segoe UI", 12)

        ' Ícone de usuário
        Dim iconUsuario As New PictureBox With {
            .Image = My.Resources.usuario, ' Substitua com o recurso correto
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(30, 30),
            .Location = New Point(10, 60),
            .BackColor = Color.Transparent
        }
        painelLogin.Controls.Add(iconUsuario)

        ' Campo: Usuário
        txtUsuarioLogin = New TextBox With {
            .Text = "Usuário",
            .ForeColor = Color.Gray,
            .Font = fonteCampo,
            .Location = New Point(50, 60),
            .Size = New Size(300, 35)
        }
        AddHandler txtUsuarioLogin.Enter, AddressOf LimparPlaceholderUsuario
        AddHandler txtUsuarioLogin.Leave, AddressOf RestaurarPlaceholderUsuario
        AddHandler txtUsuarioLogin.KeyDown, AddressOf CampoLogin_KeyDown
        painelLogin.Controls.Add(txtUsuarioLogin)

        ' Ícone de senha
        Dim iconSenha As New PictureBox With {
            .Image = My.Resources.bloqueado, ' Substitua com o recurso correto
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(30, 30),
            .BackColor = Color.Transparent,
            .Location = New Point(10, 120)
        }
        painelLogin.Controls.Add(iconSenha)

        ' Campo: Senha
        txtSenhaLogin = New TextBox With {
            .Text = "Senha",
            .ForeColor = Color.Gray,
            .Font = fonteCampo,
            .Location = New Point(50, 120),
            .Size = New Size(260, 35),
            .PasswordChar = ControlChars.NullChar
        }
        AddHandler txtSenhaLogin.Enter, AddressOf LimparPlaceholderSenha
        AddHandler txtSenhaLogin.Leave, AddressOf RestaurarPlaceholderSenha
        AddHandler txtSenhaLogin.KeyDown, AddressOf CampoLogin_KeyDown
        painelLogin.Controls.Add(txtSenhaLogin)

        ' Ícone para ver senha
        iconVerSenha = New PictureBox With {
            .Image = My.Resources.olhofechado,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(30, 30),
            .BackColor = Color.Transparent,
            .Location = New Point(320, 123),
            .Cursor = Cursors.Hand
        }
        AddHandler iconVerSenha.Click, AddressOf iconVerSenha_Click
        painelLogin.Controls.Add(iconVerSenha)

        ' Botão: Entrar
        btnEntrarLogin = New Button With {
            .Text = "Entrar",
            .Location = New Point(50, 200),
            .Size = New Size(130, 40),
            .BackColor = Color.FromArgb(0, 120, 215),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat
        }
        btnEntrarLogin.FlatAppearance.BorderSize = 0
        AddHandler btnEntrarLogin.Click, AddressOf btnEntrarLogin_Click
        painelLogin.Controls.Add(btnEntrarLogin)

        ' Botão: Sair
        btnSairLogin = New Button With {
            .Text = "Sair",
            .Location = New Point(220, 200),
            .Size = New Size(130, 40),
            .BackColor = Color.Gray,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat
        }
        btnSairLogin.FlatAppearance.BorderSize = 0
        AddHandler btnSairLogin.Click, AddressOf btnSairLogin_Click
        painelLogin.Controls.Add(btnSairLogin)
    End Sub

    ' Alterna exibição da senha
    Private Sub iconVerSenha_Click(sender As Object, e As EventArgs)
        If txtSenhaLogin.Text = "Senha" Then Return

        senhaVisivel = Not senhaVisivel

        If senhaVisivel Then
            txtSenhaLogin.PasswordChar = ControlChars.NullChar
            iconVerSenha.Image = My.Resources.olhoaberto
        Else
            txtSenhaLogin.PasswordChar = "●"c
            iconVerSenha.Image = My.Resources.olhofechado
        End If
    End Sub

    ' Limpa placeholder do campo Usuário ao entrar
    Private Sub LimparPlaceholderUsuario(sender As Object, e As EventArgs)
        If txtUsuarioLogin.Text = "Usuário" Then
            txtUsuarioLogin.Text = ""
            txtUsuarioLogin.ForeColor = Color.Black
        End If
    End Sub

    ' Restaura placeholder do campo Usuário ao sair
    Private Sub RestaurarPlaceholderUsuario(sender As Object, e As EventArgs)
        If txtUsuarioLogin.Text = "" Then
            txtUsuarioLogin.Text = "Usuário"
            txtUsuarioLogin.ForeColor = Color.Gray
        End If
    End Sub

    ' Limpa placeholder do campo Senha ao entrar
    Private Sub LimparPlaceholderSenha(sender As Object, e As EventArgs)
        If txtSenhaLogin.Text = "Senha" Then
            txtSenhaLogin.Text = ""
            txtSenhaLogin.ForeColor = Color.Black
            txtSenhaLogin.PasswordChar = "●"c
        End If
    End Sub

    ' Restaura placeholder do campo Senha ao sair
    Private Sub RestaurarPlaceholderSenha(sender As Object, e As EventArgs)
        If txtSenhaLogin.Text = "" Then
            txtSenhaLogin.Text = "Senha"
            txtSenhaLogin.ForeColor = Color.Gray
            txtSenhaLogin.PasswordChar = ControlChars.NullChar
        End If
    End Sub

    ' Evento de tecla pressionada nos campos de login
    Private Sub CampoLogin_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            btnEntrarLogin.PerformClick()
        End If
    End Sub

    ' Evento do botão Entrar
    Private Sub btnEntrarLogin_Click(sender As Object, e As EventArgs)
        Dim usuario = txtUsuarioLogin.Text.Trim()
        Dim senha = txtSenhaLogin.Text.Trim()

        If usuario = "" Or usuario = "Usuário" Or senha = "" Or senha = "Senha" Then
            MessageBox.Show("Preencha usuário e senha.")
            Exit Sub
        End If

        Try
            Using conn As New SQLiteConnection("Data Source=MercadinhoAlves.db;Version=3;")
                conn.Open()

                Dim sql As String = "SELECT COUNT(*) FROM Usuarios WHERE username = @user AND senha_hash = @pass"

                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@user", usuario)
                    cmd.Parameters.AddWithValue("@pass", senha)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count > 0 Then
                        frmDashboard.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("Usuário ou senha inválidos.")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erro ao conectar: " & ex.Message)
        End Try
    End Sub

    ' Evento do botão Sair
    Private Sub btnSairLogin_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    ' Evento do Timer para efeito de fade-in
    Private Sub fadeInTimer_Tick(sender As Object, e As EventArgs) Handles fadeInTimer.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += fadeStep
        Else
            fadeInTimer.Stop()
        End If
    End Sub

End Class


