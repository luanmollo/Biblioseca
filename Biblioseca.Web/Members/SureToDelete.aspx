<%@ Page Title="Delete Member" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SureToDelete.aspx.cs" Inherits="Biblioseca.Web.Members.SureToDelete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Members</h2>
<h4>¿Seguro de que querés eliminar el socio?</h4>

<asp:LinkButton runat="server" ID="linkDeleteMember" Text="Si, eliminar" OnClick="LinkDeleteMember_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
    <asp:LinkButton runat="server" ID="linkDoNotDeleteMember" Text="No, volver a socios" OnClick="LinkDoNotDeleteMember_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>