<%@ Page Title="Delete Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SureToDelete.aspx.cs" Inherits="Biblioseca.Web.Books.SureToDelete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Books</h2>
<h4>¿Seguro de que querés eliminar el libro?</h4>

<asp:LinkButton runat="server" ID="linkDeleteBook" Text="Si, eliminar" OnClick="LinkDeleteBook_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
    <asp:LinkButton runat="server" ID="linkDoNotDeleteBook" Text="No, volver a autores" OnClick="LinkDoNotDeleteBook_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>