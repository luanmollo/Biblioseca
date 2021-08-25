<%@ Page Title="Create Book" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Biblioseca.Web.Books.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create</h2>
    <div class="form-horizontal">
        <h4>Book</h4>
        <hr/>
        <div class="form-group">
            <div class="col-md-10">
                <asp:TextBox ID="textBoxTitle" placeholder="Title" runat="server" CssClass="form-control"/>
                <asp:RequiredFieldValidator ID="textBoxFirstNameRequiredFieldValidator" runat="server"
                                            ErrorMessage="El titulo es obligatorio" ControlToValidate="textBoxTitle" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:TextBox ID="textBoxDescription" placeholder="Description" runat="server" CssClass="form-control"/>
                <asp:RequiredFieldValidator ID="textBoxDescriptionRequiredFieldValidator" runat="server"
                                            ErrorMessage="La descripción es obligatoria" ControlToValidate="textBoxDescription" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:TextBox ID="textBoxISBN" placeholder="ISBN" runat="server" CssClass="form-control"/>
                <asp:RequiredFieldValidator ID="textBoxISBNRequiredFieldValidator" runat="server"
                                            ErrorMessage="El ISBN es obligatorio" ControlToValidate="textBoxISBN" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:TextBox ID="textBoxPrice" placeholder="Price" runat="server" CssClass="form-control"/>
                <asp:CompareValidator ID="textBoxPriceRequiredFieldValidator" Operator="DataTypeCheck" Type="Double" runat="server"
                                      ErrorMessage="El precio es obligatorio" ControlToValidate="textBoxPrice" ForeColor="Red">
                </asp:CompareValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:DropDownList ID="authorList" runat="server" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:DropDownList ID="categoryList" runat="server" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:TextBox ID="textBoxStock" placeholder="Stock" runat="server" CssClass="form-control"/>
                <asp:CompareValidator ID="textBoxStockRequiredFieldValidator" Operator="DataTypeCheck" Type="Integer" runat="server"
                                      ErrorMessage="El stock es obligatorio" ControlToValidate="textBoxStock" ForeColor="Red">
                </asp:CompareValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:Button ID="buttonCreateBook" runat="server" Text="Crear" OnClick="ButtonCreateBook_Click" CssClass="btn btn-default"/>
            </div>
        </div>
    </div>
</asp:Content>