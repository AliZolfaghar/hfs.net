
Imports System.IO

Partial Class Admin_Generator_Generator
    Inherits System.Web.UI.Page

    Function CnnStr() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("CnnStr").ConnectionString
    End Function

    Protected Sub Button_Generate_Click(sender As Object, e As EventArgs) Handles Button_Generate.Click
        Generate_SourceCode()
    End Sub


    Sub Generate_SourceCode()
        Load_Table()

        '-> get table column names : 
        'Dim unused As New List(Of String)
        Dim Cols As List(Of String) = Get_Cols()

        Dim ASPX As String = Generate_ASPX(Cols)
        Dim ASPXVB As String = Generate_ASPXVB(Cols)

        TextBox_ASPX.Text = ASPX
        TextBox_ASPXVB.Text = ASPXVB

    End Sub


    Function Get_Cols() As List(Of String)
        Dim Cols As New List(Of String)

        ' Response.Write("get cols : ")
        ' Response.Write(DropDownList_TableName.SelectedValue)

        '-> get all fields from database in a datatable 
        ' Dim SqlStr As String = "Select Top 1 * from " + TextBox_TableName.Text
        Dim SqlStr As String = "Select Top 1 * from " + DropDownList_TableName.SelectedValue
        Dim DA As New System.Data.SqlClient.SqlDataAdapter(SqlStr, CnnStr())
        Dim DT As New System.Data.DataTable
        DA.Fill(DT)
        DA.Dispose()

        For Each col As System.Data.DataColumn In DT.Columns
            Cols.Add(col.ColumnName)
        Next

        DT.Dispose()

        Return Cols

    End Function



    Function Generate_ASPX(Cols As List(Of String)) As String
        Dim RV As String = ""

        RV += "<%@ Page Title="""" Language=""VB"" MasterPageFile=""~/App.master"" AutoEventWireup=""false"" CodeFile=""Manage_" + TextBox_PageName.Text + ".aspx.vb"" Inherits=""Admin_Manage_" + TextBox_PageName.Text + """ %>" + vbCrLf

        RV += "<asp:Content ID=""Content1"" ContentPlaceHolderID=""head"" runat=""Server""></asp:Content>" + vbCrLf
        RV += "<asp:Content ID=""Content2"" ContentPlaceHolderID=""ContentPlaceHolder1"" runat=""Server"">" + vbCrLf

        RV += "    <div class='rtl'>" + vbCrLf
        'RV += "    <div class=""card text-center iran header-title"">" + vbCrLf
        RV += "        <br><h4>" + TextBox_PageTitle.Text + "</h4>" + vbCrLf
        'RV += "    </div>" + vbCrLf
        RV += "    <hr />" + vbCrLf
        RV += "    <div>" + vbCrLf
        RV += "        <asp:Panel ID=""Panel_list"" runat=""server"" Visible=""true"">" + vbCrLf

        RV += "             <div class=""col-md-12 "">" + vbCrLf
        RV += "                 <div class=""form-inline form-group-sm rtl"">" + vbCrLf
        RV += "                     <asp:Button ID=""Button_AddNew"" runat=""server"" CssClass=""btn btn-sm btn-primary btn-110"" Text=""افزودن"" UseSubmitBehavior=""false"" />" + vbCrLf
        RV += "                     &nbsp;&nbsp;&nbsp;&nbsp;" + vbCrLf
        RV += "                     جستجو در این صفحه : &nbsp;<asp:TextBox ID=""TextBox_SearchValue"" CssClass=""form-control form-control-sm"" runat=""server""></asp:TextBox> &nbsp; " + vbCrLf
        RV += "                     <asp:Button ID=""Button_Search"" runat=""server"" CssClass=""btn btn-sm btn-info btn-110"" Text=""جستجو""  />" + vbCrLf
        RV += "                         &nbsp;" + vbCrLf
        RV += "                     <asp:Label CssClass=""form-control form-control-sm"" ID=""Label_ActionMessage"" runat=""server"" Text="""" ForeColor=""Green"" EnableViewState=""False""></asp:Label>" + vbCrLf
        RV += "                 </div>"
        RV += "             </div> <br>" + vbCrLf
        RV += "    <div class=""col-md-12 "">" + vbCrLf

        RV += "            <asp:SqlDataSource ID=""SqlDataSource_List"" runat=""server"" ConnectionString=""<%$ ConnectionStrings:CnnStr %>"" SelectCommand=""" + Generate_Select_Command() + """ >" + vbCrLf
        RV += "                <SelectParameters> " + vbCrLf
        RV += "                    <asp:ControlParameter ControlID=""TextBox_SearchValue"" DefaultValue=""%"" Name=""SV"" />" + vbCrLf
        RV += "                </SelectParameters>" + vbCrLf
        RV += "            </asp:SqlDataSource>" + vbCrLf

        RV += "    <asp:GridView CssClass=""table table-sm table-hover table-bordered table-striped az-table rtl"" ID=""GridView_List"" runat=""server"" AutoGenerateColumns=""False"" DataKeyNames=""" + TextBox_PrimaryKey.Text + """ DataSourceID=""SqlDataSource_List"" AllowPaging=""True"" AllowSorting=""True"" GridLines=""None"">" + vbCrLf
        RV += "        <Columns>" + vbCrLf
        RV += "            <asp:TemplateField HeaderText=""ردیف"" ItemStyle-CssClass=""text-center"" HeaderStyle-Width=""1%""><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>" + vbCrLf

        For Each col In Cols
            RV += "            <asp:BoundField DataField=""" + col + """ HeaderText=""" + Request.Form("Persianname_" + col) + """ SortExpression=""" + col + """ />" + vbCrLf
        Next

        RV += "         <asp:TemplateField>" + vbCrLf
        RV += "             <ItemTemplate>" + vbCrLf
        RV += "                 <asp:Button CssClass=""btn btn-sm btn-primary"" ID=""Button_Edit"" runat=""server"" CommandArgument='<%# Eval(""" + TextBox_PrimaryKey.Text + """) %>' Text=""ویرایش"" OnClick=""Button_Edit_Click"" CommandName=""Select"" />" + vbCrLf
        RV += "             </ItemTemplate>" + vbCrLf
        RV += "             <HeaderStyle Width=""1%"" Wrap=""False"" />" + vbCrLf
        RV += "         </asp:TemplateField>" + vbCrLf
        RV += "     </Columns>" + vbCrLf
        RV += "        <PagerStyle CssClass=""app-pager"" />" + vbCrLf
        RV += "        <SelectedRowStyle CssClass=""table-selected"" />" + vbCrLf
        RV += " </asp:GridView>" + vbCrLf
        RV += "</asp:Panel>" + vbCrLf


        RV += "        <asp:Panel ID=""Panel_Edit"" runat=""server"" Visible=""false"">" + vbCrLf
        RV += "            <div class=""row justify-content-center"">" + vbCrLf
        ' RV += "                <div class=""col-lg-4 col-lg-push-4 col-md-6 col-md-push-3 col-sm-6 col-sm-push-3 col-xs-8 col-xs-push-2"">" + vbCrLf
        RV += "                <div class=""col-6"">" + vbCrLf
        RV += "                    <div class=""card border-primary"">" + vbCrLf
        RV += "                        <div class=""card-header bg-info text-center text-center text-white"">" + vbCrLf
        RV += "                            <asp:Label ID=""Label_EditTitle"" runat=""server"" Text=""Label""></asp:Label>" + vbCrLf
        RV += "                        </div>" + vbCrLf
        RV += "                        <div class=""card-body"">" + vbCrLf
        RV += "                            " + TextBox_PrimaryKey.Text + " : <asp:Label ID=""Label_" + TextBox_PrimaryKey.Text + """ runat=""server"" Text=""0"" Visible=""true""></asp:Label>" + vbCrLf

        For Each col In Cols
            RV += "                            <div class=""form-group mb-3"">" + vbCrLf
            If col = TextBox_PrimaryKey.Text Then
                ' RV += "                                <asp:label class=""form-control"" ID=""TextBox_" + col + """ runat=""server""></asp:TextBox>" + vbCrLf
            Else
                If Request.Form("InputType_" + col) = "Nothing" Then
                    RV = RV ' dont addd any thing 
                Else
                    RV += "                                <label>" + Request.Form("Persianname_" + col) + " : </label>" + vbCrLf
                End If

                Select Case Request.Form("InputType_" + col)
                    Case "Nothing"
                        RV = RV
                    Case "TextBox"
                        RV += "                                <asp:TextBox CssClass=""form-control"" ID=""TextBox_" + col + """ runat=""server""></asp:TextBox>" + vbCrLf
                    Case "TextArea"
                        RV += "                                <asp:TextBox CssClass=""form-control"" ID=""TextBox_" + col + """ runat=""server"" TextMode=""MultiLine""></asp:TextBox>" + vbCrLf
                    Case Else
                        RV += "                                <asp:TextBox CssClass=""form-control"" ID=""TextBox_" + col + """ runat=""server""></asp:TextBox>" + vbCrLf
                End Select

            End If

            ' RV += "                                <asp:TextBox class=""form-control"" ID=""TextBox_" + col + """ runat=""server""></asp:TextBox>" + vbCrLf
            RV += "                            </div>" + vbCrLf
        Next

        RV += "                            <div class=""form-group"">" + vbCrLf
        RV += "                                <asp:Label ID=""Label_Message"" runat=""server"" Text="""" EnableViewState=""False"" ForeColor=""Red""></asp:Label>" + vbCrLf
        RV += "                            </div>" + vbCrLf
        RV += "                            <div class=""rtl "">" + vbCrLf
        RV += "                                <asp:Button ID=""Button_SaveNew"" runat=""server"" Text=""ثبت"" CssClass=""btn btn-sm btn-success btn-110"" />" + vbCrLf
        RV += "                                <asp:Button ID=""Button_SaveChanges"" runat=""server"" Text=""ثبت تغییرات"" CssClass=""btn btn-sm btn-success btn-110"" />" + vbCrLf
        RV += "                                <asp:Button ID=""Button_Cancel"" runat=""server"" Text=""انصراف"" CssClass=""btn btn-sm btn-warning btn-110"" />" + vbCrLf
        RV += "                                <asp:Button ID=""Button_Delete"" runat=""server"" Text=""حذف"" CssClass=""btn btn-sm btn-danger btn-110 pull-left"" OnClientClick=""return confirm('اطلاعات انتخاب شده حذف شود ؟');"" />" + vbCrLf
        RV += "                            </div>" + vbCrLf
        RV += "                        </div>" + vbCrLf
        RV += "                    </div>" + vbCrLf
        RV += "                </div>" + vbCrLf
        RV += "            </div>" + vbCrLf
        RV += "        </asp:Panel>" + vbCrLf
        RV += "    </div>" + vbCrLf
        RV += "    </div>" + vbCrLf
        RV += "</asp:Content>" + vbCrLf



        RV += ""
        RV += ""
        RV += ""
        RV += ""
        RV += ""
        RV += ""
        Return RV
    End Function

    Function Generate_Select_Command() As String
        Return "SELECT * FROM (SELECT * , (SELECT * FROM " + DropDownList_TableName.SelectedValue + " WHERE " + TextBox_PrimaryKey.Text + "=SRCTBL." + TextBox_PrimaryKey.Text + " FOR JSON PATH ) AS SearchField FROM " + DropDownList_TableName.SelectedValue + " SRCTBL ) TBL Where SearchField like '%' + @SV + '%' "
    End Function

    Function Generate_ASPXVB(Cols As List(Of String)) As String
        Dim RV As String = ""
        RV += "Imports System.Drawing" + vbCrLf
        RV += "Partial Class Admin_Manage_" + TextBox_PageName.Text + vbCrLf
        RV += "    Inherits System.Web.UI.Page" + vbCrLf

        RV += "    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load" + vbCrLf
        RV += "    " + vbCrLf
        RV += "    End Sub" + vbCrLf
        RV += "    " + vbCrLf

        RV += "    Protected Sub Button_Search_Click(sender As Object, e As EventArgs) Handles Button_Search.Click" + vbCrLf
        RV += "        GridView_List.DataBind()" + vbCrLf
        RV += "    End Sub" + vbCrLf
        RV += "    " + vbCrLf

        RV += "    Protected Sub Button_AddNew_Click(sender As Object, e As EventArgs) Handles Button_AddNew.Click" + vbCrLf
        RV += "        Switch_To_Insert()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Protected Sub Button_Edit_Click(sender As Object, e As EventArgs)" + vbCrLf
        RV += "        Dim oSender As Button = sender" + vbCrLf
        RV += "        Switch_To_Edit()" + vbCrLf
        RV += "        Load_Data(oSender.CommandArgument)" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Protected Sub Button_SaveNew_Click(sender As Object, e As EventArgs) Handles Button_SaveNew.Click" + vbCrLf
        RV += "        Save_New()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Protected Sub Button_SaveChanges_Click(sender As Object, e As EventArgs) Handles Button_SaveChanges.Click" + vbCrLf
        RV += "        Save_Changes()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Protected Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click" + vbCrLf
        RV += "        Switch_To_List()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Protected Sub Button_Delete_Click(sender As Object, e As EventArgs) Handles Button_Delete.Click" + vbCrLf
        RV += "        Delete_Data()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Sub Switch_To_List()" + vbCrLf
        RV += "        Panel_list.Visible = True" + vbCrLf
        RV += "        Panel_Edit.Visible = False" + vbCrLf
        RV += "        Button_AddNew.Visible = True" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "    Sub Switch_To_Insert()" + vbCrLf
        RV += "        Panel_list.Visible = False" + vbCrLf
        RV += "        Panel_Edit.Visible = True" + vbCrLf
        RV += "        Button_AddNew.Visible = False" + vbCrLf
        RV += "        Label_EditTitle.Text = ""افزودن""" + vbCrLf
        RV += "        Button_SaveNew.Visible = True" + vbCrLf
        RV += "        Button_SaveChanges.Visible = False" + vbCrLf
        RV += "        Button_Cancel.Visible = True" + vbCrLf
        RV += "        Button_Delete.Visible = False" + vbCrLf
        RV += "        Label_Message.Text = """"" + vbCrLf
        RV += "        '-> remove old data " + vbCrLf

        For Each col In Cols
            If col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += "        TextBox_" + col + ".Text = """"" + vbCrLf

            End If
        Next
        RV += "    End Sub" + vbCrLf

        RV += "    Sub Switch_To_Edit()" + vbCrLf
        RV += "        Panel_list.Visible = False" + vbCrLf
        RV += "        Panel_Edit.Visible = True" + vbCrLf
        RV += "        Button_AddNew.Visible = False" + vbCrLf
        RV += "        Label_EditTitle.Text = ""ویرایش""" + vbCrLf
        RV += "        Button_SaveNew.Visible = False" + vbCrLf
        RV += "        Button_SaveChanges.Visible = True" + vbCrLf
        RV += "        Button_Cancel.Visible = True" + vbCrLf
        RV += "        Button_Delete.Visible = True" + vbCrLf
        RV += "    End Sub" + vbCrLf

        '-> validate input functrion 
        RV += "    Function Validate_Inputs() As Boolean" + vbCrLf
        RV += "        Dim RV As Boolean = True" + vbCrLf
        RV += "        Label_Message.ForeColor = Color.Red" + vbCrLf
        For Each col In Cols
            If col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += "        If TextBox_" + col + ".Text = """" Then" + vbCrLf
                RV += "            'RV = False" + vbCrLf
                RV += "            Label_Message.Text = ""لطفا " + Request.Form("PersianName_" + col) + " را وارد کنید.""" + vbCrLf
                RV += "            TextBox_" + col + ".Focus()" + vbCrLf
                RV += "            Return False" + vbCrLf
                RV += "        End If" + vbCrLf
                RV += "" + vbCrLf
            End If
        Next
        RV += "        Return RV" + vbCrLf
        RV += "    End Function" + vbCrLf

        '-> save new action 
        RV += "    Sub Save_New()" + vbCrLf
        RV += "        If Validate_Inputs() Then" + vbCrLf
        RV += "            Dim SqlStr As String = ""INSERT INTO " + DropDownList_TableName.SelectedValue + " ("
        For Each col In Cols
            If Request.Form("InputType_" + col) = "Nothing" Or col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += col + ","
            End If
        Next
        RV = Left(RV, Len(RV) - 1)

        RV += ") VALUES ("
        For Each col In Cols
            If Request.Form("InputType_" + col) = "Nothing" Or col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += "@" + col + ","
            End If
        Next
        RV = Left(RV, Len(RV) - 1)
        RV += ") """ + vbCrLf

        RV += "            Dim DAL as New AZDBL " + vbCrLf
        RV += "            DAL.SqlStr = SqlStr" + vbCrLf
        RV += "            DAL.Params.Clear()" + vbCrLf

        For Each col In Cols
            If Request.Form("InputType_" + col) = "Nothing" Or col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += "            DAL.Params.Add(""@" + col + """, TextBox_" + col + ".Text) " + vbCrLf
                ' TextBox_" + col + ".Text
            End If
        Next
        'RV += "            Dim DA As New DALTableAdapters." + DropDownList_TableName.SelectedValue + "TableAdapter" + vbCrLf

        RV += "            Try " + vbCrLf
        RV += "                DAL.ExecuteNonQuery()" + vbCrLf
        RV += "                Switch_To_List()" + vbCrLf
        RV += "                GridView_List.DataBind()" + vbCrLf
        RV += "                Label_ActionMessage.Text = ""اطلاعات با موفقیت ثبت شد.""" + vbCrLf
        RV += "            Catch ex As Exception" + vbCrLf
        RV += "                Label_Message.Text = ""ثبت اطلاعات در بانک اطلاعانی با اشکال مواجه شد!"" + ex.Message" + vbCrLf
        RV += "            End Try" + vbCrLf
        RV += "            DAL.Dispose() " + vbCrLf

        ' RV += "            Try" + vbCrLf
        'RV += "                DA.Insert("
        'For Each col In Cols
        '    If Request.Form("InputType_" + col) = "Nothing" Then
        '    Else
        '        RV += col + ","
        '    End If
        'Next
        'RV = Left(RV, Len(RV) - 1)
        'RV += ")" + vbCrLf
        'RV += "            Catch ex As Exception" + vbCrLf
        'RV += "            End Try" + vbCrLf
        'RV += "            DA.Dispose()" + vbCrLf
        RV += "        End If" + vbCrLf
        RV += "    End Sub" + vbCrLf

        '-> load data 
        RV += "    Sub Load_Data(ID As Integer)" + vbCrLf
        RV += "        Dim SqlStr As String = ""SELECT * FROM " + DropDownList_TableName.SelectedValue + " WHERE " + TextBox_PrimaryKey.Text + "=@" + TextBox_PrimaryKey.Text + """ " + vbCrLf
        RV += "        Dim DAL As New AZDBL" + vbCrLf
        RV += "        DAL.SqlStr = SqlStr" + vbCrLf
        RV += "        DAL.Params.Clear()" + vbCrLf
        RV += "        DAL.Params.Add(""@" + TextBox_PrimaryKey.Text + """, ID)" + vbCrLf

        RV += "        Dim DT As New System.Data.DataTable" + vbCrLf

        RV += "        Try" + vbCrLf
        RV += "            DT = DAL.ExecuteDatatable()" + vbCrLf
        RV += "        Catch ex As Exception" + vbCrLf
        RV += "            " + vbCrLf
        RV += "        End Try" + vbCrLf
        RV += "        DAL.Dispose()" + vbCrLf
        RV += "        " + vbCrLf

        'RV += "        Dim DA As New DALTableAdapters." + DropDownList_TableName.SelectedValue + "TableAdapter" + vbCrLf
        'RV += "        Dim DT As New DAL." + DropDownList_TableName.SelectedValue + "DataTable" + vbCrLf
        'RV += "        DA.FillBy_ID(DT, ID)" + vbCrLf
        'RV += "        DA.Dispose()" + vbCrLf
        'RV += "        For Each row As DAL." + DropDownList_TableName.SelectedValue + "Row In DT.Rows" + vbCrLf
        RV += "        For Each row As System.Data.DataRow In DT.Rows" + vbCrLf
        RV += "            Label_" + TextBox_PrimaryKey.Text + ".Text = row.Item(""" + TextBox_PrimaryKey.Text + """)" + vbCrLf
        For Each col In Cols
            If col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += "            TextBox_" + col + ".Text = row.Item(""" + col + """).ToString()" + vbCrLf
            End If
        Next
        RV += "        Next" + vbCrLf
        RV += "        DT.Dispose()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        '-> save changes
        RV += "    Sub Save_Changes()" + vbCrLf
        RV += "        If Validate_Inputs() Then" + vbCrLf
        RV += "            Dim SqlStr As String = ""UPDATE " + DropDownList_TableName.SelectedValue + " SET "
        For Each col In Cols
            If Request.Form("InputType_" + col) = "Nothing" Or col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += " " + col + "=@" + col + " ,"
            End If
        Next
        RV = Left(RV, Len(RV) - 1)

        RV += "WHERE " + TextBox_PrimaryKey.Text + "=@" + TextBox_PrimaryKey.Text + " """ + vbCrLf
        RV += "            Dim DAL As New AZDBL" + vbCrLf
        RV += "            DAL.SqlStr = SqlStr" + vbCrLf
        RV += "            DAL.Params.Clear()" + vbCrLf

        RV += "            DAL.Params.Add(""@" + TextBox_PrimaryKey.Text + """, Label_" + TextBox_PrimaryKey.Text + ".Text" + ")" + vbCrLf

        For Each col In Cols
            If Request.Form("InputType_" + col) = "Nothing" Or col.ToUpper = TextBox_PrimaryKey.Text.ToUpper Then

            Else
                RV += "            DAL.Params.Add(""@" + col + """, TextBox_" + col + ".Text)" + vbCrLf
            End If
        Next

        RV += "            Try" + vbCrLf
        RV += "                DAL.ExecuteNonQuery()" + vbCrLf
        RV += "                Switch_To_List()" + vbCrLf
        RV += "                GridView_List.DataBind()" + vbCrLf
        RV += "                Label_ActionMessage.Text = ""اطلاعات با موفقیت به روز رسانی شد.""" + vbCrLf
        RV += "            Catch ex As Exception" + vbCrLf
        RV += "                Label_Message.Text = ""ثبت تغییرات در بانک اطلاعانی با اشکال مواجه شد!"" + ex.Message" + vbCrLf
        RV += "                " + vbCrLf
        RV += "            End Try" + vbCrLf
        RV += "            DAL.Dispose()" + vbCrLf
        RV += "            " + vbCrLf

        'RV += "            Dim DA As New DALTableAdapters." + DropDownList_TableName.SelectedValue + "TableAdapter" + vbCrLf
        'RV += "            Try" + vbCrLf
        'RV += "                DA.Update(TextBox_FullName.Text, TextBox_UserName.Text, TextBox_Password.Text, TextBox_Department.Text, CheckBox_IsAdmin.Checked, vbNull, Label_UserID.Text)" + vbCrLf
        'RV += "            Catch ex As Exception" + vbCrLf
        'RV += "            End Try" + vbCrLf
        'RV += "            DA.Dispose()" + vbCrLf

        RV += "        End If" + vbCrLf
        RV += "    End Sub" + vbCrLf

        '-> delete data 
        RV += "    Sub Delete_Data()" + vbCrLf
        RV += "        Dim SqlStr As String = ""DELETE FROM " + DropDownList_TableName.SelectedValue + " WHERE " + TextBox_PrimaryKey.Text + "=@" + TextBox_PrimaryKey.Text + """ " + vbCrLf
        RV += "        " + vbCrLf
        RV += "        Dim DAL As New AZDBL" + vbCrLf
        RV += "        DAL.SqlStr = SqlStr" + vbCrLf
        RV += "        DAL.Params.Clear()" + vbCrLf
        RV += "        DAL.Params.Add(""@" + TextBox_PrimaryKey.Text + """, Label_" + TextBox_PrimaryKey.Text + ".Text)" + vbCrLf
        RV += "        Try" + vbCrLf
        RV += "            DAL.ExecuteNonQuery()" + vbCrLf
        RV += "            Switch_To_List()" + vbCrLf
        RV += "            GridView_List.DataBind()" + vbCrLf
        RV += "            Label_ActionMessage.Text = ""اطلاعات با موفقیت حذف شد.""" + vbCrLf
        RV += "        Catch ex As Exception" + vbCrLf
        RV += "            Label_Message.Text = ""حذف اطلاعات از بانک اطلاعانی با اشکال مواجه شد!"" + ex.Message" + vbCrLf
        RV += "        End Try" + vbCrLf
        RV += "        DAL.Dispose()" + vbCrLf
        RV += "    End Sub" + vbCrLf

        RV += "End Class" + vbCrLf

        Return RV
    End Function


    Function Add_VB_() As String
        Dim RV As String = ""
        RV += "" + vbCrLf
        Return RV
    End Function


    '-> export generated codes to file
    Sub Export(ASPX As String, ASPXVB As String, sOutPuthPath As String)
        ' Load_Table()

        Dim ASPX_Path As String = Server.MapPath("~" + sOutPuthPath + "/Manage_" + TextBox_PageName.Text + ".aspx")
        Dim ASPXVB_Path As String = Server.MapPath("~" + sOutPuthPath + "/Manage_" + TextBox_PageName.Text + ".aspx.vb")

        HyperLink_Output.Text = sOutPuthPath + "/Manage_" + TextBox_PageName.Text + ".aspx"
        HyperLink_Output.NavigateUrl = HyperLink_Output.Text

        ''-> if file exists show an error
        If System.IO.File.Exists(ASPX_Path) Or System.IO.File.Exists(ASPXVB_Path) Then
            If CheckBox_OverwriteOutput.Checked Then
                ''-> diable output tempoary
                'Label_Message.Text = "File Exists ! , Nothing Exported !"
                'Label_Message.ForeColor = Drawing.Color.Red
                'Exit Sub
                System.IO.File.Delete(ASPX_Path)
                System.IO.File.Delete(ASPXVB_Path)
            Else
                Label_Message.Text = "File Exists ! , Nothing Exported !"
                Label_Message.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
        End If

        System.IO.File.AppendAllText(ASPX_Path, ASPX, Encoding.UTF8)
        System.IO.File.AppendAllText(ASPXVB_Path, ASPXVB, Encoding.UTF8)

        Label_Message.Text = "Files Generated Successfully!"


    End Sub

    Protected Sub Button_Export_Click(sender As Object, e As EventArgs) Handles Button_Export.Click
        Generate_SourceCode()
        Export(TextBox_ASPX.Text, TextBox_ASPXVB.Text, TextBox_OutputPath.Text)
    End Sub
    Protected Sub Button_LoadTable_Click(sender As Object, e As EventArgs) Handles Button_LoadTable.Click
        Load_Table()
    End Sub
    Protected Sub Button_CheckData_Click(sender As Object, e As EventArgs) Handles Button_CheckData.Click
        Load_Table()

        '-> save each required data into a file named manager-page-name.txt
        '-> on page creation , we have a button which can load this saved data from file and place it in generator gage

        '-> create a string to store the data 
        For Each item As String In Request.Form
            If Left(item, 9) = "InputType" Or Left(item, 11) = "PersianName" Then
                ' Response.Write(item + "::" + Request.Form(item).ToString + "<br>")
            End If
        Next
    End Sub

    Sub Load_Tables()
        Dim SqlStr As String = "Select TABLE_SCHEMA + '.' + TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_SCHEMA + '.' + TABLE_NAME"
        Dim oCnn As New System.Data.SqlClient.SqlConnection(CnnStr)
        oCnn.Open()
        Dim oCmd As New System.Data.SqlClient.SqlCommand(SqlStr, oCnn)
        Dim oReader As System.Data.SqlClient.SqlDataReader = oCmd.ExecuteReader
        While oReader.Read
            DropDownList_TableName.Items.Add(New ListItem(oReader.GetString(0), oReader.GetString(0)))
        End While


        oCmd.Dispose()
        oCnn.Close()
        oCnn.Dispose()

    End Sub


    Sub Load_Table()
        '-> get database columns 
        Dim Cols As List(Of String) = Get_Cols()

        '-> generate a table to show database data 
        Dim TBL_TableDetail As New Table
        TBL_TableDetail.CssClass = "table table-striped table-sm"

        '-> create a header row
        Dim Table_Header_Row As New TableHeaderRow

        '-> create a cell to store column name 
        Dim ColName_HeaderCell As New TableHeaderCell
        ColName_HeaderCell.Text = "Column Name"
        ColName_HeaderCell.Wrap = False
        Table_Header_Row.Cells.Add(ColName_HeaderCell)

        Dim ColPersianName_HeaderCell As New TableHeaderCell
        ColPersianName_HeaderCell.Text = "Persian Name"
        Table_Header_Row.Cells.Add(ColPersianName_HeaderCell)

        Dim InputType_HeaderCell As New TableHeaderCell
        InputType_HeaderCell.Text = "Input Type"
        Table_Header_Row.Cells.Add(InputType_HeaderCell)

        Dim RefData_HeaderCell As New TableHeaderCell
        RefData_HeaderCell.Text = "Refrence Rules"
        Table_Header_Row.Cells.Add(RefData_HeaderCell)

        '-> add header row to table 
        TBL_TableDetail.Rows.Add(Table_Header_Row)

        '-> loop in table columns 
        For Each col As String In Cols
            '-> create a row to 
            Dim TR As New TableRow

            '-> create a cell to stroe column name 
            Dim ColName_Cell As New TableCell
            ColName_Cell.Text = col
            TR.Cells.Add(ColName_Cell)

            Dim ColPersianName_Cell As New TableCell
            Dim ColPersianName_TextBox As New TextBox
            ColPersianName_TextBox.ID = "PersianName_" + col
            ColPersianName_TextBox.CssClass = "form-control"

            If Len(Request.Form("PersianName_" + col)) > 0 Then
                ColPersianName_TextBox.Text = Request.Form("PersianName_" + col)
            Else
                ColPersianName_TextBox.Text = col
            End If

            ColPersianName_Cell.Controls.Add(ColPersianName_TextBox)
            ColPersianName_Cell.CssClass = "form-group-sm"
            TR.Cells.Add(ColPersianName_Cell)

            Dim InputType_Cell As New TableCell
            InputType_Cell.CssClass = "form-group-sm"
            Dim InputType_DDL As New DropDownList
            InputType_DDL.ID = "InputType_" + col
            InputType_DDL.CssClass = "form-control"
            InputType_DDL.Items.Add(New ListItem("TextBox", "TextBox"))
            InputType_DDL.Items.Add(New ListItem("Numeric TextBox", "NumericTextBox"))
            InputType_DDL.Items.Add(New ListItem("TextArea", "TextArea"))
            InputType_DDL.Items.Add(New ListItem("NicEdit", "NicEdit"))
            InputType_DDL.Items.Add(New ListItem("Boolean CheckBox", "BooleanCheckBox"))
            InputType_DDL.Items.Add(New ListItem("Boolean Radio", "BooleanRadio"))
            InputType_DDL.Items.Add(New ListItem("Boolean DropDown", "BooleanDropDown"))
            InputType_DDL.Items.Add(New ListItem("Refrence DropDown", "RefrenceDropDown"))
            InputType_DDL.Items.Add(New ListItem("Nothing", "Nothing"))


            'InputType_DDL.SelectedValue = "TextBox"

            InputType_DDL.SelectedValue = Request.Form("InputType_" + col)

            InputType_Cell.Controls.Add(InputType_DDL)
            TR.Cells.Add(InputType_Cell)

            '-> add refrence rules 
            Dim ColRefRule As New TableCell
            ColRefRule.CssClass = "form-group-sm"
            Dim TextBox_RefRule As New TextBox
            TextBox_RefRule.ID = "RefRule_" + col
            TextBox_RefRule.CssClass = "form-control"

            If Request.Form("InputType_" + col) = "RefrenceDropDown" Then
                '-> zzzzzz
            Else
                TextBox_RefRule.Enabled = False
            End If
            TextBox_RefRule.Text = Request.Form("RefRule_" + col)


            ColRefRule.Controls.Add(TextBox_RefRule)
            TR.Cells.Add(ColRefRule)


            '-> add row to table 
            TBL_TableDetail.Rows.Add(TR)

            'Dim CheckBox_Selected_Column As New CheckBox
            'CheckBox_Selected_Column.Text = col
            'CheckBox_Selected_Column.ID = "sel"

        Next

        panel_Table_Data.Controls.Add(TBL_TableDetail)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Load_Tables()
        End If
    End Sub




    'Protected Overrides Sub SavePageStateToPersistenceMedium(ViewState As Object)
    '    Response.Write("savig state")
    '    Dim lf As LosFormatter = New LosFormatter()
    '    Dim sw As StringWriter = New StringWriter()
    '    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
    '    ' Dim fileName As String = this.GenerateFileName()
    '    Dim fileName As String = "oops.txt"
    '    lf.Serialize(sw, ViewState)
    '    sb = sw.GetStringBuilder()
    '    'System.IO.File.AppendAllText(sb.ToString(), Server.MapPath("/app_data/" + fileName))
    '    System.IO.File.AppendAllText(sb.ToString(), "c:\state.txt")

    '    'sb = null
    '    'lf = null
    '    'sw = null

    'End Sub


End Class

