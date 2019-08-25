Imports System.IO
Imports System.Text
Imports System.Net
Class MainWindow
    Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim fileReader As String
        Dim cmd As New Process
        Dim argument As New ProcessStartInfo("cmd.exe", "/c py -m pip install requests > temp_a.txt && py -m pip install dpath > temp_b.txt && py -m pip install xlwt > temp_c.txt")
        Dim git_py As String = "https://gist.githubusercontent.com/Far0/78beb7eed6c5f9ca51ab0569f53ee320/raw"
        Dim git_app As String = "https://gist.githubusercontent.com/Far0/f7539ccf65cfe3b8f396b8bdd98d7cc6/raw"
        Dim git_upd As String = "https://gist.githubusercontent.com/Far0/6b6c8dc6e614d6416d46f1dc1ff3708f/raw"
        Dim git_scr As String = "https://gist.githubusercontent.com/Far0/d8cb0af2bed0284b00bcd1634f36b1d6/raw"
        Dim git_dwn As String = "https://gist.githubusercontent.com/Far0/581356ead6fa2b813ef7557fc95b9641/raw"
        Dim client As WebClient = New WebClient()
        Button1.Visibility = Visibility.Hidden
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
        Label3.Content = "Versiune aplicație: " & My.Settings.verslocal & Environment.NewLine & "Versiune script-uri: " & My.Settings.verslocal_py
        If My.Settings.testul_1 = "0" Then
            Process.Start("cmd", "/c py --version > t.txt")
            Threading.Thread.Sleep(1000)
            fileReader = My.Computer.FileSystem.ReadAllText("t.txt")
            If Not (fileReader = "") Then
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 1] Python este instalat.")
                My.Settings.testul_1 = "1"
            Else
                MsgBox("[STAGE 1] Nu ai Python 3.7.4 instalat!")
                Process.Start("https://www.python.org/downloads/")
                Me.Close()
            End If
        Else
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 1] Python este deja instalat.")
        End If
        If My.Settings.verslocal >= vers Then
            If My.Settings.verslocal > vers Then
                RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Ai o versiune BETA de Night Scrapper.")
                Button1.Visibility = Visibility.Visible
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Ai deja ultima versiune de Night Scrapper.")
            End If
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
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Nu am gasit updater.py, se descarcă oricum.")
                File.WriteAllText("updater.py", updater)
                Process.Start("cmd", "/c py updater.py")
                Me.Close()
            End If
        End If
        If My.Settings.testul_2 = "0" Then
            cmd.StartInfo = argument
            cmd.Start()
            cmd.WaitForExit()
            fileReader = My.Computer.FileSystem.ReadAllText("temp_a.txt")
            If fileReader.StartsWith("Requirement") Then
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Requests este instalat deja.")
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul Requests s-a instalat.")
            End If
            fileReader = My.Computer.FileSystem.ReadAllText("temp_b.txt")
            If fileReader.StartsWith("Requirement") Then
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Dpath este instalat deja.")
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul Dpath s-a instalat.")
            End If
            fileReader = My.Computer.FileSystem.ReadAllText("temp_c.txt")
            If fileReader.StartsWith("Requirement") Then
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Xlwt este instalat deja.")
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul Xlwt s-a instalat.")
            End If
            My.Settings.testul_2 = 1
            Try
                My.Computer.FileSystem.DeleteFile("temp_a.txt")
                My.Computer.FileSystem.DeleteFile("temp_b.txt")
                My.Computer.FileSystem.DeleteFile("temp_c.txt")
                My.Computer.FileSystem.DeleteFile("t.txt")
            Catch ex As Exception
                RichTextBox1.AppendText(Environment.NewLine + "[INFO] Nu au fost găsite fișiere temporare.")
            End Try
        Else
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Toate modulele necesare sunt instalate deja.")
        End If
        If System.IO.File.Exists("scrapper.py") And System.IO.File.Exists("downloader.py") Then
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Am găsit cele 2 scripturi python.")
            fileReader = My.Computer.FileSystem.ReadAllText("scrapper.py")
            If Not (scrap = fileReader) Then
                My.Computer.FileSystem.DeleteFile("scrapper.py")
                File.WriteAllText("scrapper.py", scrap)
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Scrapper.py s-a updatat.")
            End If
            fileReader = My.Computer.FileSystem.ReadAllText("Downloader.py")
            If Not (down = fileReader) Then
                My.Computer.FileSystem.DeleteFile("Downloader.py")
                File.WriteAllText("downloader.py", down)
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Downloader.py s-a updatat.")
            End If
        Else
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Nu am găsit cele 2 scripturi python, se descarcă...")
            File.WriteAllText("downloader.py", down)
            File.WriteAllText("scrapper.py", scrap)
        End If
        My.Settings.verslocal_py = vers_py
        Label3.Content = "Versiune aplicație: " & My.Settings.verslocal & Environment.NewLine & "Versiune script-uri: " & My.Settings.verslocal_py
        My.Settings.Save()
        RichTextBox1.AppendText(Environment.NewLine + "[FINISHED] Am terminat.")

        Dim win1 As Window1 = New Window1()
        win1.Show()
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles Button1.Click
        My.Settings.testul_1 = 0
        My.Settings.testul_2 = 0
        My.Settings.testul_3 = 0
        My.Settings.Save()
    End Sub
End Class
