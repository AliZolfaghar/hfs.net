<%@ Page Title="" Language="VB" MasterPageFile="~/app.master" AutoEventWireup="false" CodeFile="download.aspx.vb" Inherits="Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row justify-content-center">
           <div class="col-md-5">
               <div class="alert alert-danger rtl">
                   فایل مورد نظر وجود ندارد!
               </div>
                <a href="/upload">بازگشت</a>
               </div>
    </div>
</asp:Content>

