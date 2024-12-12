namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public class ServerModeInconsistencyDetectedEventArgs : HandledEventArgs
    {
        private Exception _Exception;

        public ServerModeInconsistencyDetectedEventArgs();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ServerModeInconsistencyDetectedEventArgs(Exception message);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Exception Message { get; }
    }
}

