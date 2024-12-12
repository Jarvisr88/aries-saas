namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;

    public interface ICurrentDialogService : ICurrentWindowService
    {
        void Close(MessageResult dialogResult);
        void Close(UICommand dialogResult);

        IEnumerable<UICommand> UICommands { get; }
    }
}

