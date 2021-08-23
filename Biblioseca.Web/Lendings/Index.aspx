<%@ Page Title="Lendings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Biblioseca.Web.Lendings.Index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Lendings</h2>

    <asp:DropDownList id="selectedView" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" CssClass="form-control">
        <asp:ListItem Value="all">Ver todos los préstamos</asp:ListItem>
        <asp:ListItem Value="actual">Ver préstamos en curso</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:GridView ID="GridViewLendings" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                  OnRowDeleting="GridViewLendings_RowDeleting" OnRowEditing="GridViewLendings_RowEditing" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="labelBookTitle" runat="server" Text='<%# Eval("Book.Title") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Member">
                <ItemTemplate>
                    <asp:Label ID="labelMemberName" runat="server" Text='<%# Eval("Member.FullName") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lend Date">
                <ItemTemplate>
                    <asp:Label ID="labelLendDate" runat="server" Text='<%# Eval("LendDate") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Return Date">
                <ItemTemplate>
                    <asp:Label ID="labelReturnDate" runat="server" Text='<%# Eval("ReturnDate") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"/>
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="linkCreateNewLending" Text="Crear" OnClick="LinkCreateNewLending_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
    <asp:LinkButton runat="server" ID="linkReturnLending" Text="Devolver" OnClick="LinkReturnLending_OnClick"
                    CausesValidation="false" CssClass="btn btn-primary"/>
</asp:Content>