namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class Scene3DProperties : DrawingMultiIndexObject<Scene3DProperties, PropertyKey>, ICloneable<Scene3DProperties>, ISupportsCopyFrom<Scene3DProperties>, IScene3DProperties, IScene3DCamera, IScene3DLightRig, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey CameraPresetPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey CameraFovPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey CameraZoomPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey LatitudePropertyKey = new PropertyKey(3);
        public static readonly PropertyKey LongitudePropertyKey = new PropertyKey(4);
        public static readonly PropertyKey RevolutionPropertyKey = new PropertyKey(5);
        public static readonly PropertyKey LightRigDirectionPropertyKey = new PropertyKey(6);
        public static readonly PropertyKey LightRigPresetPropertyKey = new PropertyKey(7);
        public static readonly PropertyKey BackdropPlanePropertyKey = new PropertyKey(8);
        public static readonly PropertyKey HasCameraRotationPropertyKey = new PropertyKey(9);
        public static readonly PropertyKey HasLightRigRotationPropertyKey = new PropertyKey(10);
        private static readonly Scene3DPropertiesInfoIndexAccessor infoIndexAccessor = new Scene3DPropertiesInfoIndexAccessor();
        private static readonly Scene3DCameraRotationInfoIndexAccessor cameraRotationInfoIndexAccessor = new Scene3DCameraRotationInfoIndexAccessor();
        private static readonly Scene3DLightRigRotationInfoIndexAccessor lightRigRotationInfoIndexAccessor = new Scene3DLightRigRotationInfoIndexAccessor();
        private static readonly IIndexAccessorBase<Scene3DProperties, PropertyKey>[] indexAccessors = new IIndexAccessorBase<Scene3DProperties, PropertyKey>[] { infoIndexAccessor, cameraRotationInfoIndexAccessor, lightRigRotationInfoIndexAccessor };
        private DevExpress.Office.DrawingML.BackdropPlane backdropPlane;
        private int infoIndex;
        private int cameraRotationInfoIndex;
        private int lightRigRotationInfoIndex;
        private readonly PropertyChangedNotifier notifier;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        public Scene3DProperties(IDocumentModel documentModel) : base(documentModel)
        {
            this.notifier = new PropertyChangedNotifier(this);
            this.backdropPlane = new DevExpress.Office.DrawingML.BackdropPlane(documentModel);
            this.backdropPlane.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnBackdropPlaneChanged);
        }

        protected internal override void ApplyChanges(PropertyKey propertyKey)
        {
            base.ApplyChanges(propertyKey);
            this.notifier.OnPropertyChanged(propertyKey);
        }

        public void AssignCameraRotationInfoIndex(int value)
        {
            this.cameraRotationInfoIndex = value;
        }

        public void AssignInfoIndex(int value)
        {
            this.infoIndex = value;
        }

        public void AssignLightRigRotationInfoIndex(int value)
        {
            this.lightRigRotationInfoIndex = value;
        }

        public Scene3DProperties Clone()
        {
            Scene3DProperties properties = new Scene3DProperties(base.DocumentModel);
            properties.CopyFrom(this);
            return properties;
        }

        public void CopyFrom(Scene3DProperties value)
        {
            base.CopyFrom(value);
            this.backdropPlane.CopyFrom(value.backdropPlane);
        }

        protected override MultiIndexBatchUpdateHelper CreateBatchInitHelper() => 
            new Scene3DPropertiesBatchInitHelper(this);

        protected override MultiIndexBatchUpdateHelper CreateBatchUpdateHelper() => 
            new Scene3DPropertiesBatchUpdateHelper(this);

        public override bool Equals(object obj)
        {
            Scene3DProperties properties = obj as Scene3DProperties;
            return ((properties != null) ? (base.Equals(properties) && this.backdropPlane.Equals(properties.backdropPlane)) : false);
        }

        public override PropertyKey GetBatchUpdateChangeActions() => 
            PropertyKey.Undefined;

        public override int GetHashCode() => 
            base.GetHashCode() ^ this.backdropPlane.GetHashCode();

        public override Scene3DProperties GetOwner() => 
            this;

        private void OnBackdropPlaneChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(BackdropPlanePropertyKey, sender, e);
        }

        public void ResetToStyle()
        {
            base.BeginUpdate();
            try
            {
                this.Info.CopyFrom(base.DrawingCache.Scene3DPropertiesInfoCache.DefaultItem);
                this.CameraRotationInfo.CopyFrom(base.DrawingCache.Scene3DRotationInfoCache.DefaultItem);
                this.LightRigRotationInfo.CopyFrom(base.DrawingCache.Scene3DRotationInfoCache.DefaultItem);
            }
            finally
            {
                base.EndUpdate();
            }
            this.backdropPlane.ResetToStyle();
        }

        private PropertyKey SetCameraTypeCore(Scene3DPropertiesInfo info, PresetCameraType value)
        {
            info.CameraType = value;
            return CameraPresetPropertyKey;
        }

        protected PropertyKey SetDirectionCore(Scene3DPropertiesInfo info, LightRigDirection value)
        {
            info.LightRigDirection = value;
            return LightRigDirectionPropertyKey;
        }

        private PropertyKey SetFovCore(Scene3DPropertiesInfo info, int value)
        {
            info.Fov = value;
            return CameraFovPropertyKey;
        }

        private PropertyKey SetHasCameraRotationCore(Scene3DPropertiesInfo info, bool value)
        {
            info.HasCameraRotation = true;
            return HasCameraRotationPropertyKey;
        }

        private PropertyKey SetHasLightRigRotationCore(Scene3DPropertiesInfo info, bool value)
        {
            info.HasLightRigRotation = true;
            return HasLightRigRotationPropertyKey;
        }

        private PropertyKey SetLatitudeCore(Scene3DRotationInfo info, int value)
        {
            info.Latitude = value;
            return LatitudePropertyKey;
        }

        protected PropertyKey SetLightRigPresetCore(Scene3DPropertiesInfo info, LightRigPreset value)
        {
            info.LightRigPreset = value;
            return LightRigPresetPropertyKey;
        }

        private PropertyKey SetLongitudeCore(Scene3DRotationInfo info, int value)
        {
            info.Longitude = value;
            return LongitudePropertyKey;
        }

        private PropertyKey SetRevolutionCore(Scene3DRotationInfo info, int value)
        {
            info.Revolution = value;
            return RevolutionPropertyKey;
        }

        private void SetRotationPropertyValue(IIndexAccessor<Scene3DProperties, Scene3DRotationInfo, PropertyKey> indexHolder, MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int> setter, MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool> hasRotationSetter, int newValue)
        {
            base.DocumentModel.BeginUpdate();
            try
            {
                this.SetPropertyValueCore<Scene3DRotationInfo, int>(indexHolder, setter, newValue);
                this.SetPropertyValueCore<Scene3DPropertiesInfo, bool>(infoIndexAccessor, hasRotationSetter, true);
            }
            finally
            {
                base.DocumentModel.EndUpdate();
            }
        }

        private PropertyKey SetZoomCore(Scene3DPropertiesInfo info, int value)
        {
            info.Zoom = value;
            return CameraZoomPropertyKey;
        }

        public static Scene3DPropertiesInfoIndexAccessor InfoIndexAccessor =>
            infoIndexAccessor;

        public static Scene3DCameraRotationInfoIndexAccessor CameraRotationInfoIndexAccessor =>
            cameraRotationInfoIndexAccessor;

        public static Scene3DLightRigRotationInfoIndexAccessor LightRigRotationInfoIndexAccessor =>
            lightRigRotationInfoIndexAccessor;

        public int InfoIndex =>
            this.infoIndex;

        public int CameraRotationInfoIndex =>
            this.cameraRotationInfoIndex;

        public int LightRigRotationInfoIndex =>
            this.lightRigRotationInfoIndex;

        protected override IIndexAccessorBase<Scene3DProperties, PropertyKey>[] IndexAccessors =>
            indexAccessors;

        internal Scene3DPropertiesBatchUpdateHelper BatchUpdateHelper =>
            (Scene3DPropertiesBatchUpdateHelper) base.BatchUpdateHelper;

        public Scene3DPropertiesInfo Info =>
            base.IsUpdateLocked ? this.BatchUpdateHelper.Info : this.InfoCore;

        protected internal Scene3DRotationInfo CameraRotationInfo =>
            base.IsUpdateLocked ? this.BatchUpdateHelper.CameraRotationInfo : this.CameraRotationInfoCore;

        protected internal Scene3DRotationInfo LightRigRotationInfo =>
            base.IsUpdateLocked ? this.BatchUpdateHelper.LightRigRotationInfo : this.LightRigRotationInfoCore;

        private Scene3DPropertiesInfo InfoCore =>
            infoIndexAccessor.GetInfo(this);

        private Scene3DRotationInfo CameraRotationInfoCore =>
            cameraRotationInfoIndexAccessor.GetInfo(this);

        private Scene3DRotationInfo LightRigRotationInfoCore =>
            lightRigRotationInfoIndexAccessor.GetInfo(this);

        public bool IsDefault =>
            (this.infoIndex == 0) && ((this.cameraRotationInfoIndex == 0) && ((this.lightRigRotationInfoIndex == 0) && this.backdropPlane.IsDefault));

        public IScene3DCamera Camera =>
            this;

        public IScene3DLightRig LightRig =>
            this;

        public DevExpress.Office.DrawingML.BackdropPlane BackdropPlane =>
            this.backdropPlane;

        PresetCameraType IScene3DCamera.Preset
        {
            get => 
                this.Info.CameraType;
            set
            {
                if (this.Info.CameraType != value)
                {
                    this.SetPropertyValue<Scene3DPropertiesInfo, PresetCameraType>(infoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, PresetCameraType>(this.SetCameraTypeCore), value);
                }
            }
        }

        int IScene3DCamera.Fov
        {
            get => 
                this.Info.Fov;
            set
            {
                if (this.Info.Fov != value)
                {
                    this.SetPropertyValue<Scene3DPropertiesInfo, int>(infoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, int>(this.SetFovCore), value);
                }
            }
        }

        int IScene3DCamera.Zoom
        {
            get => 
                this.Info.Zoom;
            set
            {
                if (this.Info.Zoom != value)
                {
                    this.SetPropertyValue<Scene3DPropertiesInfo, int>(infoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, int>(this.SetZoomCore), value);
                }
            }
        }

        int IScene3DCamera.Lat
        {
            get => 
                this.CameraRotationInfo.Latitude;
            set
            {
                if ((this.CameraRotationInfo.Latitude != value) || !this.Info.HasCameraRotation)
                {
                    this.SetRotationPropertyValue(cameraRotationInfoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int>(this.SetLatitudeCore), new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool>(this.SetHasCameraRotationCore), value);
                }
            }
        }

        int IScene3DCamera.Lon
        {
            get => 
                this.CameraRotationInfo.Longitude;
            set
            {
                if ((this.CameraRotationInfo.Longitude != value) || !this.Info.HasCameraRotation)
                {
                    this.SetRotationPropertyValue(cameraRotationInfoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int>(this.SetLongitudeCore), new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool>(this.SetHasCameraRotationCore), value);
                }
            }
        }

        int IScene3DCamera.Rev
        {
            get => 
                this.CameraRotationInfo.Revolution;
            set
            {
                if ((this.CameraRotationInfo.Revolution != value) || !this.Info.HasCameraRotation)
                {
                    this.SetRotationPropertyValue(cameraRotationInfoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int>(this.SetRevolutionCore), new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool>(this.SetHasCameraRotationCore), value);
                }
            }
        }

        bool IScene3DCamera.HasRotation =>
            this.Info.HasCameraRotation;

        LightRigDirection IScene3DLightRig.Direction
        {
            get => 
                this.Info.LightRigDirection;
            set
            {
                if (this.Info.LightRigDirection != value)
                {
                    this.SetPropertyValue<Scene3DPropertiesInfo, LightRigDirection>(infoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, LightRigDirection>(this.SetDirectionCore), value);
                }
            }
        }

        LightRigPreset IScene3DLightRig.Preset
        {
            get => 
                this.Info.LightRigPreset;
            set
            {
                if (this.Info.LightRigPreset != value)
                {
                    this.SetPropertyValue<Scene3DPropertiesInfo, LightRigPreset>(infoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, LightRigPreset>(this.SetLightRigPresetCore), value);
                }
            }
        }

        int IScene3DLightRig.Lat
        {
            get => 
                this.LightRigRotationInfo.Latitude;
            set
            {
                if ((this.LightRigRotationInfo.Latitude != value) || !this.Info.HasLightRigRotation)
                {
                    this.SetRotationPropertyValue(lightRigRotationInfoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int>(this.SetLatitudeCore), new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool>(this.SetHasLightRigRotationCore), value);
                }
            }
        }

        int IScene3DLightRig.Lon
        {
            get => 
                this.LightRigRotationInfo.Longitude;
            set
            {
                if ((this.LightRigRotationInfo.Longitude != value) || !this.Info.HasLightRigRotation)
                {
                    this.SetRotationPropertyValue(lightRigRotationInfoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int>(this.SetLongitudeCore), new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool>(this.SetHasLightRigRotationCore), value);
                }
            }
        }

        int IScene3DLightRig.Rev
        {
            get => 
                this.LightRigRotationInfo.Revolution;
            set
            {
                if ((this.LightRigRotationInfo.Revolution != value) || !this.Info.HasLightRigRotation)
                {
                    this.SetRotationPropertyValue(lightRigRotationInfoIndexAccessor, new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DRotationInfo, int>(this.SetRevolutionCore), new MultiIndexObject<Scene3DProperties, PropertyKey>.SetPropertyValueDelegate<Scene3DPropertiesInfo, bool>(this.SetHasLightRigRotationCore), value);
                }
            }
        }

        bool IScene3DLightRig.HasRotation =>
            this.Info.HasLightRigRotation;
    }
}

