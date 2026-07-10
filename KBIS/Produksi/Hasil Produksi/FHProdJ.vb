Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FHProdJ
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

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HPJN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintD.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTHPJ_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJam.Properties.ReadOnly = True
        Me.SLUDiv.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BProses.Enabled = False
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
        Me.BVBPrint.Enabled = False
        Me.BVBPrintD.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTHPJ_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJam.Properties.ReadOnly = False
        Me.SLUDiv.Properties.ReadOnly = False
        Me.GridView1.OptionsBehavior.Editable = True

        Me.BCopy.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTHPJ_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select DivID,Nama From M_DivProd Order By Urut Asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_DivProdLUE")
        cmsl.Fill(DsMaster, "M_DivProdLUE")
        DsMaster.Tables("M_DivProdLUE").Clear()
        cmsl.Fill(DsMaster, "M_DivProdLUE")

        Me.SLUDiv.Properties.DataSource = DsMaster.Tables("M_DivProdLUE")
        Me.SLUDiv.Properties.DisplayMember = "Nama"
        Me.SLUDiv.Properties.ValueMember = "DivID"

    End Sub

    Public Sub FillDtl(Tanggal As Date, DivID As String, Jam As Integer)

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_HPJDtl Where Tanggal='" & Me.DTPTanggal.EditValue & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_HPJDtl Where Tanggal='" & Me.DTPTanggal.EditValue & "' and Uk Like '%x%'", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select HPJIDD,D.Tanggal,Jam,DivID,D.SchIDD,D.BOMID,ArtName,Warna,D.Uk,Ouut,Ket From T_HPJDtl D Inner Join T_SchProdDtl PD On D.SchIDD=PD.SchIDD Where D.Tanggal='" & Tanggal & "' and DivID='" & DivID & "' and Jam=" & Jam & " Order By D.BOMID,Uk Asc", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select HPJIDD,D.Tanggal,Jam,DivID,D.SchIDD,D.BOMID,ArtName,Warna,D.Uk,Ouut,Ket From T_HPJDtl D Inner Join T_SchProdDtl PD On D.SchIDD=PD.SchIDD Where D.Tanggal='" & Tanggal & "' and DivID='" & DivID & "' and Jam=" & Jam & " Order By D.BOMID,Uk Asc", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select HPJIDD,D.Tanggal,Jam,DivID,D.SchIDD,D.BOMID,ArtName,Warna,D.Uk,Ouut,Ket From T_HPJDtl D Inner Join T_SchProdDtl PD On D.SchIDD=PD.SchIDD Where D.Tanggal='" & Tanggal & "' and DivID='" & DivID & "' and Jam=" & Jam & " Order By D.BOMID,Cast(D.Uk as Decimal(18,2)) Asc", koneksi)
            End If
        End If

        cmsl.TableMappings.Add("Table", "T_HPJDtl")
        Try
            DsMaster.Tables("T_HPJDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HPJDtl")

        DsMaster.Tables("T_HPJDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HPJDtl").Columns("Tanggal"), DsMaster.Tables("T_HPJDtl").Columns("Jam"), DsMaster.Tables("T_HPJDtl").Columns("DivID"), DsMaster.Tables("T_HPJDtl").Columns("BOMID"), DsMaster.Tables("T_HPJDtl").Columns("Uk")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HPJDtl"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,Tanggal,Jam,H.DivID,D.Nama As Divisi,InsDate,InsBy,UpdDate,UpdBy From T_HPJ H Inner Join M_DivProd D On H.DivID=D.DivID Where PeriodID=" & MainModule.periodAktif & "", koneksi)

        cmsl.TableMappings.Add("Table", "T_HPJ")
        Try
            DsMaster.Tables("T_HPJ").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HPJ")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_HPJ"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_HPJ")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
            .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
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


    Private Sub FHPJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Hasil Produksi Per Jam"
    End Sub
    Private Sub FHPJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FHPJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTHPJ_e.Selected = True
    End Sub

    Private Sub BVTHPJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTHPJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Hasil Produksi Per Jam"
        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HPJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HPJDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("HPJP"), Boolean)
        Me.BVBPrintD.Enabled = CType(TcodeCollection.Item("HPJPD"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Hasil Produksi Per Jam"

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
        LUE()
        Me.BProses.Enabled = True
        CekSave = True

        Me.CBOJam.EditValue = 0
        Me.SLUDiv.EditValue = ""
        Me.TBInfo.EditValue = ""

        Indicator = "100"


        FillDtl(Me.DTPTanggal.EditValue, Me.SLUDiv.EditValue, Me.CBOJam.EditValue)
        DsMaster.Tables("T_HPJDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Hasil Produksi Per Jam"

        LUE()

        Indicator = "200"
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.CBOJam.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jam")
        Me.SLUDiv.EditValue = Me.GridView2.GetFocusedDataRow.Item("DivID")

        FillDtl(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), Me.GridView2.GetFocusedDataRow.Item("DivID"), Me.GridView2.GetFocusedDataRow.Item("Jam"))

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJam.Properties.ReadOnly = True
        Me.SLUDiv.Properties.ReadOnly = True

        Me.BProses.Enabled = False
        CekSave = True
        Me.BCopy.Enabled = False
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Hasil Produksi Per Jam"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Hasil Produksi Tanggal : " & Me.GridView2.GetFocusedDataRow.Item("Tanggal") & " Jam : " & Me.GridView2.GetFocusedDataRow.Item("Jam") & " Divisi : " & Me.GridView2.GetFocusedDataRow.Item("Divisi") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim cmSP As New SqlCommand("SPDelT_HPJ")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = CDate(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
                .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.GridView2.GetFocusedDataRow.Item("Jam")
                .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("DivID")
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


    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim frm As New FLapHPJ(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
        frm.MdiParent = FUtama
        frm.Show()

        'Dim frm As New FLapHPJ(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
        'frm.ShowDialog()
        'frm.Close()
    End Sub

    Private Sub BVBPrintD_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintD.ItemClick
        'Dim frm As New FLapHPJDet(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
        'frm.MdiParent = FUtama
        'frm.Show()

        'Dim frm2 As New FLapHPJDet2(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
        'frm2.MdiParent = FUtama
        'frm2.Show()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")

        Dim XR As New XRLHPJDet
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.GridView1.RowCount <= 0 Then
            XtraMessageBox.Show("Data Kosong Tidak Bisa Disimpan !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100

                Dim cek As Integer
                Dim cmd As New SqlCommand("Select Count(*) From T_HPJ Where DivID='" & Me.SLUDiv.EditValue & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam=" & Me.CBOJam.EditValue & "", koneksi)

                With koneksi
                    .Open()
                    cek = cmd.ExecuteScalar()
                    .Close()
                End With

                If cek > 0 Then
                    XtraMessageBox.Show("Data Hasil Produksi Divisi : " & Me.SLUDiv.Text & " Jam : " & Me.CBOJam.EditValue & " Sudah Pernah Diinput !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If Me.CBOJam.EditValue = 0 Then
                    XtraMessageBox.Show("Jam Tidak Boleh 0 !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If Me.SLUDiv.EditValue = "" Or IsDBNull(Me.SLUDiv.EditValue) Then
                    XtraMessageBox.Show("Divisi Harus Diisi !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If


                Dim cmSP As New SqlCommand("SPInsT_HPJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_HPJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
                                    .Parameters.Add("@SchIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@Ouut", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ouut")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                Dim cmSP As New SqlCommand("SPUpT_HPJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_HPJDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                .Parameters.Add("@DivID", SqlDbType.Date).Value = Me.SLUDiv.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "HPJIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_HPJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
                                        .Parameters.Add("@SchIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Ouut", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ouut")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                                        Me.GridView1.SetRowCellValue(i, "HPJIDD", Me.GridView1.GetRowCellValue(i, "HPJIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_HPJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "HPJIDD")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
                                        .Parameters.Add("@SchIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Ouut", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ouut")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                        ElseIf x = -2 Then
                            XtraMessageBox.Show("Detail Data Ada yang Double", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Sub SLUDiv_Leave(sender As Object, e As EventArgs) Handles SLUDiv.Leave
        Dim cmd As New SqlCommand("Select Isnull(Max(Jam)+1,1) From T_HPJ Where DivID='" & Me.SLUDiv.EditValue & "' and Tanggal='" & Me.DTPTanggal.EditValue & "'", koneksi)

        With koneksi
            .Open()
            Me.CBOJam.EditValue = cmd.ExecuteScalar()
            .Close()
        End With
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Ouut") Then
            koneksi.Close()

            Dim Sisa As Decimal
            Dim command As New SqlCommand
            command = New SqlCommand("Select TotOrder-(Select Isnull((Select Sum(Ouut) From T_HPJDtl Where Tanggal<>'" & Me.DTPTanggal.EditValue & "' and DivID='" & Me.SLUDiv.EditValue & "' and BOMID='" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID") & "' and Uk='" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk") & "'),0))-(Select Isnull((Select Sum(Ouut) From T_HPJDtl Where Tanggal='" & Me.DTPTanggal.EditValue & "' and DivID='" & Me.SLUDiv.EditValue & "' and BOMID='" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID") & "' and Uk='" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk") & "' and Jam <>" & Me.CBOJam.EditValue & " ),0)) From T_SchProdDtl Where SchIDD=" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SchIDD") & " and  BOMID='" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID") & "' and Uk='" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk") & "'", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Ouut") > Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Total Order", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Ouut", Sisa)
            End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            'Dim frm As New FHPJ_d(Me.GridView2.GetFocusedDataRow.Item("HPJID"))
            'frm.ShowDialog()
            'frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        'Copy Tidak dipakai karena sdh ada Sch Prod

        Dim frm As New FSearch("HPJ", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "HPJIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                FillDtl(CDate(dataTrans.Item("Tanggal").ToString), dataTrans.Item("DivID").ToString, CInt(dataTrans.Item("Jam").ToString))

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FHPJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cek As Integer
        Dim cmd As New SqlCommand("Select Count(*) From T_HPJ Where DivID='" & Me.SLUDiv.EditValue & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam=" & Me.CBOJam.EditValue & "", koneksi)

        With koneksi
            .Open()
            cek = cmd.ExecuteScalar()
            .Close()
        End With

        If cek > 0 Then
            XtraMessageBox.Show("Data Hasil Produksi Divisi : " & Me.SLUDiv.Text & " Jam : " & Me.CBOJam.EditValue & " Sudah Pernah Diinput !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CBOJam.EditValue = 0 Then
            XtraMessageBox.Show("Jam Tidak Boleh 0 !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUDiv.EditValue = "" Or IsDBNull(Me.SLUDiv.EditValue) Then
            XtraMessageBox.Show("Divisi Harus Diisi !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_SchProdDtl Where Tanggal='" & Me.DTPTanggal.EditValue & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_SchProdDtl Where Tanggal='" & Me.DTPTanggal.EditValue & "' and Uk Like '%x%'", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY PD.BOMID)*-1 as HPJIDD,BOMID,'" & Me.DTPTanggal.EditValue & "' as Tanggal," & Me.CBOJam.EditValue & " As Jam,'" & Me.SLUDiv.EditValue & "' As DivID,SchIDD,BOMID,ArtName,Warna,Uk,0 as Ouut,'' as Ket From T_SchProdDtl PD Where Tanggal='" & Me.DTPTanggal.EditValue & "' Order By PD.BOMID,Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY PD.BOMID)*-1 as HPJIDD,BOMID,'" & Me.DTPTanggal.EditValue & "' as Tanggal," & Me.CBOJam.EditValue & " As Jam,'" & Me.SLUDiv.EditValue & "' As DivID,SchIDD,BOMID,ArtName,Warna,Uk,0 as Ouut,'' as Ket From T_SchProdDtl PD Where Tanggal='" & Me.DTPTanggal.EditValue & "' Order By PD.BOMID,Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY PD.BOMID)*-1 as HPJIDD,BOMID,'" & Me.DTPTanggal.EditValue & "' as Tanggal," & Me.CBOJam.EditValue & " As Jam,'" & Me.SLUDiv.EditValue & "' As DivID,SchIDD,BOMID,ArtName,Warna,Uk,0 as Ouut,'' as Ket From T_SchProdDtl PD Where Tanggal='" & Me.DTPTanggal.EditValue & "' Order By PD.BOMID,Cast(Uk as Decimal(18,2)) Asc", koneksi)
            End If
        End If


        cmsl.TableMappings.Add("Table", "T_HPJDtl")
        cmsl.Fill(DsMaster, "T_HPJDtl")
        DsMaster.Tables("T_HPJDtl").Clear()
        cmsl.Fill(DsMaster, "T_HPJDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HPJDtl"

        If DsMaster.Tables("T_HPJDtl").Rows.Count <= 0 Then
            XtraMessageBox.Show("Jadwal Produksi Untuk Tanggal " & Format(Me.DTPTanggal.EditValue, "dd-MM-yy") & " Belum Disetting!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

    End Sub

End Class