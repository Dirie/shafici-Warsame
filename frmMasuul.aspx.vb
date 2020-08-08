Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Data
Partial Class frmMasuul
    Inherits System.Web.UI.Page
    Dim cnstring As String = WebConfigurationManager.ConnectionStrings("SH-SMS").ConnectionString
    Dim mycon As New SqlConnection(cnstring)
    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpMasuul.SelectedIndexChanged
        fillGrid()
    End Sub
    Sub fillGrid()
        Try
            Dim str As String = DrpMasuul.SelectedItem.ToString
            Dim Name As String = str.Replace("'", "''")
            mycon.Open()
            Dim sql As String
            sql = "select STNO,STName,r.SCHOOL ,r.CLASS,l.AMOUNT ,r.MASUUL    from tblRegistration r join LACAGAHA l on r.SCHOOL=l.SCHOOL where r.MASUUL ='" & Name & "'"
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(sql, mycon)
            da.Fill(ds, "Masuul")
            Me.GridView1.DataSource = ds.Tables("Masuul")
            GridView1.DataBind()
        Catch ex As Exception
            LBLMESAGES.Visible = True
            lblmsg.Visible = True
            LBLMESAGES.Text = ex.Message
        Finally
            mycon.Close()
        End Try
    End Sub
    Sub fillcombox_Name()
        Try
            mycon.Open()
            Dim cmd As New SqlCommand("select distinct MASUUL from tblRegistration order by MASUUL", mycon)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                If Not IsDBNull(dr.Item(0)) Then
                    DrpMasuul.Items.Add(dr.Item(0))
                End If
            End While
            dr.Close()
            mycon.Close()
        Catch ex As Exception
            LBLMESAGES.Visible = True
            lblmsg.Visible = True
            LBLMESAGES.Text = ex.Message & " fill comb"
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillcombox_Name()
    End Sub
   
End Class
