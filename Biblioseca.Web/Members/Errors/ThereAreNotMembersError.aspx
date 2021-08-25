<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThereAreNotMembersError.aspx.cs" Inherits="Biblioseca.Web.Members.Errors.ThereAreNotMembersError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Members</h2>
    <h4>Ups! Parece que no hay socios para listar</h4>
    <asp:LinkButton runat="server" ID="linkCreateNewMember" Text="Crear" OnClick="LinkCreateNewMember_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>