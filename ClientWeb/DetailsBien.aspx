<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="DetailsBien.aspx.cs" Inherits="ClientWeb.DetailsBien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div id="photo">
        <%# (Eval("PhotoPrincipaleBase64" ).ToString())!="" 
                                ? "<img src=\"data:image/png;base64," +Eval("PhotoPrincipaleBase64" )+"\" alt=\"\" class=\"img-responsive\">" 
                                : "<img src=\"/img/photo_not_available.jpg\" alt=\"\" class=\"img-responsive\">" %>
    </div>
    <div class="caption">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
        
    </div>
</asp:Content>
