Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Web.Configuration
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim cnstring As String = WebConfigurationManager.ConnectionStrings("SH-SMS").ConnectionString
    Dim mycon As New SqlConnection(cnstring)
    Dim sno As String = String.Empty
    Dim name As String = String.Empty
    Sub fillGrid()
        Try
            mycon.Open()
            Dim sql As String
            sql = "select STNO,STName from tblRegistration"
            'MsgBox(sql)
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(sql, mycon)
            da.Fill(ds, "Masuul")
            Me.GridView1.DataSource = ds.Tables("Masuul")
            GridView1.DataBind()
        Catch ex As Exception
            'LBLMESAGES.Visible = True
            MsgBox(ex.Message)
        Finally
            mycon.Close()
        End Try
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        fillGrid()
        'mycon.Open()
        'Dim cmd As New SqlCommand("select STNO,STName from tblRegistration", mycon)
        'Dim dr As SqlDataReader
        'dr = cmd.ExecuteReader
        'While dr.Read
        '    If Not IsDBNull(dr.Item("STNO")) Then
        '        sno = dr.Item("STNO")
        '    End If
        '    If Not IsDBNull(dr.Item("STName")) Then
        '        name = dr.Item("STName")
        '    End If
        '    GridView1.DataBind()
        'End While
        'mycon.Close()

    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Cells(0).Text = sno
        '    e.Row.Cells(1).Text = name
        'End If
    End Sub
End Class
