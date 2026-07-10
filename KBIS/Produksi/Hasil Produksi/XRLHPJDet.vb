Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRLHPJDet
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGFTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim Kode As String

    Public Sub InitializeData(ByVal Bind As Collection)
        'cmsl = New SqlDataAdapter("Select Distinct '" & Bind.Item("Tanggal").ToString & "' As Tanggal,DivID,Div,BOMID,Brand,ArtName,Warna,Urut,(Select Sum(TotOrder) From T_SchProdDtl where Tanggal=x.Tanggal and BOMID=x.BOMID) As TotOrder From(Select Distinct D.Tanggal,D.DivID,Dv.Nama As Div,D.BOMID,Brand,ArtName,Warna,Urut From T_SchProdDtl SD Inner Join T_HPJDtl D On SD.Tanggal=D.Tanggal and SD.BOMID=D.BOMID and SD.Uk=D.Uk Inner Join M_DivProd Dv On D.DivID=Dv.DivID where D.Tanggal<='" & Bind.Item("Tanggal").ToString & "')as x Order By Urut", koneksi)
        cmsl = New SqlDataAdapter("Select Distinct tbl.Tanggal,DivID,Div,tbl.BOMID,Brand,ArtName,Warna,Urut,(Select Sum(TotOrder) From T_SchProdDtl where Tanggal=tbl.Tanggal and BOMID=tbl.BOMID) As TotOrder From (Select Tanggal,DivID,Div,BOMID,Uk,Urut From(Select Distinct D.Tanggal, D.DivID,Dv.Nama As Div,D.BOMID,D.Uk,Urut From T_HPJDtl D Inner Join M_DivProd Dv On D.DivID=Dv.DivID where D.Tanggal='" & Bind.Item("Tanggal").ToString & "')as x Union All Select Distinct '" & Bind.Item("Tanggal").ToString & "' As Tanggal,DivID,Div,BOMID,Uk,Urut From(Select Distinct D.Tanggal,D.DivID,Dv.Nama As Div,D.BOMID,D.Uk,Urut From T_HPJDtl D Inner Join M_DivProd Dv On D.DivID=Dv.DivID where D.Tanggal<='" & Bind.Item("Tanggal").ToString & "')as x ) as tbl Inner Join T_SchProdDtl SD On tbl.Tanggal=SD.Tanggal and tbl.BOMID=SD.BOMID and tbl.Uk=SD.Uk Order By Urut", koneksi)

        'Kode = Bind.Item("Kode").ToString

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LHPJDet", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("DivID", "DivID"), New System.Data.Common.DataColumnMapping("Div", "Div"), New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("Brand", "Brand"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Urut", "Urut")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "LHPJDet")

        Me.DataMember = "LHPJDet"
        Me.DataSource = DsLap

        Me.LBPeriode.Text = "Per Tanggal " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")

        Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BOMID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.Tanggal", "{0}")})
        Me.LBGHBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.BOMID", "{0}")})
        Me.LBDivID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.DivID", "{0}")})
        Me.LBDiv.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.Div", "{0}")})
        Me.LBBrand.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.Brand", "{0}")})
        Me.LBStyle.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.ArtName", "{0}")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.Warna", "{0}")})
        Me.LBTotOrder.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LHPJDet.TotOrder", "{0:#,#0.#}")})
        Me.ShowPreview()

    End Sub

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("Tanggal").Value = Convert.ToString(GetCurrentColumnValue("Tanggal"))
        subRep.Parameters("BOMID").Value = Convert.ToString(GetCurrentColumnValue("BOMID"))
        subRep.Parameters("DivID").Value = Convert.ToString(GetCurrentColumnValue("DivID"))

    End Sub

    Private Sub XrSubreport2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport2.BeforePrint
        Dim xrSubReport2 As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep2 As XtraReport = TryCast(xrSubReport2.ReportSource, XtraReport)
        subRep2.Parameters("Tanggal").Value = Convert.ToString(GetCurrentColumnValue("Tanggal"))
        subRep2.Parameters("BOMID").Value = Convert.ToString(GetCurrentColumnValue("BOMID"))

    End Sub

    Private Sub XRPOBJLkIntR_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class