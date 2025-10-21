<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovimentacao
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cbbLote = New System.Windows.Forms.ComboBox()
        Me.cbbTipo = New System.Windows.Forms.ComboBox()
        Me.txtQuantidade = New System.Windows.Forms.TextBox()
        Me.txtObservacao = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnRegistrar = New System.Windows.Forms.Button()
        Me.btnVoltar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cbbLote
        '
        Me.cbbLote.FormattingEnabled = True
        Me.cbbLote.Location = New System.Drawing.Point(179, 58)
        Me.cbbLote.Name = "cbbLote"
        Me.cbbLote.Size = New System.Drawing.Size(121, 21)
        Me.cbbLote.TabIndex = 0
        '
        'cbbTipo
        '
        Me.cbbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbbTipo.FormattingEnabled = True
        Me.cbbTipo.Location = New System.Drawing.Point(179, 102)
        Me.cbbTipo.Name = "cbbTipo"
        Me.cbbTipo.Size = New System.Drawing.Size(121, 21)
        Me.cbbTipo.TabIndex = 1
        '
        'txtQuantidade
        '
        Me.txtQuantidade.Location = New System.Drawing.Point(179, 166)
        Me.txtQuantidade.Name = "txtQuantidade"
        Me.txtQuantidade.Size = New System.Drawing.Size(100, 20)
        Me.txtQuantidade.TabIndex = 2
        '
        'txtObservacao
        '
        Me.txtObservacao.Location = New System.Drawing.Point(179, 231)
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(100, 20)
        Me.txtObservacao.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(65, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Lote"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Tipo de Movimentação"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(71, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Quantidade"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(71, 237)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Observação"
        '
        'btnRegistrar
        '
        Me.btnRegistrar.Location = New System.Drawing.Point(360, 301)
        Me.btnRegistrar.Name = "btnRegistrar"
        Me.btnRegistrar.Size = New System.Drawing.Size(75, 23)
        Me.btnRegistrar.TabIndex = 8
        Me.btnRegistrar.Text = "Registrar"
        Me.btnRegistrar.UseVisualStyleBackColor = True
        '
        'btnVoltar
        '
        Me.btnVoltar.Location = New System.Drawing.Point(225, 301)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Size = New System.Drawing.Size(75, 23)
        Me.btnVoltar.TabIndex = 9
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.UseVisualStyleBackColor = True
        '
        'frmMovimentacao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnVoltar)
        Me.Controls.Add(Me.btnRegistrar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtObservacao)
        Me.Controls.Add(Me.txtQuantidade)
        Me.Controls.Add(Me.cbbTipo)
        Me.Controls.Add(Me.cbbLote)
        Me.Name = "frmMovimentacao"
        Me.Text = "frmMovimentacao"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbbLote As ComboBox
    Friend WithEvents cbbTipo As ComboBox
    Friend WithEvents txtQuantidade As TextBox
    Friend WithEvents txtObservacao As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnRegistrar As Button
    Friend WithEvents btnVoltar As Button
End Class
