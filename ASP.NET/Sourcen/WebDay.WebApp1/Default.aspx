<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebDay.WebApp1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Willkommen zum WebDay Blog!
    </h2>
    <p>
        &nbsp;<asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1">
            <ItemTemplate>
                <div class="post-header">
                    <div class="title">
                        <%#Eval("Title") %>
                    </div>
                    <div class="info">
                        <%#Eval("Username") %>
                        schreib am
                        <%#Eval("DateCreated") %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="post-content">
                    <%#Eval("Content") %>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </p>
    <p>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRecentPosts"
            TypeName="WebDay.WebApp1.RepositoryServiceReference.BlogRepositoryClient">
            <SelectParameters>
                <asp:Parameter DefaultValue="10" Name="maxCount" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </p>
</asp:Content>
