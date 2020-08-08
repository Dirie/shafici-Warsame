Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Configuration
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Class frmPaymentPrint
    Inherits System.Web.UI.Page
    Dim cnstring As String = WebConfigurationManager.ConnectionStrings("SH-SMS").ConnectionString
    Dim mycon As New SqlConnection(cnstring)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillGrid(Session("str"))
        lblschool.Text = Session("school")
        lblclass.Text = Session("Class")
        lblMonths.Text = Session("Month")

        'fillGrid("")
    End Sub
    Sub fillGrid(ByVal str As String)
        'Try

        'Dim Name As String = str.Replace("'", "''")
        mycon.Open()
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(str, mycon)
        da.Fill(ds, "Masuul")
        Me.GridView1.DataSource = ds.Tables("Masuul")
        GridView1.DataBind()
        'Catch ex As Exception
        'LBLMESAGES.Visible = True
        ''lblmsg.Visible = True
        'LBLMESAGES.Text = ex.Message
        'Finally
        mycon.Close()
        'End Try
    End Sub
End Class
