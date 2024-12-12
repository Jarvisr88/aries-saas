namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class MessageBoxButtonLocalizerExtensions
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static IMessageButtonLocalizer ToMessageButtonLocalizer(this IMessageBoxButtonLocalizer localizer) => 
            new MessageBoxButtonLocalizerWrapper(localizer);

        private class MessageBoxButtonLocalizerWrapper : IMessageButtonLocalizer
        {
            private IMessageBoxButtonLocalizer localizer;

            public MessageBoxButtonLocalizerWrapper(IMessageBoxButtonLocalizer localizer)
            {
                this.localizer = localizer;
            }

            string IMessageButtonLocalizer.Localize(MessageResult button) => 
                this.localizer.Localize(button.ToMessageBoxResult());
        }
    }
}

