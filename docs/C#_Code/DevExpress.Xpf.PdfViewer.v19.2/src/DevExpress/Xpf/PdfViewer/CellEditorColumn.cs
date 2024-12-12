namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class CellEditorColumn : IInplaceEditorColumn, IDefaultEditorViewInfo
    {
        private readonly PdfEditorSettings editorSettings;
        private readonly PdfBehaviorProvider provider;
        private readonly PdfPageViewModel page;
        private bool isReadOnly;

        public event ColumnContentChangedEventHandler ContentChanged;

        public CellEditorColumn(PdfBehaviorProvider provider, PdfPageViewModel page, PdfEditorSettings editorSettings)
        {
            this.page = page;
            this.editorSettings = editorSettings;
            this.provider = provider;
            this.EditSettings = this.CreateEditSettings();
        }

        private System.Windows.Media.Brush CreateBorderBrush()
        {
            System.Windows.Media.Brush brush = new SolidColorBrush(this.ToWpfColor(this.editorSettings.Border.Color));
            if (this.editorSettings.Border.BorderStyle != PdfEditorBorderStyle.Dashed)
            {
                return brush;
            }
            System.Windows.Shapes.Rectangle visual = new System.Windows.Shapes.Rectangle();
            visual.Stroke = brush;
            visual.StrokeThickness = this.editorSettings.Border.BorderWidth;
            visual.StrokeDashArray = new DoubleCollection(this.editorSettings.Border.DashPattern);
            visual.Width = this.editorSettings.DocumentArea.Area.Width;
            visual.Height = this.editorSettings.DocumentArea.Area.Height;
            return new VisualBrush(visual);
        }

        private Thickness CreateBorderThickness() => 
            (this.editorSettings.Border.BorderStyle == PdfEditorBorderStyle.Underline) ? new Thickness(0.0, 0.0, 0.0, this.editorSettings.Border.BorderWidth) : new Thickness(this.editorSettings.Border.BorderWidth);

        private BaseEditSettings CreateComboBoxEditSettings(PdfComboBoxSettings ces)
        {
            IEnumerable values;
            List<string> list4;
            this.isReadOnly = ces.ReadOnly;
            ComboBoxEditSettings settings1 = new ComboBoxEditSettings();
            settings1.VerticalContentAlignment = VerticalAlignment.Center;
            ComboBoxEditSettings settings2 = settings1;
            if (!ces.Editable)
            {
                values = ces.Values;
                list4 = (List<string>) values;
            }
            else
            {
                List<string> list2;
                List<string> list3;
                IList<PdfOptionsFormFieldOption> values = ces.Values;
                if (values != null)
                {
                    list2 = values.Select<PdfOptionsFormFieldOption, string>((<>c.<>9__8_0 ??= x => x.Text)).ToList<string>();
                    list3 = list2;
                }
                else
                {
                    IList<PdfOptionsFormFieldOption> local1 = values;
                    list2 = null;
                    list3 = list2;
                }
                values = list2;
                list4 = (List<string>) values;
            }
            list4.ItemsSource = values;
            ComboBoxEditSettings local6 = settings2;
            ComboBoxEditSettings local7 = settings2;
            local7.ValueMember = ces.Editable ? string.Empty : "Text";
            ComboBoxEditSettings local4 = local7;
            ComboBoxEditSettings local5 = local7;
            local5.DisplayMember = ces.Editable ? string.Empty : "ExportText";
            ComboBoxEditSettings local3 = local5;
            local3.ValidateOnTextInput = !ces.Editable;
            local3.AutoComplete = true;
            return local3;
        }

        protected virtual BaseEditSettings CreateEditSettings()
        {
            switch (this.editorSettings.EditorType)
            {
                case PdfEditorType.TextEdit:
                    return this.CreateTextEditSettings((PdfTextEditSettings) this.editorSettings);

                case PdfEditorType.ComboBox:
                    return this.CreateComboBoxEditSettings((PdfComboBoxSettings) this.editorSettings);

                case PdfEditorType.ListBox:
                    return this.CreateListBoxEditSettings((PdfListBoxSettings) this.editorSettings);
            }
            throw new NotImplementedException();
        }

        private BaseEditSettings CreateListBoxEditSettings(PdfListBoxSettings lbs)
        {
            ListBoxEditSettings settings1 = new ListBoxEditSettings();
            settings1.VerticalContentAlignment = VerticalAlignment.Center;
            settings1.ItemsSource = lbs.Values;
            settings1.ValueMember = "Text";
            settings1.DisplayMember = "ExportText";
            settings1.SelectionMode = lbs.Multiselect ? SelectionMode.Extended : SelectionMode.Single;
            return settings1;
        }

        private BaseEditSettings CreateStickyNoteSettings(PdfToolTipSettings settings)
        {
            StickyNotesEditSettings settings1 = new StickyNotesEditSettings();
            settings1.Title = settings.Title;
            return settings1;
        }

        private BaseEditSettings CreateTextEditSettings(PdfTextEditSettings tes)
        {
            this.isReadOnly = tes.ReadOnly;
            if (this.editorSettings.UsePasswordChar)
            {
                PasswordBoxEditSettings settings1 = new PasswordBoxEditSettings();
                settings1.VerticalContentAlignment = VerticalAlignment.Center;
                settings1.HorizontalContentAlignment = this.GetHorizontalAlignment(tes.TextJustification);
                return settings1;
            }
            TextEditSettings settings2 = new TextEditSettings();
            settings2.VerticalContentAlignment = VerticalAlignment.Center;
            settings2.AcceptsReturn = tes.Multiline;
            settings2.HorizontalContentAlignment = this.GetHorizontalAlignment(tes.TextJustification);
            settings2.MaxLength = tes.MaxLen;
            return settings2;
        }

        private EditSettingsHorizontalAlignment GetHorizontalAlignment(PdfTextJustification textJustification) => 
            (textJustification != PdfTextJustification.Centered) ? ((textJustification != PdfTextJustification.LeftJustified) ? ((textJustification != PdfTextJustification.RightJustified) ? EditSettingsHorizontalAlignment.Stretch : EditSettingsHorizontalAlignment.Right) : EditSettingsHorizontalAlignment.Left) : EditSettingsHorizontalAlignment.Center;

        private void RaiseContentChanged()
        {
            this.ContentChanged.Do<ColumnContentChangedEventHandler>(x => x(this, new ColumnContentChangedEventArgs(null)));
        }

        private System.Windows.Media.Color ToWpfColor(PdfColor color)
        {
            if (color == null)
            {
                return System.Windows.Media.Color.FromArgb(0, 0, 0, 0);
            }
            PdfRGBColorData data = new PdfRGBColorData(color);
            return System.Windows.Media.Color.FromArgb(0xff, Convert.ToByte((double) (data.R * 255.0)), Convert.ToByte((double) (data.G * 255.0)), Convert.ToByte((double) (data.B * 255.0)));
        }

        private System.Windows.Media.Color ToWpfColor(System.Drawing.Color color) => 
            System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

        public bool IsReadOnly =>
            this.isReadOnly;

        public HorizontalAlignment DefaultHorizontalAlignment { get; private set; }

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;

        public BaseEditSettings EditSettings { get; private set; }

        public DataTemplateSelector EditorTemplateSelector { get; private set; }

        public ControlTemplate EditTemplate { get; private set; }

        public ControlTemplate DisplayTemplate { get; private set; }

        public PdfEditorType EditorType =>
            this.editorSettings.EditorType;

        public bool DoNotScroll
        {
            get
            {
                Func<PdfTextEditSettings, bool> evaluator = <>c.<>9__38_0;
                if (<>c.<>9__38_0 == null)
                {
                    Func<PdfTextEditSettings, bool> local1 = <>c.<>9__38_0;
                    evaluator = <>c.<>9__38_0 = x => x.DoNotScroll;
                }
                return (this.editorSettings as PdfTextEditSettings).Return<PdfTextEditSettings, bool>(evaluator, (<>c.<>9__38_1 ??= () => false));
            }
        }

        public bool IsMultiline
        {
            get
            {
                Func<PdfTextEditSettings, bool> evaluator = <>c.<>9__40_0;
                if (<>c.<>9__40_0 == null)
                {
                    Func<PdfTextEditSettings, bool> local1 = <>c.<>9__40_0;
                    evaluator = <>c.<>9__40_0 = x => x.Multiline;
                }
                return (this.editorSettings as PdfTextEditSettings).Return<PdfTextEditSettings, bool>(evaluator, (<>c.<>9__40_1 ??= () => false));
            }
        }

        public int MaxLength
        {
            get
            {
                Func<PdfTextEditSettings, int> evaluator = <>c.<>9__42_0;
                if (<>c.<>9__42_0 == null)
                {
                    Func<PdfTextEditSettings, int> local1 = <>c.<>9__42_0;
                    evaluator = <>c.<>9__42_0 = x => x.MaxLen;
                }
                return (this.editorSettings as PdfTextEditSettings).Return<PdfTextEditSettings, int>(evaluator, (<>c.<>9__42_1 ??= () => 0));
            }
        }

        internal PdfEditorSettings EditorSettings =>
            this.editorSettings;

        public System.Windows.Media.Brush Background
        {
            get
            {
                PdfRgbaColor backgroundColor = this.editorSettings.BackgroundColor;
                return ((backgroundColor != null) ? new SolidColorBrush(System.Windows.Media.Color.FromArgb(Convert.ToByte((double) (backgroundColor.A * 255.0)), Convert.ToByte((double) (backgroundColor.R * 255.0)), Convert.ToByte((double) (backgroundColor.G * 255.0)), Convert.ToByte((double) (backgroundColor.B * 255.0)))) : System.Windows.Media.Brushes.Transparent);
            }
        }

        public System.Windows.Media.Brush Foreground =>
            new SolidColorBrush(this.ToWpfColor(this.editorSettings.FontColor));

        public double FontSize =>
            (this.provider.ZoomFactor * this.page.DpiMultiplier) * this.editorSettings.FontSize;

        public System.Windows.Media.FontFamily FontFamily =>
            (this.editorSettings.FontData != null) ? new System.Windows.Media.FontFamily(this.editorSettings.FontData.FontFamily) : null;

        public System.Windows.Media.Brush BorderBrush =>
            this.CreateBorderBrush();

        public double RotateAngle =>
            this.provider.RotateAngle;

        public Thickness BorderThickness =>
            this.CreateBorderThickness();

        public System.Windows.CornerRadius CornerRadius =>
            new System.Windows.CornerRadius(this.editorSettings.Border.HorizontalRadius, this.editorSettings.Border.VerticalRadius, this.editorSettings.Border.HorizontalRadius, this.editorSettings.Border.VerticalRadius);

        public object InitialValue =>
            this.editorSettings.EditValue;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CellEditorColumn.<>c <>9 = new CellEditorColumn.<>c();
            public static Func<PdfOptionsFormFieldOption, string> <>9__8_0;
            public static Func<PdfTextEditSettings, bool> <>9__38_0;
            public static Func<bool> <>9__38_1;
            public static Func<PdfTextEditSettings, bool> <>9__40_0;
            public static Func<bool> <>9__40_1;
            public static Func<PdfTextEditSettings, int> <>9__42_0;
            public static Func<int> <>9__42_1;

            internal string <CreateComboBoxEditSettings>b__8_0(PdfOptionsFormFieldOption x) => 
                x.Text;

            internal bool <get_DoNotScroll>b__38_0(PdfTextEditSettings x) => 
                x.DoNotScroll;

            internal bool <get_DoNotScroll>b__38_1() => 
                false;

            internal bool <get_IsMultiline>b__40_0(PdfTextEditSettings x) => 
                x.Multiline;

            internal bool <get_IsMultiline>b__40_1() => 
                false;

            internal int <get_MaxLength>b__42_0(PdfTextEditSettings x) => 
                x.MaxLen;

            internal int <get_MaxLength>b__42_1() => 
                0;
        }
    }
}

