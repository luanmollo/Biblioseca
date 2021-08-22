<%@ Page Title="Edit Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Biblioseca.Web.Categories.Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit</h2>
    <div class="form-horizontal">
        <h4>Category</h4>
        <hr/>
        <asp:TextBox ID="textBoxName" placeholder="Name" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El nombre es obligatorio" ControlToValidate="textBoxName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:Button ID="buttonEditCategory" runat="server" Text="Guardar" OnClick="ButtonEditCategory_Click" CssClass="btn btn-primary"/>
    </div>
</asp:Content>