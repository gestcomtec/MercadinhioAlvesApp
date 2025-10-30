' Importa namespaces necessários para desenho gráfico e interface
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

' Classe auxiliar para aplicar estilos visuais reutilizáveis em controles
Public Class UIHelper

    ' 🔘 Aplica bordas arredondadas a botões
    Public Shared Sub ArredondarBotao(botao As Button)
        Dim radius As Integer = 20 ' Define o raio das bordas arredondadas

        ' Cria um caminho gráfico para desenhar a forma arredondada
        Dim path As New GraphicsPath()
        path.StartFigure()

        ' Desenha os cantos arredondados e linhas entre eles
        path.AddArc(New Rectangle(0, 0, radius, radius), 180, 90) ' Canto superior esquerdo
        path.AddLine(radius, 0, botao.Width - radius, 0)          ' Linha superior
        path.AddArc(New Rectangle(botao.Width - radius, 0, radius, radius), -90, 90) ' Canto superior direito
        path.AddLine(botao.Width, radius, botao.Width, botao.Height - radius)       ' Linha direita
        path.AddArc(New Rectangle(botao.Width - radius, botao.Height - radius, radius, radius), 0, 90) ' Canto inferior direito
        path.AddLine(botao.Width - radius, botao.Height, radius, botao.Height)       ' Linha inferior
        path.AddArc(New Rectangle(0, botao.Height - radius, radius, radius), 90, 90) ' Canto inferior esquerdo
        path.CloseFigure()

        ' Aplica o caminho como região do botão (define o formato visual)
        botao.Region = New Region(path)
    End Sub

    ' 🌫️ Cria uma sombra atrás de qualquer controle (botão, campo de texto etc.)
    Public Shared Sub CriarSombraControle(controle As Control, container As Control)
        ' Cria um painel que simula a sombra
        Dim sombra As New Panel()
        sombra.Size = controle.Size ' Mesma dimensão do controle original
        sombra.Location = New Point(controle.Location.X + 5, controle.Location.Y + 5) ' Desloca 5px para baixo e direita
        sombra.BackColor = Color.FromArgb(60, Color.Black) ' Cor preta com transparência (60/255)
        sombra.SendToBack() ' Garante que a sombra fique atrás do controle original

        ' Adiciona a sombra ao container (formulário)
        container.Controls.Add(sombra)
    End Sub

    ' 🔲 Aplica bordas arredondadas a campos de texto (TextBox)
    Public Shared Sub ArredondarCampo(campo As TextBox)
        Dim radius As Integer = 15 ' Define o raio das bordas

        ' Cria um caminho gráfico para desenhar a forma arredondada
        Dim path As New GraphicsPath()
        path.AddArc(0, 0, radius, radius, 180, 90) ' Canto superior esquerdo
        path.AddArc(campo.Width - radius, 0, radius, radius, 270, 90) ' Canto superior direito
        path.AddArc(campo.Width - radius, campo.Height - radius, radius, radius, 0, 90) ' Canto inferior direito
        path.AddArc(0, campo.Height - radius, radius, radius, 90, 90) ' Canto inferior esquerdo
        path.CloseFigure()

        ' Aplica o caminho como região do campo de texto
        campo.Region = New Region(path)
    End Sub

End Class



