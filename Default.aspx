<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Height="34px" Text="Button" 
            Width="126px" />
        <br />
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Height="72px" Width="565px">
            <Columns>
                <asp:BoundField ApplyFormatInEditMode="True" HeaderText="Student-ID" />
                <asp:BoundField HeaderText="Name" />
            </Columns>
        </asp:GridView>
    
        <br />
    
    </div>
    </form>
</body>
</html>
