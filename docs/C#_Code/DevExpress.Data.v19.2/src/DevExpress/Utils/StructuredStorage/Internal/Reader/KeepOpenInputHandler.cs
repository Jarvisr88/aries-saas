namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class KeepOpenInputHandler : InputHandler
    {
        public KeepOpenInputHandler(Stream stream) : base(stream)
        {
        }

        public override void CloseStream()
        {
        }
    }
}

