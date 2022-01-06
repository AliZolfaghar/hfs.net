<%@ Page Title="" Language="VB" MasterPageFile="~/app.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row justify-content-center rtl">
        <div class="col-md-5">
            <div class="card">
                <div class="card-header text-center bg-primary text-white ">
                     ورود به سامانه
                </div>
                <div class="card-body">

                    <div class="row mb-3">
                        <div class="col-md-12">
                            <asp:TextBox runat="server" ID="username" CssClass="form-control" placeholder="نام کاربری"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-12">
                            <asp:TextBox runat="server" ID="password" TextMode="Password" CssClass="form-control" placeholder="کلمه عبور"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-12">
                            <asp:TextBox runat="server" ID="captcha"  CssClass="form-control" placeholder="حروف زیر را وارد کنید" ClientIDMode="Static"></asp:TextBox>
                            <div class="mt-3 text-center">
                                <BotDetect:WebFormsCaptcha runat="server" id="captchabox" UserInputID="captcha" CodeLength="4"></BotDetect:WebFormsCaptcha>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="message_alert" class="alert alert-danger" visible="false" enableviewstate="false"> 
                                <asp:Literal runat="server" ID="message"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 text-center">
                            <asp:Button runat="server" ID="btn_login" Text="ورود به سامانه" CssClass="btn btn-success" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

