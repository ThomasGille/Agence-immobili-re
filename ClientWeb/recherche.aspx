<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="recherche.aspx.cs" Inherits="ClientWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container thumbs">
            
			<h2 style="text-align: center;">Recherche avanc&eacute;e</h2>
			

			<b>Type de transaction : </b>

            <asp:DropDownList id="type_transaction" name="type_transaction"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Vente </asp:ListItem>
                <asp:ListItem Value="1"> Location </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Type de biens : </b>
        <asp:DropDownList id="type_bien" name="type_bien"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Appartement </asp:ListItem>
                <asp:ListItem Value="1"> Garage </asp:ListItem>
                <asp:ListItem Value="2"> Maison </asp:ListItem>
                <asp:ListItem Value="3"> Terrain </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Type de chauffage : </b>
        <asp:DropDownList id="type_chauffage" name="type_chauffage"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Aucun </asp:ListItem>
                <asp:ListItem Value="1"> Individuel </asp:ListItem>
                <asp:ListItem Value="2"> Collectif </asp:ListItem>
            </asp:DropDownList>
      <br />

      <b>Energie de chauffage : </b>
        <asp:DropDownList id="energie_chauffage" name="energie_chauffage"
                    AutoPostBack="False"
                    runat="server">
                <asp:ListItem Value="0"> Aucun </asp:ListItem>
                <asp:ListItem Value="1"> Fioul </asp:ListItem>
                <asp:ListItem Value="2"> Gaz </asp:ListItem>
                <asp:ListItem Value="3"> Electrique </asp:ListItem>                
                <asp:ListItem Value="3"> Bois </asp:ListItem>

            </asp:DropDownList>
      <br />

      <b>Prix demand&eacute; (&euro;): </b>
      <input type="hidden" id="prix_demande" name="prix_demande" class="range-slider-price" value="23" />
      <br />

      <b>Surface (m²) : </b>
      <input type="hidden" id="surface_demande" name="surface_demande" class="range-slider-surface" value="23" />
      <br />

      <b>Nombre de pi&egrave;ces : </b>
      <input type="hidden" id="nombre_piece_demande" name="nombre_piece_demande" class="range-slider-piece-number" value="23" />
      <br />


        <asp:Button ID="button_valider" runat="server" OnClick="button_valider_Click" Text="Valider"/>
        </div>

    <script>
          $('.range-slider-price').jRange({
          from: 0,
          to: 100,
          step: 1,
          scale: [0,25,50,75,100],
          format: '%s',
          width: 300,
          showLabels: true,
          isRange : true
          });

      </script>

      <script>
          $('.range-slider-surface').jRange({
          from: 0,
          to: 200,
          step: 1,
          scale: [0,200],
          format: '%s',
          width: 300,
          showLabels: true,
          isRange : true
          });
      </script>

      <script>
          $('.range-slider-piece-number').jRange({
          from: 0,
          to: 200,
          step: 1,
          scale: [0,200],
          format: '%s',
          width: 300,
          showLabels: true,
          isRange : true
          });
      </script>

    <script>
              $('.slider-container').css("margin-top", "25px");
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    
     

</asp:Content>
