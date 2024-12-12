namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Settings;
    using System;

    public interface ITokenEditorBehavior
    {
        void ProcessTokenActivated(TokenStateChangedEventArgs args);
        void ProcessTokenActivating(TokenActivatingEventArgs args);
        void ProcessTokenDeactivated(TokenStateChangedEventArgs args);
        void ProcessTokensChanged(TokensChangedEventArgs args);
        void ProcessTokensChanging(TokensChangingEventArgs args);
        void ProcessTokenTextChanging(TokenTextChangingEventArgs args);
        void SetActiveToken(ButtonEdit token);

        ButtonEditSettings NewTokenEditSettings { get; }

        ButtonEditSettings TokenEditSettings { get; }
    }
}

