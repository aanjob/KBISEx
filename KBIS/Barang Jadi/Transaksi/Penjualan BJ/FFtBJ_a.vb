Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFtBJ_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Manual, MnlInsUpd As String
    Dim Kode, Gudang, Doc, PromoID, Paket, Metode, JnsCust, JnsPerhit, Kelipatan, BeliMin, JnsPot, Gol, MtUang As String
    Dim Tanggal As Date

    Public Sub New(ByVal JenisCust As String, Gol As String, Gd As String, Tgl As Date, DocID As String, Manuall As Boolean, Promo As String, Metod As String, Pakett As String, MataUang As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        Dim cmsl As SqlDataAdapter
        DsAddDt = New System.Data.DataSet

        Manual = Manuall
        Gudang = Gd
        Tanggal = Tgl
        Doc = DocID
        Paket = Pakett
        Metode = Metod
        JnsCust = JenisCust
        MtUang = MataUang

        If Manual = True Then
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,'' as stsHarga,0.00 As Harga,0.000 as DiscOB,0 As RpDiscL,0 As Stok,0 As Qty,0 As Selisih,Gol From M_Brg B Where B.Aktif='True' and Gol In ('" & Gol & "','Promosi')", koneksi)

            cmsl.TableMappings.Add("Table", "Harga" & Gol)
            cmsl.Fill(DsAddDt, "Harga" & Gol)

            'Me.GridColumn2.OptionsColumn.AllowEdit = True
            Me.GridColumn5.OptionsColumn.AllowEdit = True
            Me.GridColumn7.OptionsColumn.AllowEdit = True

        Else

            If Promo = "" Then
                cmsl = New SqlDataAdapter("Select * From (Select B.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,0 As RpDiscL,0 As Qty,0 As Selisih,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=B.ArtCode and GdID='" & Gudang & "' and Tanggal <='" & Tgl & "'),0)) As Stok,Gol From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True' and MtUang ='" & MtUang & "') As x Where Stok >0", koneksi)

                cmsl.TableMappings.Add("Table", "Harga" & Gol)
                cmsl.SelectCommand.CommandTimeout = 90000
                cmsl.Fill(DsAddDt, "Harga" & Gol)

            Else
                If Metode = "Extra Harga Lama" Then
                    cmsl = New SqlDataAdapter("Select * From (Select Distinct D.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,0 As RpDiscL,Harga-(Select Isnull(((Select Top 1 Harga From M_BrgHarga Where Jenis='" & JnsCust & "' and Gol='" & Gol & "' and MtUang ='" & MtUang & "' and ArtCode=D.ArtCode and Tanggal <H.Tanggal Order By Tanggal desc)),Harga)) As Selisih,0 As Qty,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=B.ArtCode and GdID='" & Gudang & "' and Tanggal <='" & Tgl & "'),0)) As Stok,Gol From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and PromoID='" & Promo & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True' and MtUang ='" & MtUang & "') As x Where Stok >0", koneksi)

                    cmsl.TableMappings.Add("Table", "Harga" & Gol)
                    cmsl.Fill(DsAddDt, "Harga" & Gol)

                Else
                    cmsl = New SqlDataAdapter("Select * From (Select Distinct D.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,0 As RpDiscL,0 As Qty,0 As Selisih,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=B.ArtCode and GdID='" & Gudang & "' and Tanggal <='" & Tgl & "'),0)) As Stok,Gol From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and Gol In ('" & Gol & "','Promosi') and PromoID='" & Promo & "' and Paket='" & Paket & "' and B.Aktif='True' and H.Aktif='True' and MtUang ='" & MtUang & "') As x Where Stok >0", koneksi)

                    cmsl.TableMappings.Add("Table", "Harga" & Gol)
                    cmsl.Fill(DsAddDt, "Harga" & Gol)
                End If

                If Metode = "Gratis Barang" Then
                    Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    cmsl = New SqlDataAdapter("Select * From (Select Distinct D.ArtCode,ArtName,B.SatID,B.Isi,0 As Qty,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=B.ArtCode and GdID='" & Gudang & "' and Tanggal <='" & Tgl & "'),0)) As Stok,Gol From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where PromoID='" & Promo & "' and Paket='" & Paket & "' and B.Aktif='True') As x Where Stok >0", koneksi)

                    cmsl.TableMappings.Add("Table", "T_PromoFreeJ" & Gol)
                    cmsl.Fill(DsAddDt, "T_PromoFreeJ" & Gol)

                    Me.GridControl2.DataSource = DsAddDt
                    Me.GridControl2.DataMember = "T_PromoFreeJ" & Gol
                Else
                    Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If

            'Me.GridColumn2.OptionsColumn.AllowEdit = False
            Me.GridColumn5.OptionsColumn.AllowEdit = False
            Me.GridColumn7.OptionsColumn.AllowEdit = False
        End If

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "Harga" & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        'DtTrans = New System.Data.DataTable
        'DtTrans.Columns.Add("ArtCode")
        'DtTrans.Columns.Add("ArtName")
        'DtTrans.Columns.Add("SatID")
        'DtTrans.Columns.Add("Isi")
        'DtTrans.Columns.Add("stsHarga")
        'DtTrans.Columns.Add("Harga")
        'DtTrans.Columns.Add("DiscOB")
        'DtTrans.Columns.Add("Qty")

        Me.GridView1.ActiveFilter.Clear()
        Me.GridView2.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                'DtTrans.Rows.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), Me.GridView1.GetRowCellValue(i, "ArtName"), Me.GridView1.GetRowCellValue(i, "SatID"), Me.GridView1.GetRowCellValue(i, "Isi"), Me.GridView1.GetRowCellValue(i, "stsHarga"), Me.GridView1.GetRowCellValue(i, "Harga"), Me.GridView1.GetRowCellValue(i, "DiscOB"), Me.GridView1.GetRowCellValue(i, "Qty"))
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "stsHarga"), "stsHarga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Selisih"), "Selisih" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Harga"), "Harga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscOB"), "DiscOB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "RpDiscL"), "RpDiscL" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Gol"), "Gol" & x)
                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        dataTrans2 = New Collection
        dataTrans2.Clear()

        Dim y As Integer = 0
        Dim z : For z = 0 To GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(z, "Qty") > 0 Then
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtCode"), "ArtCode" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtName"), "ArtName" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "SatID"), "SatID" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Isi"), "Isi" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Qty"), "Qty" & y)
                y += 1
            End If
        Next
        dataTrans2.Add(y, "Baris")

        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then Me.Dispose()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then

            Dim Stok As Integer
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Gudang & "','" & Tanggal & "','" & Doc & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Manual = True Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Stok", Stok)
            End If

            If Manual = False Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If
            End If
        End If

    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        Dim Stok As Integer
        Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Gudang & "','" & Tanggal & "','" & Doc & "')", koneksi)

        With koneksi
            .Open()
            Stok = command.ExecuteScalar()
            .Close()
        End With

        If Manual = True Then
            Me.GridView2.SetRowCellValue(e.RowHandle, "Stok", Stok)
        End If

        If e.Column Is GridView2.Columns("Qty") Then
            If Manual = False Then
                If Me.GridView2.GetRowCellValue(e.RowHandle, "Qty") > Me.GridView2.GetRowCellValue(e.RowHandle, "Stok") Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView2.SetRowCellValue(e.RowHandle, "Qty", Me.GridView2.GetRowCellValue(e.RowHandle, "Stok"))
                End If
            End If
        End If

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            dataTrans2 = New Collection
            dataTrans2.Clear()

            Timer1.Enabled = True
        End If
    End Sub
End Class