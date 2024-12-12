namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Media;

    public abstract class ColorBase : BindableBase
    {
        private int a = 0xff;
        private System.Windows.Media.Color color;
        private DevExpress.Xpf.Editors.EditMode editMode;
        protected readonly Locker updateColorLocker = new Locker();

        public event EventHandler<ColorViewModelValueChangedEventArgs> ColorChanged;

        static ColorBase()
        {
            LookUpEditEnumItem<ColorPickerColorMode> item = new LookUpEditEnumItem<ColorPickerColorMode>();
            item.Text = EditorLocalizer.GetString(EditorStringId.RGB);
            item.Value = ColorPickerColorMode.RGB;
            List<LookUpEditEnumItem<ColorPickerColorMode>> list1 = new List<LookUpEditEnumItem<ColorPickerColorMode>>();
            list1.Add(item);
            LookUpEditEnumItem<ColorPickerColorMode> item2 = new LookUpEditEnumItem<ColorPickerColorMode>();
            item2.Text = EditorLocalizer.GetString(EditorStringId.CMYK);
            item2.Value = ColorPickerColorMode.CMYK;
            list1.Add(item2);
            LookUpEditEnumItem<ColorPickerColorMode> item3 = new LookUpEditEnumItem<ColorPickerColorMode>();
            item3.Text = EditorLocalizer.GetString(EditorStringId.HLS);
            item3.Value = ColorPickerColorMode.HLS;
            list1.Add(item3);
            LookUpEditEnumItem<ColorPickerColorMode> item4 = new LookUpEditEnumItem<ColorPickerColorMode>();
            item4.Text = EditorLocalizer.GetString(EditorStringId.HSB);
            item4.Value = ColorPickerColorMode.HSB;
            list1.Add(item4);
            ComboBoxItemsSource = list1;
        }

        protected ColorBase()
        {
        }

        protected virtual void OnColorChanged()
        {
            this.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateValue));
            if (this.ColorChanged != null)
            {
                this.ColorChanged(this, new ColorViewModelValueChangedEventArgs(this.Color));
            }
        }

        protected abstract void UpdateColor();
        protected abstract void UpdateValue();

        public int A
        {
            get => 
                this.a;
            set => 
                base.SetProperty<int>(ref this.a, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(ColorBase)), (MethodInfo) methodof(ColorBase.get_A)), new ParameterExpression[0]), () => this.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public System.Windows.Media.Color Color
        {
            get => 
                this.color;
            set => 
                base.SetProperty<System.Windows.Media.Color>(ref this.color, value, Expression.Lambda<Func<System.Windows.Media.Color>>(Expression.Property(Expression.Constant(this, typeof(ColorBase)), (MethodInfo) methodof(ColorBase.get_Color)), new ParameterExpression[0]), new Action(this.OnColorChanged));
        }

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                this.editMode;
            set => 
                base.SetProperty<DevExpress.Xpf.Editors.EditMode>(ref this.editMode, value, Expression.Lambda<Func<DevExpress.Xpf.Editors.EditMode>>(Expression.Property(Expression.Constant(this, typeof(ColorBase)), (MethodInfo) methodof(ColorBase.get_EditMode)), new ParameterExpression[0]));
        }

        public static IEnumerable<LookUpEditEnumItem<ColorPickerColorMode>> ComboBoxItemsSource { get; private set; }

        public ColorPickerColorMode ColorMode { get; protected set; }
    }
}

