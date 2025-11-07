Imports System.Data.SQLite

Public Class frmMovimentacao

    ' Controles criados via código com nomes únicos
    Private comboLote As ComboBox
    Private comboTipoMov As ComboBox
    Private txtQtd As TextBox
    Private txtObs As TextBox
    Private btnSalvar As Button
    Private btnVoltarTela As Button

    ' Evita flickering na renderização
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    ' Evento de carregamento do formulário
    Private Sub frmMovimentacao_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()

        ' Configurações visuais do formulário
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackgroundImage = My.Resources.frmMoviemntoEstoque
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Caixa de agrupamento transparente
        Dim gbControles As New GroupBox With {
            .Text = "",
            .BackColor = Color.Transparent,
            .Size = New Size(520, 280),
            .Location = New Point((Me.Width - 520) \ 2, Me.Height - 320)
        }
        Me.Controls.Add(gbControles)

        ' Fontes
        Dim fonteLabel As New Font("Segoe UI", 11, FontStyle.Bold)
        Dim fonteCampo As New Font("Segoe UI", 11, FontStyle.Regular)

        ' Label e ComboBox: Lote
        gbControles.Controls.Add(New Label With {.Text = "Lote:", .Location = New Point(30, 25), .AutoSize = True, .Font = fonteLabel})
        comboLote = New ComboBox With {.Location = New Point(180, 20), .Width = 300, .Font = fonteCampo}
        gbControles.Controls.Add(comboLote)

        ' Label e ComboBox: Tipo de Movimento
        gbControles.Controls.Add(New Label With {.Text = "Tipo de Movimento:", .Location = New Point(30, 65), .AutoSize = True, .Font = fonteLabel})
        comboTipoMov = New ComboBox With {.Location = New Point(180, 60), .Width = 300, .Font = fonteCampo}
        gbControles.Controls.Add(comboTipoMov)

        ' Label e TextBox: Quantidade
        gbControles.Controls.Add(New Label With {.Text = "Quantidade:", .Location = New Point(30, 105), .AutoSize = True, .Font = fonteLabel})
        txtQtd = New TextBox With {.Location = New Point(180, 100), .Width = 300, .Font = fonteCampo}
        gbControles.Controls.Add(txtQtd)

        ' Label e TextBox: Observação
        gbControles.Controls.Add(New Label With {.Text = "Observação:", .Location = New Point(30, 145), .AutoSize = True, .Font = fonteLabel})
        txtObs = New TextBox With {.Location = New Point(180, 140), .Width = 300, .Font = fonteCampo}
        gbControles.Controls.Add(txtObs)

        ' Botão: Registrar
        btnSalvar = New Button With {
            .Text = "Registrar",
            .Location = New Point(80, 200),
            .Size = New Size(120, 45),
            .BackColor = Color.DodgerBlue,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        AddHandler btnSalvar.Click, AddressOf btnSalvar_Click
        gbControles.Controls.Add(btnSalvar)

        ' Botão: Voltar
        btnVoltarTela = New Button With {
            .Text = "Voltar",
            .Location = New Point(300, 200),
            .Size = New Size(120, 45),
            .BackColor = Color.Gray,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        AddHandler btnVoltarTela.Click, AddressOf btnVoltarTela_Click
        gbControles.Controls.Add(btnVoltarTela)

        ' Carrega dados dos lotes
        Dim dtLotes = DBHelper.Consultar("SELECT lote_id, codigo_lote FROM Lotes WHERE ativo = 1")
        comboLote.DataSource = dtLotes
        comboLote.DisplayMember = "codigo_lote"
        comboLote.ValueMember = "lote_id"

        ' Tipos de movimentação (corrigido para valores válidos no banco)
        comboTipoMov.Items.Add("entrada")
        comboTipoMov.Items.Add("saida") ' sem acento, conforme CHECK constraint
        comboTipoMov.SelectedIndex = 0

        Me.ResumeLayout()
    End Sub

    ' Botão: Registrar movimentação
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs)
        If comboLote.SelectedIndex = -1 Or comboTipoMov.SelectedIndex = -1 Or txtQtd.Text = "" Then
            MessageBox.Show("Preencha os campos obrigatórios.")
            Exit Sub
        End If

        Dim quantidade As Integer
        If Not Integer.TryParse(txtQtd.Text, quantidade) Or quantidade <= 0 Then
            MessageBox.Show("Quantidade inválida.")
            Exit Sub
        End If

        Dim loteId As Integer = Convert.ToInt32(comboLote.SelectedValue)
        Dim tipoMov As String = comboTipoMov.Text ' já corrigido para valores válidos

        Try
            ' Validação de estoque para saída
            If tipoMov = "saida" Then
                Dim dtEstoque As New DataTable()
                Using conn As New SQLiteConnection("Data Source=MercadinhoAlves.db;Version=3;")
                    conn.Open()
                    Dim sqlConsultaEstoque As String = "
                        SELECT quantidade 
                        FROM Estoque 
                        WHERE lote = (SELECT codigo_lote FROM Lotes WHERE lote_id = @lote)"
                    Using cmd As New SQLiteCommand(sqlConsultaEstoque, conn)
                        cmd.Parameters.AddWithValue("@lote", loteId)
                        Using da As New SQLiteDataAdapter(cmd)
                            da.Fill(dtEstoque)
                        End Using
                    End Using
                End Using

                If dtEstoque.Rows.Count = 0 Then
                    MessageBox.Show("Lote não encontrado no estoque.")
                    Exit Sub
                End If

                Dim quantidadeAtual As Integer = Convert.ToInt32(dtEstoque.Rows(0)("quantidade"))
                If quantidade > quantidadeAtual Then
                    MessageBox.Show("Quantidade solicitada excede o estoque disponível.")
                    Exit Sub
                End If
            End If

            ' Insere movimentação no banco
            Dim sqlMov As String = "
                INSERT INTO Movimentacoes 
                (lote_id, tipo, quantidade, usuario, observacao) 
                VALUES (@lote, @tipo, @qtd, @user, @obs)"
            DBHelper.ExecutarComando(sqlMov, New Dictionary(Of String, Object) From {
                {"@lote", loteId},
                {"@tipo", tipoMov},
                {"@qtd", quantidade},
                {"@user", Environment.UserName},
                {"@obs", txtObs.Text}
            })

            ' Atualiza estoque e lote com operador correto
            Dim operador As String = If(tipoMov = "entrada", "+", "-")
            DBHelper.ExecutarComando($"UPDATE Lotes SET quantidade = quantidade {operador} @qtd WHERE lote_id = @lote",
                New Dictionary(Of String, Object) From {{"@qtd", quantidade}, {"@lote", loteId}})
            DBHelper.ExecutarComando($"UPDATE Estoque SET quantidade = quantidade {operador} @qtd WHERE lote = (SELECT codigo_lote FROM Lotes WHERE lote_id = @lote)",
                New Dictionary(Of String, Object) From {{"@qtd", quantidade}, {"@lote", loteId}})

            MessageBox.Show("Movimentação registrada e estoque atualizado!")
            LimparCampos()

        Catch ex As Exception
            MessageBox.Show("Erro ao registrar movimentação: " & ex.Message)
        End Try
    End Sub

    ' Botão: Voltar ao dashboard
    Private Sub btnVoltarTela_Click(sender As Object, e As EventArgs)
        LimparCampos()
        frmDashboard.Show()
        Me.Hide()
    End Sub

    ' Limpa os campos
    Private Sub LimparCampos()
        comboLote.SelectedIndex = -1
        comboTipoMov.SelectedIndex = 0
        txtQtd.Clear()
        txtObs.Clear()
    End Sub

End Class
