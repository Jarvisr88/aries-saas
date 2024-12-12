namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing.Native.Dialogs;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class BrickImageEdit : BaseEdit
    {
        public static readonly DependencyProperty RendererProperty;
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty HasImageProperty;
        public static readonly DependencyPropertyKey HasImagePropertyKey;
        public static readonly DependencyProperty EmptyContentTemplateProperty;
        public static readonly DependencyProperty MenuTemplateProperty;
        public static readonly DependencyProperty MenuContainerTemplateProperty;
        public static readonly DependencyProperty OpenFileDialogServiceTemplateProperty;
        private static readonly Action<BrickImageEdit, Func<BrickImageEdit, DependencyObject>, Action<IOpenFileDialogService>> openFileDialogServiceAccessor;
        public static readonly DependencyProperty MessageBoxServiceTemplateProperty;
        private static readonly Action<BrickImageEdit, Func<BrickImageEdit, DependencyObject>, Action<IMessageBoxService>> messageBoxServiceAccessor;
        private BrickImageEditStrategy editStrategy;
        private NativeImage nativeImage;

        public event EventHandler<ImageSource> ImageLoaded;

        static BrickImageEdit()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<BrickImageEdit> registrator1 = DependencyPropertyRegistrator<BrickImageEdit>.New().Register<ControlTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, ControlTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_EmptyContentTemplate)), parameters), out EmptyContentTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BrickImageEdit> registrator2 = registrator1.Register<IPictureRenderer>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, IPictureRenderer>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_Renderer)), expressionArray2), out RendererProperty, null, delegate (BrickImageEdit d, IPictureRenderer o, IPictureRenderer n) {
                Action<IPictureRenderer> action = <>c.<>9__43_1;
                if (<>c.<>9__43_1 == null)
                {
                    Action<IPictureRenderer> local1 = <>c.<>9__43_1;
                    action = <>c.<>9__43_1 = x => x.AssignEditor(null);
                }
                o.Do<IPictureRenderer>(action);
                n.Do<IPictureRenderer>(x => x.AssignEditor(d.nativeImage));
            }, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BrickImageEdit> registrator3 = registrator2.Register<ControlTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, ControlTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_MenuTemplate)), expressionArray3), out MenuTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BrickImageEdit> registrator = registrator3.Register<ControlTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, ControlTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_MenuContainerTemplate)), expressionArray4), out MenuContainerTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<BrickImageEdit> registrator5 = registrator.RegisterServiceTemplateProperty<BrickImageEdit, DependencyObject, IOpenFileDialogService>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_OpenFileDialogServiceTemplate)), expressionArray5), out OpenFileDialogServiceTemplateProperty, out openFileDialogServiceAccessor, null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<BrickImageEdit> registrator6 = registrator5.RegisterServiceTemplateProperty<BrickImageEdit, DependencyObject, IMessageBoxService>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_MessageBoxServiceTemplate)), expressionArray6), out MessageBoxServiceTemplateProperty, out messageBoxServiceAccessor, null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<BrickImageEdit> registrator7 = registrator6.Register<ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, ImageSource>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_Source)), expressionArray7), out SourceProperty, null, delegate (BrickImageEdit d, ImageSource o, ImageSource n) {
                d.editStrategy.OnSourceChanged(o, n);
                d.HasImage = n != null;
                d.Invalidate();
            }, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BrickImageEdit), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator7.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<BrickImageEdit, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BrickImageEdit.get_HasImage)), expressionArray8), out HasImagePropertyKey, out HasImageProperty, false, frameworkOptions).OverrideDefaultStyleKey();
            CommandManager.RegisterClassCommandBinding(typeof(BrickImageEdit), new CommandBinding(ApplicationCommands.Open, (d, e) => ((BrickImageEdit) d).Load(), (d, e) => ((BrickImageEdit) d).CanLoad(e)));
        }

        private void CanLoad(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new BrickImageEditPropertyProvider(this);

        protected override BaseEditSettings CreateEditorSettings() => 
            new BrickImageSettings();

        protected override EditStrategyBase CreateEditStrategy()
        {
            BrickImageEditStrategy strategy;
            this.editStrategy = strategy = new BrickImageEditStrategy(this);
            return strategy;
        }

        protected void DoWithMessageBoxService(Action<IMessageBoxService> action)
        {
            Func<BrickImageEdit, DependencyObject> func1 = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Func<BrickImageEdit, DependencyObject> local1 = <>c.<>9__30_0;
                func1 = <>c.<>9__30_0 = x => x;
            }
            messageBoxServiceAccessor(this, func1, action);
        }

        protected void DoWithOpenFileDialogService(Action<IOpenFileDialogService> action)
        {
            openFileDialogServiceAccessor(this, x => Window.GetWindow(this), action);
        }

        internal void Invalidate()
        {
            Action<NativeImage> action = <>c.<>9__55_0;
            if (<>c.<>9__55_0 == null)
            {
                Action<NativeImage> local1 = <>c.<>9__55_0;
                action = <>c.<>9__55_0 = x => x.Invalidate();
            }
            this.nativeImage.Do<NativeImage>(action);
        }

        private void Load()
        {
            this.LoadCore();
        }

        private void LoadCore()
        {
            this.DoWithOpenFileDialogService(delegate (IOpenFileDialogService service) {
                service.AssignImageFileFilter();
                if (service.ShowDialog())
                {
                    try
                    {
                        ImageSource e = ImageSource.FromFile(service.GetFullFileName());
                        if (this.ImageLoaded == null)
                        {
                            EventHandler<ImageSource> imageLoaded = this.ImageLoaded;
                        }
                        else
                        {
                            this.ImageLoaded(this, e);
                        }
                    }
                    catch (Exception exception1)
                    {
                        this.DoWithMessageBoxService(delegate (IMessageBoxService mService) {
                            Func<Exception, IEnumerable<Exception>> getItems = <>c.<>9__53_2;
                            if (<>c.<>9__53_2 == null)
                            {
                                Func<Exception, IEnumerable<Exception>> local1 = <>c.<>9__53_2;
                                getItems = <>c.<>9__53_2 = delegate (Exception x) {
                                    Func<Exception, IEnumerable<Exception>> evaluator = <>c.<>9__53_3;
                                    if (<>c.<>9__53_3 == null)
                                    {
                                        Func<Exception, IEnumerable<Exception>> local1 = <>c.<>9__53_3;
                                        evaluator = <>c.<>9__53_3 = ex => ex.Yield<Exception>();
                                    }
                                    return x.InnerException.Return<Exception, IEnumerable<Exception>>(evaluator, <>c.<>9__53_4 ??= () => Enumerable.Empty<Exception>());
                                };
                            }
                            Exception exception = exception1.Yield<Exception>().Flatten<Exception>(getItems).Last<Exception>();
                            mService.ShowMessage(exception.Message, "Error", MessageButton.OK, MessageIcon.Error, MessageResult.OK);
                        });
                    }
                }
            });
        }

        protected override void OnEditCoreAssigned()
        {
            base.OnEditCoreAssigned();
            this.nativeImage = base.EditCore as NativeImage;
            this.nativeImage.Do<NativeImage>(delegate (NativeImage x) {
                x.Renderer = this.Renderer;
                this.Renderer.Do<IPictureRenderer>(r => r.AssignEditor(x));
            });
        }

        public Size ViewportSize =>
            this.nativeImage.RenderSize;

        public ImageSource Source
        {
            get => 
                (ImageSource) base.GetValue(SourceProperty);
            set => 
                base.SetValue(SourceProperty, value);
        }

        public bool HasImage
        {
            get => 
                (bool) base.GetValue(HasImageProperty);
            internal set => 
                base.SetValue(HasImagePropertyKey, value);
        }

        public ControlTemplate EmptyContentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyContentTemplateProperty);
            set => 
                base.SetValue(EmptyContentTemplateProperty, value);
        }

        public DataTemplate OpenFileDialogServiceTemplate
        {
            get => 
                (DataTemplate) base.GetValue(OpenFileDialogServiceTemplateProperty);
            set => 
                base.SetValue(OpenFileDialogServiceTemplateProperty, value);
        }

        public DataTemplate MessageBoxServiceTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MessageBoxServiceTemplateProperty);
            set => 
                base.SetValue(MessageBoxServiceTemplateProperty, value);
        }

        public ControlTemplate MenuTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MenuTemplateProperty);
            set => 
                base.SetValue(MenuTemplateProperty, value);
        }

        public ControlTemplate MenuContainerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MenuContainerTemplateProperty);
            set => 
                base.SetValue(MenuContainerTemplateProperty, value);
        }

        public IPictureRenderer Renderer
        {
            get => 
                (IPictureRenderer) base.GetValue(RendererProperty);
            set => 
                base.SetValue(RendererProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrickImageEdit.<>c <>9 = new BrickImageEdit.<>c();
            public static Func<BrickImageEdit, DependencyObject> <>9__30_0;
            public static Action<IPictureRenderer> <>9__43_1;
            public static Func<Exception, IEnumerable<Exception>> <>9__53_3;
            public static Func<IEnumerable<Exception>> <>9__53_4;
            public static Func<Exception, IEnumerable<Exception>> <>9__53_2;
            public static Action<NativeImage> <>9__55_0;

            internal void <.cctor>b__43_0(BrickImageEdit d, IPictureRenderer o, IPictureRenderer n)
            {
                Action<IPictureRenderer> action = <>9__43_1;
                if (<>9__43_1 == null)
                {
                    Action<IPictureRenderer> local1 = <>9__43_1;
                    action = <>9__43_1 = x => x.AssignEditor(null);
                }
                o.Do<IPictureRenderer>(action);
                n.Do<IPictureRenderer>(x => x.AssignEditor(d.nativeImage));
            }

            internal void <.cctor>b__43_1(IPictureRenderer x)
            {
                x.AssignEditor(null);
            }

            internal void <.cctor>b__43_3(BrickImageEdit d, ImageSource o, ImageSource n)
            {
                d.editStrategy.OnSourceChanged(o, n);
                d.HasImage = n != null;
                d.Invalidate();
            }

            internal void <.cctor>b__43_4(object d, ExecutedRoutedEventArgs e)
            {
                ((BrickImageEdit) d).Load();
            }

            internal void <.cctor>b__43_5(object d, CanExecuteRoutedEventArgs e)
            {
                ((BrickImageEdit) d).CanLoad(e);
            }

            internal DependencyObject <DoWithMessageBoxService>b__30_0(BrickImageEdit x) => 
                x;

            internal void <Invalidate>b__55_0(NativeImage x)
            {
                x.Invalidate();
            }

            internal IEnumerable<Exception> <LoadCore>b__53_2(Exception x)
            {
                Func<Exception, IEnumerable<Exception>> evaluator = <>9__53_3;
                if (<>9__53_3 == null)
                {
                    Func<Exception, IEnumerable<Exception>> local1 = <>9__53_3;
                    evaluator = <>9__53_3 = ex => ex.Yield<Exception>();
                }
                return x.InnerException.Return<Exception, IEnumerable<Exception>>(evaluator, (<>9__53_4 ??= () => Enumerable.Empty<Exception>()));
            }

            internal IEnumerable<Exception> <LoadCore>b__53_3(Exception ex) => 
                ex.Yield<Exception>();

            internal IEnumerable<Exception> <LoadCore>b__53_4() => 
                Enumerable.Empty<Exception>();
        }

        private class BrickImageEditPropertyProvider : ActualPropertyProvider
        {
            public BrickImageEditPropertyProvider(BaseEdit editor) : base(editor)
            {
            }

            public override bool CalcSuppressFeatures() => 
                false;

            public BrickImageEdit Editor =>
                base.Editor as BrickImageEdit;
        }

        private class BrickImageEditStrategy : EditStrategyBase
        {
            public BrickImageEditStrategy(BrickImageEdit editor) : base(editor)
            {
            }

            public virtual void OnSourceChanged(ImageSource oldValue, ImageSource newValue)
            {
                if (!base.ShouldLockUpdate)
                {
                    base.SyncWithValue(BrickImageEdit.SourceProperty, oldValue, newValue);
                }
            }

            protected override void RegisterUpdateCallbacks()
            {
                // Unresolved stack state at '0000007E'
            }

            public void SetImage(ImageSource image)
            {
                this.Editor.SetCurrentValue(BrickImageEdit.SourceProperty, image);
            }

            protected override void SyncWithValueInternal()
            {
                this.Editor.HasImage = this.Editor.Source != null;
            }

            protected BrickImageEdit Editor =>
                (BrickImageEdit) base.Editor;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly BrickImageEdit.BrickImageEditStrategy.<>c <>9 = new BrickImageEdit.BrickImageEditStrategy.<>c();
                public static PropertyCoercionHandler <>9__5_0;
                public static PropertyCoercionHandler <>9__5_1;
                public static PropertyCoercionHandler <>9__5_2;
                public static PropertyCoercionHandler <>9__5_3;

                internal object <RegisterUpdateCallbacks>b__5_0(object baseValue) => 
                    baseValue;

                internal object <RegisterUpdateCallbacks>b__5_1(object baseValue) => 
                    baseValue;

                internal object <RegisterUpdateCallbacks>b__5_2(object baseValue) => 
                    baseValue;

                internal object <RegisterUpdateCallbacks>b__5_3(object baseValue) => 
                    baseValue;
            }
        }

        private class BrickImageSettings : BaseEditSettings
        {
        }
    }
}

