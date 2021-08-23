<%@ Page Title="Return Lending" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Return.aspx.cs" Inherits="Biblioseca.Web.Lendings.Return" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Return</h2>
    <div class="form-horizontal">
        <h4>Lending</h4>
        <hr/>
        <div class="form-group">
            <label class="col-sm-2 control-label">Book</label>
            <div class="col-md-10">
                <asp:DropDownList ID="bookList" runat="server" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Member</label>
            <div class="col-md-10">
                <asp:DropDownList ID="memberList" runat="server" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="buttonReturnLending" runat="server" Text="Devolver" OnClick="ButtonReturnLending_Click" CssClass="btn btn-default"/>
            </div>
        </div>
    </div>
    <div>
        <asp:Button ID="buttonBackToLendings" runat="server" Text="Volver a préstamos" OnClick="ButtonBackToLendings_Click" CssClass="btn btn-default"/>
    </div>
</asp:Content>