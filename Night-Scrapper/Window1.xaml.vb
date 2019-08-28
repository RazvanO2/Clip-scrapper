Imports System.IO
Public Class Window1
    Private Sub Mainscreen_loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim fisier As String
        Dim locatie_stream As String = "Resurse/streamer_list"
        If Not (System.IO.File.Exists(locatie_stream & ".txt")) Then
            File.WriteAllText(locatie_stream & ".txt", "")
        Else
            fisier = My.Computer.FileSystem.ReadAllText(locatie_stream & ".txt")
            Cutie.Document.Blocks.Clear()
            Cutie.Document.Blocks.Add(New Paragraph(New Run(fisier)))
        End If
    End Sub
    Private Sub mickey_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles mickey.MouseDown
        My.Computer.Audio.Play(My.Resources.mik, AudioPlayMode.Background)
    End Sub

    Private Sub Scrapper_Click(sender As Object, e As RoutedEventArgs) Handles Scrapper.Click

    End Sub
End Class
