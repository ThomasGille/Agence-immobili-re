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
        <asp:Label ID="Adresse" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="CodePostal" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="DateMiseEnTransaction" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="DateTransaction" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Description" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="EnergieChauffage" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="MontantCharges" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="NbEtages" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="NbPieces" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="NumEtage" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Prix" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Surface" runat="server" Text="<br />"></asp:Label>
        <asp:Label ID="Titre" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="TransactionEffectuee" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="TypeBien" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="TypeChauffage" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="TypeTransaction" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Ville" runat="server" Text="Label"></asp:Label>


    
        <!-- Slide gallery -->
        <div class="jumbotron">
            <div class="container">
                <div class="col-xs-12">
                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        </ol>
                        <!-- Wrapper for slides -->
                        <div class="carousel-inner">
                        <asp:Repeater ID="rpResultats" runat="server">
                            <ItemTemplate>
                                
                                <div class='<%# (Container.ItemIndex==0)? "item active" : "item" %>'>
                                <img src='<%#"data:image/png;base64,"+  Container.DataItem.ToString() %> ' alt="" class="img-responsive">
                            
                                <div class="carousel-caption">
                                </div>
                            </div>
                            
                            </ItemTemplate>
                        </asp:Repeater>
                        </div>

                        <!--
                            -->

                        <!-- Controls -->
                        <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </a>
                        <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </a>
                    </div>
                </div>
            </div>
            <!-- End Slide gallery -->
        </div>
    </div>
</asp:Content>
