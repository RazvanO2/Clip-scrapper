Imports System.IO
Public Class Form2
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Dim ayyy As StreamReader
        Try
            ayyy = New StreamReader("streameri.txt")
        Catch ex As Exception
            File.WriteAllText("streameri.txt", "")
            ayyy = New StreamReader("streameri.txt")
        End Try

        Dim x As String = ayyy.ReadToEnd
        Dim y As String() = x.Split(",")
        For Each s As String In y
            ComboBox1.Items.Add(s)
        Next
        ComboBox1.Update()
        ayyy.Close()
        Updateq()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox1.Items.Remove(ComboBox1.SelectedItem)
        Updateq()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox1.Items.Add(TextBox1.Text)
        Updateq()

    End Sub
    Public Sub Updateq()
        ComboBox1.Update()
        Dim i As Integer
        Dim txt_File As String
        txt_File = ""
        Label1.Text = ""
        For i = 1 To ComboBox1.Items.Count - 1 Step 1
            Label1.Text = Label1.Text + Environment.NewLine + ComboBox1.Items.Item(i)
            If Not (i = 1) Then
                txt_File = txt_File + "," + ComboBox1.Items.Item(i)
            Else
                Try
                    txt_File = ComboBox1.Items.Item(1)
                Catch ex As Exception

                End Try
            End If
        Next
        Try
            My.Computer.FileSystem.DeleteFile("streameri.txt")
        Catch ex As Exception

        End Try
        File.WriteAllText("streameri.txt", txt_File)
        '  Label3.Text = ComboBox1.SelectedIndex
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form1.Close()
    End Sub
End Class