namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;
    using System.IO;

    public class Scene3DRotationInfo : ICloneable<Scene3DRotationInfo>, ISupportsCopyFrom<Scene3DRotationInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private static readonly Scene3DRotationInfo defaultInfo = new Scene3DRotationInfo();
        private int latitude;
        private int longitude;
        private int revolution;

        public Scene3DRotationInfo Clone()
        {
            Scene3DRotationInfo info = new Scene3DRotationInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(Scene3DRotationInfo value)
        {
            this.latitude = value.latitude;
            this.longitude = value.longitude;
            this.revolution = value.revolution;
        }

        public static Scene3DRotationInfo CreateFromLatitude(int value, Scene3DRotationInfo oldInfo)
        {
            Scene3DRotationInfo info = oldInfo.Clone();
            info.Latitude = value;
            return info;
        }

        public static Scene3DRotationInfo CreateFromLongitude(int value, Scene3DRotationInfo oldInfo)
        {
            Scene3DRotationInfo info = oldInfo.Clone();
            info.Longitude = value;
            return info;
        }

        public static Scene3DRotationInfo CreateFromRevolution(int value, Scene3DRotationInfo oldInfo)
        {
            Scene3DRotationInfo info = oldInfo.Clone();
            info.Revolution = value;
            return info;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.latitude = reader.ReadInt32();
            this.longitude = reader.ReadInt32();
            this.revolution = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.latitude);
            writer.Write(this.longitude);
            writer.Write(this.revolution);
        }

        public override bool Equals(object obj)
        {
            Scene3DRotationInfo info = obj as Scene3DRotationInfo;
            return ((info != null) ? ((this.latitude == info.latitude) && ((this.longitude == info.longitude) && (this.revolution == info.revolution))) : false);
        }

        public override int GetHashCode() => 
            (this.latitude ^ this.longitude) ^ this.revolution;

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        public static Scene3DRotationInfo DefaultInfo =>
            defaultInfo;

        public int Latitude
        {
            get => 
                this.latitude;
            set => 
                this.latitude = value;
        }

        public int Longitude
        {
            get => 
                this.longitude;
            set => 
                this.longitude = value;
        }

        public int Revolution
        {
            get => 
                this.revolution;
            set => 
                this.revolution = value;
        }
    }
}

