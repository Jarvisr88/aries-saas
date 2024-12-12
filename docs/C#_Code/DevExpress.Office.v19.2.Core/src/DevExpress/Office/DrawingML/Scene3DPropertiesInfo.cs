namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;
    using System.IO;

    public class Scene3DPropertiesInfo : ICloneable<Scene3DPropertiesInfo>, ISupportsCopyFrom<Scene3DPropertiesInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private static readonly Scene3DPropertiesInfo defaultInfo = new Scene3DPropertiesInfo();
        private const uint MaskPresetCameraType = 0x3f;
        private const uint MaskLightRigDirection = 960;
        private const uint MaskLightRigPreset = 0x7c00;
        private const uint MaskHasCameraRotation = 0x8000;
        private const uint MaskHasLightRigRotation = 0x10000;
        private uint packedValues;
        private int fov;
        private int zoom = 0x186a0;

        public Scene3DPropertiesInfo Clone()
        {
            Scene3DPropertiesInfo info = new Scene3DPropertiesInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(Scene3DPropertiesInfo value)
        {
            this.packedValues = value.packedValues;
            this.fov = value.fov;
            this.zoom = value.zoom;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.fov = reader.ReadInt32();
            this.zoom = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.fov);
            writer.Write(this.zoom);
        }

        public override bool Equals(object obj)
        {
            Scene3DPropertiesInfo info = obj as Scene3DPropertiesInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && ((this.fov == info.fov) && (this.zoom == info.zoom))) : false);
        }

        private bool GetBooleanValue(uint mask) => 
            (this.packedValues & mask) != 0;

        public override int GetHashCode() => 
            (this.packedValues.GetHashCode() ^ this.fov) ^ this.zoom;

        private uint GetUIntValue(uint mask, int bits) => 
            (this.packedValues & mask) >> (bits & 0x1f);

        private void SetBooleanValue(uint mask, bool bitVal)
        {
            if (bitVal)
            {
                this.packedValues |= mask;
            }
            else
            {
                this.packedValues &= ~mask;
            }
        }

        private void SetUIntValue(uint mask, int bits, uint value)
        {
            this.packedValues &= ~mask;
            this.packedValues |= (value << (bits & 0x1f)) & mask;
        }

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        public static Scene3DPropertiesInfo DefaultInfo =>
            defaultInfo;

        public PresetCameraType CameraType
        {
            get => 
                (PresetCameraType) this.GetUIntValue(0x3f, 0);
            set => 
                this.SetUIntValue(0x3f, 0, (uint) value);
        }

        public DevExpress.Office.DrawingML.LightRigDirection LightRigDirection
        {
            get => 
                (DevExpress.Office.DrawingML.LightRigDirection) this.GetUIntValue(960, 6);
            set => 
                this.SetUIntValue(960, 6, (uint) value);
        }

        public DevExpress.Office.DrawingML.LightRigPreset LightRigPreset
        {
            get => 
                (DevExpress.Office.DrawingML.LightRigPreset) this.GetUIntValue(0x7c00, 10);
            set => 
                this.SetUIntValue(0x7c00, 10, (uint) value);
        }

        public bool HasCameraRotation
        {
            get => 
                this.GetBooleanValue(0x8000);
            set => 
                this.SetBooleanValue(0x8000, value);
        }

        public bool HasLightRigRotation
        {
            get => 
                this.GetBooleanValue(0x10000);
            set => 
                this.SetBooleanValue(0x10000, value);
        }

        public int Fov
        {
            get => 
                this.fov;
            set => 
                this.fov = value;
        }

        public int Zoom
        {
            get => 
                this.zoom;
            set => 
                this.zoom = value;
        }

        public bool IsDefault =>
            this.Equals(defaultInfo);
    }
}

