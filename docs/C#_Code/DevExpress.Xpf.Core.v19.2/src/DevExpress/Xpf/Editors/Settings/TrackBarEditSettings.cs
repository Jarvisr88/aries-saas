namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class TrackBarEditSettings : RangeBaseEditSettings
    {
        public static readonly DependencyProperty TickPlacementProperty;
        public static readonly DependencyProperty TickFrequencyProperty;

        static TrackBarEditSettings()
        {
            Type ownerType = typeof(TrackBarEditSettings);
            TickPlacementProperty = DependencyProperty.Register("TickPlacement", typeof(System.Windows.Controls.Primitives.TickPlacement), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.Primitives.TickPlacement.BottomRight));
            TickFrequencyProperty = DependencyProperty.Register("TickFrequency", typeof(double), ownerType, new FrameworkPropertyMetadata(5.0));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            TrackBarEdit editor = edit as TrackBarEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(TickFrequencyProperty, () => editor.TickFrequency = this.TickFrequency);
                base.SetValueFromSettings(TickPlacementProperty, () => editor.TickPlacement = this.TickPlacement);
            }
        }

        [Category("Behavior")]
        public System.Windows.Controls.Primitives.TickPlacement TickPlacement
        {
            get => 
                (System.Windows.Controls.Primitives.TickPlacement) base.GetValue(TickPlacementProperty);
            set => 
                base.SetValue(TickPlacementProperty, value);
        }

        [Category("Behavior")]
        public double TickFrequency
        {
            get => 
                (double) base.GetValue(TickFrequencyProperty);
            set => 
                base.SetValue(TickFrequencyProperty, value);
        }
    }
}

