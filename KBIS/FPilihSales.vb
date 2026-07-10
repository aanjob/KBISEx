Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPilihSales
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        If Golongan = "Job Order" Then
            Gol = Golongan
        Else
            Gol = "Lokal"
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FPilihSales_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SalID,Nama From M_Sales Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SalesL")
        cmsl.Fill(DsMaster, "M_SalesL")
        DsMaster.Tables("M_SalesL").Clear()
        cmsl.Fill(DsMaster, "M_SalesL")
        DsMaster.Tables("M_SalesL").Rows.Add("%", "Semua")

        Me.SLUSales.Properties.DataSource = DsMaster.Tables("M_SalesL")
        Me.SLUSales.Properties.DisplayMember = "Nama"
        Me.SLUSales.Properties.ValueMember = "SalID"
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihSal = Me.SLUSales.EditValue
        Me.Dispose()
    End Sub
End Class