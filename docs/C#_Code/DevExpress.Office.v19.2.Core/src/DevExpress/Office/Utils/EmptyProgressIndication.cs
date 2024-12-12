namespace DevExpress.Office.Utils
{
    using System;

    public class EmptyProgressIndication : ProgressIndication
    {
        public EmptyProgressIndication(IServiceProvider provider) : base(provider)
        {
        }

        public override void Begin(string displayName, int minProgress, int maxProgress, int currentProgress)
        {
        }

        public override void End()
        {
        }

        public override void SetProgress(int currentProgress)
        {
        }
    }
}

