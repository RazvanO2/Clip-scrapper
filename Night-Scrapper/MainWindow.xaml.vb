Imports System.IO
Imports System.Net
Class MainWindow
    Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim fisier As String
        Dim cmd As New Process
        Dim linkuri() As String = {"https://gist.githubusercontent.com/Far0/78beb7eed6c5f9ca51ab0569f53ee320/raw", "https://gist.githubusercontent.com/Far0/f7539ccf65cfe3b8f396b8bdd98d7cc6/raw", "https://gist.githubusercontent.com/Far0/6b6c8dc6e614d6416d46f1dc1ff3708f/raw", "https://gist.githubusercontent.com/Far0/d8cb0af2bed0284b00bcd1634f36b1d6/raw", "https://gist.githubusercontent.com/Far0/581356ead6fa2b813ef7557fc95b9641/raw"}
        'linkuri(0) - versiune script; linkuri(1) - versiune aplicatie; linkuri(2) updater; linkuri(3) scrapper; linkuri(4) downloader
        Dim versiune(1) As Double
        'versiune(0) - script; versiune(1) aplicatie;
        Dim scripturi(2) As String
        Dim scripturi_nume() As String = {"updater", "scrapper", "downloader"}
        Dim modul() As String = {"requests", "dpath", "xlwt"}
        Dim locatie As String = "Resurse/"
        Dim argument As New ProcessStartInfo("cmd.exe", "/c py -m pip install " + modul(0) + " > " + modul(0) + ".info && py -m pip install " + modul(1) + " > " + modul(1) + ".info && py -m pip install " + modul(2) + " > " + modul(2) + ".info")
        Dim web As WebClient = New WebClient()
        Label3.Content = "Versiune aplicație: " & My.Settings.aplicatie & Environment.NewLine & "Versiune script-uri: " & My.Settings.scripturi
        System.IO.Directory.CreateDirectory("Resurse")
        For i = 0 To linkuri.Length - 1
            Dim browser As StreamReader = New StreamReader(web.OpenRead(linkuri(i)))
            If 1 >= i Then
                Dim z As Double = browser.ReadToEnd
                versiune(i) = z
            Else
                Dim zz As String = browser.ReadToEnd
                scripturi(i - 2) = zz
            End If
            If linkuri.Length - 1 = i Then
                browser.Close()
            End If
        Next
        If My.Settings.testul_1 = 0 Then
            Process.Start("cmd", "/c py --version > python_versiune.info")
            Threading.Thread.Sleep(1500)
            fisier = My.Computer.FileSystem.ReadAllText("python_versiune.info")
            If (fisier.Contains("Python")) Then
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 1] Python este deja instalat.")
                My.Settings.testul_1 = 1
                My.Computer.FileSystem.DeleteFile("python_versiune.info")
            Else
                Process.Start("https://www.python.org/ftp/python/3.7.4/python-3.7.4.exe")
                MsgBox("[STAGE 1] Nu ai Python 3.7 instalat!")
                Close()
            End If
        Else
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 1] Python este deja instalat.")
        End If
        If My.Settings.aplicatie >= versiune(1) Then
            If My.Settings.aplicatie > versiune(1) Then
                RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Ai o versiune BETA de Night Scrapper.")
                My.Settings.BETA = 1
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Ai deja ultima versiune de Night Scrapper.")
            End If
        Else
            MsgBox("Apasă ok pentru a descărca ultima versiune de Night Scrapper.", vbExclamation, "Night Scrapper")
            If System.IO.File.Exists(scripturi_nume(0) & ".py") Then
                fisier = My.Computer.FileSystem.ReadAllText(scripturi_nume(0) & ".py")
                If Not (scripturi(0) = fisier(0)) Then
                    My.Computer.FileSystem.DeleteFile(scripturi_nume(0) & ".py")
                    File.WriteAllText(scripturi_nume(0) & ".py", scripturi(0))
                    Process.Start("cmd", "/c py " & scripturi_nume(0) & ".py")
                    Close()
                End If
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Nu am gasit " & scripturi_nume(0) & ".py, se descarcă.")
                File.WriteAllText(scripturi_nume(0) & ".py", scripturi(0))
                Process.Start("cmd", "/c py " & scripturi_nume(0) & ".py")
                Close()
            End If
        End If

        If (My.Settings.testul_1 = 1) And (My.Settings.testul_2 = 0) Then
            cmd.StartInfo = argument
            cmd.Start()
            cmd.WaitForExit()
            For i = 0 To modul.Length - 1
                fisier = My.Computer.FileSystem.ReadAllText(modul(i) & ".info")
                If fisier.StartsWith("Requirement") Then
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul " & modul(i) & " este deja instalat.")
                Else
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul " & modul(i) & " s-a instalat.")
                End If
                Try
                    My.Computer.FileSystem.DeleteFile(modul(i) & ".info")
                Catch ex As Exception
                    RichTextBox1.AppendText(Environment.NewLine + "[INFO] Nu am găsit log file-ul " & modul(i) & ".info.")
                End Try
                If i = modul.Length - 1 Then
                    My.Settings.testul_2 = 1
                End If
            Next
        Else
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Toate modulele necesare sunt deja instalate.")
        End If
        For i = 1 To 2
            If System.IO.File.Exists(locatie & scripturi_nume(i) & ".py") Then
                fisier = My.Computer.FileSystem.ReadAllText(locatie & scripturi_nume(i) & ".py")
                If Not (fisier = scripturi(i)) Then
                    My.Computer.FileSystem.DeleteFile(locatie & scripturi_nume(i) & ".py")
                    File.WriteAllText(locatie & scripturi_nume(i) & ".py", scripturi(1))
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] " & scripturi_nume(i) & ".py s-a actualizat.")
                Else
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] " & scripturi_nume(i) & ".py corespunde cu cea mai recentă versiune.")
                End If
            Else
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Nu am găsit " & scripturi_nume(i) & ".py, se descarcă...")
                File.WriteAllText(locatie & scripturi_nume(i) & ".py", scripturi(i))
            End If
        Next
        My.Settings.scripturi = versiune(0)
        Label3.Content = "Versiune aplicație: " & My.Settings.aplicatie & Environment.NewLine & "Versiune script-uri: " & My.Settings.scripturi

        If My.Settings.BETA = 1 Then
            Button1.Visibility = Visibility.Visible
        End If


        RichTextBox1.AppendText(Environment.NewLine + "[FINISHED] Am terminat.")
        Dim win1 As Window1 = New Window1()
        win1.Show()
        Redeschide.Visibility = Visibility.Visible
        ' Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles Button1.Click
        My.Settings.testul_1 = 0
        My.Settings.testul_2 = 0
        My.Settings.testul_3 = 0
        My.Settings.Save()
    End Sub

    Private Sub Redeschide_Click(sender As Object, e As RoutedEventArgs) Handles Redeschide.Click
        Dim win1 As Window1 = New Window1()
        win1.Show()
    End Sub
End Class
