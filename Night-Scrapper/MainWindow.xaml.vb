Imports System.IO
Imports System.Text
Imports System.Net
Class MainWindow
    Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim fisier As String
        Dim cmd As New Process
        Dim linkuri() As String = {"https://gist.githubusercontent.com/Far0/78beb7eed6c5f9ca51ab0569f53ee320/raw", "https://gist.githubusercontent.com/Far0/f7539ccf65cfe3b8f396b8bdd98d7cc6/raw", "https://gist.githubusercontent.com/Far0/6b6c8dc6e614d6416d46f1dc1ff3708f/raw", "https://gist.githubusercontent.com/Far0/d8cb0af2bed0284b00bcd1634f36b1d6/raw", "https://gist.githubusercontent.com/Far0/581356ead6fa2b813ef7557fc95b9641/raw"}
        'linkuri(0) - versiune script; linkuri(1) - versiune aplicatie; linkuri(2) updater; linkuri(3) scrapper; linkuri(4) downloader
        Dim versiune() As Double = {}
        'versiune(0) - aplicatie; versiune(1) python;
        Dim scripturi() As String = {}
        'scripturi(0) - updater; scripturi(1) - downloader; scripturi(2) scrapper;

        Dim modul() As String = {"requests", "dpath", "xlwt"}
        Dim argument As New ProcessStartInfo("cmd.exe", "/c py --version > python_versiune.txt && py -m pip install " + modul(0) + " > " + modul(0) + ".txt && py -m pip install " + modul(1) + " > " + modul(1) + ".txt && py -m pip install " + modul(2) + " > " + modul(2) + ".txt")
        Dim web As WebClient = New WebClient()
        Button1.Visibility = Visibility.Hidden
        For i = 0 To linkuri.Length - 1 Step 1
            Dim browser As StreamReader = New StreamReader(web.OpenRead(linkuri(i)))
            Dim valori(i) As String = browser.ReadToEnd
            If linkuri.Length - 1 = i Then
                browser.Close()
            End If
        Next

        Label3.Content = "Versiune aplicație: " & My.Settings.verslocal & Environment.NewLine & "Versiune script-uri: " & My.Settings.verslocal_py
        If My.Settings.testul_1 = "0" Then
            fisier = My.Computer.FileSystem.ReadAllText("python_versiune.txt")
            If Not (fisier = "") Then
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
                fisier = My.Computer.FileSystem.ReadAllText("updater.py")
                If Not (updater = fisier) Then
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
        ' My.Settings.testul_2 = "0"
        If True Then
            For i = 0 To modul.Length - 1 Step 1
                fisier = My.Computer.FileSystem.ReadAllText(modul(i) & ".txt")
                If fisier.StartsWith("Requirement") Then
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul " & modul(i) & " este instalat deja.")
                Else
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul " & modul(i) & " s-a instalat.")
                End If
                Try
                    My.Computer.FileSystem.DeleteFile(modul(i) & ".txt")
                Catch ex As Exception
                    RichTextBox1.AppendText(Environment.NewLine + "[INFO] Nu a fost log file-ul pentru " & modul(i) & ".txt")

                End Try
            Next

        End If
















        If System.IO.File.Exists("scrapper.py") And System.IO.File.Exists("downloader.py") Then
            RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Am găsit cele 2 scripturi python.")
            fisier = My.Computer.FileSystem.ReadAllText("scrapper.py")
            If Not (scrap = fisier) Then
                My.Computer.FileSystem.DeleteFile("scrapper.py")
                File.WriteAllText("scrapper.py", scrap)
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Scrapper.py s-a updatat.")
            End If
            fisier = My.Computer.FileSystem.ReadAllText("Downloader.py")
            If Not (down = fisier) Then
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
        '     win1.Show()
        '   Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles Button1.Click
        My.Settings.testul_1 = 0
        My.Settings.testul_2 = 0
        My.Settings.testul_3 = 0
        My.Settings.Save()
    End Sub
End Class
