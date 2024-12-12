namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class WindowWrapper : IWindowWrapper<Window>, ITargetWrapper<Window>
    {
        public event EventHandler Activated
        {
            add
            {
                this.Target.Activated += value;
            }
            remove
            {
                this.Target.Activated -= value;
            }
        }

        public event EventHandler Closed
        {
            add
            {
                this.Target.Closed += value;
            }
            remove
            {
                this.Target.Closed -= value;
            }
        }

        public event CancelEventHandler Closing
        {
            add
            {
                this.Target.Closing += value;
            }
            remove
            {
                this.Target.Closing -= value;
            }
        }

        public virtual void Activate()
        {
            this.Target.Activate();
        }

        public virtual void Close()
        {
            this.Target.Close();
        }

        public static MessageBoxResult ConvertDialogResult(bool? result)
        {
            bool? nullable = result;
            if (nullable != null)
            {
                bool valueOrDefault = nullable.GetValueOrDefault();
                if (!valueOrDefault)
                {
                    return MessageBoxResult.Cancel;
                }
                if (valueOrDefault)
                {
                    return MessageBoxResult.OK;
                }
            }
            return MessageBoxResult.None;
        }

        public virtual void Show()
        {
            this.Target.Show();
        }

        public virtual MessageBoxResult ShowDialog() => 
            ConvertDialogResult(WindowProxy.GetWindowSurrogate(this.Target).ShowDialog());

        public Window Target { get; set; }

        public object Content
        {
            get => 
                this.Target.Content;
            set => 
                this.Target.Content = value;
        }

        public DataTemplate ContentTemplate
        {
            get => 
                this.Target.ContentTemplate;
            set => 
                this.Target.ContentTemplate = value;
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                this.Target.ContentTemplateSelector;
            set
            {
                this.Target.ContentTemplateSelector = value;
                if (value != null)
                {
                    this.ContentTemplate = value.SelectTemplate(this.Content, this.Target);
                }
            }
        }

        public object DataContext
        {
            get => 
                this.Target.DataContext;
            set => 
                this.Target.DataContext = value;
        }
    }
}

