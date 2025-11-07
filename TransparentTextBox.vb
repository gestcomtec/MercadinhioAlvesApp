Imports System.Windows.Forms
Imports System.Drawing

Public Class TransparentTextBox
    Inherits TextBox

    Public Sub New()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent
        Me.BorderStyle = BorderStyle.None
        Me.Font = New Font("Segoe UI", 12)
    End Sub

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        ' Evita que o fundo seja pintado, mantendo a transparência
    End Sub
End Class
