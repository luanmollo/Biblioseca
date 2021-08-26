<%@ Page Title="Delete Lending" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SureToDelete.aspx.cs" Inherits="Biblioseca.Web.Lendings.SureToDelete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Lendings</h2>
<h4 runat="server" id="title"></h4>

<asp:LinkButton runat="server" ID="linkDeleteLending" Text="Si, eliminar" OnClick="LinkDeleteLending_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
    <asp:LinkButton runat="server" ID="linkDoNotDeleteLending" Text="No, volver a préstamos" OnClick="LinkDoNotDeleteLending_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>