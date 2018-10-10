<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="CVGS.Reports" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
            <asp:DropDownList ID="DdlReports" runat="server">
                <asp:ListItem Enabled="true" Text="Select Report" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Game List" Value="GameList" ></asp:ListItem>
                <asp:ListItem Text="Game Detail" Value="GameDetail"></asp:ListItem>
                <asp:ListItem Text="Member List" Value="3"></asp:ListItem>
                <asp:ListItem Text="Member Detail" Value="4"></asp:ListItem>
                <asp:ListItem Text="Wish List" Value="5"></asp:ListItem>
                <asp:ListItem Text="Sales Report" Value="6"></asp:ListItem>
                <asp:ListItem Text="Other Reports" Value="7"></asp:ListItem>
            </asp:DropDownList>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="DownloadReport_Click" Text="Download Report" CssClass="btn btn-default" />
            </div>
        </div>
     </div>
</asp:Content>

