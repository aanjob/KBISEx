<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLapHPJ
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FLapHPJ))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.BExExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.PivotGridControl1 = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField7 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField8 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField9 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.BExExcel)
        Me.LayoutControl1.Controls.Add(Me.PivotGridControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(951, 421)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'BExExcel
        '
        Me.BExExcel.Image = Global.KBIS.My.Resources.Resources.ExExcel
        Me.BExExcel.Location = New System.Drawing.Point(841, 379)
        Me.BExExcel.Name = "BExExcel"
        Me.BExExcel.Size = New System.Drawing.Size(98, 30)
        Me.BExExcel.StyleController = Me.LayoutControl1
        Me.BExExcel.TabIndex = 11
        Me.BExExcel.Text = "Export Excel"
        '
        'PivotGridControl1
        '
        Me.PivotGridControl1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5, Me.PivotGridField6, Me.PivotGridField7, Me.PivotGridField8, Me.PivotGridField9})
        Me.PivotGridControl1.Location = New System.Drawing.Point(12, 12)
        Me.PivotGridControl1.Name = "PivotGridControl1"
        Me.PivotGridControl1.Size = New System.Drawing.Size(927, 363)
        Me.PivotGridControl1.TabIndex = 4
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "ID BOM"
        Me.PivotGridField1.FieldName = "BOMID"
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.[False]
        Me.PivotGridField1.Width = 120
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField2.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 1
        Me.PivotGridField2.Caption = "Art Name"
        Me.PivotGridField2.FieldName = "ArtName"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.[False]
        Me.PivotGridField2.Width = 120
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField3.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField3.AreaIndex = 2
        Me.PivotGridField3.Caption = "Warna"
        Me.PivotGridField3.FieldName = "Warna"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.[False]
        Me.PivotGridField3.Width = 120
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField4.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField4.AreaIndex = 4
        Me.PivotGridField4.Caption = "Total Order"
        Me.PivotGridField4.FieldName = "TotOrder"
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.[False]
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField5.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Today"
        Me.PivotGridField5.CellFormat.FormatString = "{0:n1}"
        Me.PivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.FieldName = "Ouut"
        Me.PivotGridField5.GrandTotalCellFormat.FormatString = "{0:n1}"
        Me.PivotGridField5.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.Name = "PivotGridField5"
        Me.PivotGridField5.RunningTotal = True
        Me.PivotGridField5.TotalCellFormat.FormatString = "{0:n1}"
        Me.PivotGridField5.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.TotalValueFormat.FormatString = "{0:n1}"
        Me.PivotGridField5.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.ValueFormat.FormatString = "{0:n1}"
        Me.PivotGridField5.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField6.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField6.AreaIndex = 1
        Me.PivotGridField6.Caption = "Balance"
        Me.PivotGridField6.CellFormat.FormatString = "{0:n1}"
        Me.PivotGridField6.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField6.FieldName = "Balance"
        Me.PivotGridField6.GrandTotalCellFormat.FormatString = "{0:n1}"
        Me.PivotGridField6.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField6.Name = "PivotGridField6"
        Me.PivotGridField6.RunningTotal = True
        Me.PivotGridField6.TotalCellFormat.FormatString = "{0:n1}"
        Me.PivotGridField6.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField6.TotalValueFormat.FormatString = "{0:n1}"
        Me.PivotGridField6.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField6.ValueFormat.FormatString = "{0:n1}"
        Me.PivotGridField6.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'PivotGridField7
        '
        Me.PivotGridField7.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField7.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField7.AreaIndex = 0
        Me.PivotGridField7.Caption = "Urut"
        Me.PivotGridField7.FieldName = "Urut"
        Me.PivotGridField7.Name = "PivotGridField7"
        '
        'PivotGridField8
        '
        Me.PivotGridField8.Appearance.Header.Options.UseTextOptions = True
        Me.PivotGridField8.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField8.AreaIndex = 1
        Me.PivotGridField8.Caption = "Divisi"
        Me.PivotGridField8.FieldName = "Div"
        Me.PivotGridField8.Name = "PivotGridField8"
        '
        'PivotGridField9
        '
        Me.PivotGridField9.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField9.AreaIndex = 3
        Me.PivotGridField9.Caption = "Keterangan"
        Me.PivotGridField9.FieldName = "Ket"
        Me.PivotGridField9.Name = "PivotGridField9"
        Me.PivotGridField9.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem5, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(951, 421)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.PivotGridControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(931, 367)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.BExExcel
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(829, 367)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(102, 34)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(102, 34)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(102, 34)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 367)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(829, 34)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FLapHPJ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 421)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Blue"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "FLapHPJ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".: Laporan Hasil Produksi :."
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PivotGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PivotGridControl1 As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField7 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField8 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BExExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents PivotGridField9 As DevExpress.XtraPivotGrid.PivotGridField
End Class
