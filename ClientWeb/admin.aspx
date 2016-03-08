﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="ClientWeb.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="mLabel" runat="server" Text=""></asp:Label>


      <asp:GridView ID="gvResultats" runat="server" 
          AutoGenerateColumns="false" 
          AutoGenerateEditButton="true" 
          AutoGenerateDeleteButton="true" 
          OnRowEditing="TaskGridView_RowEditing" 
          OnRowCancelingEdit="TaskGridView_RowCancelingEdit" 
          OnRowUpdating="TaskGridView_RowUpdating" 
          OnRowDeleting="TaskGridView_RowDeleting" > 
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Titre" HeaderText="Titre" />
            <asp:BoundField DataField="Prix" HeaderText="Prix" />
            <asp:BoundField DataField="MontantCharges" HeaderText="MontantCharges" />
            <asp:BoundField DataField="Ville" HeaderText="Ville" />
            <asp:BoundField DataField="CodePostal" HeaderText="CodePostal" />
            <asp:BoundField DataField="TypeBien" HeaderText="TypeBien" />
            <asp:BoundField DataField="TypeTransaction" HeaderText="TypeTransaction" />
            <asp:BoundField DataField="TransactionEffectuee" HeaderText="TransactionEffectuee" />
            <asp:BoundField DataField="DateMiseEnTransaction" HeaderText="DateMiseEnTransaction" />
            <asp:BoundField DataField="DateTransaction" HeaderText="DateTransaction" />

            
            
            

            
        </Columns>
    </asp:GridView>

    
</asp:Content>
