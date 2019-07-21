<%--<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="web._Default" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
        <div style="direction: ltr">
			<asp:GridView ID="GridView1" runat="server" ViewStateMode="Enabled" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowSorting ="True"
				OnSorting="GridView1_Sorting" 
                DataKeyNames="ItemCode" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">

                <Columns>    
				
					<asp:BoundField HeaderText="דירוג מוצר" DataField="ItemRank" SortExpression="ItemRank" /> 
					
					<asp:BoundField HeaderText="מק''ט" DataField="ItemCode" SortExpression="ItemCode" />
					
					<asp:BoundField HeaderText="תאור מוצר" DataField="ItemDesc" SortExpression="ItemDesc" />
                    
					<asp:BoundField HeaderText="סך היקף מכירות המוצר ברשת" DataField="TotalSales" SortExpression="TotalSales" />
                    
					<asp:BoundField HeaderText="קוד חנות" DataField="StoreCode" SortExpression="StoreCode" />
                    
					<asp:BoundField HeaderText="שם חנות" DataField="StoreDesc" SortExpression="StoreDesc" />
                    
					<asp:BoundField HeaderText="היקף מכירות המוצר בחנות" DataField="TotalStoreSales" SortExpression="TotalStoreSales" />
                </Columns>            
            	<FooterStyle BackColor="#CCCCCC" />
				<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
				<PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
				<RowStyle BackColor="White" />
				<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
				<SortedAscendingCellStyle BackColor="#F1F1F1" />
				<SortedAscendingHeaderStyle BackColor="#808080" />
				<SortedDescendingCellStyle BackColor="#CAC9C9" />
				<SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        	
        </div>
    	<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterByStore">
		</asp:DropDownList>
    </form>
</body>
</html>
