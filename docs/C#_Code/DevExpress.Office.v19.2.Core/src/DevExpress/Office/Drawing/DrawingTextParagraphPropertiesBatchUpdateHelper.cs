namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class DrawingTextParagraphPropertiesBatchUpdateHelper : MultiIndexBatchUpdateHelper
    {
        private readonly DrawingTextSpacingInfo[] textSpacingInfos;
        private DrawingTextParagraphInfo textParagraphInfo;
        private int suppressDirectNotificationsCount;

        public DrawingTextParagraphPropertiesBatchUpdateHelper(IBatchUpdateHandler handler) : base(handler)
        {
            this.textSpacingInfos = new DrawingTextSpacingInfo[3];
        }

        public void ResumeDirectNotifications()
        {
            this.suppressDirectNotificationsCount--;
        }

        public void SuppressDirectNotifications()
        {
            this.suppressDirectNotificationsCount++;
        }

        public DrawingTextParagraphInfo TextParagraphInfo
        {
            get => 
                this.textParagraphInfo;
            set => 
                this.textParagraphInfo = value;
        }

        public DrawingTextSpacingInfo[] TextSpacingInfos =>
            this.textSpacingInfos;

        public bool IsDirectNotificationsEnabled =>
            this.suppressDirectNotificationsCount == 0;
    }
}

