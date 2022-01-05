
Partial Class logout
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session("islogin") = False
        Response.Redirect("/login")
    End Sub
End Class
