<%@ Page Title="Delete Author" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SureToDelete.aspx.cs" Inherits="Biblioseca.Web.Authors.SureToDelete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Authors</h2>
<h4>¿Seguro de que querés eliminar el autor?</h4>

<asp:LinkButton runat="server" ID="linkDeleteAuthor" Text="Si, eliminar" OnClick="LinkDeleteAuthor_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
    <asp:LinkButton runat="server" ID="linkDoNotDeleteAuthor" Text="No, volver a autores" OnClick="LinkDoNotDeleteAuthor_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>