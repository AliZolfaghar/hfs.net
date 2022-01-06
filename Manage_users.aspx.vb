Imports System.Drawing
Partial Class Admin_Manage_users
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    
    End Sub
    
    Protected Sub Button_Search_Click(sender As Object, e As EventArgs) Handles Button_Search.Click
        GridView_List.DataBind()
    End Sub
    
    Protected Sub Button_AddNew_Click(sender As Object, e As EventArgs) Handles Button_AddNew.Click
        Switch_To_Insert()
    End Sub
    Protected Sub Button_Edit_Click(sender As Object, e As EventArgs)
        Dim oSender As Button = sender
        Switch_To_Edit()
        Load_Data(oSender.CommandArgument)
    End Sub
    Protected Sub Button_SaveNew_Click(sender As Object, e As EventArgs) Handles Button_SaveNew.Click
        Save_New()
    End Sub
    Protected Sub Button_SaveChanges_Click(sender As Object, e As EventArgs) Handles Button_SaveChanges.Click
        Save_Changes()
    End Sub
    Protected Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Switch_To_List()
    End Sub
    Protected Sub Button_Delete_Click(sender As Object, e As EventArgs) Handles Button_Delete.Click
        Delete_Data()
    End Sub
    Sub Switch_To_List()
        Panel_list.Visible = True
        Panel_Edit.Visible = False
        Button_AddNew.Visible = True
    End Sub
    Sub Switch_To_Insert()
        Panel_list.Visible = False
        Panel_Edit.Visible = True
        Button_AddNew.Visible = False
        Label_EditTitle.Text = "افزودن"
        Button_SaveNew.Visible = True
        Button_SaveChanges.Visible = False
        Button_Cancel.Visible = True
        Button_Delete.Visible = False
        Label_Message.Text = ""
        '-> remove old data 
        TextBox_username.Text = ""
        TextBox_cname.Text = ""
        TextBox_fullname.Text = ""
        TextBox_password.Text = ""
        TextBox_isactive.Text = ""
        TextBox_detail.Text = ""
    End Sub
    Sub Switch_To_Edit()
        Panel_list.Visible = False
        Panel_Edit.Visible = True
        Button_AddNew.Visible = False
        Label_EditTitle.Text = "ویرایش"
        Button_SaveNew.Visible = False
        Button_SaveChanges.Visible = True
        Button_Cancel.Visible = True
        Button_Delete.Visible = True
    End Sub
    Function Validate_Inputs() As Boolean
        Dim RV As Boolean = True
        Label_Message.ForeColor = Color.Red
        If TextBox_username.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام کاربری را وارد کنید."
            TextBox_username.Focus()
            Return False
        End If

        If TextBox_cname.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام انگلیسی را وارد کنید."
            TextBox_cname.Focus()
            Return False
        End If

        If TextBox_fullname.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام و نام خانوادگ را وارد کنید."
            TextBox_fullname.Focus()
            Return False
        End If

        If TextBox_password.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا کلمه عبور را وارد کنید."
            TextBox_password.Focus()
            Return False
        End If

        If TextBox_isactive.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا فعال را وارد کنید."
            TextBox_isactive.Focus()
            Return False
        End If

        If TextBox_detail.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا توضیحات را وارد کنید."
            TextBox_detail.Focus()
            Return False
        End If

        Return RV
    End Function
    Sub Save_New()
        If Validate_Inputs() Then
            Dim SqlStr As String = "INSERT INTO dbo.tbl_users (username,cname,fullname,password,isactive,detail) VALUES (@username,@cname,@fullname,@password,@isactive,@detail) "
            Dim DAL as New AZDBL 
            DAL.SqlStr = SqlStr
            DAL.Params.Clear()
            DAL.Params.Add("@username", TextBox_username.Text) 
            DAL.Params.Add("@cname", TextBox_cname.Text) 
            DAL.Params.Add("@fullname", TextBox_fullname.Text) 
            DAL.Params.Add("@password", TextBox_password.Text) 
            DAL.Params.Add("@isactive", TextBox_isactive.Text) 
            DAL.Params.Add("@detail", TextBox_detail.Text) 
            Try 
                DAL.ExecuteNonQuery()
                Switch_To_List()
                GridView_List.DataBind()
                Label_ActionMessage.Text = "اطلاعات با موفقیت ثبت شد."
            Catch ex As Exception
                Label_Message.Text = "ثبت اطلاعات در بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
            End Try
            DAL.Dispose() 
        End If
    End Sub
    Sub Load_Data(ID As Integer)
        Dim SqlStr As String = "SELECT * FROM dbo.tbl_users WHERE id=@id" 
        Dim DAL As New AZDBL
        DAL.SqlStr = SqlStr
        DAL.Params.Clear()
        DAL.Params.Add("@id", ID)
        Dim DT As New System.Data.DataTable
        Try
            DT = DAL.ExecuteDatatable()
        Catch ex As Exception
            
        End Try
        DAL.Dispose()
        
        For Each row As System.Data.DataRow In DT.Rows
            Label_id.Text = row.Item("id")
            TextBox_username.Text = row.Item("username").ToString()
            TextBox_cname.Text = row.Item("cname").ToString()
            TextBox_fullname.Text = row.Item("fullname").ToString()
            TextBox_password.Text = row.Item("password").ToString()
            TextBox_isactive.Text = row.Item("isactive").ToString()
            TextBox_detail.Text = row.Item("detail").ToString()
        Next
        DT.Dispose()
    End Sub
    Sub Save_Changes()
        If Validate_Inputs() Then
            Dim SqlStr As String = "UPDATE dbo.tbl_users SET  username=@username , cname=@cname , fullname=@fullname , password=@password , isactive=@isactive , detail=@detail WHERE id=@id "
            Dim DAL As New AZDBL
            DAL.SqlStr = SqlStr
            DAL.Params.Clear()
            DAL.Params.Add("@id", Label_id.Text)
            DAL.Params.Add("@username", TextBox_username.Text)
            DAL.Params.Add("@cname", TextBox_cname.Text)
            DAL.Params.Add("@fullname", TextBox_fullname.Text)
            DAL.Params.Add("@password", TextBox_password.Text)
            DAL.Params.Add("@isactive", TextBox_isactive.Text)
            DAL.Params.Add("@detail", TextBox_detail.Text)
            Try
                DAL.ExecuteNonQuery()
                Switch_To_List()
                GridView_List.DataBind()
                Label_ActionMessage.Text = "اطلاعات با موفقیت به روز رسانی شد."
            Catch ex As Exception
                Label_Message.Text = "ثبت تغییرات در بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
                
            End Try
            DAL.Dispose()
            
        End If
    End Sub
    Sub Delete_Data()
        Dim SqlStr As String = "DELETE FROM dbo.tbl_users WHERE id=@id" 
        
        Dim DAL As New AZDBL
        DAL.SqlStr = SqlStr
        DAL.Params.Clear()
        DAL.Params.Add("@id", Label_id.Text)
        Try
            DAL.ExecuteNonQuery()
            Switch_To_List()
            GridView_List.DataBind()
            Label_ActionMessage.Text = "اطلاعات با موفقیت حذف شد."
        Catch ex As Exception
            Label_Message.Text = "حذف اطلاعات از بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
        End Try
        DAL.Dispose()
    End Sub
End Class
