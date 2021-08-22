<%@ Page Title="Create Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Biblioseca.Web.Categories.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create</h2>
    <div class="form-horizontal">
        <h4>Category</h4>
        <hr/>
        <asp:TextBox ID="textBoxName" placeholder="Name" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El nombre es obligatorio" ControlToValidate="textBoxName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:Button ID="buttonCreateCategory" runat="server" Text="Crear" OnClick="ButtonCreateCategory_Click" CssClass="btn btn-primary"/>
    </div>
</asp:Content>