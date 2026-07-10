Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FListPOSPKBlmLns
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
    'Dim DsLapF As New System.Data.DataSet

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

    Private Sub ExportTo(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView1.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub

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

    Private Sub FListPOSPKBlmLns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CekAlert = False Then
            DsLap = New System.Data.DataSet

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * From (Select 'SPK' As Doc,* From(Select Cust,BOMID,POID,Unit,Tanggal,TglKirim,ArtName,Warna,(SELECT substring ((Select Distinct ', ' + Uk From (Select Distinct BOMID,Uk From T_BOMPO where BOMID=x.BOMID and Qty>0) as x where BOMID=x.BOMID FOR XML PATH('')), 3, 500)) As Size,Sum(Qty) As Qty,TglTrm,DATEDIFF(dd,TglTrm,GetDate()) As Hari,Sum(Sisa) As Sisa,Grup From(Select C.Nama As Cust,P.BOMID,H.POID,Unit,Tanggal,TglKirim,P.ArtCodeInd,B.ArtName,Warna,Sum(Qty+QtyPol) As Qty,Sum(Qty+QtyPol-P.BtlOrder-P.Upp-P.Hancur-P.Hilang-LunasMan)-(Select Isnull((Select Sum(Psg) From T_BSTBDtl Where BOMID=P.BOMID and ArtCode=P.ArtCodeInd),0)) As Sisa,(Select Max (Tanggal) From T_BSTB H1 Inner Join T_BSTBDtl D1 On H1.BSTBID=D1.BSTBID Where BOMID=P.BOMID) As TglTrm,H.Grup From T_BOM H Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner Join M_Brg B On P.ArtCodeInd=B.ArtCode Where Qty+QtyPol-P.BtlOrder-P.Upp-P.Hancur-P.Hilang > 0 Group By C.Nama,P.BOMID,H.POID,Unit, Tanggal,TglKirim,P.ArtCodeInd,B.ArtName,Warna,H.Grup) as x Group By Cust,BOMID,POID,Unit,Tanggal,TglKirim,ArtName,Warna,TglTrm,Grup Having Sum(Sisa)>0) as y Where Sisa <= ((Qty * 5) / 100) Or DateDiff(DD, Tanggal, GetDate()) > 90 Union All	Select 'PO' As Doc,C.Nama As Cust,'' As BOMID,H.POID,'' As Unit,Tanggal,TglKirim,ArtName,W.Nama As Warna,(SELECT substring ((Select Distinct ', ' + Uk From (Select Distinct POID,Uk From T_POBJJODtl where POID=H.POID) as x	where POID=H.POID FOR XML PATH('')), 3, 500)) As Size,Sum(Qty) As Qty,null As TglTrm,DATEDIFF(DD,Tanggal,GetDate()) As Hari,Sum(Qty) As Sisa,H.Grup From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Brg B On D.ArtCode = B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where D.POID+D.ArtCode Not In (Select POID+ArtCodeInd From T_BOMPO) and D.stsProd=0 and DATEDIFF(DD,Tanggal,GetDate())>30 and (H.stsBatal=0 or H.stsLunasMan=0) Group By C.Nama,H.POID,Tanggal,TglKirim,ArtName,W.Nama,H.Grup Union All Select 'PO' As Doc,C.Nama As Cust,'' As BOMID,H.POID, '' As Unit,Tanggal,TglKrmAkh As TglKirim,ArtName,W.Nama As Warna,	(SELECT substring ((Select Distinct ', ' + Uk From (Select Distinct POID,Uk From T_POBJLkDtl2 where POID=H.POID) as x where POID=H.POID FOR XML PATH('')), 3, 500)) As Size,Sum(Qty) As Qty,null As TglTrm,DATEDIFF(DD,Tanggal,GetDate()) As Hari,Sum(Qty) As Sisa,H.Grup From T_POBJLk H Inner Join T_POBJLkDtl D On H.POID=D.POID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Brg B On D.ArtCode = B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where D.POID+D.ArtCode Not In (Select POID+ArtCodeInd From T_BOMPO) and D.stsLunas=0 and DATEDIFF(DD,Tanggal,GetDate())>30 Group By C.Nama, H.POID,Tanggal,TglKrmAkh,ArtName,W.Nama, H.Grup) as z Where Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Cust", koneksi)
            cmsl.SelectCommand.CommandTimeout = 90000
            cmsl.TableMappings.Add("Table", "BOMBlmLns")

            Try
                DsLap.Tables("BOMBlmLns").Clear()
            Catch ex As Exception

            End Try

            cmsl.Fill(DsLap, "BOMBlmLns")
        End If


        Me.GridControl1.DataSource = DsLap
        Me.GridControl1.DataMember = "BOMBlmLns"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "List BOM Belum Lunas Per Tanggal " & Format(DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Hari") > 14 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                Else
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class