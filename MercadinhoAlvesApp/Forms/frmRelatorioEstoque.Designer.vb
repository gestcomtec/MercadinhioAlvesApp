<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRelatorioEstoque
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
        Me.dgvRelatorioEstoque = New System.Windows.Forms.DataGridView()
        Me.btnVoltar = New System.Windows.Forms.Button()
        CType(Me.dgvRelatorioEstoque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvRelatorioEstoque
        '
        Me.dgvRelatorioEstoque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRelatorioEstoque.Location = New System.Drawing.Point(141, 211)
        Me.dgvRelatorioEstoque.Name = "dgvRelatorioEstoque"
        Me.dgvRelatorioEstoque.Size = New System.Drawing.Size(403, 150)
        Me.dgvRelatorioEstoque.TabIndex = 0
        '
        'btnVoltar
        '
        Me.btnVoltar.Location = New System.Drawing.Point(268, 167)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Size = New System.Drawing.Size(75, 23)
        Me.btnVoltar.TabIndex = 1
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.UseVisualStyleBackColor = True
        '
        'frmRelatorioEstoque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnVoltar)
        Me.Controls.Add(Me.dgvRelatorioEstoque)
        Me.Name = "frmRelatorioEstoque"
        Me.Text = "frmRelatorioEstoque"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvRelatorioEstoque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvRelatorioEstoque As DataGridView
    Friend WithEvents btnVoltar As Button
End Class
