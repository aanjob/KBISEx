Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid

Public Class FSales
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SalN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SalEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SalDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTSales_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.TBArea.Properties.ReadOnly = True
        Me.MAlamat.Properties.ReadOnly = True
        Me.SLUKota.Properties.ReadOnly = True
        Me.TBTelp.Properties.ReadOnly = True
        Me.TBHp.Properties.ReadOnly = True
        Me.TBEmail.Properties.ReadOnly = True
        Me.CBOGol.Properties.ReadOnly = True
        Me.CEPerwakilan.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTSales_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.TBArea.Properties.ReadOnly = False
        Me.MAlamat.Properties.ReadOnly = False
        Me.SLUKota.Properties.ReadOnly = False
        Me.TBTelp.Properties.ReadOnly = False
        Me.TBHp.Properties.ReadOnly = False
        Me.TBEmail.Properties.ReadOnly = False
        Me.CBOGol.Properties.ReadOnly = False
        Me.CEPerwakilan.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSales_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select KotaID,Nama From M_Kota", koneksi)
        cmsl.TableMappings.Add("Table", "M_KotaLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_KotaLUE")
        DsMaster.Tables("M_KotaLUE").Clear()
        cmsl.Fill(DsMaster, "M_KotaLUE")

        Me.SLUKota.Properties.DataSource = DsMaster.Tables("M_KotaLUE")
        Me.SLUKota.Properties.DisplayMember = "Nama"
        Me.SLUKota.Properties.ValueMember = "KotaID"
    End Sub

    Public Sub FillDt()
        
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SalID, Area, S.Nama, Alamat, S.KotaID, K.Nama As Kota, NoTelp, NoHp, Email, Perwakilan, Gol, Aktif, S.InsDate, S.InsBy, S.UpdDate, S.UpdBy From M_Sales S Inner Join M_Kota K On S.KotaID=K.KotaID", koneksi)

        cmsl.TableMappings.Add("Table", "M_Sales")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_Sales")
        DsMaster.Tables("M_Sales").Clear()
        cmsl.Fill(DsMaster, "M_Sales")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_Sales"
    End Sub

    Private Sub FSales_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Sales"
    End Sub

    Private Sub FSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTSales_e.Selected = True
    End Sub

    Private Sub BVTSales_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSales_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Sales"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Sales"

        DsMaster.Clear()

        OpenControl()
        LUE()
        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True


        Me.TBKode.EditValue = ""
        Me.TBArea.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.MAlamat.EditValue = ""
        Me.SLUKota.EditValue = ""
        Me.TBTelp.EditValue = ""
        Me.TBHp.EditValue = ""
        Me.TBEmail.EditValue = ""
        Me.CEPerwakilan.EditValue = False
        Me.CEAktif.EditValue = True
        Me.TBInfo.EditValue = ""

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubId, SalID, SL.GdID,L.Nama, SL.Aktif From M_SalGd SL Inner Join M_Gudang L On SL.GdID=L.GdID Where SalID='--'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SalGd")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_SalGd")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_SalGd"
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Sales"

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("SalID")
        Me.TBArea.EditValue = Me.GridView2.GetFocusedDataRow.Item("Area")
        Me.TBNama.EditValue = Me.GridView2.GetFocusedDataRow.Item("Nama")
        Me.MAlamat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Alamat")
        Me.SLUKota.EditValue = Me.GridView2.GetFocusedDataRow.Item("KotaID")
        Me.TBTelp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoTelp")
        Me.TBHp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoHp")
        Me.TBEmail.EditValue = Me.GridView2.GetFocusedDataRow.Item("Email")
        Me.CEPerwakilan.EditValue = Me.GridView2.GetFocusedDataRow.Item("Perwakilan")
        Me.CBOGol.EditValue = Me.GridView2.GetFocusedDataRow.Item("Gol")
        Me.CEAktif.EditValue = Me.GridView2.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID, SalID, SL.GdID,L.Nama, SL.Aktif From M_SalGd SL Inner Join M_Gudang L On SL.GdID=L.GdID Where SalID = '" & Me.TBKode.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SalGd")
        Try
            DsMaster.Tables("M_SalGd").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "M_SalGd")


        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_SalGd"

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Sales"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Sales : " & Me.GridView2.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Sales")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SalID")
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
                        Exit Sub
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End With
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Sales")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Area", SqlDbType.VarChar).Value = Me.TBArea.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@NoTelp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
                    .Parameters.Add("@NoHp", SqlDbType.VarChar).Value = Me.TBHp.EditValue
                    .Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TBEmail.EditValue
                    .Parameters.Add("@Wakil", SqlDbType.Bit).Value = Me.CEPerwakilan.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            Dim cmSPDtl As New SqlCommand("SPInsM_SalGd")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@SalId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "GdID")
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
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Sales")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Area", SqlDbType.VarChar).Value = Me.TBArea.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@NoTelp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
                    .Parameters.Add("@NoHp", SqlDbType.VarChar).Value = Me.TBHp.EditValue
                    .Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TBEmail.EditValue
                    .Parameters.Add("@Wakil", SqlDbType.Bit).Value = Me.CEPerwakilan.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
                    .Parameters.Add("@Aktif", SqlDbType.Bit).Value = Me.CEAktif.EditValue
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

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_SalGd")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_SalGd")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@SalId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "GdID")
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
                                    Me.GridView1.SetRowCellValue(i, "HubID", Me.GridView1.GetRowCellValue(i, "HubID") * -1)
                                End If

                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_SalGd")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "HubID")
                                    .Parameters.Add("@SalId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "GdID")
                                    .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Aktif")
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
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetRowCellValue(e.RowHandle, "HubID", Me.GridView1.RowCount * -1)
        Me.GridView1.SetRowCellValue(e.RowHandle, "SalID", Me.TBKode.EditValue)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Aktif", "True")
    End Sub

    Private Sub BEdGdID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdGdID.ButtonClick
        Dim frm As New FSearch("Gudang", "", "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Nama", dataTrans.Item("Nama").ToString)
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("HubId")
        End If
    End Sub

    Private Sub BEdGdID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdGdID.KeyPress
        e.Handled = True
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress, TBArea.KeyPress, MAlamat.KeyPress, TBTelp.KeyPress, TBHp.KeyPress, TBEmail.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class