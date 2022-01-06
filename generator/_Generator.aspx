<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_Generator.aspx.vb" Inherits="Admin_Generator_Generator" ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/css/bootstrap.css" rel="stylesheet" />

    <title>ver 1.2.4</title>
</head>
<body>
    <form id="form1" runat="server">
        <br />

        <div class="container-fluid">
            <div class="row">
                <div class="col-6">
                    <div class="card border-success ">
                        <div class="card-header bg-success">
                            Generator
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <div class="row">

                                    <div class="col-6">
                                        <asp:TextBox CssClass="form-control" ID="TextBox_PageName" runat="server" placeholder="English : File Name"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <asp:TextBox CssClass="form-control" ID="TextBox_PageTitle" runat="server" placeholder="Persian Name"></asp:TextBox>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <asp:TextBox runat="server" ID="TextBox_OutputPath" CssClass="form-control" placeholder="Output Path" Text="/GeneratedPages"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-group form-group-sm">
                                <label>Table Name : </label>
                                <asp:DropDownList ID="DropDownList_TableName" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="Button_LoadTable" runat="server" Text="Load Table" CssClass="btn btn-sm btn-success" />
                            </div>
                            <div class="form-group">
                                <asp:Panel ID="panel_Table_Data" runat="server" EnableViewState="true"></asp:Panel>
                                <asp:Button ID="Button_CheckData" runat="server" Text="Check Data" CssClass="btn btn-warning btn-sm" />
                            </div>

                            <div class="form-group form-group-sm">
                                <label>Primary Key : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_PrimaryKey" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button_Generate" runat="server" Text="Generate" CssClass="btn btn-primary btn-sm" />
                        </div>
                    </div>
                </div>

                <div class="col-6">
                    <div class="card border-success">
                        <div class="card-header bg-success">
                            Result
                        </div>
                        <div class="card-body">

                            <div class="row mb-3">
                                <div class="col-md-12">
                                    ASPX:
                                    <asp:TextBox ID="TextBox_ASPX" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-12">
                                    ASPX.VB:
                                    <asp:TextBox ID="TextBox_ASPXVB" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <asp:Button ID="Button_Export" runat="server" Text="Export" CssClass="btn btn-sm btn-primary" />

                                </div>
                            </div>

                            <div class="form-check">
                                <asp:CheckBox runat="server" ID="CheckBox_OverwriteOutput" Checked="false" Text=" &nbsp;Overwrite Current Files !" CssClass="" />
                            </div>
                            <asp:Label ID="Label_Message" runat="server" Text="" ForeColor="Green" EnableViewState="false"></asp:Label>
                            <br />
                            <asp:HyperLink runat="server" ID="HyperLink_Output" Target="_blank"></asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <%--<asp:Button runat="server" ID="btn_test_vs" Text ="test view state" />--%>

    </form>

</body>
</html>
