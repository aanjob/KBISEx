Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekBOMv2
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGFKeb As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim No As Integer = 0

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select H.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotOrder,D.BBID,B.Nama,D.Sat,Round(Sum(Keb),2) As Keb,D.UkBB + ' ' + D.Ket As Ket From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.BOMID In (" & Bind.Item("BOMID").ToString & ") and D.BBID In (" & Bind.Item("BBID").ToString & ") Group By H.BOMID,ArtName,Warna,TotPsg,TotPsgPol,D.BBID,B.Nama,D.UkBB,D.Ket,D.Sat Order By B.Nama", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekBOM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("TotOrder", "TotOrder"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Keb", "Keb"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})


        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "RekBOM")

        Me.DataMember = "RekBOM"
        Me.DataSource = DsLap

        Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.BOMID")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Nama")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Sat")})
        Me.LBUk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Ket")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Sat")})
        Me.LBKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Keb", "{0:n2}")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTotKeb})

        Me.LBTotKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Keb", "{0:n2}")})
        SumGFKeb.FormatString = "{0:n2}"
        SumGFKeb.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotKeb.Summary = SumGFKeb

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()

    End Sub

    Private Sub LBBBID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBBBID.BeforePrint
        No += 1
    End Sub

    Private Sub LBNo_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBNo.BeforePrint
        Me.LBNo.Text = No
    End Sub

    'Private Sub GroupHeader1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
    '    No = 1
    'End Sub
End Class