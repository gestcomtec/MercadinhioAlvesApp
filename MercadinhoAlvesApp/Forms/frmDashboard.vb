Public Class frmDashboard

    ' Ativa dupla renderização para evitar flickering (tremulação) na interface
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    ' Propriedade pública para armazenar o nome do usuário logado
    Public Property NomeUsuarioLogado As String

    ' Painel principal que conterá os botões
    Private painelMenu As Panel

    ' Evento disparado ao carregar o formulário
    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Suspende o layout para evitar redesenhos durante a configuração
        Me.SuspendLayout()

        ' Maximiza a janela e remove bordas
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

        ' Define imagem de fundo do formulário
        Me.BackgroundImage = My.Resources.dashboardFundo
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Cria painel centralizado na parte inferior para os botões
        painelMenu = New Panel With {
            .Size = New Size(600, 200),
            .BackColor = Color.FromArgb(180, Color.White),
            .Location = New Point((Me.Width - 600) \ 2, Me.Height - 300),
            .Anchor = AnchorStyles.Bottom
        }
        Me.Controls.Add(painelMenu)

        ' Mensagem de boas-vindas no canto superior esquerdo
        Dim lblBoasVindas As New Label With {
            .Text = $"Bem-vindo!",
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(20, 20),
            .BackColor = Color.Transparent
        }
        Me.Controls.Add(lblBoasVindas)

        ' Criação e posicionamento dos botões em 2 colunas
        Dim botoes As (Button, String)() = {
            (btnCadastroProduto, "Cadastrar Produto"),
            (btnMovimentacao, "Movimentar Estoque"),
            (btnRelatorioVencimento, "Relatório de Vencimentos"),
            (btnRelatorioEstoque, "Relatório de Estoque")
        }

        Dim larguraBotao As Integer = 260
        Dim alturaBotao As Integer = 60
        Dim espacamentoHorizontal As Integer = 20
        Dim espacamentoVertical As Integer = 20
        Dim colunas As Integer = 2

        For i = 0 To botoes.Length - 1
            Dim linha As Integer = i \ colunas
            Dim coluna As Integer = i Mod colunas
            Dim posX As Integer = coluna * (larguraBotao + espacamentoHorizontal) + 30
            Dim posY As Integer = linha * (alturaBotao + espacamentoVertical) + 30
            ConfigurarBotao(botoes(i).Item1, botoes(i).Item2, painelMenu, posX, posY, larguraBotao, alturaBotao)
        Next

        ' Botão de fechar no canto superior direito
        btnFecharApp.Text = "X"
        btnFecharApp.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnFecharApp.ForeColor = Color.White
        btnFecharApp.BackColor = Color.Black
        btnFecharApp.FlatStyle = FlatStyle.Flat
        btnFecharApp.FlatAppearance.BorderSize = 0
        btnFecharApp.Size = New Size(40, 40)
        btnFecharApp.Location = New Point(Me.Width - 70, 15)
        btnFecharApp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Controls.Add(btnFecharApp)

        ' Retoma o layout após a configuração
        Me.ResumeLayout()
    End Sub

    ' Método auxiliar para configurar botões com posição e tamanho personalizados
    Private Sub ConfigurarBotao(botao As Button, texto As String, painel As Panel, posX As Integer, posY As Integer, largura As Integer, altura As Integer)
        botao.Text = texto
        botao.Size = New Size(largura, altura)
        botao.Location = New Point(posX, posY)
        botao.BackColor = Color.FromArgb(0, 120, 215)
        botao.ForeColor = Color.White
        botao.FlatStyle = FlatStyle.Flat
        botao.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        painel.Controls.Add(botao)
    End Sub

    ' Eventos de clique dos botões
    Private Sub btnCadastroProduto_Click(sender As Object, e As EventArgs) Handles btnCadastroProduto.Click
        Dim frm As New frmCadastroProduto
        frm.ShowDialog()
    End Sub

    Private Sub btnRelatorioVencimento_Click(sender As Object, e As EventArgs) Handles btnRelatorioVencimento.Click
        Dim frm As New frmRelatorioVencimento
        frm.CarregarRelatorio("vencidos")
        frm.ShowDialog()
    End Sub

    Private Sub btnMovimentacao_Click(sender As Object, e As EventArgs) Handles btnMovimentacao.Click
        Dim frm As New frmMovimentacao
        frm.ShowDialog()
    End Sub

    Private Sub btnRelatorioEstoque_Click(sender As Object, e As EventArgs) Handles btnRelatorioEstoque.Click
        Dim frm As New frmRelatorioEstoque
        frm.ShowDialog()
    End Sub

    ' Evento de clique no botão de fechar
    Private Sub btnFecharApp_Click(sender As Object, e As EventArgs) Handles btnFecharApp.Click
        Application.Exit()
    End Sub

End Class


