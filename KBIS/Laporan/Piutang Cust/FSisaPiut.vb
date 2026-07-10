Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FSisaPiut
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan
        Me.LBGol.Text = "    " & Golongan

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

    Private Sub FSisaPiut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter

        If Gol = "Job Order" Then
            cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='" & Gol & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
            cmsl.Fill(DsLapF, "M_SalesLUE" & Gol)
            DsLapF.Tables("M_SalesLUE" & Gol).Clear()
            cmsl.Fill(DsLapF, "M_SalesLUE" & Gol)

        Else
            cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='Lokal'", koneksi)
            cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
            cmsl.Fill(DsLapF, "M_SalesLUE" & Gol)
            DsLapF.Tables("M_SalesLUE" & Gol).Clear()
            cmsl.Fill(DsLapF, "M_SalesLUE" & Gol)
        End If

        DsLapF.Tables("M_SalesLUE" & Gol).Rows.Add("%", "", "Semua Sales")

        Me.SLUSales.Properties.DataSource = DsLapF.Tables("M_SalesLUE" & Gol)
        Me.SLUSales.Properties.DisplayMember = "Nama"
        Me.SLUSales.Properties.ValueMember = "SalID"

    End Sub

    Private Sub SLUSales_Leave(sender As Object, e As EventArgs) Handles SLUSales.Leave
        If Not IsDBNull(Me.SLUSales.EditValue) Then
            Try
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_SalCust SC On C.CustID=SC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where SC.SalID Like '" & Me.SLUSales.EditValue & "' and SC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustL" & Gol)
                Try
                    DsLapF.Tables("M_CustL" & Gol).Clear()

                Catch ex As Exception

                End Try
                cmsl.Fill(DsLapF, "M_CustL" & Gol)
                DsLapF.Tables("M_CustL" & Gol).Rows.Add("%", "Semua Customer", "", "")

                Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustL" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLSisaPiutCust", koneksi)

        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
        cmsl.SelectCommand.Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Report", SqlDbType.VarChar).Value = Me.CBOPiutang.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SisaPiut" & Gol)
        Try
            DsLapF.Tables("SisaPiut" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SisaPiut" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SisaPiut" & Gol
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Sisa Piutang" & Gol)
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class