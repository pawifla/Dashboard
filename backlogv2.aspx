<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="backlogv2.aspx.cs" Inherits="SouthForkDamnDashboard.backlogv2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <template>
        <div style="position: absolute; top: 0; right: 0;">

            <div class="toast" id="toast" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="toast-header">
                    <img src="..." class="rounded mr-2" alt="...">
                    <strong class="mr-auto"><span id="strong"></span></strong>
                    <small class="text-muted"><span id="small"></span></small>
                    <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                    <span id="body"></span>
                <div class="toast-body">
                </div>
            </div>

        </div>
    </template>

     <br />
        <br />
        <br />
    <br />
    <div class="center">
        <div class="row">
        <div class="col-3"></div>
    <div class="col-6">
        <h3>Select Backlog Data</h3></div><div class="col-3"></div></div>
    <div class="row"><div class="col-4"></div>
    <div class="col-4">
        <div class="btn-primary dropdown">
        <button class="btn-primary dropbtn" onclick="return false;">Select Year</button>
            <div class="col-0">
            <div class="row">
        <div class="dropdown-content" style="text-align:center; align-content:center">
            <asp:ListView ID ="ListView1" runat="server" DataKeyNames="year" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton ID="yearsList" runat="server" OnClientClick="return CheckDouble()"><%# DataBinder.Eval(Container.DataItem,"year") %></asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
            </div>
                    </div>
                </div>
        </div>
    </div>
        </div>
    </div>
        
   
    <br />
   
    
    <div class="container">
        <div id="title" runat="server"></div>
    <div class="row" runat="server">    
    <div id="hiddenTables" class="col-6" runat="server"></div><div id="otherHiddenTables" class="col-6" runat="server"></div>
</div></div>
    <%--<a runat="server" id="refresh" onServerClick="HTML_AnchorClick" >ANCHOR</a>
        <span runat="server" id="span">Hello</span>
    <asp:Button ID="linkbutton" runat="server" OnClick="HTML_AnchorClick" ClientIDMode="static" Text=""></asp:Button>--%>
<%--    <span runat="server" class="RefreshClick" id="spanID" onclick="return false;"></span>--%>
    <asp:HiddenField ID="spanID" runat="server" ClientIDMode="Static" />
</asp:Content>
