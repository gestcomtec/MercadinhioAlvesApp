<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCadastroProduto
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
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.txtCodigoBarras = New System.Windows.Forms.TextBox()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.txtUnidadeMedida = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CbtCategoria = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.txtCodigoLote = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpValidade = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnVoltar = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtQuantidade = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMarca = New System.Windows.Forms.TextBox()
        Me.txtFornecedor = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtNome
        '
        Me.txtNome.Location = New System.Drawing.Point(192, 113)
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(100, 20)
        Me.txtNome.TabIndex = 0
        '
        'txtCodigoBarras
        '
        Me.txtCodigoBarras.Location = New System.Drawing.Point(192, 151)
        Me.txtCodigoBarras.Name = "txtCodigoBarras"
        Me.txtCodigoBarras.Size = New System.Drawing.Size(100, 20)
        Me.txtCodigoBarras.TabIndex = 1
        '
        'txtDescricao
        '
        Me.txtDescricao.Location = New System.Drawing.Point(192, 178)
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(100, 20)
        Me.txtDescricao.TabIndex = 2
        '
        'txtUnidadeMedida
        '
        Me.txtUnidadeMedida.Location = New System.Drawing.Point(201, 214)
        Me.txtUnidadeMedida.Name = "txtUnidadeMedida"
        Me.txtUnidadeMedida.Size = New System.Drawing.Size(100, 20)
        Me.txtUnidadeMedida.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(92, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Nome do Produto"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(95, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Código de Barras"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(98, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Descrição"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 214)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Unidade de Medida"
        '
        'CbtCategoria
        '
        Me.CbtCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbtCategoria.FormattingEnabled = True
        Me.CbtCategoria.Location = New System.Drawing.Point(497, 107)
        Me.CbtCategoria.Name = "CbtCategoria"
        Me.CbtCategoria.Size = New System.Drawing.Size(121, 21)
        Me.CbtCategoria.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(411, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = " Categoria"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(427, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Fornecedor"
        '
        'btnSalvar
        '
        Me.btnSalvar.Location = New System.Drawing.Point(376, 360)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(203, 43)
        Me.btnSalvar.TabIndex = 12
        Me.btnSalvar.Text = "Salvar Produto"
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'txtCodigoLote
        '
        Me.txtCodigoLote.Location = New System.Drawing.Point(201, 257)
        Me.txtCodigoLote.Name = "txtCodigoLote"
        Me.txtCodigoLote.Size = New System.Drawing.Size(100, 20)
        Me.txtCodigoLote.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(78, 263)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Código do Lote"
        '
        'dtpValidade
        '
        Me.dtpValidade.Location = New System.Drawing.Point(201, 297)
        Me.dtpValidade.Name = "dtpValidade"
        Me.dtpValidade.Size = New System.Drawing.Size(200, 20)
        Me.dtpValidade.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(81, 304)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Data de Vencimento"
        '
        'btnVoltar
        '
        Me.btnVoltar.Location = New System.Drawing.Point(657, 374)
        Me.btnVoltar.Name = "btnVoltar"
        Me.btnVoltar.Size = New System.Drawing.Size(75, 23)
        Me.btnVoltar.TabIndex = 16
        Me.btnVoltar.Text = "Voltar"
        Me.btnVoltar.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(414, 220)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Quantidade"
        '
        'txtQuantidade
        '
        Me.txtQuantidade.Location = New System.Drawing.Point(522, 220)
        Me.txtQuantidade.MaxLength = 6
        Me.txtQuantidade.Name = "txtQuantidade"
        Me.txtQuantidade.Size = New System.Drawing.Size(100, 20)
        Me.txtQuantidade.TabIndex = 18
        Me.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(420, 303)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Marca"
        '
        'txtMarca
        '
        Me.txtMarca.Location = New System.Drawing.Point(463, 296)
        Me.txtMarca.Name = "txtMarca"
        Me.txtMarca.Size = New System.Drawing.Size(100, 20)
        Me.txtMarca.TabIndex = 20
        '
        'txtFornecedor
        '
        Me.txtFornecedor.Location = New System.Drawing.Point(497, 172)
        Me.txtFornecedor.Name = "txtFornecedor"
        Me.txtFornecedor.Size = New System.Drawing.Size(100, 20)
        Me.txtFornecedor.TabIndex = 21
        '
        'frmCadastroProduto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtFornecedor)
        Me.Controls.Add(Me.txtMarca)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtQuantidade)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnVoltar)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dtpValidade)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCodigoLote)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CbtCategoria)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUnidadeMedida)
        Me.Controls.Add(Me.txtDescricao)
        Me.Controls.Add(Me.txtCodigoBarras)
        Me.Controls.Add(Me.txtNome)
        Me.Name = "frmCadastroProduto"
        Me.Text = "frmCadastroProduto"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtNome As TextBox
    Friend WithEvents txtCodigoBarras As TextBox
    Friend WithEvents txtDescricao As TextBox
    Friend WithEvents txtUnidadeMedida As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CbtCategoria As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnSalvar As Button
    Friend WithEvents txtCodigoLote As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpValidade As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents btnVoltar As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents txtQuantidade As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtMarca As TextBox
    Friend WithEvents txtFornecedor As TextBox
End Class
