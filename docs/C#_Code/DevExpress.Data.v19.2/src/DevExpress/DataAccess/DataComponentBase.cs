namespace DevExpress.DataAccess
{
    using DevExpress.Data;
    using DevExpress.Data.Utils;
    using DevExpress.DataAccess.Native;
    using DevExpress.Services.Internal;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Xml.Linq;

    public abstract class DataComponentBase : Component, IObject, IServiceContainer, IServiceProvider, IDataComponent, IComponent, IDisposable, IParametersRenamer, ISensitiveInfoContainer
    {
        private readonly ServiceManager serviceManager = new ServiceManager();
        private string name = string.Empty;

        protected DataComponentBase()
        {
        }

        public abstract void Fill(IEnumerable<IParameter> sourceParameters);
        protected abstract string GetDataMember();
        public object GetService(Type serviceType) => 
            this.serviceManager.GetService(serviceType);

        public abstract void LoadFromXml(XElement element);
        public void RenameParameter(string oldName, string newName)
        {
            Dictionary<string, string> renamingMap = new Dictionary<string, string>();
            renamingMap.Add(oldName, newName);
            this.RenameParameters(renamingMap);
        }

        public void RenameParameters(IDictionary<string, string> renamingMap)
        {
            new ParametersRenamingHelper(renamingMap).Process(this.AllParameters);
        }

        protected void ReplaceService(Type type, object service)
        {
            if (service != null)
            {
                ((IServiceContainer) this).RemoveService(type);
                ((IServiceContainer) this).AddService(type, service);
            }
        }

        protected void ReplaceServiceFromProvider(Type type, IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                object service = serviceProvider.GetService(type);
                this.ReplaceService(type, service);
            }
        }

        public abstract XElement SaveToXml();
        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            this.serviceManager.AddService(serviceType, callback);
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance)
        {
            this.serviceManager.AddService(serviceType, serviceInstance);
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            this.serviceManager.AddService(serviceType, callback, promote);
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
        {
            this.serviceManager.AddService(serviceType, serviceInstance, promote);
        }

        void IServiceContainer.RemoveService(Type serviceType)
        {
            this.serviceManager.RemoveService(serviceType);
        }

        void IServiceContainer.RemoveService(Type serviceType, bool promote)
        {
            this.serviceManager.RemoveService(serviceType, promote);
        }

        object IServiceProvider.GetService(Type serviceType) => 
            this.GetService(serviceType);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(-1)]
        public string ObjectType
        {
            get
            {
                Type type = base.GetType();
                return $"{type.FullName},{type.Assembly.GetName().Name}";
            }
        }

        [DefaultValue(""), Browsable(false), XtraSerializableProperty(-1)]
        public virtual string Name
        {
            get => 
                (this.Site != null) ? this.Site.Name : this.name;
            set
            {
                try
                {
                    if (this.Site == null)
                    {
                        this.name = value;
                    }
                    else
                    {
                        this.Site.Name = value;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        protected IExtensionsProvider ExtensionsProvider =>
            this.GetService<IExtensionsProvider>();

        protected abstract IEnumerable<IParameter> AllParameters { get; }

        string IDataComponent.DataMember =>
            this.GetDataMember();

        bool ISensitiveInfoContainer.HasSensitiveInfo =>
            this.HasSensitiveInfo;

        protected abstract bool HasSensitiveInfo { get; }
    }
}

