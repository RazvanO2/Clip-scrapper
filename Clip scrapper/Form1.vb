Imports System.IO
Imports System.Text
Imports System.Net

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fileReader As String
        Dim git_py As String = "https://gist.githubusercontent.com/Far0/78beb7eed6c5f9ca51ab0569f53ee320/raw"
        Dim git_app As String = "https://gist.githubusercontent.com/Far0/f7539ccf65cfe3b8f396b8bdd98d7cc6/raw"
        Dim git_upd As String = "https://gist.githubusercontent.com/Far0/6b6c8dc6e614d6416d46f1dc1ff3708f/raw"
        Dim git_scr As String = "https://gist.githubusercontent.com/Far0/d8cb0af2bed0284b00bcd1634f36b1d6/raw"
        Dim git_dwn As String = "https://gist.githubusercontent.com/Far0/581356ead6fa2b813ef7557fc95b9641/raw"
        Dim client As WebClient = New WebClient()

        Dim updaterrn As StreamReader = New StreamReader(client.OpenRead(git_upd), Encoding.UTF8)
        Dim updater As String = updaterrn.ReadToEnd
        updaterrn.Close()

        Dim versn_py As StreamReader = New StreamReader(client.OpenRead(git_py))
        Dim vers_py As Double = versn_py.ReadToEnd
        versn_py.Close()

        Dim versn_app As StreamReader = New StreamReader(client.OpenRead(git_app))
        Dim vers As Double = versn_app.ReadToEnd
        versn_app.Close()

        Dim dowwn As StreamReader = New StreamReader(client.OpenRead(git_dwn))
        Dim down As String = dowwn.ReadToEnd
        dowwn.Close()

        Dim sccr As StreamReader = New StreamReader(client.OpenRead(git_scr))
        Dim scrap As String = sccr.ReadToEnd
        sccr.Close()

        Label1.Parent = PictureBox1
        Label1.BackColor = Color.Transparent
        Label2.Parent = PictureBox1
        Label2.BackColor = Color.Transparent
        Label3.Parent = PictureBox1
        Label3.BackColor = Color.Transparent
        Label3.Text = "Versiune aplicație: " & My.Settings.verslocal & Environment.NewLine & "Versiune script-uri: " & My.Settings.verslocal_py


        Timer1.Start()


        If My.Settings.testul_1 = "0" Then

            Process.Start("cmd", "/c py --version > t.txt")
            Threading.Thread.Sleep(500)
            fileReader = My.Computer.FileSystem.ReadAllText("t.txt")
            If Not (fileReader = "") Then
                RichTextBox1.Text += Environment.NewLine + "[STAGE 1] Python este instalat, trecem mai departe."
                My.Settings.testul_1 = "1"
            Else
                MsgBox("[STAGE 1] Nu ai Python 3.7.4 instalat!")
                Process.Start("https://www.python.org/downloads/")
                Me.Close()
            End If
        Else
            RichTextBox1.Text += Environment.NewLine + "[STAGE 1] Python este deja instalat, trecem mai departe."
        End If

        MsgBox(vers)
        If vers = My.Settings.verslocal Then
            RichTextBox1.Text += Environment.NewLine + "[UPDATER] Ai deja ultima versiune de Night Scrapper."
        Else

            MsgBox("Apasă ok pentru a descărca ultima versiune de Night Scrapper :)", vbExclamation, "Nu ai ultima versiune de Night Scrapper")
            If System.IO.File.Exists("updater.py") Then
                fileReader = My.Computer.FileSystem.ReadAllText("updater.py")
                If Not (updater = fileReader) Then
                    My.Computer.FileSystem.DeleteFile("updater.py")
                    File.WriteAllText("updater.py", updater)
                    Process.Start("cmd", "/c py updater.py")
                    Me.Close()
                End If
            End If

        End If

        If My.Settings.testul_2 = "0" Then

            Process.Start("cmd", "/c py -m pip install requests > temp_a.txt")
            Threading.Thread.Sleep(3000)
            fileReader = My.Computer.FileSystem.ReadAllText("temp_a.txt")
            If fileReader.StartsWith("Requirement") Then
                RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Requests este instalat deja, trecem mai departe."
            Else
                RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Modulul Requests s-a instalat, trecem mai departe."
            End If

            Process.Start("cmd", "/c py -m pip install dpath > temp_b.txt")
            Threading.Thread.Sleep(3000)
            fileReader = My.Computer.FileSystem.ReadAllText("temp_b.txt")
            If fileReader.StartsWith("Requirement") Then
                RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Dpath este instalat deja, trecem mai departe."
            Else
                RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Modulul Dpath s-a instalat, trecem mai departe."
            End If


            Process.Start("cmd", "/c py -m pip install xlwt > temp_c.txt")
            Threading.Thread.Sleep(3000)
            fileReader = My.Computer.FileSystem.ReadAllText("temp_c.txt")
            If fileReader.StartsWith("Requirement") Then
                RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Xlwt este instalat deja, trecem mai departe."
            Else
                RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Modulul Xlwt s-a instalat, trecem mai departe."
            End If
            My.Settings.testul_2 = 1


        Else
            RichTextBox1.Text += Environment.NewLine + "[STAGE 2] Toate modulele necesare sunt instalate deja, trecem mai departe."
        End If

        Try
            My.Computer.FileSystem.DeleteFile("temp_a.txt")
            My.Computer.FileSystem.DeleteFile("temp_b.txt")
            My.Computer.FileSystem.DeleteFile("temp_c.txt")
            My.Computer.FileSystem.DeleteFile("t.txt")
        Catch ex As Exception
            RichTextBox1.Text += Environment.NewLine + "[INFO] Nu au fost gasite fisiere temporare."
        End Try


        If System.IO.File.Exists("scrapper.py") And System.IO.File.Exists("downloader.py") Then
            RichTextBox1.Text += Environment.NewLine + "[STAGE 3] Am găsit cele 2 scripturi python, trecem mai departe."
            fileReader = My.Computer.FileSystem.ReadAllText("scrapper.py")
            If Not (scrap = fileReader) Then
                My.Computer.FileSystem.DeleteFile("scrapper.py")
                File.WriteAllText("scrapper.py", scrap)
                RichTextBox1.Text += Environment.NewLine + "[STAGE 3] Scrapper.py s-a updatat."

            End If
            fileReader = My.Computer.FileSystem.ReadAllText("Downloader.py")
            If Not (down = fileReader) Then
                My.Computer.FileSystem.DeleteFile("Downloader.py")
                File.WriteAllText("downloader.py", down)
                RichTextBox1.Text += Environment.NewLine + "[STAGE 3] Downloader.py s-a updatat."

            End If

        Else
            RichTextBox1.Text += Environment.NewLine + "[STAGE 3] Nu am găsit cele 2 scripturi python, se descarcă..."
            File.WriteAllText("downloader.py", down)
            File.WriteAllText("scrapper.py", scrap)
        End If
        RichTextBox1.Text += Environment.NewLine + "[FINISHED] Am terminat."

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Label1.Text = "Loading..." Then
            Label1.Text = "Loading"
        Else
            Label1.Text = Label1.Text + "."
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.testul_1 = 0
        My.Settings.testul_2 = 0
        My.Settings.testul_3 = 0
        My.Settings.Save()
    End Sub
End Class