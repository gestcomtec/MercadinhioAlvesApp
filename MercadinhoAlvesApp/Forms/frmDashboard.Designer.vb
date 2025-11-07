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
        Me.btnCadastroProduto.Location = New System.Drawing.Point(78, 51)
        Me.btnCadastroProduto.Name = "btnCadastroProduto"
        Me.btnCadastroProduto.Size = New System.Drawing.Size(175, 33)
        Me.btnCadastroProduto.TabIndex = 0
        Me.btnCadastroProduto.Text = "Cadastrar Produto"
        Me.btnCadastroProduto.UseVisualStyleBackColor = True
        '
        'btnRelatorioVencimento
        '
        Me.btnRelatorioVencimento.Location = New System.Drawing.Point(78, 104)
        Me.btnRelatorioVencimento.Name = "btnRelatorioVencimento"
        Me.btnRelatorioVencimento.Size = New System.Drawing.Size(175, 33)
        Me.btnRelatorioVencimento.TabIndex = 1
        Me.btnRelatorioVencimento.Text = "Relatório de Vencimento"
        Me.btnRelatorioVencimento.UseVisualStyleBackColor = True
        '
        'btnMovimentacao
        '
        Me.btnMovimentacao.Location = New System.Drawing.Point(78, 165)
        Me.btnMovimentacao.Name = "btnMovimentacao"
        Me.btnMovimentacao.Size = New System.Drawing.Size(175, 38)
        Me.btnMovimentacao.TabIndex = 2
        Me.btnMovimentacao.Text = "Movimentar Estoque"
        Me.btnMovimentacao.UseVisualStyleBackColor = True
        '
        'btnRelatorioEstoque
        '
        Me.btnRelatorioEstoque.Location = New System.Drawing.Point(78, 224)
        Me.btnRelatorioEstoque.Name = "btnRelatorioEstoque"
        Me.btnRelatorioEstoque.Size = New System.Drawing.Size(175, 50)
        Me.btnRelatorioEstoque.TabIndex = 4
        Me.btnRelatorioEstoque.Text = "Relatório de Estoque"
        Me.btnRelatorioEstoque.UseVisualStyleBackColor = True
        '
        'btnFecharApp
        '
        Me.btnFecharApp.Location = New System.Drawing.Point(438, 129)
        Me.btnFecharApp.Name = "btnFecharApp"
        Me.btnFecharApp.Size = New System.Drawing.Size(165, 41)
        Me.btnFecharApp.TabIndex = 3
        Me.btnFecharApp.Text = "Fechar a Aplicação"
        Me.btnFecharApp.UseVisualStyleBackColor = True
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnRelatorioEstoque)
        Me.Controls.Add(Me.btnFecharApp)
        Me.Controls.Add(Me.btnMovimentacao)
        Me.Controls.Add(Me.btnRelatorioVencimento)
        Me.Controls.Add(Me.btnCadastroProduto)
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
