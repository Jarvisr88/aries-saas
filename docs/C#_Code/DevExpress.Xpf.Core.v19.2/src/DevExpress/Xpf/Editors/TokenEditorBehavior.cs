namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class TokenEditorBehavior : Behavior<LookUpEditBase>, ITokenEditorBehavior
    {
        public static readonly DependencyProperty NewTokenEditSettingsProperty;
        public static readonly DependencyProperty TokenEditSettingsProperty;
        public static readonly DependencyProperty ActiveTokenProperty;
        private static readonly DependencyPropertyKey ActiveTokenPropertyKey;

        public event EventHandler<TokenStateChangedEventArgs> TokenActivated;

        public event EventHandler<TokenActivatingEventArgs> TokenActivating;

        public event EventHandler<TokenStateChangedEventArgs> TokenDeactivated;

        public event EventHandler<TokensChangedEventArgs> TokensChanged;

        public event EventHandler<TokensChangingEventArgs> TokensChanging;

        public event EventHandler<TokenTextChangingEventArgs> TokenTextChanging;

        static TokenEditorBehavior()
        {
            Type ownerType = typeof(TokenEditorBehavior);
            NewTokenEditSettingsProperty = DependencyProperty.Register("NewTokenEditSettings", typeof(ButtonEditSettings), ownerType);
            TokenEditSettingsProperty = DependencyProperty.Register("TokenEditSettings", typeof(ButtonEditSettings), ownerType);
            ActiveTokenPropertyKey = DependencyProperty.RegisterReadOnly("ActiveToken", typeof(ButtonEdit), ownerType, new FrameworkPropertyMetadata(null));
            ActiveTokenProperty = ActiveTokenPropertyKey.DependencyProperty;
        }

        public virtual void ActivateToken(int index)
        {
            this.TokenEditor.ActivateToken(index);
        }

        public virtual void CancelActiveToken()
        {
            this.TokenEditor.CancelActiveToken();
        }

        void ITokenEditorBehavior.SetActiveToken(ButtonEdit token)
        {
            this.SetActiveToken(token);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            ((LookUpEditBasePropertyProvider) base.AssociatedObject.PropertyProvider).TokenEditorBehavior = this;
        }

        protected override void OnDetaching()
        {
            ((LookUpEditBasePropertyProvider) base.AssociatedObject.PropertyProvider).TokenEditorBehavior = null;
            base.OnDetaching();
        }

        public virtual void PostActiveToken()
        {
            this.TokenEditor.CommitActiveEditor();
        }

        public virtual void ProcessTokenActivated(TokenStateChangedEventArgs args)
        {
            this.RaiseTokenActivated(args);
        }

        public virtual void ProcessTokenActivating(TokenActivatingEventArgs args)
        {
            this.RaiseTokenActivating(args);
        }

        public virtual void ProcessTokenDeactivated(TokenStateChangedEventArgs args)
        {
            this.RaiseTokenDeactivated(args);
        }

        public virtual void ProcessTokensChanged(TokensChangedEventArgs args)
        {
            this.RaiseTokensChanged(args);
        }

        public virtual void ProcessTokensChanging(TokensChangingEventArgs args)
        {
            this.RaiseTokensChanging(args);
        }

        public virtual void ProcessTokenTextChanging(TokenTextChangingEventArgs args)
        {
            this.RaiseTokenTextChanging(args);
        }

        private void RaiseTokenActivated(TokenStateChangedEventArgs args)
        {
            if (this.TokenActivated != null)
            {
                this.TokenActivated(this, args);
            }
        }

        private void RaiseTokenActivating(TokenActivatingEventArgs args)
        {
            if (this.TokenActivating != null)
            {
                this.TokenActivating(this, args);
            }
        }

        private void RaiseTokenDeactivated(TokenStateChangedEventArgs args)
        {
            if (this.TokenDeactivated != null)
            {
                this.TokenDeactivated(this, args);
            }
        }

        private void RaiseTokensChanged(TokensChangedEventArgs args)
        {
            if (this.TokensChanged != null)
            {
                this.TokensChanged(this, args);
            }
        }

        private void RaiseTokensChanging(TokensChangingEventArgs args)
        {
            if (this.TokensChanging != null)
            {
                this.TokensChanging(this, args);
            }
        }

        private void RaiseTokenTextChanging(TokenTextChangingEventArgs args)
        {
            if (this.TokenTextChanging != null)
            {
                this.TokenTextChanging(this, args);
            }
        }

        protected virtual void SetActiveToken(ButtonEdit token)
        {
            this.ActiveToken = token;
        }

        public ButtonEditSettings NewTokenEditSettings
        {
            get => 
                (ButtonEditSettings) base.GetValue(NewTokenEditSettingsProperty);
            set => 
                base.SetValue(NewTokenEditSettingsProperty, value);
        }

        public ButtonEditSettings TokenEditSettings
        {
            get => 
                (ButtonEditSettings) base.GetValue(TokenEditSettingsProperty);
            set => 
                base.SetValue(TokenEditSettingsProperty, value);
        }

        public ButtonEdit ActiveToken
        {
            get => 
                (ButtonEdit) base.GetValue(ActiveTokenProperty);
            private set => 
                base.SetValue(ActiveTokenPropertyKey, value);
        }

        public DevExpress.Xpf.Editors.Internal.TokenEditor TokenEditor =>
            base.AssociatedObject.EditCore as DevExpress.Xpf.Editors.Internal.TokenEditor;
    }
}

