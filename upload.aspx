<%@ Page Title="" Language="VB" MasterPageFile="~/app.master" AutoEventWireup="false" CodeFile="upload.aspx.vb" Inherits="upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row justify-content-center rtl">
        <div class="col-md-5">
            <div class="card">
                <div class="card-header text-center bg-primary text-white">
                    ارسال فایل به سرور
                </div>
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label for="fileupload" class="btn btn-primary">انتخاب فایل</label>
                            <label id="filename"></label>
                            <asp:FileUpload runat="server" ID="fileupload" CssClass=" " ClientIDMode="Static" style="visibility:hidden;"/>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 text-center">
                            <asp:Button runat="server" ID="btn_upload"  Text="ارسال فایل" CssClass="btn btn-success"/>
                        </div>
                    </div>
                    <div class="row mb-3" id="alert" runat="server" visible="false" enableviewstate="false">
                        <div class="col-md-12">
                            <div class="alert alert-danger">فایل ارسال شده تکراری می باشد</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row mt-5 justify-content-center">
        <div class="col-md-5">
                <% If files.Length > 0 Then  %>
            <div class="rtl text-right">

                    تعداد فایل ها : <%= files.Length %>
            </div>
                <% End If  %>
            <ul class="list-group">
                <% If files.Length > 0 Then  %>
                    
                    <% For Each file In files %>
                        <li class="list-group-item"> <a href="/download?filename=<%= New System.IO.FileInfo(file).Name %>" target="_blank"><%= New System.IO.FileInfo(file).Name %></a></li>
                    <% Next %>

                <% Else %>
                    <li class="list-group-item list-group-item-danger text-right">هیچ فایلی در سامانه ثبت نشده است</li>
                <% end If %>
        
            </ul>
        </div>
    </div>

    <script>
        $("#fileupload").change(function () {
            if (this.files[0]) {
                filename = this.files[0].name;
                console.log(filename);
                $("#filename").text(filename)
            } else {
                $("#filename").text("")
            }

        });
    </script>

</asp:Content>

