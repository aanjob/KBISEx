Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Public Class FSchProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SchPN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SchPEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SchPDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTPlotProd_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BCopy.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPlotProd_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.GridView1.OptionsBehavior.Editable = True

        Me.BCopy.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPlotProd_e.Selected = True
    End Sub

    Public Sub FillDtl(Tanggal As Date)

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SchIDD,Tanggal,stsJasa,BOMID,Brand,ArtName,Warna,Uk,TotOrder,(Select Isnull((Select Sum(Ouut) From T_HPJDtl where BOMID=T_SchProdDtl.BOMID and Uk=T_SchProdDtl.Uk and Tanggal='" & Tanggal & "'),0)) As CekHPJ From T_SchProdDtl Where Tanggal='" & Tanggal & "' Order By BOMID Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchProdDtl")
        Try
            DsMaster.Tables("T_SchProdDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchProdDtl")

        DsMaster.Tables("T_SchProdDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_SchProdDtl").Columns("Tanggal"), DsMaster.Tables("T_SchProdDtl").Columns("BOMID"), DsMaster.Tables("T_SchProdDtl").Columns("Uk")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchProdDtl"

        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = False
        Else
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True
        End If

        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekHPJ") = 0 Then
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
            Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True
        Else
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
            Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = False
        End If

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,Tanggal,InsDate,InsBy,UpdDate,UpdBy From T_SchProd Where PeriodID=" & MainModule.periodAktif & "", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchProd")
        Try
            DsMaster.Tables("T_SchProd").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchProd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SchProd"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_PlotProd")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = "DelEr"
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            Try
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub


    Private Sub FPlotProd_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Jadwal Produksi"
    End Sub
    Private Sub FPlotProd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPlotProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTPlotProd_e.Selected = True
    End Sub

    Private Sub BVTPlotProd_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPlotProd_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Jadwal Produksi"
        FillDt()
        'Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PlotProdP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Jadwal Produksi"

        DsMaster.Clear()

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
           If MainModule.SlstsPeriod() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

           Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        CekSave = True

        Indicator = "100"


        FillDtl(Me.DTPTanggal.EditValue)
        DsMaster.Tables("T_SchProdDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Jadwal Produksi"

        Indicator = "200"
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")

        FillDtl(Me.DTPTanggal.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True
        Me.BCopy.Enabled = False
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Jadwal Produksi"

        koneksi.Close()

        If MainModule.SlCekHProd(Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Hasil Produksi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Plot Tanggal : " & Me.GridView2.GetFocusedDataRow.Item("Tanggal") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim cmSP As New SqlCommand("SPDelT_SchProd")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.UserAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi

                Try
                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With


                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    Else
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()

        If Me.GridView1.RowCount <= 0 Then
            XtraMessageBox.Show("Data Kosong Tidak Bisa Disimpan !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_SchProd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_SchProdDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@Brand", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Brand")
                                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@TotOrder", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotOrder")
                                    .Parameters.Add("@Return", SqlDbType.Int)
                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                    .Connection = koneksi
                                End With

                                With koneksi
                                    .Open()
                                    cmSPDtl.ExecuteNonQuery()
                                    x = cmSPDtl.Parameters("@Return").Value
                                    .Close()
                                End With
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            Del()
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_SchProd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_SchProdDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi

                                With koneksi
                                    .Open()
                                    cmSPDel.ExecuteNonQuery()
                                    .Close()
                                End With

                            End With
                        Next

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "SchIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SchProdDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@Brand", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Brand")
                                        .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                        .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@TotOrder", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotOrder")
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        .Close()
                                    End With

                                    If x = 0 Then
                                        Me.GridView1.SetRowCellValue(i, "SchIDD", Me.GridView1.GetRowCellValue(i, "SchIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_SchProdDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@Brand", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Brand")
                                        .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                        .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@TotOrder", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotOrder")
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        .Close()
                                    End With

                                    If x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
        End Select

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Dim jml As Integer

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        If jml > 0 Then
            Me.GridView1.DeleteRow(Me.GridView1.FocusedRowHandle)
            Exit Sub
        End If

        If Me.CETambahManual.EditValue = False Then

            If CStr(dataTrans.Item("BOMID" & rw).ToString) <> "" Then

                Dim row As Integer

                Dim Reader As SqlClient.SqlDataReader
                koneksi.Close()

                Dim command As New SqlCommand("Select H.BOMID,Brand,H.ArtName,H.Warna,Uk,Tot-P.BtlOrder As TotOrder From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner Join M_Model M On H.MdlID=M.MdlID Inner Join M_Spec S On M.SpecID=S.SpecID Where stsLunas='False' and H.BOMID='" & dataTrans.Item("BOMID" & rw).ToString & "'", koneksi)

                Me.GridView1.DeleteRow(Me.GridView1.FocusedRowHandle)

                With koneksi
                    .Open()
                    Reader = command.ExecuteReader

                    If Reader.HasRows Then
                        While Reader.Read
                            If IsDBNull(Reader.Item(0)) = True Then

                            Else
                                RemoveHandler GridView1.InitNewRow, AddressOf GridView1_InitNewRow

                                Me.GridView1.AddNewRow()
                                If Me.CETambahManual.EditValue = False Then
                                    Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", False)
                                Else
                                    Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", True)

                                End If
                                Me.GridView1.SetRowCellValue(e.RowHandle, "SchIDD", Me.GridView1.RowCount * -1)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID", Reader.Item(0))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Brand", Reader.Item(1))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", Reader.Item(2))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Warna", Reader.Item(3))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", Reader.Item(4))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TotOrder", Reader.Item(5))
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", Me.GridView1.GetFocusedDataRow.Item("stsJasa"))
                                Me.GridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
                                Me.GridView1.SetRowCellValue(e.RowHandle, "CekHPJ", 0)

                                AddHandler GridView1.InitNewRow, AddressOf GridView1_InitNewRow

                            End If
                        End While
                    End If
                    rw += 1

                    Reader.Close()
                    .Close()
                End With
            End If
        Else
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True

            Me.GridView1.SetRowCellValue(e.RowHandle, "SchIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Brand", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Warna", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "TotOrder", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekHPJ", 0)
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If DsMaster.Tables("T_SchProdDtl").Rows.Count - 1 >= 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID")) Then

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = False Then
                        Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = False
                    Else
                        Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True
                    End If

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekHPJ") = 0 Then
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                        Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True
                    Else
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                        Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = False
                    End If

                End If
            End If
        End If
    End Sub
    Private Sub GridView1_RowCountChanged(sender As Object, e As EventArgs) Handles GridView1.RowCountChanged
        If DsMaster.Tables("T_SchProdDtl").Rows.Count - 1 >= 0 Then

            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID")) Then

                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = False

                Else
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("Brand").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("Warna").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("Uk").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True
                End If

                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekHPJ") = 0 Then
                    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                    Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = True
                Else
                    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    Me.GridView1.Columns("TotOrder").OptionsColumn.AllowEdit = False
                End If
            End If

        End If
    End Sub

    Dim BOMIDLama As String
    Dim BOMIDHapus As String

    Dim Hapus As Boolean
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            Me.GridView1.ActiveFilter.Clear()

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("SchIDD")

            BOMIDHapus = Me.GridView1.GetFocusedDataRow.Item("BOMID")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then

            If Indicator = 100 Then
                Dim command1 As New SqlCommand
                command1 = New SqlCommand("Select Count(*) From T_SchProd Where Tanggal='" & Me.DTPTanggal.EditValue & "'", koneksi)

                With koneksi
                    .Open()
                    jml = command1.ExecuteScalar()
                    .Close()
                End With

                If jml > 0 Then
                    XtraMessageBox.Show("Tanggal Sudah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If

            If Me.CETambahManual.EditValue = False Then
                rw = 0
                Dim frm As New FSchProd_a()
                frm.ShowDialog()

                Try
                    If Not IsDBNull(dataTrans.Item("Baris").ToString) Then
                        Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                            If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                                Me.GridView1.AddNewRow()
                            End If
                        Next
                    End If

                Catch ex As Exception

                End Try
            End If

        End If

    End Sub
    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            If Me.GridView1.GetRowCellValue(i, "BOMID") = BOMIDHapus Then

                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SchIDD")

                Me.GridView1.DeleteRow(i)

            End If
        Next
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FSchProd_d(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        If Indicator = 100 Then
            Dim command1 As New SqlCommand
            command1 = New SqlCommand("Select Count(*) From T_SchProd Where Tanggal='" & Me.DTPTanggal.EditValue & "'", koneksi)

            With koneksi
                .Open()
                jml = command1.ExecuteScalar()
                .Close()
            End With

            If jml > 0 Then
                XtraMessageBox.Show("Tanggal Sudah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Dim frm As New FSearch("Plot Prod", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Tanggal").ToString) Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SchIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                FillDtl(CDate(dataTrans.Item("Tanggal").ToString))

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FPlotProd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Uk" Or view.FocusedColumn.FieldName = "ArtName" Or view.FocusedColumn.FieldName = "Brand" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

    Private Sub GridView1_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView1.ValidateRow
        Dim View As GridView = CType(sender, GridView)

        Dim Brand As GridColumn = View.Columns("Brand")
        Dim Uk As GridColumn = View.Columns("Uk")

        If Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") <> "" And Me.GridView1.GetRowCellValue(e.RowHandle, "Brand") = "" Then
            e.Valid = False
            View.SetColumnError(Brand, "Brand Harus Diisi")
        Else
            e.Valid = True
            View.ClearColumnErrors()
        End If

        If Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") <> "" And Me.GridView1.GetRowCellValue(e.RowHandle, "Uk") = "" Then
            e.Valid = False
            View.SetColumnError(Uk, "Size Harus Diisi")
        Else
            e.Valid = True
            View.ClearColumnErrors()
        End If

    End Sub

    Private Sub GridView1_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles GridView1.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

End Class