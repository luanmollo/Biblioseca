<%@ Page Title="Delete Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SureToDelete.aspx.cs" Inherits="Biblioseca.Web.Categories.SureToDelete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Categories</h2>
<h4 runat="server" id="title"></h4>

<asp:LinkButton runat="server" ID="linkDeleteCategory" Text="Si, eliminar" OnClick="LinkDeleteCategory_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
    <asp:LinkButton runat="server" ID="linkDoNotDeleteCategory" Text="No, volver a categorías" OnClick="LinkDoNotDeleteCategory_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>