Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedTextBox
    Inherits UserControl

    Private innerTextBox As New TextBox()

    Public Property UseSystemPasswordChar As Boolean
        Get
            Return innerTextBox.UseSystemPasswordChar
        End Get
        Set(value As Boolean)
            innerTextBox.UseSystemPasswordChar = value
        End Set
    End Property

    Public Overrides Property Text As String
        Get
            Return innerTextBox.Text
        End Get
        Set(value As String)
            innerTextBox.Text = value
        End Set
    End Property

    Public Sub New()
        Me.Size = New Size(250, 35)
        Me.BackColor = Color.Transparent

        innerTextBox.BorderStyle = BorderStyle.None
        innerTextBox.Font = New Font("Segoe UI", 12)
        innerTextBox.ForeColor = Color.Black
        innerTextBox.BackColor = Color.White
        innerTextBox.Location = New Point(10, 7)
        innerTextBox.Width = Me.Width - 20

        Me.Controls.Add(innerTextBox)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As Rectangle = New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim path As GraphicsPath = New GraphicsPath()
        path.AddArc(rect.X, rect.Y, 10, 10, 180, 90)
        path.AddArc(rect.Right - 10, rect.Y, 10, 10, 270, 90)
        path.AddArc(rect.Right - 10, rect.Bottom - 10, 10, 10, 0, 90)
        path.AddArc(rect.X, rect.Bottom - 10, 10, 10, 90, 90)
        path.CloseFigure()

        Using pen As New Pen(Color.Gray, 1)
            g.DrawPath(pen, path)
        End Using
    End Sub
End Class




