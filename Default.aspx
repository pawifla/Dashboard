<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SouthForkDamnDashboard.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    <div class="row">
        <div class="container bg-solid">
            <div class="card-deck mb-3 text-center">
                <div class="card mb-4 box-shadow">
                    <div class="card-header bg-primary">
                        <h4 class="my-0 font-weight-normal text-white pod1">Connection Status</h4>
                    </div>
                    <div class="card-body bg-solid">
                        <h2 class="connStatus"></h2>
                        <ul class="list-unstyled mt-3 mb-4">
                            <li>4G Modem is  <label id="lbl_modemStatus"></label><div id="modemSpinner" class="spinner-border spinner-border-sm fa-spin text-primary" role="status"><span class="sr-only"></span></div></li>
                            <li>Data Collector is  <label id="lbl_sensorStatus"></label><div id="sensorSpinner" class="spinner-border spinner-border-sm fa-spin text-primary" role="status"><span class="sr-only"></span></div></li>
                            <li>FTP Port is  <label id="lbl_ftpPort"></label><div id="wakeupSpinner" class="spinner-border spinner-border-sm fa-spin text-primary" role="status"><span class="sr-only"></span></div></li>
                        </ul>
                    </div>
                </div>
                <div class="card mb-4 box-shadow">
                    <div class="card-header bg-primary">
                        <h4 class="my-0 font-weight-normal text-white pod2">Last Database Update</h4>
                    </div>
                    <div class="card-body">
                        <h2 class="lastDbUpdate"></h2>
                        <ul class="list-unstyled mt-3 mb-4">
                            <li><strong>Date:</strong> <span id="dateLastDB"></span></li>
                            <li><strong>Elevation:</strong> <span id="elevLastDB"></span></li>
                            <li><strong>Upstream:</strong> <span id="upLastDB"></span></li>
                            <li><strong>Downsteam:</strong> <span id="downLastDB"></span></li>
                        </ul>
                    </div>
                </div>
                <div class="card mb-4 box-shadow ">
                    <div class="card-header bg-primary">
                        <h4 class="my-0 font-weight-normal text-white pod3">Last Data Sync</h4>
                    </div>
                    <div class="card-body">
                        <h2 class="lastDataSync"></h2>
                        <ul class="list-unstyled mt-3 mb-4">
                            <li><strong>Date: </strong><span id="dateLastSync"></span></li>
                            <li><strong>Time: </strong><span id="timeLastSync"></span></li>
                            <li><strong>File: </strong><span id="fileLastSync"></span></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        </div>

<%--    <asp:HiddenField runat="server" ID="App" ClientIDMode="Static" OnInit="Pass_App" />--%>
    <script src="JS/DataService.js"></script>
</asp:Content>
