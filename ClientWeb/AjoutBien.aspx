<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AjoutBien.aspx.cs" Inherits="ClientWeb.AjoutBien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container thumbs">


            
			<h2 style="text-align: center;">Ajouter un bien</h2>
			
          <b>Titre : </b><asp:TextBox runat="server" ID="BoxTitre" /><br />
          
          <b>Description : </b>&nbsp;<asp:TextBox ID="BoxDescription" runat="server" Height="58px" Width="207px"></asp:TextBox> <br />

         <b>Type de transaction : </b>
            <asp:DropDownList id="type_transaction"    AutoPostBack="False"  runat="server">
                <asp:ListItem Value="0" Text="Vente">  </asp:ListItem>
                <asp:ListItem Value="1" Text="Location">  </asp:ListItem>
            </asp:DropDownList>
            <br />

        <b>Type de biens : </b>
        <asp:DropDownList id="type_bien"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0" Text="Appartement">  </asp:ListItem>
                <asp:ListItem Value="1" Text="Garage">  </asp:ListItem>
                <asp:ListItem Value="2" Text="Maison">  </asp:ListItem>
                <asp:ListItem Value="3" Text="Terrain">  </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Type de chauffage : </b>
        <asp:DropDownList id="type_chauffage"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0" Text="Aucun">  </asp:ListItem>
                <asp:ListItem Value="1" Text="Individuel">  </asp:ListItem>
                <asp:ListItem Value="2" Text="Collectif">  </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Energie de chauffage : </b>
        <asp:DropDownList id="type_energie_chauffage"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0" Text="Aucun">  </asp:ListItem>
                <asp:ListItem Value="1" Text="Fioul">  </asp:ListItem>
                <asp:ListItem Value="2" Text="Gaz">  </asp:ListItem>
                <asp:ListItem Value="3" Text="Electrique">  </asp:ListItem>                
                <asp:ListItem Value="4" Text="Bois">  </asp:ListItem>

            </asp:DropDownList>
      <br />

        <b>Montant des charges (&euro;): </b>
      &nbsp;<asp:TextBox ID="BoxMontantCharges" runat="server"></asp:TextBox>
      <br />

      <b>Prix demand&eacute; (&euro;): </b>&nbsp;
      <asp:TextBox ID="BoxPrixDemande" runat="server"></asp:TextBox>
      <br />

      <b>Surface (m²) : </b>
      &nbsp;<asp:TextBox ID="BoxSurface" runat="server"></asp:TextBox>
            <br />

      <b>Nombre de pi&egrave;ces : </b>
      &nbsp;<asp:TextBox ID="BoxNbPiece" runat="server"></asp:TextBox>
            <br />

      <b>Num&eacute;ro d'&eacute;tage : </b>
      &nbsp;<asp:TextBox ID="BoxNumEtage" runat="server"></asp:TextBox>
            <br />

      <b>Nombre d'&eacute;tage : </b>
      &nbsp;<asp:TextBox ID="BoxNbEtage" runat="server"></asp:TextBox>
            <br />

        <b>Adresse : </b>
      &nbsp;<asp:TextBox ID="BoxAdresse" runat="server"></asp:TextBox>
            <br />

        <b>Code postal : </b>
      &nbsp;<asp:TextBox ID="BoxCodePostal" runat="server"></asp:TextBox>
            <br />

        <b>Ville : </b>
      &nbsp;<asp:TextBox ID="BoxVille" runat="server"></asp:TextBox>
            <br />

        
        <asp:fileupload id="FileuploadGroup" runat="Server"  Text="Photo Principale" AllowMultiple="true" />
        
            
        <asp:Button ID="Button1" runat="server" Text="Valider" OnClick="Button1_Click"/>
        <asp:Label ID="mLabel" runat="server" Text="Je suis le futur texte de validation"></asp:Label>

            </div>
    </asp:Content>
