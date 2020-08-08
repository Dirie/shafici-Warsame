<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMasuul.aspx.vb" Inherits="frmMasuul" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 142%;
        }
        .style2
        {
            height: 43px;
            width: 634px;
        }
        .style3
        {
            width: 91px;
        }
        .style4
        {
        }
        .style5
        {
            width: 634px;
        }
        .style6
        {
            width: 634px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="style4">
        <tr>
            <td class="style5">
                <table class="style1">
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label1" runat="server" Text="Mas'uul Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpMasuul" runat="server" AutoPostBack="True" 
                                Height="30px" Width="350px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td bgcolor="#CCCCCC" class="style6">
                </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="786px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:HyperLinkField DataTextField="STNO" HeaderText="StudentID" 
                            DataNavigateUrlFields="STNO" 
                            DataNavigateUrlFormatString="~/frmPayment.aspx?STNO={0}" />
                        <asp:BoundField DataField="STName" HeaderText="Name" SortExpression="STName" />
                        <asp:BoundField DataField="SCHOOL" HeaderText="School" SortExpression="SCHOOL" />
                        <asp:BoundField DataField="CLASS" HeaderText="Class" SortExpression="CLASS" />
                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" 
                            SortExpression="AMOUNT" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
        </tr>
    </table>
    <div>
    
    </div>
    <p>
        <asp:Label ID="lblmsg" runat="server" Text="Messages:" Visible="False"></asp:Label>
        <asp:Label ID="LBLMESAGES" runat="server" Text="LBLMESSAGES" Visible="False"></asp:Label>
    </p>
    </form>
</body>
</html>
