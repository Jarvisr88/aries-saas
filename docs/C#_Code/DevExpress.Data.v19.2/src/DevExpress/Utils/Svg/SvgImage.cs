namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Security;

    [Serializable, TypeConverter(typeof(BinaryTypeConverter))]
    public class SvgImage : ISerializable, ICloneable
    {
        private List<SvgElement> elementsCore;
        private List<SvgStyle> stylesCore;
        private IEnumerable<string> unknownTagsCore;
        private SvgRoot rootCore;
        private object ownerCore;
        private SvgStyle defaultStyleCore;

        public SvgImage()
        {
            this.elementsCore = new List<SvgElement>();
            this.stylesCore = new List<SvgStyle>();
            this.unknownTagsCore = new List<string>();
        }

        public SvgImage(Stream stream) : this()
        {
            if (stream != null)
            {
                SvgLoader.LoadFromStream(stream, this);
            }
        }

        protected SvgImage(SerializationInfo info, StreamingContext context) : this()
        {
            if (info.MemberCount != 0)
            {
                byte[] buffer = info.GetValue("Data", typeof(object)) as byte[];
                if (buffer != null)
                {
                    using (MemoryStream stream = new MemoryStream(buffer))
                    {
                        SvgLoader.LoadFromStream(stream, this);
                    }
                }
            }
        }

        public SvgImage(Type type, string resource) : this()
        {
            Stream manifestResourceStream = type.Module.Assembly.GetManifestResourceStream(type, resource);
            if (manifestResourceStream != null)
            {
                SvgLoader.LoadFromStream(manifestResourceStream, this);
            }
        }

        public SvgImage Clone() => 
            this.Clone(null);

        public SvgImage Clone(Action<SvgElement, Hashtable> updateStyle)
        {
            SvgImage image = new SvgImage {
                Width = this.Width,
                Height = this.Height,
                OffsetX = this.OffsetX,
                OffsetY = this.OffsetY,
                rootCore = this.Root.DeepCopy<SvgRoot>(updateStyle)
            };
            image.Elements.Add(image.rootCore);
            Func<SvgStyle, SvgStyle> get = <>c.<>9__56_0;
            if (<>c.<>9__56_0 == null)
            {
                Func<SvgStyle, SvgStyle> local1 = <>c.<>9__56_0;
                get = <>c.<>9__56_0 = x => x.DeepCopy();
            }
            image.DefaultStyle = this.DefaultStyle.Get<SvgStyle, SvgStyle>(get, null);
            return image;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static SvgImage Create(SvgRoot root)
        {
            SvgImage image = new SvgImage();
            image.SetRoot(root);
            image.Elements.Add(root);
            return image;
        }

        public static SvgImage FromFile(string path) => 
            SvgLoader.LoadFromFile(path);

        public static SvgImage FromResources(string name, Assembly asm)
        {
            Stream manifestResourceStream = asm.GetManifestResourceStream(name);
            return ((manifestResourceStream != null) ? FromStream(manifestResourceStream) : null);
        }

        public static SvgImage FromStream(Stream stream) => 
            SvgLoader.LoadFromStream(stream);

        private void GetDimensions()
        {
            if ((this.rootCore != null) && ((this.rootCore.ViewBox != null) || ((this.rootCore.Width != null) && (this.rootCore.Height != null))))
            {
                this.Width = ((this.rootCore.Width == null) || (this.rootCore.Width.UnitType == SvgUnitType.Percentage)) ? this.rootCore.ViewBox.Width : this.rootCore.Width.Value;
                this.Height = ((this.rootCore.Height == null) || (this.rootCore.Height.UnitType == SvgUnitType.Percentage)) ? this.rootCore.ViewBox.Height : this.rootCore.Height.Value;
                this.OffsetX = (this.rootCore.ViewBox != null) ? this.rootCore.ViewBox.MinX : 0.0;
                this.OffsetY = (this.rootCore.ViewBox != null) ? this.rootCore.ViewBox.MinY : 0.0;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Matrix GetTransform()
        {
            Matrix result = new Matrix();
            Func<SvgElement, bool> predicate = <>c.<>9__60_0;
            if (<>c.<>9__60_0 == null)
            {
                Func<SvgElement, bool> local1 = <>c.<>9__60_0;
                predicate = <>c.<>9__60_0 = x => x is SvgTransformGroup;
            }
            this.Root.Elements.FirstOrDefault<SvgElement>(predicate).Do<SvgElement>(delegate (SvgElement x) {
                Func<Matrix, SvgTransform, Matrix> func = <>c.<>9__60_2;
                if (<>c.<>9__60_2 == null)
                {
                    Func<Matrix, SvgTransform, Matrix> local1 = <>c.<>9__60_2;
                    func = <>c.<>9__60_2 = delegate (Matrix matrix, SvgTransform transform) {
                        matrix.Multiply(transform.GetMatrix(1.0));
                        return matrix;
                    };
                }
                x.Transformations.Aggregate<SvgTransform, Matrix>(result, func);
            });
            return result;
        }

        public Matrix GetViewBoxTransform() => 
            this.Root.GetViewBoxTransform();

        public static implicit operator SvgImage(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                return SvgLoader.LoadFromStream(stream);
            }
        }

        public void Save(Stream stream)
        {
            SvgSerializer.SaveSvgImageToXML(stream, this);
        }

        public void Save(string filePath)
        {
            SvgSerializer.SaveSvgImageToXML(filePath, this);
        }

        protected internal void SetRoot(SvgRoot root)
        {
            this.rootCore = root;
            this.GetDimensions();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public SvgImage SetTransform(Matrix transform)
        {
            SvgImage image = this.Clone();
            if (!transform.IsIdentity)
            {
                Func<SvgElement, bool> predicate = <>c.<>9__59_0;
                if (<>c.<>9__59_0 == null)
                {
                    Func<SvgElement, bool> local1 = <>c.<>9__59_0;
                    predicate = <>c.<>9__59_0 = x => x is SvgTransformGroup;
                }
                SvgTransformGroup group = image.Root.Elements.FirstOrDefault<SvgElement>(predicate) as SvgTransformGroup;
                if ((group == null) || (image.Root.Elements.Count != 1))
                {
                    group = new SvgTransformGroup();
                    group.SetParent(image.Root);
                    foreach (SvgElement element in image.Root.Elements)
                    {
                        group.AddElement(element);
                        Action<SvgTransformGroup> @do = <>c.<>9__59_1;
                        if (<>c.<>9__59_1 == null)
                        {
                            Action<SvgTransformGroup> local2 = <>c.<>9__59_1;
                            @do = <>c.<>9__59_1 = delegate (SvgTransformGroup x) {
                                x.SetParent(null);
                            };
                        }
                        (element as SvgTransformGroup).Do<SvgTransformGroup>(@do);
                    }
                    image.Root.Elements.Clear();
                    image.Root.AddElement(group);
                }
                group.Transformations.Add(new SvgMatrix(transform));
            }
            return image;
        }

        object ICloneable.Clone() => 
            this.Clone();

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                SvgSerializer.SaveSvgImageToXML(stream, this);
                info.AddValue("Data", stream.ToArray());
            }
        }

        protected virtual void UpdateDefaultStyle(SvgElement element)
        {
            element.DefaultStyle = this.DefaultStyle;
            foreach (SvgElement element2 in element.Elements)
            {
                this.UpdateDefaultStyle(element2);
            }
        }

        public IEnumerable<string> UnknownTags =>
            this.unknownTagsCore;

        internal object Owner
        {
            get => 
                this.ownerCore;
            set => 
                this.ownerCore = value;
        }

        public SvgStyle DefaultStyle
        {
            get => 
                this.defaultStyleCore;
            set
            {
                if (!ReferenceEquals(this.defaultStyleCore, value))
                {
                    this.defaultStyleCore = value;
                    this.UpdateDefaultStyle(this.Root);
                }
            }
        }

        public List<SvgElement> Elements =>
            this.elementsCore;

        public List<SvgStyle> Styles =>
            this.stylesCore;

        public object Tag { get; set; }

        public double Width { get; private set; }

        public double Height { get; private set; }

        public double OffsetX { get; private set; }

        public double OffsetY { get; private set; }

        public SvgRoot Root =>
            this.rootCore;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgImage.<>c <>9 = new SvgImage.<>c();
            public static Func<SvgStyle, SvgStyle> <>9__56_0;
            public static Func<SvgElement, bool> <>9__59_0;
            public static Action<SvgTransformGroup> <>9__59_1;
            public static Func<SvgElement, bool> <>9__60_0;
            public static Func<Matrix, SvgTransform, Matrix> <>9__60_2;

            internal SvgStyle <Clone>b__56_0(SvgStyle x) => 
                x.DeepCopy();

            internal bool <GetTransform>b__60_0(SvgElement x) => 
                x is SvgTransformGroup;

            internal Matrix <GetTransform>b__60_2(Matrix matrix, SvgTransform transform)
            {
                matrix.Multiply(transform.GetMatrix(1.0));
                return matrix;
            }

            internal bool <SetTransform>b__59_0(SvgElement x) => 
                x is SvgTransformGroup;

            internal void <SetTransform>b__59_1(SvgTransformGroup x)
            {
                x.SetParent(null);
            }
        }
    }
}

