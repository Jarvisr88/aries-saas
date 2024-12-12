namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class MessageBoxEnumsConverter
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool? ToBool(this MessageResult result)
        {
            if (result != MessageResult.Cancel)
            {
                return ((result == MessageResult.No) ? ((bool?) false) : ((bool?) true));
            }
            return null;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool? ToBool(this MessageBoxResult result)
        {
            if (result != MessageBoxResult.Cancel)
            {
                return ((result == MessageBoxResult.No) ? ((bool?) false) : ((bool?) true));
            }
            return null;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxButton ToMessageBoxButton(this MessageButton button)
        {
            switch (button)
            {
                case MessageButton.OKCancel:
                    return MessageBoxButton.OKCancel;

                case MessageButton.YesNoCancel:
                    return MessageBoxButton.YesNoCancel;

                case MessageButton.YesNo:
                    return MessageBoxButton.YesNo;
            }
            return MessageBoxButton.OK;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxImage ToMessageBoxImage(this MessageIcon icon)
        {
            switch (icon)
            {
                case MessageIcon.Error:
                    return MessageBoxImage.Hand;

                case MessageIcon.Question:
                    return MessageBoxImage.Question;

                case MessageIcon.Warning:
                    return MessageBoxImage.Exclamation;

                case MessageIcon.Information:
                    return MessageBoxImage.Asterisk;
            }
            return MessageBoxImage.None;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult ToMessageBoxResult(this MessageResult result)
        {
            switch (result)
            {
                case MessageResult.OK:
                    return MessageBoxResult.OK;

                case MessageResult.Cancel:
                    return MessageBoxResult.Cancel;

                case MessageResult.Yes:
                    return MessageBoxResult.Yes;

                case MessageResult.No:
                    return MessageBoxResult.No;
            }
            return MessageBoxResult.None;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageButton ToMessageButton(this MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    return MessageButton.OKCancel;

                case MessageBoxButton.YesNoCancel:
                    return MessageButton.YesNoCancel;

                case MessageBoxButton.YesNo:
                    return MessageButton.YesNo;
            }
            return MessageButton.OK;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageIcon ToMessageIcon(this MessageBoxImage icon)
        {
            if (icon <= MessageBoxImage.Question)
            {
                if (icon == MessageBoxImage.Hand)
                {
                    return MessageIcon.Error;
                }
                if (icon == MessageBoxImage.Question)
                {
                    return MessageIcon.Question;
                }
            }
            else
            {
                if (icon == MessageBoxImage.Exclamation)
                {
                    return MessageIcon.Warning;
                }
                if (icon == MessageBoxImage.Asterisk)
                {
                    return MessageIcon.Information;
                }
            }
            return MessageIcon.None;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageResult ToMessageResult(this MessageBoxResult result)
        {
            switch (result)
            {
                case MessageBoxResult.OK:
                    return MessageResult.OK;

                case MessageBoxResult.Cancel:
                    return MessageResult.Cancel;

                case MessageBoxResult.Yes:
                    return MessageResult.Yes;

                case MessageBoxResult.No:
                    return MessageResult.No;
            }
            return MessageResult.None;
        }
    }
}

