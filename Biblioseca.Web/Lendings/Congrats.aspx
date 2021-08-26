<%@ Page Title="Congrats!" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Congrats.aspx.cs" Inherits="Biblioseca.Web.Lendings.Congrats" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Lendings</h2>
<h4>Felicitaciones! El libro es tuyo</h4>

    <asp:LinkButton runat="server" ID="linkBackToLendings" Text="Volver a préstamos" OnClick="LinkBackToLendings_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>