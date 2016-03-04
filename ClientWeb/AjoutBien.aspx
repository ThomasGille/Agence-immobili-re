<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AjoutBien.aspx.cs" Inherits="ClientWeb.AjoutBien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container thumbs">


            
			<h2 style="text-align: center;">Ajouter un bien</h2>
			
          <b>Titre : </b>
          <input class="range-slider-piece-number" />
          <br />

        <b>Description : </b>
          <textarea rows="3" cols="20"></textarea>

          <br />

			<b>Type de transaction : </b>

            <asp:DropDownList id="type_transaction"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Vente </asp:ListItem>
                <asp:ListItem Value="1"> Location </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Type de biens : </b>
        <asp:DropDownList id="type_bien"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Appartement </asp:ListItem>
                <asp:ListItem Value="1"> Garage </asp:ListItem>
                <asp:ListItem Value="2"> Maison </asp:ListItem>
                <asp:ListItem Value="3"> Terrain </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Type de chauffage : </b>
        <asp:DropDownList id="type_chauffage"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Aucun </asp:ListItem>
                <asp:ListItem Value="1"> Individuel </asp:ListItem>
                <asp:ListItem Value="2"> Collectif </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Energie de chauffage : </b>
        <asp:DropDownList id="type_energie_chauffage"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Aucun </asp:ListItem>
                <asp:ListItem Value="1"> Fioul </asp:ListItem>
                <asp:ListItem Value="2"> Gaz </asp:ListItem>
                <asp:ListItem Value="3"> Electrique </asp:ListItem>                
                <asp:ListItem Value="3"> Bois </asp:ListItem>

            </asp:DropDownList>
      <br />

        <b>Montant des charges (&euro;): </b>
      <input class="range-slider-surface" />
      <br />

      <b>Prix demand&eacute; (&euro;): </b>
      <input class="range-slider-price" />
      <br />

      <b>Surface (m²) : </b>
      <input type="number" min="0" class="range-slider-surface" />
      <br />

      <b>Nombre de pi&egrave;ces : </b>
      <input type="number" min="0" class="range-slider-piece-number" />
      <br />

      <b>Num&eacute;ro d'&eacute;tage : </b>
      <input type="number" min="0" class="range-slider-piece-number" />
      <br />

      <b>Nombre d'&eacute;tage : </b>
      <input type="number" min="0" class="range-slider-piece-number" />
      <br />

        <b>Adresse : </b>
      <input class="range-slider-piece-number" />
      <br />

        <b>Code postal : </b>
      <input name="code_postal" id="code_postal" class="range-slider-piece-number" />
      <br />

        <b>Ville : </b>
      <input class="range-slider-piece-number" />
      <br />

        <input type="file" name="photo_send_1[]" accept="image/*" multiple/>

      <input type="submit" value="Valider"/>
        </div>

    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
