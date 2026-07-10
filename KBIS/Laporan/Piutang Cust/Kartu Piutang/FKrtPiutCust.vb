Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKrtPiutCust
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FKrtPiutCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,CustID,C.Nama As Cust,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL2")
        cmsl.Fill(DsLapF, "M_CustL2")
        DsLapF.Tables("M_CustL2").Clear()
        cmsl.Fill(DsLapF, "M_CustL2")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_CustL2"

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"
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
        Me.GridView1.ActiveFilter.Clear()

        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue

        Dim x, i As Integer
        Dim CustID As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("M_CustL2").Rows.Count - 1
            If DsLapF.Tables("M_CustL2").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    CustID = "'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                Else
                    CustID &= ",'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(Me.SLUMtUang.EditValue, "MtUang")
        bind.Add(CustID, "CustID")
        bind.Add(Gol, "Gol")

        Dim XR As New XRKrtPiutCust
        XR.InitializeData(bind)

    End Sub
End Class