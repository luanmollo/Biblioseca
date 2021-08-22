<%@ Page Title="Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Biblioseca.Web.Categories.Index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Categories</h2>
    <asp:GridView ID="GridViewCategories" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                  OnRowDeleting="GridViewCategories_RowDeleting" OnRowEditing="GridViewCategories_RowEditing" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="labelName" runat="server" Text='<%# Eval("Name") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textName" runat="server" Text='<%# Eval("Name") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"/>
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="linkCreateNewCategory" Text="Crear" OnClick="LinkCreateNewCategory_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>