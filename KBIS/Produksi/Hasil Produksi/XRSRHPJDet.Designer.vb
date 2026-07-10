<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSRHPJDet
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBOut = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Tanggal = New DevExpress.XtraReports.Parameters.Parameter()
        Me.BOMID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.DivID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.LBUk = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBOut, Me.LBUk})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 46.12165!
        Me.Detail.MultiColumn.ColumnWidth = 120.0!
        Me.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBOut
        '
        Me.LBOut.Dpi = 254.0!
        Me.LBOut.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.LBOut.Name = "LBOut"
        Me.LBOut.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBOut.SizeF = New System.Drawing.SizeF(119.0625!, 45.19083!)
        Me.LBOut.Text = "Ouut"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 0.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 5.291667!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'Tanggal
        '
        Me.Tanggal.Name = "Tanggal"
        Me.Tanggal.Type = GetType(Date)
        Me.Tanggal.Visible = False
        '
        'BOMID
        '
        Me.BOMID.Name = "BOMID"
        Me.BOMID.Visible = False
        '
        'DivID
        '
        Me.DivID.Name = "DivID"
        Me.DivID.Visible = False
        '
        'LBUk
        '
        Me.LBUk.Dpi = 254.0!
        Me.LBUk.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.9308217!)
        Me.LBUk.Name = "LBUk"
        Me.LBUk.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUk.SizeF = New System.Drawing.SizeF(5.291667!, 45.19083!)
        Me.LBUk.Text = "Uk"
        Me.LBUk.Visible = False
        '
        'XRSRHPJDet
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 5)
        Me.PageHeight = 3300
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.Tanggal, Me.BOMID, Me.DivID})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents LBOut As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Tanggal As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents BOMID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents DivID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents LBUk As DevExpress.XtraReports.UI.XRLabel
End Class
