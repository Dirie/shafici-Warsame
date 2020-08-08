Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Data

Partial Class frmPayment
    Inherits System.Web.UI.Page
    'This is the class converts numbers into words
    Dim AmntToWrd As New AmountToWords
    Dim cnstring As String = WebConfigurationManager.ConnectionStrings("SH-SMS").ConnectionString
    Dim mycon As New SqlConnection(cnstring)
    'this variable will hold selected item to fill an-active combobox.
    Dim S_STNO, S_STNAME As String
    'this will temporory hold the Fee Amount when fee-type Change.
    Dim FeeAmount As Decimal = 0.0
    'this will hold bono Number to print student voucher.
    Dim bono As Integer = 0
    Sub fillcombox_Name()
        Try
            mycon.Open()
            Dim cmd As New SqlCommand("select STName from tblRegistration order by STName", mycon)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                If Not IsDBNull(dr.Item(0)) Then
                    drSearchName.Items.Add(dr.Item(0))
                End If
            End While
            dr.Close()
            mycon.Close()
        Catch ex As Exception
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = ex.Message
        End Try
    End Sub
    Sub fillcombox_ID()
        Try
            mycon.Open()
            Dim cmd As New SqlCommand("select STNO from tblRegistration order by STNO", mycon)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                If Not IsDBNull(dr.Item(0)) Then
                    drpSearchNo.Items.Add(dr.Item(0))
                End If
            End While
            dr.Close()
            mycon.Close()
        Catch ex As Exception
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = ex.Message
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        fillcombox_ID()
        fillcombox_Name()
        LBLMESAGES.Visible = False
        If Me.IsPostBack = False Then
            Dim str As String = Request.QueryString("STNO")
            If Not str = "" Then
                fill_Fields_By_ID(str)
                drpSearchNo.Text = S_STNO
            End If
        End If
    End Sub
    Function GenerateID()
        Try
            mycon.Open()
            Dim objCommand As SqlCommand = New SqlCommand("select RECNO as RECNO from TBLCashReciept  Order by RECNO", mycon)

            ' objCommand.CommandType = CommandType.Text

            Dim dreader As SqlDataReader

            dreader = objCommand.ExecuteReader()
            Dim RecCount, temp As Long
            Dim Flag, Varmyid As String
            Flag = ""
            Varmyid = ""

            If dreader.HasRows = True Then
                Do While (dreader.Read())
                    RecCount = RecCount + 1
                    Varmyid = dreader("RECNO")
                Loop

                temp = Mid(Varmyid, 4, 8)
                temp = temp + 1
                If temp < 10 Then
                    Flag = "000"
                ElseIf temp >= 10 And temp < 100 Then
                    Flag = "00"
                ElseIf temp >= 100 And temp < 1000 Then
                    Flag = "0"
                End If
                GenerateID = "REC" & Flag & temp
                txtrecvoucher.Text = GenerateID
            Else
                GenerateID = "REC" & "000" & 1
                txtrecvoucher.Text = GenerateID
            End If
            dreader.Close()
        Catch ex As Exception
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = ex.Message
        Finally
            mycon.Close()
        End Try
    End Function
    Sub GenerateID_Bono()
        Try
            mycon.Open()
            Dim objCommand As SqlCommand = New SqlCommand("select max(bono) as bono from TBLCashReciept  Order by bono", mycon)
            Dim dreader As SqlDataReader
            dreader = objCommand.ExecuteReader()
            If dreader.Read Then
                If Not IsDBNull(dreader.Item("bono")) Then
                    txtbono.Text = Val(dreader.Item("bono")) + 1
                Else
                    txtbono.Text = 1
                End If
            Else
                txtbono.Text = 1
            End If
            dreader.Close()
        Catch ex As Exception
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = ex.Message
        Finally
            mycon.Close()
        End Try
    End Sub
    Protected Sub drSearchName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drSearchName.SelectedIndexChanged
        If IsPostBack = True Then
            Try
                GenerateID()
                GenerateID_Bono()
                txtdate.Text = Now.Date
                Dim str As String = drSearchName.SelectedItem.ToString
                Dim Name As String = str.Replace("'", "''")
                Dim sql As String = "select STNO,STName,r.SCHOOL ,r.CLASS,l.AMOUNT    from tblRegistration r join LACAGAHA l on r.SCHOOL=l.SCHOOL where STName='" & Name & "'"

                mycon.Open()
                Dim cmd As New SqlCommand(sql, mycon)
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txtstno.Text = dr.Item("STNO").ToString
                    S_STNO = dr.Item("STNO").ToString
                    txtname.Text = dr.Item("STName").ToString
                    S_STNAME = dr.Item("STName").ToString
                    txtschool.Text = dr.Item("SCHOOL").ToString
                    txtclass.Text = dr.Item("CLASS").ToString
                    txtamount.Text = dr.Item("AMOUNT").ToString
                    FeeAmount = CDec(txtamount.Text)
                    txtamountInWords.Text = AmntToWrd.Convert(txtamount.Text)
                End If
                dr.Close()
                mycon.Close()
                read_Old_Month_By_Name("select bono,AMOUNT,MONTH  from TBLCashReciept where STNAME ='" & Name & "' and year='" & Now.Date.Year & "'")
            Catch ex As Exception
                LBLMESAGES.Visible = True
                LBLMESAGES.Text = ex.Message
            End Try
            drpSearchNo.Text = S_STNO
        End If
    End Sub

    Sub clear()
        txtdate.Text = ""
        txtrecvoucher.Text = ""
        txtamount.Text = ""
        txtstno.Text = ""
        txtname.Text = ""
        txtschool.Text = ""
        'txtmonth.Text = ""
        txtamountInWords.Text = ""
        txtusdno.Text = ""
        txtclass.Text = ""
        txtdate.Text = ""
        txtbono.Text = ""
        'this will clear all Amount fields of each month.
        txtamountAug.Text = ""
        txtamountSep.Text = ""
        txtamountOct.Text = ""
        txtamountNov.Text = ""
        txtamountDec.Text = ""
        txtamountJan.Text = ""
        txtamountFeb.Text = ""
        txtamountMar.Text = ""
        txtamountApr.Text = ""
        txtamountMay.Text = ""
        txtamountJune.Text = ""
        txtamountJuly.Text = ""

        'this will clear all receipt fields of each month.
        txtBonoAug.Text = ""
        txtBonoSep.Text = ""
        txtBonoOct.Text = ""
        txtBonoNov.Text = ""
        txtBonoDec.Text = ""
        txtBonoJan.Text = ""
        txtBonoFeb.Text = ""
        txtBonoMar.Text = ""
        txtBonoApr.Text = ""
        txtBonoMay.Text = ""
        txtBonoJune.Text = ""
        txtBonoJuly.Text = ""

    End Sub
    Protected Sub btngebono_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngebono.Click
        If IsPostBack = True Then
            GenerateID_Bono()
            txtdate.Text = Now.Date
        End If
    End Sub
    Sub GetAccademicYear()
        Try
            mycon.Open()
            Dim cmd As New SqlCommand("GetAccademicYear ' " & Format(CDate(txtdate.Text), "yyyy-MM-dd") & "'", mycon)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblacedemicYear.Text = dr.Item(0).ToString
            End If
        Catch ex As Exception
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = ex.Message
        Finally
            mycon.Close()
        End Try
    End Sub
    Sub AddRecord(ByVal Month_Name As String)
        Try
            mycon.Open()
            Dim cmd As New SqlCommand, prm As New SqlParameter

            cmd.Connection = mycon
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "sp_addCashReceipt"

            prm = cmd.Parameters.Add("@RECNO", SqlDbType.VarChar, 50)
            prm.Value = txtrecvoucher.Text.ToString
            prm = cmd.Parameters.Add("@TRNDATE", SqlDbType.Date, 50)
            prm.Value = Format(CDate(txtdate.Text), "yyyy-MM-dd")
            prm = cmd.Parameters.Add("@STNO", SqlDbType.VarChar, 50)
            prm.Value = txtstno.Text.ToString
            prm = cmd.Parameters.Add("@STNAME", SqlDbType.VarChar, 50)
            prm.Value = txtname.Text.ToString
            prm = cmd.Parameters.Add("@SCHOOL", SqlDbType.VarChar, 50)
            prm.Value = txtschool.Text.ToString
            prm = cmd.Parameters.Add("@CLASS", SqlDbType.VarChar, 50)
            prm.Value = txtclass.Text.ToString
            prm = cmd.Parameters.Add("@FEETYPE", SqlDbType.VarChar, 50)
            prm.Value = drpdfeeType.Text.ToString
            prm = cmd.Parameters.Add("@MONTH", SqlDbType.VarChar, 50)
            prm.Value = Month_Name
            prm = cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar, 50)
            prm.Value = txtamount.Text.ToString
            prm = cmd.Parameters.Add("@AMNT_IN_WORDS", SqlDbType.VarChar, 50)
            prm.Value = txtamountInWords.Text.ToString
            prm = cmd.Parameters.Add("@USDNO", SqlDbType.VarChar, 50)
            prm.Value = txtusdno.Text.ToString
            prm = cmd.Parameters.Add("@USR", SqlDbType.VarChar, 50)
            prm.Value = "Ali"
            prm = cmd.Parameters.Add("@STATUS", SqlDbType.VarChar, 50)
            prm.Value = "1"
            prm = cmd.Parameters.Add("@year", SqlDbType.VarChar, 50)
            prm.Value = lblacedemicYear.Text
            prm = cmd.Parameters.Add("@bono", SqlDbType.VarChar, 50)
            prm.Value = txtbono.Text.ToString

            cmd.ExecuteNonQuery()
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = "Record Saved."
            bono = txtbono.Text
        Catch ex As Exception
            LBLMESAGES.Visible = True
            'lblmsg.Visible = True
            LBLMESAGES.Text = ex.Message
        Finally
            mycon.Close()
        End Try
    End Sub

    Protected Sub btnbixi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbixi.Click
        If txtstno.Text = "" Then
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = "Please Select the student."
            Exit Sub
        End If

        If chkAug.Checked = False And chkSep.Checked = False And chkOct.Checked = False And chkNov.Checked = False And chkDec.Checked = False And chkJan.Checked = False And chkFeb.Checked = False And chkMar.Checked = False And chkApr.Checked = False And chkMay.Checked = False And chkJune.Checked = False And chkJuly.Checked = False Then
            LBLMESAGES.Visible = True
            LBLMESAGES.Text = "NO Month is Selected."
            Exit Sub
        End If
        If IsPostBack = True Then

            If chkAug.Checked = True Then
                AddRecord(chkAug.Text)
            End If
            If chkSep.Checked = True Then
                AddRecord(chkSep.Text)
            End If
            If chkOct.Checked = True Then
                AddRecord(chkOct.Text)
            End If
            If chkNov.Checked = True Then
                AddRecord(chkNov.Text)
            End If
            If chkDec.Checked = True Then
                AddRecord(chkDec.Text)
            End If
            If chkJan.Checked = True Then
                AddRecord(chkJan.Text)
            End If
            If chkFeb.Checked = True Then
                AddRecord(chkFeb.Text)
            End If
            If chkMar.Checked = True Then
                AddRecord(chkMar.Text)
            End If
            If chkApr.Checked = True Then
                AddRecord(chkApr.Text)
            End If
            If chkMay.Checked = True Then
                AddRecord(chkMay.Text)
            End If
            If chkJune.Checked = True Then
                AddRecord(chkJune.Text)
            End If
            If chkJuly.Checked = True Then
                AddRecord(chkJuly.Text)
            End If

            clear()

        End If
    End Sub
    Protected Sub drpSearchNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSearchNo.SelectedIndexChanged
        If IsPostBack = True Then
            fill_Fields_By_ID(drpSearchNo.SelectedItem.ToString)
        End If
    End Sub
    Function isStudentExist1(ByVal str As String) As Boolean
        mycon.Open()
        Dim cmd As New SqlCommand("select * from tblRegistration where STNO='" & str & "'", mycon)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        If dr.Read Then
            isStudentExist1 = True
        Else
            isStudentExist1 = False
        End If
        dr.Close()
        mycon.Close()
    End Function
    Function isStudentExist(ByVal str As String) As Boolean
        mycon.Open()
        Dim cmd As New SqlCommand("select * from tblRegistration where STName='" & str & "'", mycon)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        If dr.Read Then
            isStudentExist = True
        Else
            isStudentExist = False
        End If
        dr.Close()
        mycon.Close()
    End Function
    Sub fill_Fields_By_ID(ByVal ID As String)
        Try
            If ID = "" Then
                Exit Sub
            End If
            GenerateID()
            GenerateID_Bono()
            txtdate.Text = Now.Date
            Dim str As String = ID
            Dim Name As String = str.Replace("'", "''")
            Dim sql As String = "select STNO,STName,r.School ,r.CLASS,l.AMOUNT    from tblRegistration r join LACAGAHA l on r.SCHOOL=l.SCHOOL where STNO='" & Name & "'"
            mycon.Open()
            Dim cmd As New SqlCommand(sql, mycon)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtstno.Text = dr.Item("STNO").ToString
                S_STNO = dr.Item("STNO").ToString
                txtname.Text = dr.Item("STName").ToString
                S_STNAME = dr.Item("STName").ToString
                txtschool.Text = dr.Item("SCHOOL").ToString
                txtclass.Text = dr.Item("CLASS").ToString
                txtamount.Text = dr.Item("AMOUNT").ToString
                FeeAmount = CDec(txtamount.Text)
                txtamountInWords.Text = AmntToWrd.Convert(txtamount.Text)
            End If
            dr.Close()
            mycon.Close()
            GetAccademicYear()
            read_Old_Month_By_Name("select bono,AMOUNT,MONTH  from TBLCashReciept where STNO ='" & Name & "' and year='" & lblacedemicYear.Text & "'")
        Catch ex As Exception
            LBLMESAGES.Visible = True
            'lblmsg.Visible = True
            LBLMESAGES.Text = ex.Message
        End Try
        drSearchName.Text = S_STNAME
    End Sub
    Sub read_Old_Month_By_Name(ByVal Name As String)
        activate_All()
        mycon.Open()
        Dim cmd As New SqlCommand(Name, mycon)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            If Not IsDBNull(dr.Item(2)) Then
                If dr.Item(2).ToString = chkAug.Text Then
                    txtBonoAug.Text = dr.Item(0)
                    txtamountAug.Text = dr.Item(1)
                    chkAug.Enabled = False
                End If
                If dr.Item(2).ToString = chkSep.Text Then
                    txtBonoSep.Text = dr.Item(0)
                    txtamountSep.Text = dr.Item(1)
                    chkSep.Enabled = False
                End If
                If dr.Item(2).ToString = chkOct.Text Then
                    txtBonoOct.Text = dr.Item(0)
                    txtamountOct.Text = dr.Item(1)
                    chkOct.Enabled = False
                End If
                If dr.Item(2).ToString = chkNov.Text Then
                    txtBonoNov.Text = dr.Item(0)
                    txtamountNov.Text = dr.Item(1)
                    chkNov.Enabled = False
                End If
                If dr.Item(2).ToString = chkDec.Text Then
                    txtBonoDec.Text = dr.Item(0)
                    txtamountDec.Text = dr.Item(1)
                    chkDec.Enabled = False
                End If
                If dr.Item(2).ToString = chkJan.Text Then
                    txtBonoJan.Text = dr.Item(0)
                    txtamountJan.Text = dr.Item(1)
                    chkJan.Enabled = False
                End If
                If dr.Item(2).ToString = chkFeb.Text Then
                    txtBonoFeb.Text = dr.Item(0)
                    txtamountFeb.Text = dr.Item(1)
                    chkFeb.Enabled = False
                End If
                If dr.Item(2).ToString = chkMar.Text Then
                    txtBonoMar.Text = dr.Item(0)
                    txtamountMar.Text = dr.Item(1)
                    chkMar.Enabled = False
                End If
                If dr.Item(2).ToString = chkApr.Text Then
                    txtBonoApr.Text = dr.Item(0)
                    txtamountApr.Text = dr.Item(1)
                    chkApr.Enabled = False
                End If
                If dr.Item(2).ToString = chkMay.Text Then
                    txtBonoMay.Text = dr.Item(0)
                    txtamountMay.Text = dr.Item(1)
                    chkMay.Enabled = False
                End If
                If dr.Item(2).ToString = chkJune.Text Then
                    txtBonoJune.Text = dr.Item(0)
                    txtamountJune.Text = dr.Item(1)
                    chkJune.Enabled = False
                End If
                If dr.Item(2).ToString = chkJuly.Text Then
                    txtBonoJuly.Text = dr.Item(0)
                    txtamountJuly.Text = dr.Item(1)
                    chkJuly.Enabled = False
                End If
            End If
        End While
        dr.Close()
        mycon.Close()
        GetAccademicYear()
    End Sub
    Sub activate_All()
        chkAug.Enabled = True
        chkAug.Checked = False
        chkSep.Enabled = True
        chkSep.Checked = False
        chkOct.Enabled = True
        chkOct.Checked = False
        chkNov.Enabled = True
        chkNov.Checked = False
        chkDec.Enabled = True
        chkDec.Checked = False
        chkJan.Enabled = True
        chkJan.Checked = False
        chkFeb.Enabled = True
        chkFeb.Checked = False
        chkMar.Enabled = True
        chkMar.Checked = False
        chkApr.Enabled = True
        chkApr.Checked = False
        chkMay.Enabled = True
        chkMay.Checked = False
        chkJune.Enabled = True
        chkJune.Checked = False
        chkJuly.Enabled = True
        chkJuly.Checked = False
        'this will clear all Amount fields of each month.
        txtamountAug.Text = ""
        txtamountSep.Text = ""
        txtamountOct.Text = ""
        txtamountNov.Text = ""
        txtamountDec.Text = ""
        txtamountJan.Text = ""
        txtamountFeb.Text = ""
        txtamountMar.Text = ""
        txtamountApr.Text = ""
        txtamountMay.Text = ""
        txtamountJune.Text = ""
        txtamountJuly.Text = ""

        'this will clear all receipt fields of each month.
        txtBonoAug.Text = ""
        txtBonoSep.Text = ""
        txtBonoOct.Text = ""
        txtBonoNov.Text = ""
        txtBonoDec.Text = ""
        txtBonoJan.Text = ""
        txtBonoFeb.Text = ""
        txtBonoMar.Text = ""
        txtBonoApr.Text = ""
        txtBonoMay.Text = ""
        txtBonoJune.Text = ""
        txtBonoJuly.Text = ""

    End Sub
    Protected Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Response.Redirect("frmMasuul.aspx")
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("frmPaymentPerDate.aspx")
    End Sub
    Protected Sub drpdfeeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdfeeType.SelectedIndexChanged
        If drpdfeeType.SelectedItem.ToString = "TUITION" Then
            txtamount.Text = FeeAmount
            txtamount.ReadOnly = True
        Else
            txtamount.Text = ""
            txtamount.ReadOnly = False
        End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("frmPaymentInfo.aspx")
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If bono = 0 Then
            Dim url As String = "frmPaymentVoucher.aspx?"
            url = url & "q=1" '& bono
            MsgBox(url)
            Response.Redirect(url)
        End If

    End Sub
End Class
