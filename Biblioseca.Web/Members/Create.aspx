<%@ Page Title="Create Member" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Biblioseca.Web.Members.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create</h2>
    <div class="form-horizontal">
        <h4>Member</h4>
        <hr/>
        <asp:TextBox ID="textBoxFirstName" placeholder="First name" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxFirstNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El nombre es obligatorio" ControlToValidate="textBoxFirstName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:TextBox ID="textBoxLastName" placeholder="Lastname" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxLastNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El apellido es obligatorio" ControlToValidate="textBoxLastName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:TextBox ID="textBoxUserName" placeholder="UserName" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxUserNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El nombre de usuario es obligatorio" ControlToValidate="textBoxUserName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:Button ID="buttonCreateMember" runat="server" Text="Crear" OnClick="ButtonCreateMember_Click" CssClass="btn btn-primary"/>
    </div>
</asp:Content>