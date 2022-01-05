
Partial Class app
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Check_login()
    End Sub



    Sub Check_login()
        'Response.Write(Session("islogin"))

        If Session("islogin") = "True" Then
            ' -> login ok 
            login_link.Visible = False
            logout_link.Visible = True
            currentuser.Text = Session("username") + " / " + Session("fullname")
        Else
            login_link.Visible = True
            logout_link.Visible = False

            If Request.Path.ToUpper = "/login".ToUpper Then
                '-> in login page  
            Else
                '-> goto login page 
                Response.Redirect("/login")
            End If
        End If
    End Sub

End Class

