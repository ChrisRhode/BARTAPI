<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleForStation.aspx.cs" Inherits="BARTInfo.ScheduleForStation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BART Train Departure Lookup</title>
</head>
<body>
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <form id="form1" runat="server">
        <div>
            <h1>Look up scheduled BART Train Departures for Today By Station</h1><p></p>
            <asp:DropDownList ID="lbStations" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lbStations_SelectedIndexChanged">
            </asp:DropDownList><br />
            <asp:DropDownList ID="lbFinalDestination" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lbFinalDestination_SelectedIndexChanged">
            </asp:DropDownList><p></p>
            <asp:Label ID="lblDescription" runat="server" Text="Departure times for today"></asp:Label>
            <p></p>
            <asp:Table ID="tblDepartureTimes" runat="server">
        </asp:Table>
        </div>
    </form>
</body>
</html>
