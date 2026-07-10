Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFilterPersBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Report As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Param As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Report = Param

        If Report = "Qty" Then
            Me.LCGerak.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            Me.LCGerak.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FFilterPersBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,JnsID,Nama As Jenis From M_BBJns", koneksi)
        cmsl.TableMappings.Add("Table", "M_BBJns")
        cmsl.Fill(DsLapF, "M_BBJns")
        DsLapF.Tables("M_BBJns").Clear()
        cmsl.Fill(DsLapF, "M_BBJns")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_BBJns"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol In ('Bahan','Sparepart-Mesin')", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsLapF, "M_GudangLUE")
        DsLapF.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsLapF, "M_GudangLUE")

        Me.SLUGudang.Properties.DataSource = DsLapF.Tables("M_GudangLUE")
        Me.SLUGudang.Properties.DisplayMember = "Nama"
        Me.SLUGudang.Properties.ValueMember = "GdID"
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To GridView1.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                GridView1.SetRowCellValue(i, "Cek", 0)
                'End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To GridView1.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                GridView1.SetRowCellValue(i, "Cek", 1)
                'End If
            Next
        End If
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue
        MainModule.PilihGudang = Me.SLUGudang.Text
        MainModule.PilihGudangID = Me.SLUGudang.EditValue

        Dim x, i As Integer
        Dim JnsID As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("M_BBJns").Rows.Count - 1
            If DsLapF.Tables("M_BBJns").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    JnsID = "'" & DsLapF.Tables("M_BBJns").Rows(i).Item("JnsID") & "'"
                Else
                    JnsID &= ",'" & DsLapF.Tables("M_BBJns").Rows(i).Item("JnsID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(JnsID, "JnsID")

        If Report = "Qty" Then
            Dim XR As New XRPersBB
            XR.InitializeData(bind)
        Else
            If Me.CBOStatus.EditValue = "Semua" Then
                bind.Add("%", "stsGerak")
            Else
                bind.Add(Me.CBOStatus.Text, "stsGerak")
            End If

            Dim XR As New XRPersBBNom
            XR.InitializeData(bind)
        End If
    End Sub
End Class