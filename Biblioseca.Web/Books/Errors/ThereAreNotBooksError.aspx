<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThereAreNotBooksError.aspx.cs" Inherits="Biblioseca.Web.Books.Errors.ThereAreNotBooksError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Books</h2>
    <h4>Ups! Parece que no hay libros para listar</h4>
    <asp:LinkButton runat="server" ID="linkCreateNewBook" Text="Crear" OnClick="LinkCreateNewBook_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>