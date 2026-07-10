Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base

Public Class FLapHPJDet
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Tanggal As Date
    Public Sub New(Tgl As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Tanggal = Tgl

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Export Excel"

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Apakah Anda Mau Membuka File Ini?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'Private Sub ExportTo(ByVal provider As IExportProvider)
    '    Try
    '        Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
    '        Windows.Forms.Cursor.Current = Cursors.WaitCursor

    '        Me.FindForm().Refresh()
    '        Dim link As BaseExportLink = GridView1.CreateExportLink(provider)
    '        TryCast(link, GridViewExportLink).ExpandAll = False

    '        link.ExportTo(True)
    '        provider.Dispose()

    '        Windows.Forms.Cursor.Current = currentCursor
    '    Catch ex As System.IO.IOException
    '        XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
    '    End Try
    'End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String, ByVal Nama As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Nama
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

#End Region

    Private Sub FLapHPJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BOMID,ArtName,Warna,Uk,Sum(TotOrder) As TotOrder,DivID,Divisi,Urut,Sum(Ouut) as Ouut, Sum(Balance) as Balance,(Select Ket + '- '  From (Select Distinct Ket From T_HPJDtl Where BOMID=tbl.BOMID and DivID=tbl.DivID) as q order By Urut FOR XML PATH('')) As Ket From(Select BOMID,ArtName,Warna,Uk,TotOrder,DivID,Divisi,Urut,Ouut,TotOrder-Before-Ouut As Balance From(Select BOMID, ArtName,Warna,Uk,DivID,Divisi,Urut, Ouut,(Select Isnull((Select Sum(TotOrder) From T_SchProdDtl where BOMID=x.BOMID and Uk=x.Uk and Tanggal='" & Tanggal & "'),0)) As TotOrder,(Select Isnull((Select Sum(Ouut) From T_HPJ H Inner Join T_HPJDtl D On H.Tanggal=D.Tanggal and H.Jam=D.Jam and H.DivID=D.DivID Where D.BOMID=x.BOMID and D.Uk=x.Uk and H.DivID=x.DivID and H.Tanggal<'" & Tanggal & "'),0)) As Before From(Select D.BOMID,PD.ArtName, PD.Warna,D.Uk,D.DivID,Dv.Nama As Divisi,Dv.Urut,Sum(Ouut) as Ouut From T_HPJ H Inner Join T_HPJDtl D On H.Tanggal=D.Tanggal and H.Jam=D.Jam and H.DivID=D.DivID Inner Join M_DivProd Dv On H.DivID=Dv.DivID Inner Join T_SchProdDtl PD On D.SchIDD=PD.SchIDD Where H.Tanggal>='" & Tanggal & "' and H.Tanggal<='" & Tanggal & "' Group By D.BOMID,D.DivID,Dv.Nama,Dv.Urut, PD.ArtName,PD.Warna,D.Uk) as x)as y Union All Select BOMID,ArtName,Warna,Uk,(Select Isnull((Select Sum(TotOrder) From T_SchProdDtl where BOMID=z.BOMID and Uk=z.Uk and Tanggal='" & Tanggal & "'),0)) As TotOrder,DivID,Divisi,Urut, Ouut,(Select Isnull((Select Sum(TotOrder) From T_SchProdDtl where BOMID=z.BOMID and Uk=z.Uk and Tanggal='" & Tanggal & "'),0))-(Select Isnull((Select Sum(Ouut) From T_HPJ H2 Inner Join T_HPJDtl D2 On H2.Tanggal=D2.Tanggal and H2.Jam=D2.Jam and H2.DivID=D2.DivID Where D2.BOMID=z.BOMID and D2.Uk=z.Uk and H2.DivID=z.DivID and H2.Tanggal<'" & Tanggal & "'),0)) As Balance From(Select Distinct D.BOMID,ArtName,Warna,D.Uk,H.DivID,Dv.Nama As Divisi,Urut,0 As Ouut From T_HPJ H Inner Join T_HPJDtl D On H.Tanggal=D.Tanggal and H.Jam=D.Jam and H.DivID=D.DivID Inner Join M_DivProd Dv On H.DivID=Dv.DivID Inner Join T_SchProdDtl PD On D.SchIDD=PD.SchIDD Where H.DivID Not In (Select DivID From T_HPJDtl Where Tanggal>='" & Tanggal & "' and Tanggal<='" & Tanggal & "' )) as z) as tbl Group By BOMID,ArtName,Warna,Uk,TotOrder, DivID,Divisi,Urut Order By BOMID", koneksi)

        cmsl.TableMappings.Add("Table", "LapHPJD" & Tanggal)
        cmsl.Fill(DsMaster, "LapHPJD" & Tanggal)
        DsMaster.Tables("LapHPJD" & Tanggal).Clear()
        cmsl.Fill(DsMaster, "LapHPJD" & Tanggal)

        Me.PivotGridControl1.DataSource = DsMaster
        Me.PivotGridControl1.DataMember = "LapHPJD" & Tanggal
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Laporan Harian Produksi Per " & Format(System.DateTime.Now, "dd-MM-yy") & "")

        PivotGridControl1.ExportToXlsx(fileName)

    End Sub
End Class