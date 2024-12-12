namespace DevExpress.Services
{
    using System;

    public interface IProgressIndicationService
    {
        void Begin(string displayName, int minProgress, int maxProgress, int currentProgress);
        void End();
        void SetProgress(int currentProgress);
    }
}

