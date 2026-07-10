<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRLapHPJ
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
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid()
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.pivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.pivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.pivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.pivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.pivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.pivotGridField6 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPeriod = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBPeriod, Me.XrLabel4, Me.XrPivotGrid1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 445.263!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.Appearance.Cell.Font = New System.Drawing.Font("Agency FB", 7.75!)
        Me.XrPivotGrid1.Appearance.FieldHeader.Font = New System.Drawing.Font("Agency FB", 7.75!)
        Me.XrPivotGrid1.Appearance.FieldValue.Font = New System.Drawing.Font("Agency FB", 7.75!)
        Me.XrPivotGrid1.Dpi = 254.0!
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.XrPivotGridField4, Me.XrPivotGridField5, Me.pivotGridField1, Me.pivotGridField2, Me.pivotGridField3, Me.pivotGridField4, Me.pivotGridField6, Me.pivotGridField5})
        Me.XrPivotGrid1.LocationFloat = New DevExpress.Utils.PointFloat(60.97092!, 156.1042!)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.OptionsDataField.RowValueLineCount = 3
        Me.XrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3
        Me.XrPivotGrid1.SizeF = New System.Drawing.SizeF(1419.225!, 271.3333!)
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField4.AreaIndex = 0
        Me.XrPivotGridField4.FieldName = "BOMID"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        Me.XrPivotGridField4.RowValueLineCount = 2
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField5.AreaIndex = 1
        Me.XrPivotGridField5.FieldName = "ArtName"
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        Me.XrPivotGridField5.RowValueLineCount = 2
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(58.04914!, 10.99999!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 50.4825!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(245.0491!, 10.99999!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 50.4825!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 35.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 55.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.LBUser})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 61.48249!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        '
        'pivotGridField1
        '
        Me.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.pivotGridField1.AreaIndex = 2
        Me.pivotGridField1.FieldName = "Warna"
        Me.pivotGridField1.Name = "pivotGridField1"
        Me.pivotGridField1.Width = 80
        '
        'pivotGridField2
        '
        Me.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.pivotGridField2.AreaIndex = 3
        Me.pivotGridField2.FieldName = "TotOrder"
        Me.pivotGridField2.Name = "pivotGridField2"
        Me.pivotGridField2.Width = 50
        '
        'pivotGridField3
        '
        Me.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.pivotGridField3.AreaIndex = 0
        Me.pivotGridField3.CellFormat.FormatString = "{0:n1}"
        Me.pivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField3.FieldName = "Ouut"
        Me.pivotGridField3.GrandTotalCellFormat.FormatString = "{0:n1}"
        Me.pivotGridField3.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField3.Name = "pivotGridField3"
        Me.pivotGridField3.RowValueLineCount = 3
        Me.pivotGridField3.TotalCellFormat.FormatString = "{0:n1}"
        Me.pivotGridField3.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField3.TotalValueFormat.FormatString = "{0:n1}"
        Me.pivotGridField3.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField3.ValueFormat.FormatString = "{0:n1}"
        Me.pivotGridField3.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField3.Width = 50
        '
        'pivotGridField4
        '
        Me.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.pivotGridField4.AreaIndex = 1
        Me.pivotGridField4.CellFormat.FormatString = "{0:n1}"
        Me.pivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField4.FieldName = "Balance"
        Me.pivotGridField4.GrandTotalCellFormat.FormatString = "{0:n1}"
        Me.pivotGridField4.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField4.Name = "pivotGridField4"
        Me.pivotGridField4.RowValueLineCount = 3
        Me.pivotGridField4.TotalCellFormat.FormatString = "{0:n1}"
        Me.pivotGridField4.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField4.TotalValueFormat.FormatString = "{0:n1}"
        Me.pivotGridField4.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField4.ValueFormat.FormatString = "{0:n1}"
        Me.pivotGridField4.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pivotGridField4.Width = 50
        '
        'pivotGridField5
        '
        Me.pivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.pivotGridField5.AreaIndex = 1
        Me.pivotGridField5.FieldName = "Divisi"
        Me.pivotGridField5.Name = "pivotGridField5"
        Me.pivotGridField5.Width = 50
        '
        'pivotGridField6
        '
        Me.pivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.pivotGridField6.AreaIndex = 0
        Me.pivotGridField6.FieldName = "Urut"
        Me.pivotGridField6.Name = "pivotGridField6"
        Me.pivotGridField6.Width = 50
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(44.24166!, 0.0!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(2655.702!, 58.42!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "Daily Production Report"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBPeriod
        '
        Me.LBPeriod.Dpi = 254.0!
        Me.LBPeriod.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBPeriod.LocationFloat = New DevExpress.Utils.PointFloat(44.24175!, 58.42002!)
        Me.LBPeriod.Name = "LBPeriod"
        Me.LBPeriod.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPeriod.SizeF = New System.Drawing.SizeF(2655.702!, 58.42!)
        Me.LBPeriod.StylePriority.UseFont = False
        Me.LBPeriod.StylePriority.UseTextAlignment = False
        Me.LBPeriod.Text = "Periode"
        Me.LBPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XRLapHPJ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportFooter})
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 35, 55)
        Me.PageHeight = 2159
        Me.PageWidth = 2760
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents pivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents pivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents pivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents pivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents pivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents pivotGridField6 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents LBPeriod As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
End Class
