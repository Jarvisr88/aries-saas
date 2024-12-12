namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public abstract class PreviewModelBase : IPreviewModel, INotifyPropertyChanged
    {
        private const double defaultZoomValue = 100.0;
        protected readonly Dictionary<PrintingSystemCommand, DelegateCommand<object>> commands = new Dictionary<PrintingSystemCommand, DelegateCommand<object>>();
        private readonly DevExpress.Xpf.Printing.InputController inputController;
        private bool isLoading;
        private bool isIncorrectPageContent;
        private double zoom = 100.0;
        private string zoomDisplayFormat = PrintingLocalizer.GetString(PrintingStringId.ZoomDisplayFormat);
        private ReadOnlyCollection<double> zoomValues;
        private ReadOnlyCollection<ZoomItemBase> zoomModes;
        private ZoomItemBase zoomMode;
        private DevExpress.Xpf.Printing.IDialogService dialogService = new NonInteractiveDialogService();
        private ICursorService cursorService;

        public event PropertyChangedEventHandler PropertyChanged;

        public PreviewModelBase()
        {
            this.CreateCommands();
            PreviewInputController controller1 = new PreviewInputController();
            controller1.Model = this;
            this.inputController = controller1;
            this.ZoomMode = this.GetInitialZoomMode(this.ZoomModes);
        }

        private bool CanZoomIn(object parameter) => 
            this.Zoom < this.ZoomValues[this.ZoomValues.Count - 1];

        private bool CanZoomOut(object parameter) => 
            this.Zoom > this.ZoomValues[0];

        private void CreateCommands()
        {
            this.commands.Add(PrintingSystemCommand.ZoomOut, DelegateCommandFactory.Create<object>(new Action<object>(this.ZoomOut), new Func<object, bool>(this.CanZoomOut), false));
            this.commands.Add(PrintingSystemCommand.ZoomIn, DelegateCommandFactory.Create<object>(new Action<object>(this.ZoomIn), new Func<object, bool>(this.CanZoomIn), false));
        }

        protected ReadOnlyCollection<ZoomItemBase> CreateZoomModes()
        {
            List<ZoomItemBase> list = new List<ZoomItemBase>();
            foreach (double num in this.ZoomValues)
            {
                list.Add(new ZoomValueItem(num));
            }
            list.Add(new ZoomSeparatorItem());
            foreach (object obj2 in typeof(ZoomFitMode).GetValues())
            {
                list.Add(new ZoomFitModeItem((ZoomFitMode) obj2));
            }
            return new ReadOnlyCollection<ZoomItemBase>(list);
        }

        private ZoomItemBase GetInitialZoomMode(IEnumerable<ZoomItemBase> zoomModes)
        {
            Func<ZoomItemBase, bool> predicate = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                Func<ZoomItemBase, bool> local1 = <>c.<>9__71_0;
                predicate = <>c.<>9__71_0 = mode => (mode is ZoomValueItem) && (((ZoomValueItem) mode).ZoomValue == 100.0);
            }
            return zoomModes.First<ZoomItemBase>(predicate);
        }

        protected abstract FrameworkElement GetPageContent();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract void HandlePreviewDoubleClick(MouseEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract void HandlePreviewMouseLeftButtonDown(MouseButtonEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract void HandlePreviewMouseLeftButtonUp(MouseButtonEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract void HandlePreviewMouseMove(MouseEventArgs e, FrameworkElement source);
        protected virtual void OnIsLoadingChanged()
        {
        }

        protected virtual void ProcessPageContent(FrameworkElement pageContent)
        {
        }

        protected void RaiseAllCommandsCanExecuteChanged()
        {
            foreach (DelegateCommand<object> command in this.commands.Values)
            {
                command.RaiseCanExecuteChanged();
            }
        }

        protected void RaiseAllPropertiesChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
        }

        protected void RaiseOperationError(Exception error, EventHandler<FaultEventArgs> errorEvent)
        {
            bool flag = true;
            if (errorEvent != null)
            {
                FaultEventArgs e = new FaultEventArgs(error);
                errorEvent(this, e);
                flag = !e.Handled;
            }
            if (flag)
            {
                this.ShowError(error.Message);
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            this.RaisePropertyChanged<T>(this.PropertyChanged, property);
        }

        protected void RaiseZoomCommandsCanExecuteChanged()
        {
            this.commands[PrintingSystemCommand.ZoomOut].RaiseCanExecuteChanged();
            this.commands[PrintingSystemCommand.ZoomIn].RaiseCanExecuteChanged();
        }

        protected void SafeCommandHandler(Action handler)
        {
            try
            {
                handler();
            }
            catch (Exception exception)
            {
                this.dialogService.ShowError(exception.Message, PrintingLocalizer.GetString(PrintingStringId.Error));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetZoom(double value)
        {
            if ((this.zoom != value) && (value > 0.0))
            {
                this.zoom = value;
                this.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_Zoom)), new ParameterExpression[0]));
                this.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_ZoomDisplayText)), new ParameterExpression[0]));
                this.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageViewWidth)), new ParameterExpression[0]));
                this.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageViewHeight)), new ParameterExpression[0]));
                this.RaiseZoomCommandsCanExecuteChanged();
            }
        }

        protected void ShowError(string message)
        {
            this.DialogService.ShowError(message, PrintingLocalizer.GetString(PrintingStringId.Error));
        }

        private void ZoomIn(object parameter)
        {
            if (!this.CanZoomIn(parameter))
            {
                throw new InvalidOperationException();
            }
            for (int i = 0; i < this.ZoomValues.Count; i++)
            {
                if (this.Zoom < this.ZoomValues[i])
                {
                    this.Zoom = this.ZoomValues[i];
                    return;
                }
            }
        }

        private void ZoomOut(object parameter)
        {
            if (!this.CanZoomOut(parameter))
            {
                throw new InvalidOperationException();
            }
            for (int i = this.ZoomValues.Count - 1; i >= 0; i--)
            {
                if (this.Zoom > this.ZoomValues[i])
                {
                    this.Zoom = this.ZoomValues[i];
                    return;
                }
            }
        }

        internal Dictionary<PrintingSystemCommand, DelegateCommand<object>> Commands =>
            this.commands;

        protected virtual ReadOnlyCollection<double> ZoomValues
        {
            get
            {
                ReadOnlyCollection<double> zoomValues = this.zoomValues;
                if (this.zoomValues == null)
                {
                    ReadOnlyCollection<double> local1 = this.zoomValues;
                    double[] list = new double[] { 10.0, 25.0, 50.0, 75.0, 100.0, 150.0, 200.0, 400.0, 800.0 };
                    zoomValues = this.zoomValues = new ReadOnlyCollection<double>(list);
                }
                return zoomValues;
            }
        }

        public FrameworkElement PageContent
        {
            get
            {
                FrameworkElement pageContent = this.GetPageContent();
                this.ProcessPageContent(pageContent);
                return pageContent;
            }
        }

        public double PageViewWidth =>
            (this.PageContent != null) ? ((this.PageContent.Width * this.Zoom) / 100.0) : 0.0;

        public double PageViewHeight =>
            (this.PageContent != null) ? ((this.PageContent.Height * this.Zoom) / 100.0) : 0.0;

        public double Zoom
        {
            get => 
                this.zoom;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Zoom");
                }
                if (this.zoom != value)
                {
                    this.SetZoom(value);
                    this.ZoomMode = new ZoomValueItem(value);
                    this.RaisePropertyChanged<ZoomItemBase>(System.Linq.Expressions.Expression.Lambda<Func<ZoomItemBase>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_ZoomMode)), new ParameterExpression[0]));
                }
            }
        }

        public abstract int PageCount { get; }

        public string ZoomDisplayFormat
        {
            get => 
                this.zoomDisplayFormat;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ZoomDisplayFormat");
                }
                if (this.zoomDisplayFormat != value)
                {
                    this.zoomDisplayFormat = value;
                    this.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_ZoomDisplayFormat)), new ParameterExpression[0]));
                    this.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_ZoomDisplayText)), new ParameterExpression[0]));
                }
            }
        }

        public string ZoomDisplayText =>
            string.Format(this.ZoomDisplayFormat, this.Zoom);

        public IEnumerable<ZoomItemBase> ZoomModes
        {
            get
            {
                ReadOnlyCollection<ZoomItemBase> zoomModes = this.zoomModes;
                if (this.zoomModes == null)
                {
                    ReadOnlyCollection<ZoomItemBase> local1 = this.zoomModes;
                    zoomModes = this.zoomModes = this.CreateZoomModes();
                }
                return zoomModes;
            }
        }

        public ZoomItemBase ZoomMode
        {
            get => 
                this.zoomMode;
            set
            {
                this.zoomMode = value;
                if (value is ZoomValueItem)
                {
                    this.SetZoom(((ZoomValueItem) value).ZoomValue);
                }
                this.RaisePropertyChanged<ZoomItemBase>(System.Linq.Expressions.Expression.Lambda<Func<ZoomItemBase>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_ZoomMode)), new ParameterExpression[0]));
            }
        }

        public abstract bool IsCreating { get; protected set; }

        public bool IsLoading
        {
            get => 
                this.isLoading;
            protected set
            {
                if (this.isLoading != value)
                {
                    this.isLoading = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_IsLoading)), new ParameterExpression[0]));
                    this.OnIsLoadingChanged();
                }
            }
        }

        public bool IsIncorrectPageContent
        {
            get => 
                this.isIncorrectPageContent;
            set
            {
                if (this.isIncorrectPageContent != value)
                {
                    this.isIncorrectPageContent = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_IsIncorrectPageContent)), new ParameterExpression[0]));
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual DevExpress.Xpf.Printing.IDialogService DialogService
        {
            get => 
                this.dialogService;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DialogService");
                }
                this.dialogService = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual ICursorService CursorService
        {
            get => 
                this.cursorService;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CursorService");
                }
                this.cursorService = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool UseSimpleScrolling { get; set; }

        public virtual DevExpress.Xpf.Printing.InputController InputController =>
            this.inputController;

        public ICommand ZoomOutCommand =>
            this.commands[PrintingSystemCommand.ZoomOut];

        public ICommand ZoomInCommand =>
            this.commands[PrintingSystemCommand.ZoomIn];

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PreviewModelBase.<>c <>9 = new PreviewModelBase.<>c();
            public static Func<ZoomItemBase, bool> <>9__71_0;

            internal bool <GetInitialZoomMode>b__71_0(ZoomItemBase mode) => 
                (mode is ZoomValueItem) && (((ZoomValueItem) mode).ZoomValue == 100.0);
        }
    }
}

