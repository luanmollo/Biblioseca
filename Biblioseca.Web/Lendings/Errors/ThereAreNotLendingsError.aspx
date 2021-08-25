<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThereAreNotLendingsError.aspx.cs" Inherits="Biblioseca.Web.Lendings.Errors.ThereAreNotLendingsError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Lendings</h2>
    <h4>Ups! Parece que no hay préstamos para listar</h4>
    <asp:LinkButton runat="server" ID="linkCreateNewLending" Text="Crear" OnClick="LinkCreateNewLending_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>