namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class SparklineStyleSettings : BaseEditStyleSettings
    {
        public static readonly DependencyProperty HighlightMinPointProperty;
        public static readonly DependencyProperty HighlightMaxPointProperty;
        public static readonly DependencyProperty HighlightStartPointProperty;
        public static readonly DependencyProperty HighlightEndPointProperty;
        public static readonly DependencyProperty BrushProperty;
        public static readonly DependencyProperty MaxPointBrushProperty;
        public static readonly DependencyProperty MinPointBrushProperty;
        public static readonly DependencyProperty StartPointBrushProperty;
        public static readonly DependencyProperty EndPointBrushProperty;
        public static readonly DependencyProperty NegativePointBrushProperty;

        static SparklineStyleSettings()
        {
            Type ownerType = typeof(SparklineStyleSettings);
            HighlightMinPointProperty = DependencyPropertyManager.Register("HighlightMinPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightMaxPointProperty = DependencyPropertyManager.Register("HighlightMaxPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightStartPointProperty = DependencyPropertyManager.Register("HighlightStartPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightEndPointProperty = DependencyPropertyManager.Register("HighlightEndPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            BrushProperty = DependencyPropertyManager.Register("Brush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            MaxPointBrushProperty = DependencyPropertyManager.Register("MaxPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            MinPointBrushProperty = DependencyPropertyManager.Register("MinPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            StartPointBrushProperty = DependencyPropertyManager.Register("StartPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            EndPointBrushProperty = DependencyPropertyManager.Register("EndPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            NegativePointBrushProperty = DependencyPropertyManager.Register("NegativePointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
        }

        protected SparklineStyleSettings()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            SolidColorBrush brush;
            SolidColorBrush maxPointBrush;
            SolidColorBrush minPointBrush;
            SolidColorBrush startPointBrush;
            SolidColorBrush endPointBrush;
            SolidColorBrush negativePointBrush;
            base.ApplyToEdit(editor);
            ((SparklineEdit) editor).SparklineType = this.ViewType;
            SparklinePropertyProvider propertyProvider = (SparklinePropertyProvider) editor.PropertyProvider;
            propertyProvider.HighlightMinPoint = this.HighlightMinPoint;
            propertyProvider.HighlightMaxPoint = this.HighlightMaxPoint;
            propertyProvider.HighlightStartPoint = this.HighlightStartPoint;
            propertyProvider.HighlightEndPoint = this.HighlightEndPoint;
            if (this.IsPropertySet(BrushProperty))
            {
                brush = this.Brush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.Brush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                brush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.Brush = brush;
            if (this.IsPropertySet(MaxPointBrushProperty))
            {
                maxPointBrush = this.MaxPointBrush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.MaxPointBrush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                maxPointBrush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.MaxPointBrush = maxPointBrush;
            if (this.IsPropertySet(MinPointBrushProperty))
            {
                minPointBrush = this.MinPointBrush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.MinPointBrush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                minPointBrush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.MinPointBrush = minPointBrush;
            if (this.IsPropertySet(StartPointBrushProperty))
            {
                startPointBrush = this.StartPointBrush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.StartPointBrush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                startPointBrush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.StartPointBrush = startPointBrush;
            if (this.IsPropertySet(EndPointBrushProperty))
            {
                endPointBrush = this.EndPointBrush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.EndPointBrush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                endPointBrush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.EndPointBrush = endPointBrush;
            if (this.IsPropertySet(NegativePointBrushProperty))
            {
                negativePointBrush = this.NegativePointBrush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.NegativePointBrush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                negativePointBrush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.NegativePointBrush = negativePointBrush;
        }

        public bool HighlightMinPoint
        {
            get => 
                (bool) base.GetValue(HighlightMinPointProperty);
            set => 
                base.SetValue(HighlightMinPointProperty, value);
        }

        public bool HighlightMaxPoint
        {
            get => 
                (bool) base.GetValue(HighlightMaxPointProperty);
            set => 
                base.SetValue(HighlightMaxPointProperty, value);
        }

        public bool HighlightStartPoint
        {
            get => 
                (bool) base.GetValue(HighlightStartPointProperty);
            set => 
                base.SetValue(HighlightStartPointProperty, value);
        }

        public bool HighlightEndPoint
        {
            get => 
                (bool) base.GetValue(HighlightEndPointProperty);
            set => 
                base.SetValue(HighlightEndPointProperty, value);
        }

        public SolidColorBrush Brush
        {
            get => 
                (SolidColorBrush) base.GetValue(BrushProperty);
            set => 
                base.SetValue(BrushProperty, value);
        }

        public SolidColorBrush MaxPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(MaxPointBrushProperty);
            set => 
                base.SetValue(MaxPointBrushProperty, value);
        }

        public SolidColorBrush MinPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(MinPointBrushProperty);
            set => 
                base.SetValue(MinPointBrushProperty, value);
        }

        public SolidColorBrush StartPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(StartPointBrushProperty);
            set => 
                base.SetValue(StartPointBrushProperty, value);
        }

        public SolidColorBrush EndPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(EndPointBrushProperty);
            set => 
                base.SetValue(EndPointBrushProperty, value);
        }

        public SolidColorBrush NegativePointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(NegativePointBrushProperty);
            set => 
                base.SetValue(NegativePointBrushProperty, value);
        }

        protected abstract SparklineViewType ViewType { get; }
    }
}

