<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
     var filterRowFiler = false;

     function ApplyFilter(dateFrom, dateTo) {
      var d1 = dateFrom.GetText();
      var d2 = dateTo.GetText();
      if (d1 == "" || d2 == "")
          return;
      filterRowFiler = true;
      grid.AutoFilterByColumn("BirthDate", d1 + "|" + d2);
     }

     function BeginCallBack(s, e) {
      if (e.command == "APPLYHEADERCOLUMNFILTER" || e.command == "SHOWFILTERCONTROL")
          filterRowFiler = false;
     }

     function EndCallBack(s, e) {
       if (grid.cpDates != null && !filterRowFiler) {
        dFrom.SetValue(grid.cpDates.DateFrom);
        dTo.SetValue(grid.cpDates.DateTo);
       }
     }
    </script>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="ads"
            KeyFieldName="EmployeeID" OnProcessColumnAutoFilter="grid_ProcessColumnAutoFilter" OnCustomJSProperties="grid_CustomJSProperties">
            <ClientSideEvents EndCallback="EndCallBack" BeginCallback="BeginCallBack" />
            <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowHeaderFilterButton="true" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="EmployeeID" ReadOnly="True" VisibleIndex="0">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="LastName" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FirstName" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="BirthDate" VisibleIndex="4">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="City" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Region" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PostalCode" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:AccessDataSource ID="ads" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT [EmployeeID], [LastName], [FirstName], [Title], [BirthDate], [HireDate], [City], [Region], [PostalCode] FROM [Employees]"></asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
