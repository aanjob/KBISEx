Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRSRHPJDet
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()
    End Sub

    Private Sub XRSRHPJDet_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_HPJDtl Where Tanggal<='" & CDate(Tanggal.Value) & "' and BOMID='" & CStr(BOMID.Value) & "' and DivID='" & CStr(DivID.Value) & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_HPJDtl Where Tanggal<='" & CDate(Tanggal.Value) & "' and BOMID='" & CStr(BOMID.Value) & "' and Uk Like '%x%' and DivID='" & CStr(DivID.Value) & "'", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select Uk,Sum(Ouut) As Out From T_HPJDtl where Tanggal<='" & CDate(Tanggal.Value) & "' and BOMID='" & CStr(BOMID.Value) & "' and DivID='" & CStr(DivID.Value) & "' Group By Uk Order By Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select Uk,Sum(Ouut) As Out From T_HPJDtl where Tanggal<='" & CDate(Tanggal.Value) & "' and BOMID='" & CStr(BOMID.Value) & "' and DivID='" & CStr(DivID.Value) & "' Group By Uk Order By Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select Uk,Sum(Ouut) As Out From T_HPJDtl where Tanggal<='" & CDate(Tanggal.Value) & "' and BOMID='" & CStr(BOMID.Value) & "' and DivID='" & CStr(DivID.Value) & "' Group By Uk Order By Cast(Uk as Decimal(18,2)) Asc", koneksi)
            End If
        End If



        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "HPJDet", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Out", "Out")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "HPJDet")

        Me.DataMember = "HPJDet"
        Me.DataSource = DsLap

        Me.LBUk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "HPJDet.Uk")})
        Me.LBOut.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "HPJDet.Out", "{0:#,#0.#}")})
    End Sub

End Class