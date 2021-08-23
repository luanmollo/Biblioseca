<%@ Page Title="Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Biblioseca.Web.Books.Index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Books</h2>
    <asp:GridView ID="GridViewBooks" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                  OnRowDeleting="GridViewBooks_RowDeleting" OnRowEditing="GridViewBooks_RowEditing" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="labelTitle" runat="server" Text='<%# Eval("Title") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textTitle" runat="server" Text='<%# Eval("Title") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Author">
                <ItemTemplate>
                    <asp:Label ID="labelAuthorName" runat="server" Text='<%# Eval("Author.FullName") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textAuthorName" runat="server" Text='<%# Eval("Author.FullName") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Stock">
                <ItemTemplate>
                    <asp:Label ID="labelStock" runat="server" Text='<%# Eval("Stock") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textStock" runat="server" Text='<%# Eval("Stock") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"/>
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="linkCreateBook" Text="Crear" OnClick="LinkCreateBook_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>