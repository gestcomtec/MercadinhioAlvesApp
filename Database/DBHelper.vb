Imports System.Data.SQLite
Imports System.IO
Imports System.Windows.Forms

' Módulo principal para acesso ao banco de dados
Public Module DBHelper

    Public Function ConsultarScalar(query As String) As Object
        Dim caminhoBanco As String = Application.StartupPath & "\MercadinhoAlves.db"

        Using conn As New SQLiteConnection("Data Source=" & caminhoBanco)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                Dim resultado = cmd.ExecuteScalar()
                Return resultado
            End Using
        End Using
    End Function




    ' Método para consultar dados e retornar um DataTable
    Public Function Consultar(query As String) As DataTable
        Dim dt As New DataTable()
        Dim caminhoBanco As String = Application.StartupPath & "\MercadinhoAlves.db"

        Using conn As New SQLiteConnection("Data Source=" & caminhoBanco)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                Using da As New SQLiteDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    ' Método para executar comandos SQL com parâmetros (INSERT, UPDATE, DELETE)
    Public Sub ExecutarComando(query As String, parametros As Dictionary(Of String, Object))
        Dim caminhoBanco As String = Application.StartupPath & "\MercadinhoAlves.db"

        Using conn As New SQLiteConnection("Data Source=" & caminhoBanco)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                If parametros IsNot Nothing Then
                    For Each param In parametros
                        cmd.Parameters.AddWithValue(param.Key, param.Value)
                    Next
                End If
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Método para validar login de usuário
    Public Function ValidarLogin(usuario As String, senha As String) As Boolean
        Dim caminhoBanco As String = Application.StartupPath & "\MercadinhoAlves.db"
        Dim query As String = "SELECT COUNT(*) FROM Usuarios WHERE username = @usuario AND senha_hash = @senha"

        Using conn As New SQLiteConnection("Data Source=" & caminhoBanco)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@usuario", usuario)
                cmd.Parameters.AddWithValue("@senha", senha)

                Dim resultado As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return resultado > 0
            End Using
        End Using
    End Function


End Module

' Classe auxiliar para verificar e criar o banco de dados se necessário
Public Class BancoHelper
    Public Sub VerificarBancoInicial()
        Dim caminhoCompleto As String = Application.StartupPath & "\MercadinhoAlves.db"

        If Not File.Exists(caminhoCompleto) Then
            SQLiteConnection.CreateFile(caminhoCompleto)
        End If

        Using conn As New SQLiteConnection("Data Source=" & caminhoCompleto)
            conn.Open()
        End Using
    End Sub
End Class




