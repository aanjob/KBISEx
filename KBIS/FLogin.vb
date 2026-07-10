Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices

Public Class FLogin
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Versi As String

#Region "Volume"
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Private Const WM_APPCOMMAND As Integer = &H319

    <DllImport("user32.dll")> _
    Public Shared Function SendMessageW(ByVal hWnd As IntPtr, _
                ByVal Msg As Integer, ByVal wParam As IntPtr, _
                ByVal lParam As IntPtr) As IntPtr
    End Function
#End Region

    Private Sub FLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TBUser.Focus()
    End Sub

    Private Sub TBPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBPassword.KeyPress
        Dim AsciiCode As Integer = Asc(e.KeyChar)
        Select Case AsciiCode
            Case 13
                TcodeCollection.Clear()

                Dim Reader As SqlClient.SqlDataReader
                Dim cmd As SqlCommand
                cmd = New SqlCommand("Select UserID,[dbo].[fcDecrypt](LoginID) as LoginID,Inisial,PosisiID,Versi,NoHarga,BackDate,Alert,AlertSpec, AlertBOM,AlertPO,AlertSampR,AlertPass,AlertBSTB,AlertBOMstsPO,AlertTrmPO,AlertSJKBB,AlertBlmLns,Proses,Line,FormDef,SU From M_User Where [dbo].[fcDecrypt](LoginID)='" & Me.TBUser.Text & "' and [dbo].[fcDecrypt](Passwordd)= '" & Me.TBPassword.Text & "' and Aktif = 'True'")
                cmd.Connection = koneksi

                With koneksi
                    .Open()
                    Reader = cmd.ExecuteReader
                    If Reader.HasRows Then
                        While Reader.Read
                            MainModule.UserAktif = Reader.Item(0)
                            MainModule.LoginAktif = Reader.Item(1)
                            MainModule.InisialAktif = Reader.Item(2)
                            MainModule.Posisi = Reader.Item(3)
                            Versi = Reader.Item(4)
                            MainModule.NoHarga = Reader.Item(5)
                            MainModule.BackDate = Reader.Item(6)
                            MainModule.stsAlert = Reader.Item(7)
                            MainModule.stsAlertSpec = Reader.Item(8)
                            MainModule.stsAlertBOM = Reader.Item(9)
                            MainModule.stsAlertPO = Reader.Item(10)
                            MainModule.stsAlertSampR = Reader.Item(11)
                            MainModule.stsAlertPass = Reader.Item(12)
                            MainModule.stsAlertBSTB = Reader.Item(13)
                            MainModule.stsAlertBOMstsPO = Reader.Item(14)
                            MainModule.stsAlertTrmPO = Reader.Item(15)
                            MainModule.stsAlertSJKBB = Reader.Item(16)
                            MainModule.stsAlertBlmLns = Reader.Item(17)
                            MainModule.ProsesAktif = Reader.Item(18)
                            MainModule.LineAktif = Reader.Item(19)
                            MainModule.FormDef = Reader.Item(20)
                            MainModule.SU = Reader.Item(21)
                            MainModule.PassAktif = Me.TBPassword.EditValue

                        End While
                    Else
                        XtraMessageBox.Show("User Tidak Aktif Atau Tidak Terdatar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Reader.Close()
                        koneksi.Close()
                        Exit Sub
                    End If
                End With
                Reader.Close()
                koneksi.Close()

                If Me.TBUser.EditValue = Me.TBPassword.EditValue Then
                    XtraMessageBox.Show("Please Change Your Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                If Versi <> Version Then
                    XtraMessageBox.Show("Program Anda Belum Diupdate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'TcodeCollection.Clear()
                    FUtama.Close()
                    Exit Sub
                End If

                Me.Dispose()
        End Select
    End Sub

    Private Sub TBUser_EditValueChanged(sender As Object, e As EventArgs) Handles TBUser.EditValueChanged

    End Sub

    Private Sub TBPassword_EditValueChanged(sender As Object, e As EventArgs) Handles TBPassword.EditValueChanged

    End Sub
End Class