namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.IO;

    public class XtraSerializer
    {
        private ICustomObjectConverter customObjectConverter;
        private ObjectConverterImplementation objectConverterImpl;
        public const string NullValueString = "~Xtra#NULL";
        public const string Base64Value = "~Xtra#Base64";
        public const string ArrayValue = "~Xtra#Array";

        protected virtual DeserializeHelper CreateDeserializeHelper(object rootObj, bool useRootObj) => 
            useRootObj ? new DeserializeHelper(rootObj, true, null) : new DeserializeHelper();

        protected virtual SerializeHelper CreateSerializeHelper(object rootObj, bool useRootObj) => 
            useRootObj ? new SerializeHelper(rootObj) : new SerializeHelper();

        protected static XtraObjectInfo[] CreateXtraObjectInfoArray(object obj) => 
            new XtraObjectInfo[] { new XtraObjectInfo(string.Empty, obj) };

        private void CustomObjectConverterChanged()
        {
            if (this.customObjectConverter == null)
            {
                this.objectConverterImpl = null;
            }
            else
            {
                this.objectConverterImpl = new CustomObjectConverterImplementation(this.CustomObjectConverter);
            }
        }

        protected virtual IXtraPropertyCollection Deserialize(Stream stream, string appName, IList objects) => 
            null;

        protected virtual IXtraPropertyCollection Deserialize(string path, string appName, IList objects) => 
            null;

        protected virtual void DeserializeObject(object obj, IXtraPropertyCollection store, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            if (store != null)
            {
                XtraPropertyCollection propertys = new XtraPropertyCollection();
                propertys.AddRange(store);
                DeserializeHelper helper = this.CreateDeserializeHelper(obj, true);
                helper.ObjectConverterImpl = this.ObjectConverterImpl;
                helper.DeserializeObject(obj, propertys, options);
                helper.AfterDeserializeRootObject();
            }
        }

        public void DeserializeObject(object obj, Stream stream, string appName)
        {
            this.DeserializeObject(obj, stream, appName, OptionsLayoutBase.FullLayout);
        }

        public void DeserializeObject(object obj, object path, string appName)
        {
            this.DeserializeObject(obj, path, appName, OptionsLayoutBase.FullLayout);
        }

        public void DeserializeObject(object obj, string path, string appName)
        {
            this.DeserializeObject(obj, path, appName, OptionsLayoutBase.FullLayout);
        }

        public virtual void DeserializeObject(object obj, Stream stream, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            this.DeserializeObject(obj, this.Deserialize(stream, appName, CreateXtraObjectInfoArray(obj)), options);
        }

        public void DeserializeObject(object obj, object path, string appName, OptionsLayoutBase options)
        {
            Stream stream = path as Stream;
            if (stream != null)
            {
                this.DeserializeObject(obj, stream, appName, options);
            }
            else
            {
                this.DeserializeObject(obj, path.ToString(), appName, options);
            }
        }

        public virtual void DeserializeObject(object obj, string path, string appName, OptionsLayoutBase options)
        {
            this.DeserializeObject(obj, this.Deserialize(path, appName, CreateXtraObjectInfoArray(obj)), options);
        }

        public void DeserializeObjects(XtraObjectInfo[] objects, Stream stream, string appName)
        {
            this.DeserializeObjects(objects, stream, appName, OptionsLayoutBase.FullLayout);
        }

        public void DeserializeObjects(XtraObjectInfo[] objects, string path, string appName)
        {
            this.DeserializeObjects(objects, path, appName, OptionsLayoutBase.FullLayout);
        }

        public virtual void DeserializeObjects(IList objects, Stream stream, string appName, OptionsLayoutBase options)
        {
            this.DeserializeObjects(null, objects, stream, appName, options);
        }

        public virtual void DeserializeObjects(IList objects, string path, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            DeserializeHelper helper = this.CreateDeserializeHelper(null, false);
            helper.ObjectConverterImpl = this.ObjectConverterImpl;
            helper.DeserializeObjects(objects, this.Deserialize(path, appName, objects), options);
        }

        public virtual void DeserializeObjects(object rootObject, IList objects, Stream stream, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            DeserializeHelper helper = this.CreateDeserializeHelper(rootObject, true);
            helper.ObjectConverterImpl = this.ObjectConverterImpl;
            helper.DeserializeObjects(objects, this.Deserialize(stream, appName, objects), options);
        }

        protected virtual ObjectConverterImplementation GetDefaultObjectConverterImplementation() => 
            ObjectConverter.Instance;

        protected virtual bool Serialize(Stream stream, IXtraPropertyCollection props, string appName) => 
            false;

        protected virtual bool Serialize(string path, IXtraPropertyCollection props, string appName) => 
            false;

        public bool SerializeObject(object obj, Stream stream, string appName) => 
            this.SerializeObject(obj, stream, appName, OptionsLayoutBase.FullLayout);

        public bool SerializeObject(object obj, object path, string appName) => 
            this.SerializeObject(obj, path, appName, OptionsLayoutBase.FullLayout);

        public bool SerializeObject(object obj, string path, string appName) => 
            this.SerializeObject(obj, path, appName, OptionsLayoutBase.FullLayout);

        public virtual bool SerializeObject(object obj, Stream stream, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            SerializeHelper helper = this.CreateSerializeHelper(obj, true);
            helper.ObjectConverterImpl = this.ObjectConverterImpl;
            return this.Serialize(stream, helper.SerializeObject(obj, options), appName);
        }

        public bool SerializeObject(object obj, object path, string appName, OptionsLayoutBase options)
        {
            Stream stream = path as Stream;
            return ((stream == null) ? this.SerializeObject(obj, path.ToString(), appName, options) : this.SerializeObject(obj, stream, appName, options));
        }

        public virtual bool SerializeObject(object obj, string path, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            SerializeHelper helper = this.CreateSerializeHelper(obj, true);
            helper.ObjectConverterImpl = this.ObjectConverterImpl;
            return this.Serialize(path, helper.SerializeObject(obj, options), appName);
        }

        public bool SerializeObjects(XtraObjectInfo[] objects, Stream stream, string appName) => 
            this.SerializeObjects(objects, stream, appName, OptionsLayoutBase.FullLayout);

        public bool SerializeObjects(XtraObjectInfo[] objects, string path, string appName) => 
            this.SerializeObjects(objects, path, appName, OptionsLayoutBase.FullLayout);

        public bool SerializeObjects(XtraObjectInfo[] objects, Stream stream, string appName, OptionsLayoutBase options) => 
            this.SerializeObjects(null, objects, stream, appName, options);

        public virtual bool SerializeObjects(XtraObjectInfo[] objects, string path, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            SerializeHelper helper = this.CreateSerializeHelper(null, false);
            helper.ObjectConverterImpl = this.ObjectConverterImpl;
            return this.Serialize(path, helper.SerializeObjects(objects, options), appName);
        }

        public virtual bool SerializeObjects(object rootObject, IList objects, Stream stream, string appName, OptionsLayoutBase options)
        {
            options ??= OptionsLayoutBase.FullLayout;
            SerializeHelper helper = this.CreateSerializeHelper(rootObject, true);
            helper.ObjectConverterImpl = this.ObjectConverterImpl;
            bool flag = this.Serialize(stream, helper.SerializeObjects(objects, options), appName);
            if (helper.RootSerializationObject != null)
            {
                helper.RootSerializationObject.AfterSerialize();
            }
            return flag;
        }

        protected ObjectConverterImplementation ObjectConverterImpl
        {
            get
            {
                ObjectConverterImplementation objectConverterImpl = this.objectConverterImpl;
                if (this.objectConverterImpl == null)
                {
                    ObjectConverterImplementation local1 = this.objectConverterImpl;
                    objectConverterImpl = this.objectConverterImpl = this.GetDefaultObjectConverterImplementation();
                }
                return objectConverterImpl;
            }
        }

        protected bool HasCustomObjectConverter =>
            this.CustomObjectConverter != null;

        public ICustomObjectConverter CustomObjectConverter
        {
            get => 
                this.customObjectConverter;
            set
            {
                if (!ReferenceEquals(this.customObjectConverter, value))
                {
                    this.customObjectConverter = value;
                    this.CustomObjectConverterChanged();
                }
            }
        }

        public virtual bool CanUseStream =>
            false;
    }
}

