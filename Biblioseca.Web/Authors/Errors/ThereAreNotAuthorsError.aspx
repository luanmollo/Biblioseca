<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThereAreNotAuthorsError.aspx.cs" Inherits="Biblioseca.Web.Authors.Errors.ThereAreNotAuthorsError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Authors</h2>
    <h4>Ups! Parece que no hay autores para listar</h4>
    <asp:LinkButton runat="server" ID="linkCreateNewAuthor" Text="Crear" OnClick="LinkCreateNewAuthor_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>