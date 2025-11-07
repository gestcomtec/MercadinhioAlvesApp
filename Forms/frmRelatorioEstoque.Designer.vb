<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRelatorioEstoque
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dgvEstoque = New System.Windows.Forms.DataGridView()
        CType(Me.dgvEstoque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvEstoque
        '
        Me.dgvEstoque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEstoque.Location = New System.Drawing.Point(48, 260)
        Me.dgvEstoque.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvEstoque.Name = "dgvEstoque"
        Me.dgvEstoque.RowHeadersWidth = 62
        Me.dgvEstoque.Size = New System.Drawing.Size(953, 185)
        Me.dgvEstoque.TabIndex = 0
        '
        'frmRelatorioEstoque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 554)
        Me.Controls.Add(Me.dgvEstoque)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmRelatorioEstoque"
        Me.Text = "frmRelatorioEstoque"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvEstoque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvEstoque As DataGridView
End Class
