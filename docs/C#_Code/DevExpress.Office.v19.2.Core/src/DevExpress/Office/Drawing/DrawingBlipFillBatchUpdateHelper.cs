namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class DrawingBlipFillBatchUpdateHelper : MultiIndexBatchUpdateHelper
    {
        private DrawingBlipFillInfo blipFillInfo;
        private DrawingBlipTileInfo blipTileInfo;
        private int suppressDirectNotificationsCount;

        public DrawingBlipFillBatchUpdateHelper(IBatchUpdateHandler handler) : base(handler)
        {
        }

        public void ResumeDirectNotifications()
        {
            this.suppressDirectNotificationsCount--;
        }

        public void SuppressDirectNotifications()
        {
            this.suppressDirectNotificationsCount++;
        }

        public DrawingBlipFillInfo BlipFillInfo
        {
            get => 
                this.blipFillInfo;
            set => 
                this.blipFillInfo = value;
        }

        public DrawingBlipTileInfo BlipTileInfo
        {
            get => 
                this.blipTileInfo;
            set => 
                this.blipTileInfo = value;
        }

        public bool IsDirectNotificationsEnabled =>
            this.suppressDirectNotificationsCount == 0;
    }
}

