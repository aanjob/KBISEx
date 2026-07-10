Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRLapHPJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("Select BOMID,ArtName,Warna,TotOrder,DivID,Divisi,Urut,Ouut,TotOrder-Before-Ouut As Balance From(Select BOMID,ArtName,Warna,DivID,Divisi,Urut,Ouut,(Select Isnull((Select Distinct Sum(Distinct TotOrder) From T_SchProdDtl where BOMID=x.BOMID),0)) As TotOrder,(Select Isnull((Select Sum(Ouut) From T_HPJ H Inner Join T_HPJDtl D On H.Tanggal=D.Tanggal and H.Jam=D.Jam and H.DivID=D.DivID Where D.BOMID=x.BOMID and H.DivID=x.DivID and H.Tanggal<'2020/11/26'),0))  As Before From(Select D.BOMID,PD.ArtName,PD.Warna,D.DivID,Dv.Nama As Divisi,Dv.Urut,Sum(Ouut) as Ouut From T_HPJ H Inner Join T_HPJDtl D On H.Tanggal=D.Tanggal and H.Jam=D.Jam and H.DivID=D.DivID Inner Join M_DivProd Dv On H.DivID=Dv.DivID	Inner Join T_SchProdDtl PD On D.SchIDD=PD.SchIDD Where H.Tanggal>='2020/11/26' and H.Tanggal<='2020/11/26' Group By D.BOMID,D.DivID,Dv.Nama,Dv.Urut,PD.ArtName,PD.Warna) as x)as y", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LapHPJ", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("TotOrder", "TotOrder"), New System.Data.Common.DataColumnMapping("DivID", "DivID"), New System.Data.Common.DataColumnMapping("Divisi", "Divisi"), New System.Data.Common.DataColumnMapping("Urut", "Urut"), New System.Data.Common.DataColumnMapping("Ouut", "Ouut"), New System.Data.Common.DataColumnMapping("Balance", "Balance")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "LapHPJ")

        Me.XrPivotGrid1.DataMember = "LapHPJ"
        Me.XrPivotGrid1.DataSource = DsLap

        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBUser.Text = MainModule.LoginAktif

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()

    End Sub

End Class