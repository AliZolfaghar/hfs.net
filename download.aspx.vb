Imports System.IO
Partial Class Download
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim file_path = Server.MapPath("~/app_data/" + Session("cname") + "/" + Request.QueryString("filename"))
        If File.Exists(file_path) Then

            Dim file_info = New FileInfo(file_path)
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment;filename=" + file_info.Name)
            Response.TransmitFile(file_path)
            Response.End()
        End If
    End Sub
End Class
