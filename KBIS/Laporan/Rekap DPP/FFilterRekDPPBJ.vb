Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFilterRekDPPBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Lap, Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(ByVal Report As String, ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Lap = Report
        Gol = Golongan

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Me.DTPTanggal.EditValue = System.DateTime.Now
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FFilterRekDPP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SalID,Nama From M_Sales", koneksi)
        cmsl.TableMappings.Add("Table", "M_SalesL")
        cmsl.Fill(DsLapF, "M_SalesL")
        DsLapF.Tables("M_SalesL").Clear()
        cmsl.Fill(DsLapF, "M_SalesL")

        Me.SLUSales.Properties.DataSource = DsLapF.Tables("M_SalesL")
        Me.SLUSales.Properties.DisplayMember = "Nama"
        Me.SLUSales.Properties.ValueMember = "SalID"
    End Sub

    Private Sub SLUSales_Leave(sender As Object, e As EventArgs) Handles SLUSales.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,C.CustID,C.Nama As Cust,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_SalCust SC On C.CustID=SC.CustID Where SalID='" & Me.SLUSales.EditValue & "' and SC.Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL2")
        cmsl.Fill(DsLapF, "M_CustL2")
        DsLapF.Tables("M_CustL2").Clear()
        cmsl.Fill(DsLapF, "M_CustL2")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_CustL2"
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
        If Lap = "Rekap" Then
            Dim x, i As Integer
            Dim Cust As String = ""

            x = 0
            i = 0
            For i = 0 To DsLapF.Tables("M_CustL2").Rows.Count - 1
                If DsLapF.Tables("M_CustL2").Rows(i).Item("Cek") = True Then
                    x += 1
                    If x = 1 Then
                        Cust = "'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                    Else
                        Cust &= ",'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                    End If
                End If
            Next

            Dim bind As New Collection
            bind.Add(Cust, "Cust")
            bind.Add(Me.SLUSales.EditValue, "SalID")
            bind.Add(Me.SLUSales.Text, "Sales")
            bind.Add(Me.DTPTanggal.EditValue, "Tanggal")
            MainModule.PilihAwal = Me.DTPAwal.EditValue
            MainModule.PilihAkhir = Me.DTPAkhir.EditValue

            Dim XR As New XRRekDPPBJ
            XR.InitializeData(bind)

        ElseIf Lap = "Serah Terima" Then
            Dim bind As New Collection
            bind.Add(Me.SLUSales.EditValue, "SalID")
            bind.Add(Me.SLUSales.Text, "Sales")
            bind.Add(Me.DTPTanggal.EditValue, "Tanggal")
            bind.Add(Gol, "Gol")
            MainModule.PilihAwal = Me.DTPAwal.EditValue
            MainModule.PilihAkhir = Me.DTPAkhir.EditValue

            Dim XR As New XRSTDPP
            XR.InitializeData(bind)
        End If
    End Sub

  
End Class