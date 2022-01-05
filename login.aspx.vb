
Partial Class login
    Inherits System.Web.UI.Page

    Sub Fail_login()
        Session("islogin") = False
        Session("userid") = ""
        Session("username") = ""
        Session("cname") = ""
        Session("fullname") = ""

        Response.Redirect("/upload")

        message.Text = "نام کاربری یا کلمه عبور اشتباه است"
        message_alert.Visible = True

    End Sub

    Sub Success_login(user As dal.tbl_usersRow)
        Session("islogin") = True
        Session("userid") = user.Id
        Session("username") = user.username
        Session("cname") = user.cname
        Session("fullname") = user.fullname
        Response.Redirect("/upload")
    End Sub

    Protected Sub Btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click

        Dim isHuman = captchabox.Validate(captcha.Text)

        If isHuman Then


            Dim cmd As New dalTableAdapters.tbl_usersTableAdapter
            Dim tbl As New dal.tbl_usersDataTable
            cmd.FillBy_userpass(tbl, username.Text, password.Text)
            If tbl.Rows.Count > 0 Then
                Dim user As dal.tbl_usersRow = tbl.Rows(0)
                If user.username = username.Text And user.password = password.Text Then
                    Success_login(user)
                Else
                    Fail_login()
                End If
            Else
                Fail_login()
            End If
        Else
            message.Text = "کپجای وارد شده اشتباه است"
            message_alert.Visible = True
        End If

    End Sub
End Class
