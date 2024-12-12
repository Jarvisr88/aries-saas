namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Async;
    using System;

    public class AsyncRowInfo
    {
        private bool isValid;
        private Command loadingCommand;
        private object row;
        private object key;

        public AsyncRowInfo(Command loadingCommand);
        public AsyncRowInfo(object row, object key);
        public void MakeInvalid();
        public void MakeLoading(Command loading);

        public object Row { get; set; }

        public object Key { get; }

        public Command LoadingCommand { get; }

        public bool IsLoading { get; }

        public bool IsValid { get; }

        public bool IsLoaded { get; }
    }
}

