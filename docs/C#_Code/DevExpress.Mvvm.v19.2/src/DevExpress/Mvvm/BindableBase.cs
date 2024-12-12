namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Threading;

    [DataContract]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        private DevExpress.Mvvm.Native.PropertyManager propertyManager;

        public event PropertyChangedEventHandler PropertyChanged;

        protected BindableBase()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual DevExpress.Mvvm.Native.PropertyManager CreatePropertyManager() => 
            new DevExpress.Mvvm.Native.PropertyManager();

        protected T GetProperty<T>(Expression<Func<T>> expression) => 
            this.PropertyManager.GetProperty<T>(GetPropertyName<T>(expression));

        public static string GetPropertyName<T>(Expression<Func<T>> expression) => 
            GetPropertyNameFast(expression);

        internal static string GetPropertyNameFast(LambdaExpression expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("MemberExpression is expected in expression.Body", "expression");
            }
            MemberInfo member = body.Member;
            return (((member.MemberType != MemberTypes.Field) || ((member.Name == null) || !member.Name.StartsWith("$VB$Local_"))) ? member.Name : member.Name.Substring("$VB$Local_".Length));
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            GuardPropertyName(propertyName);
            return this.PropertyManager.GetProperty<T>(propertyName);
        }

        private static void GuardPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
        }

        protected void RaisePropertiesChanged(params string[] propertyNames)
        {
            if ((propertyNames == null) || (propertyNames.Length == 0))
            {
                this.RaisePropertyChanged(string.Empty);
            }
            else
            {
                foreach (string str in propertyNames)
                {
                    this.RaisePropertyChanged(str);
                }
            }
        }

        protected void RaisePropertiesChanged<T1, T2>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2)
        {
            this.RaisePropertyChanged<T1>(expression1);
            this.RaisePropertyChanged<T2>(expression2);
        }

        protected void RaisePropertiesChanged<T1, T2, T3>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3)
        {
            this.RaisePropertyChanged<T1>(expression1);
            this.RaisePropertyChanged<T2>(expression2);
            this.RaisePropertyChanged<T3>(expression3);
        }

        protected void RaisePropertiesChanged<T1, T2, T3, T4>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4)
        {
            this.RaisePropertyChanged<T1>(expression1);
            this.RaisePropertyChanged<T2>(expression2);
            this.RaisePropertyChanged<T3>(expression3);
            this.RaisePropertyChanged<T4>(expression4);
        }

        protected void RaisePropertiesChanged<T1, T2, T3, T4, T5>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4, Expression<Func<T5>> expression5)
        {
            this.RaisePropertyChanged<T1>(expression1);
            this.RaisePropertyChanged<T2>(expression2);
            this.RaisePropertyChanged<T3>(expression3);
            this.RaisePropertyChanged<T4>(expression4);
            this.RaisePropertyChanged<T5>(expression5);
        }

        protected void RaisePropertyChanged()
        {
            this.RaisePropertiesChanged(null);
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            this.RaisePropertyChanged(GetPropertyName<T>(expression));
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            }
            else
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetProperty<T>(Expression<Func<T>> expression, T value) => 
            this.SetProperty<T>(expression, value, (Action) null);

        protected bool SetProperty<T>(Expression<Func<T>> expression, T value, Action changedCallback)
        {
            string propertyName = GetPropertyName<T>(expression);
            return this.PropertyManager.SetProperty<T>(propertyName, value, new Action<string>(this.RaisePropertyChanged), changedCallback);
        }

        protected bool SetProperty<T>(Expression<Func<T>> expression, T value, Action<T> changedCallback)
        {
            string propertyName = GetPropertyName<T>(expression);
            return this.PropertyManager.SetProperty<T>(propertyName, value, new Action<string>(this.RaisePropertyChanged), changedCallback);
        }

        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> expression) => 
            this.SetProperty<T>(ref storage, value, expression, null);

        protected bool SetProperty<T>(ref T storage, T value, string propertyName) => 
            this.SetProperty<T>(ref storage, value, propertyName, null);

        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> expression, Action changedCallback) => 
            this.SetProperty<T>(ref storage, value, GetPropertyName<T>(expression), changedCallback);

        protected bool SetProperty<T>(ref T storage, T value, string propertyName, Action changedCallback) => 
            this.PropertyManager.SetProperty<T>(ref storage, value, propertyName, new Action<string>(this.RaisePropertyChanged), changedCallback);

        protected bool SetValue<T>(T value, [CallerMemberName] string propertyName = null) => 
            this.SetValue<T>(value, (Action) null, propertyName);

        protected bool SetValue<T>(T value, Action changedCallback, [CallerMemberName] string propertyName = null) => 
            this.PropertyManager.SetProperty<T>(propertyName, value, new Action<string>(this.RaisePropertyChanged), changedCallback);

        protected bool SetValue<T>(T value, Action<T> changedCallback, [CallerMemberName] string propertyName = null) => 
            this.PropertyManager.SetProperty<T>(propertyName, value, new Action<string>(this.RaisePropertyChanged), changedCallback);

        protected bool SetValue<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) => 
            this.SetValue<T>(ref storage, value, null, propertyName);

        protected bool SetValue<T>(ref T storage, T value, Action changedCallback, [CallerMemberName] string propertyName = null)
        {
            GuardPropertyName(propertyName);
            return this.PropertyManager.SetProperty<T>(ref storage, value, propertyName, new Action<string>(this.RaisePropertyChanged), changedCallback);
        }

        internal DevExpress.Mvvm.Native.PropertyManager PropertyManager
        {
            get
            {
                DevExpress.Mvvm.Native.PropertyManager propertyManager = this.propertyManager;
                if (this.propertyManager == null)
                {
                    DevExpress.Mvvm.Native.PropertyManager local1 = this.propertyManager;
                    propertyManager = this.propertyManager = this.CreatePropertyManager();
                }
                return propertyManager;
            }
        }
    }
}

