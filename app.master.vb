
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
            Check_Access()

        Else
            login_link.Visible = True
            logout_link.Visible = False
            users_link.Visible = False

            If Request.Path.ToUpper = "/login".ToUpper Then
                '-> in login page  
            Else
                '-> goto login page 
                Response.Redirect("/login")
            End If
        End If
    End Sub

    Sub Check_Access()

        '-> only admin can see /users 
        If Session("username").ToString.ToUpper = "Admin".ToUpper Then
            users_link.Visible = True
        Else
            users_link.Visible = False
        End If

        If Request.Path.ToUpper = "/users".ToUpper Then
            If Session("username").ToString.ToUpper = "Admin".ToUpper Then
                '-> ok 
            Else
                Response.Redirect("/404")
            End If
        End If

    End Sub


End Class

