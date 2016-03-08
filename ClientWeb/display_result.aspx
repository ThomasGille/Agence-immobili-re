<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="display_result.aspx.cs" Inherits="ClientWeb.display_result" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="mLabel" runat="server" Text=""></asp:Label>

    <%--  <asp:GridView ID="gvResultats" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" runat="server">
        <Columns>
            <asp:BoundField DataField="Titre" ReadOnly="True" 
                            SortExpression="Titre" HeaderText="Titre" />
            <asp:BoundField DataField="Prix" ReadOnly="True" 
                            SortExpression="Prix" HeaderText="Prix" />
            <asp:TemplateField>
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="btnModifier" runat="server"
                                     CommandArgument='<%#Eval("Id")%>'
                                     CommandName="Modifier" 
                                     ImageUrl="~/Images/edit_20.png" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>
    <div class="container thumbs">
        <%-- On affiche les élements dans des miniatures --%>
        <asp:Repeater ID="rpResultats" runat="server">
            <ItemTemplate>
                <%--  --%>

                <div class="col-sm-6 col-md-4">
                    <div class="thumbnail">
                        <div id="photo">
                        <%# (Eval("PhotoPrincipaleBase64" ).ToString())!="" 
                                ? "<img src=\"data:image/png;base64," +Eval("PhotoPrincipaleBase64" )+"\" alt=\"\" class=\"img-responsive\">" 
                                : "<img src=\"/img/photo_not_available.jpg\" alt=\"\" class=\"img-responsive\">" %>
                        </div>
                        <div class="caption">
                            <h3 class=""><%# cutString(Eval("Titre").ToString(),22) %></h3>
                            <%-- On coupe le titre --%>
                            <p><b>Prix : </b><%#((Double) Eval("Prix")).ToString("C") %></p>
                            <%-- Affichage en mode € --%>
                            <p><b>Code postal : </b><%#( Eval("CodePostal")) %></p>
                            <%--  --%>
                            <p><b>Ville : </b><%#( Eval("Ville")) %></p>
                            <%--  --%>
                            <div class="btn-toolbar text-center">
                                <a href="./DetailsBien.aspx?id=<%# Eval("Id") %>"
                                    role="button"
                                    class="btn btn-primary pull-right">Voir les d&eacute;tails</a>
                            </div>
                        </div>
                    </div>
                </div>


            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
