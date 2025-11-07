<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
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
        Me.btnCadastroProduto = New System.Windows.Forms.Button()
        Me.btnRelatorioVencimento = New System.Windows.Forms.Button()
        Me.btnMovimentacao = New System.Windows.Forms.Button()
        Me.btnRelatorioEstoque = New System.Windows.Forms.Button()
        Me.btnFecharApp = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCadastroProduto
        '
        Me.btnCadastroProduto.Location = New System.Drawing.Point(104, 63)
        Me.btnCadastroProduto.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCadastroProduto.Name = "btnCadastroProduto"
        Me.btnCadastroProduto.Size = New System.Drawing.Size(233, 41)
        Me.btnCadastroProduto.TabIndex = 0
        Me.btnCadastroProduto.Text = "Cadastrar Produto"
        Me.btnCadastroProduto.UseVisualStyleBackColor = True
        '
        'btnRelatorioVencimento
        '
        Me.btnRelatorioVencimento.Location = New System.Drawing.Point(104, 128)
        Me.btnRelatorioVencimento.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRelatorioVencimento.Name = "btnRelatorioVencimento"
        Me.btnRelatorioVencimento.Size = New System.Drawing.Size(233, 41)
        Me.btnRelatorioVencimento.TabIndex = 1
        Me.btnRelatorioVencimento.Text = "Relatório de Vencimento"
        Me.btnRelatorioVencimento.UseVisualStyleBackColor = True
        '
        'btnMovimentacao
        '
        Me.btnMovimentacao.Location = New System.Drawing.Point(104, 203)
        Me.btnMovimentacao.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovimentacao.Name = "btnMovimentacao"
        Me.btnMovimentacao.Size = New System.Drawing.Size(233, 47)
        Me.btnMovimentacao.TabIndex = 2
        Me.btnMovimentacao.Text = "Movimentar Estoque"
        Me.btnMovimentacao.UseVisualStyleBackColor = True
        '
        'btnRelatorioEstoque
        '
        Me.btnRelatorioEstoque.Location = New System.Drawing.Point(104, 276)
        Me.btnRelatorioEstoque.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRelatorioEstoque.Name = "btnRelatorioEstoque"
        Me.btnRelatorioEstoque.Size = New System.Drawing.Size(233, 62)
        Me.btnRelatorioEstoque.TabIndex = 4
        Me.btnRelatorioEstoque.Text = "Relatório de Estoque"
        Me.btnRelatorioEstoque.UseVisualStyleBackColor = True
        '
        'btnFecharApp
        '
        Me.btnFecharApp.Location = New System.Drawing.Point(584, 159)
        Me.btnFecharApp.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFecharApp.Name = "btnFecharApp"
        Me.btnFecharApp.Size = New System.Drawing.Size(220, 50)
        Me.btnFecharApp.TabIndex = 3
        Me.btnFecharApp.Text = "Fechar a Aplicação"
        Me.btnFecharApp.UseVisualStyleBackColor = True
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 554)
        Me.Controls.Add(Me.btnRelatorioEstoque)
        Me.Controls.Add(Me.btnFecharApp)
        Me.Controls.Add(Me.btnMovimentacao)
        Me.Controls.Add(Me.btnRelatorioVencimento)
        Me.Controls.Add(Me.btnCadastroProduto)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmDashboard"
        Me.Text = "frmDashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnCadastroProduto As Button
    Friend WithEvents btnRelatorioVencimento As Button
    Friend WithEvents btnMovimentacao As Button
    Friend WithEvents btnRelatorioEstoque As Button
    Friend WithEvents btnFecharApp As Button
End Class
