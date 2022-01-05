Imports System.IO

Partial Class upload
    Inherits System.Web.UI.Page


    Dim upload_folder As String = "" ' Server.MapPath("~/app_data/" + Session("cname"))
    Public files As String()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        upload_folder = Server.MapPath("~/app_data/" + Session("cname"))

        Load_file_list()
    End Sub

    Sub Load_file_list()

        If Directory.Exists(upload_folder) Then
            ' Response.Write("exists")
        Else
            ' Response.Write("not exists")
            Directory.CreateDirectory(upload_folder)
        End If

        files = Directory.GetFiles(upload_folder)

        For Each file In files
            '    Response.Write(file + "<br>")
        Next

    End Sub


    Protected Sub Btn_upload_Click(sender As Object, e As EventArgs) Handles btn_upload.Click
        If fileupload.HasFile Then
            Dim file = upload_folder + "\" + fileupload.FileName
            ' Response.Write(file)
            If System.IO.File.Exists(file) Then
                alert.Visible = True
            Else

                fileupload.SaveAs(file)
            End If
            Load_file_list()

        End If
    End Sub

    'Public Function filename(file As String) As String
    '    Dim info As New FileInfo(file)
    '    Return info.Name
    'End Function


End Class
