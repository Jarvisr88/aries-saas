namespace DevExpress.Utils.About
{
    using DevExpress.Logify;
    using System;
    using System.ComponentModel;

    public class UAlgoLogify : UAlgoDefault
    {
        public UAlgoLogify()
        {
            LogifyExceptionHandler.Instance.CanReportException += new CancelEventHandler(this.OnLogifyCanReportException);
        }

        internal override void DoEvent(byte kind, byte platform, string action, uint actionNumber)
        {
            try
            {
                try
                {
                    if (kind == 3)
                    {
                        LogifyState.Instance.CustomData["DXLastDemoModule"] = action;
                    }
                }
                catch
                {
                }
                base.DoEvent(kind, platform, action, actionNumber);
            }
            catch
            {
            }
        }

        public override void DoEventException(Exception e)
        {
            try
            {
                base.DoEventException(e);
                if (UAlgo.Enabled)
                {
                    LogifyExceptionHandler.Instance.Send(e);
                }
            }
            catch
            {
            }
        }

        public override void DoEventException(UnhandledExceptionEventArgs e)
        {
            base.DoEventException(e);
        }

        private void OnLogifyCanReportException(object sender, CancelEventArgs e)
        {
            e.Cancel = !UAlgo.Enabled;
        }
    }
}

