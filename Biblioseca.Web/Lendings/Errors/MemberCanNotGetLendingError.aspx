<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberCanNotGetLendingError.aspx.cs" Inherits="Biblioseca.Web.Lendings.Errors.MemberCanNotGetLendingError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Lendings</h2>
    <h4>Ups! Parece que el socio no puede pedir prestado más libros</h4>
    <asp:LinkButton runat="server" ID="linkBackToLendings" Text="Volver a préstamos" OnClick="LinkBackToLendings_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>