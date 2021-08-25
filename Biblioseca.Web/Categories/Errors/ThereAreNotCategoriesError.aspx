<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThereAreNotCategoriesError.aspx.cs" Inherits="Biblioseca.Web.Categories.Errors.ThereAreNotCategoriesError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Categories</h2>
    <h4>Ups! Parece que no hay categorías para listar</h4>
    <asp:LinkButton runat="server" ID="linkCreateNewCategory" Text="Crear" OnClick="LinkCreateNewCategory_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>
