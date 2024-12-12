namespace DevExpress.Xpf.Editors
{
    using System;

    public interface ICalculatorViewOwner
    {
        void AddToHistory(string text);
        void AnimateButtonClick(object buttonID);
        void GetCustomErrorText(ref string errorText);
        void SetDisplayText(string value);
        void SetHasError(bool value);
        void SetMemory(decimal value);
        void SetValue(decimal value);
    }
}

