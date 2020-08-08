<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPaymentPrint.aspx.vb" Inherits="frmPaymentPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language ="javascript" type="text/javascript" >
        function PrintPages() {

            var Pstr = document.getElementById("Print");
            var Cstr = document.getElementById("Close");

            if (Pstr = "Print") {
                window.print
            }
            else if (Cstr = "Close") {
                        window.alert("Asalaamu Calaykum?");
            }
        }
    </script>
    <title>Untitled Page
    
    
    
    </title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        #Print
        {
            width: 59px;
        }
        #Button2
        {
            width: 58px;
        }
        #Close
        {
            width: 61px;
        }
        .style2
        {
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
                    <input id="Print" name="Print" type="button" value="Print" onclick="PrintPages()" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Close" name="Close" type="button" value="Close" onclick="PrintPages()"/><table class="style1">
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td class="style2" style="text-align: center">
                                <asp:Label ID="lblTitle" runat="server" style="text-align: center" 
                                    Text="Ardayda Lacagta Bixisay Bilaha:"></asp:Label>
&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblMonths" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="Label1" runat="server" Text="School:"></asp:Label>
&nbsp;
                                <asp:Label ID="lblschool" runat="server" Text="Shiekh Madar Sare"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="Class:"></asp:Label>
&nbsp;&nbsp;
                                <asp:Label ID="lblclass" runat="server" Text="3/1"></asp:Label>
&nbsp;&nbsp;
                                <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label>
&nbsp;&nbsp;
                                <asp:Label ID="lblyear" runat="server" Text="2013-2014"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:GridView ID="GridView1" runat="server" Width="988px" ShowFooter="True" 
                        Font-Size="Medium" Height="228px">
                    </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;&nbsp;
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
