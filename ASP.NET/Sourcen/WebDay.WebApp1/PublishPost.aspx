<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PublishPost.aspx.cs" Inherits="WebDay.WebApp1.PublishPost"
     ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="error">
        <asp:ValidationSummary runat="server" HeaderText="Es ist ein Fehler aufgetreten" />
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" AssociatedControlID="TitleBox" Text="Betreff" />
        <asp:TextBox runat="server" ID="TitleBox" Width="80%" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TitleBox" ErrorMessage="Betreff ist erforderlich"
            Text="*" />
    </div>
    <div>
        <cc1:Editor ID="Editor1" runat="server" />
    </div>
    <div style="text-align: right;">
        <asp:Button runat="server" ID="PublishButton" Text="Veröffentlichen" OnClick="PublishButton_Click" />
        <asp:Button runat="server" ID="CancelButton" Text="Abbrechen" OnClick="CancelButton_Click"
            CausesValidation="false" />
    </div>
</asp:Content>
