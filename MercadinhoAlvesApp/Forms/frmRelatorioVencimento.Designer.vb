<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRelatorioVencimento
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
        Me.dgvRelatorio = New System.Windows.Forms.DataGridView()
        Me.btnVencidos = New System.Windows.Forms.Button()
        Me.btnSeteDias = New System.Windows.Forms.Button()
        Me.btnQuinzeDias = New System.Windows.Forms.Button()
        Me.btnEmDia = New System.Windows.Forms.Button()
        Me.btnVoltar = New System.Windows.Forms.Button()
        Me.btnExportar = New System.Windows.Forms.Button()
        CType(Me.dgvRelatorio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvRelatorio
        '
        Me.dgvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRelatorio.Location = New System.Drawing.Point(37, 167)
        Me.dgvRelatorio.Name = "dgvRelatorio"
        Me.dgvRelatorio.Size = New System.Drawing.Size(623, 150)
        Me.dgvRelatorio.TabIndex = 0
        '
        'btnVencidos
        '
        Me.btnVencidos.Location = New System.Drawing.Point(37, 12)
        Me.btnVencidos.Name = "btnVencidos"
        Me.btnVencidos.Size = New System.Drawing.Size(176, 43)
        Me.btnVencidos.TabIndex = 1
        Me.btnVencidos.Text = "Vencidos"
        Me.btnVencidos.UseVisualStyleBackColor = True
        '
        'btnSeteDias
        '
        Me.btnSeteDias.Location = New System.Drawing.Point(268, 12)
        Me.btnSeteDias.Name = "btnSeteDias"
        Me.btnSeteDias.Size = New System.Drawing.Size(182, 43)
        Me.btnSeteDias.TabIndex = 2
        Me.btnSeteDias.Text = "Até 7 dias para vencer"
        Me.btnSeteDias.UseVisualStyleBackColor = True
        '
        'btnQuinzeDias
        '
        Me.btnQuinzeDias.Location = New System.Drawing.Point(37, 106)
        Me.btnQuinzeDias.Name = "btnQuinzeDias"
        Me.btnQuinzeDias.Size = New System.Drawing.Size(176, 43)
        Me.btnQuinzeDias.TabIndex = 3
        Me.btnQuinzeDias.Text = "Até 15 dias para vencer"
        Me.btnQuinzeDias.UseVisualStyleBackColor = True
        '
        'btnEmDia
        '
        Me.btnEmDia.Location = New System.Drawing.Point(268, 106)
        Me.btnEmDia.Name = "btnEmDia"
        Me.btnEmDia.Size = New System.Drawing.Size(182, 43)
        Me.btnEmDia.TabIndex = 4
        Me.btnEmDia.Text = "Em dia"
        Me.btnEmDia.UseVisualStyleBackColor = True
        '
        'btnVoltar
        '
        Me.btnVoltar.Location = New System.Drawing.Point(488, 62)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Size = New System.Drawing.Size(124, 42)
        Me.btnVoltar.TabIndex = 5
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.UseVisualStyleBackColor = True
        '
        'btnExportar
        '
        Me.btnExportar.Location = New System.Drawing.Point(533, 125)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(75, 23)
        Me.btnExportar.TabIndex = 6
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'frmRelatorioVencimento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.btnVoltar)
        Me.Controls.Add(Me.btnEmDia)
        Me.Controls.Add(Me.btnQuinzeDias)
        Me.Controls.Add(Me.btnSeteDias)
        Me.Controls.Add(Me.btnVencidos)
        Me.Controls.Add(Me.dgvRelatorio)
        Me.Name = "frmRelatorioVencimento"
        Me.Text = "frmRelatorioVencimento"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvRelatorio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvRelatorio As DataGridView
    Friend WithEvents btnVencidos As Button
    Friend WithEvents btnSeteDias As Button
    Friend WithEvents btnQuinzeDias As Button
    Friend WithEvents btnEmDia As Button
    Friend WithEvents btnVoltar As Button
    Friend WithEvents btnExportar As Button
End Class
