namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class Scene3DPropertiesBatchUpdateHelper : MultiIndexBatchUpdateHelper
    {
        private Scene3DPropertiesInfo info;
        private Scene3DRotationInfo cameraRotationInfo;
        private Scene3DRotationInfo lightRigRotationInfo;
        private int suppressDirectNotificationsCount;

        public Scene3DPropertiesBatchUpdateHelper(IBatchUpdateHandler handler) : base(handler)
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

        public Scene3DPropertiesInfo Info
        {
            get => 
                this.info;
            set => 
                this.info = value;
        }

        public Scene3DRotationInfo CameraRotationInfo
        {
            get => 
                this.cameraRotationInfo;
            set => 
                this.cameraRotationInfo = value;
        }

        public Scene3DRotationInfo LightRigRotationInfo
        {
            get => 
                this.lightRigRotationInfo;
            set => 
                this.lightRigRotationInfo = value;
        }

        public bool IsDirectNotificationsEnabled =>
            this.suppressDirectNotificationsCount == 0;
    }
}

