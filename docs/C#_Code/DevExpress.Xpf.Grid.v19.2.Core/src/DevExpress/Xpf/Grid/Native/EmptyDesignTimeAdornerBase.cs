namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EmptyDesignTimeAdornerBase : IDesignTimeAdornerBase
    {
        public static EmptyDesignTimeAdornerBase Instance = new EmptyDesignTimeAdornerBase();

        protected EmptyDesignTimeAdornerBase()
        {
        }

        internal EmptyDesignTimeAdornerBase(DataControlBase dataControl)
        {
            this.<DataControlModelItem>k__BackingField = this.GetDataControlModelItem(dataControl);
        }

        IModelItem IDesignTimeAdornerBase.CreateModelItem(object obj, IModelItem parent)
        {
            EditingContextBase context = parent.Context as EditingContextBase;
            return context?.CreateModelItem(obj, parent);
        }

        Type IDesignTimeAdornerBase.GetDefaultColumnType(ColumnBase column) => 
            typeof(object);

        DataViewBase IDesignTimeAdornerBase.GetDefaultView(DataControlBase dataControl) => 
            !DesignerProperties.GetIsInDesignMode(dataControl) ? null : dataControl.CreateDefaultView();

        void IDesignTimeAdornerBase.InvalidateDataSource()
        {
        }

        bool IDesignTimeAdornerBase.IsSelectGridArea(Point point) => 
            false;

        void IDesignTimeAdornerBase.OnColumnHeaderClick()
        {
        }

        void IDesignTimeAdornerBase.OnColumnMoved()
        {
        }

        void IDesignTimeAdornerBase.OnColumnResized()
        {
        }

        void IDesignTimeAdornerBase.OnColumnsLayoutChanged()
        {
        }

        void IDesignTimeAdornerBase.RemoveGeneratedColumns(DataControlBase dataControl)
        {
        }

        void IDesignTimeAdornerBase.UpdateDesignTimeInfo()
        {
        }

        void IDesignTimeAdornerBase.UpdateVisibleIndexes(DataControlBase dataControl)
        {
        }

        private IModelItem GetDataControlModelItem(DataControlBase dataControl) => 
            new RuntimeEditingContext(dataControl, null).GetRoot();

        public void SelectModelItem(IModelItem item)
        {
        }

        public void ShowDialogContent(FrameworkElement content, FrameworkElement root, Size size, FloatingContainerParameters containerParameters)
        {
        }

        bool IDesignTimeAdornerBase.ForceAllowUseColumnInFilterControl =>
            false;

        bool IDesignTimeAdornerBase.SkipColumnXamlGenerationProperties
        {
            get => 
                false;
            set
            {
            }
        }

        bool IDesignTimeAdornerBase.IsDesignTime =>
            false;

        public IModelItem DataControlModelItem { get; }
    }
}

