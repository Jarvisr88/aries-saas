namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.DescendantBuilding;
    using DevExpress.Entity.Model.Metadata;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class EFContainerBuilderBase : ContainerBuilder
    {
        private DescendantBuilderFactoryBase descendantBuilderFactory;

        public override IDbContainerInfo Build(IDXTypeInfo type, ISolutionTypesProvider typesProvider)
        {
            IDbContainerInfo info;
            try
            {
                IDbDescendantBuilder dbDescendantBuilder = this.DescendantBuilderFactory.GetDbDescendantBuilder(type, typesProvider);
                if (dbDescendantBuilder == null)
                {
                    EdmxResource edmxResource = EdmxResource.GetEdmxResource(type);
                    string providerName = "";
                    if (edmxResource != null)
                    {
                        providerName = edmxResource.GetProviderName();
                    }
                    throw new ProviderNotSupportedException(providerName);
                }
                try
                {
                    info = this.BuildCore(type, dbDescendantBuilder);
                }
                finally
                {
                    dbDescendantBuilder.Dispose();
                }
            }
            catch (TargetInvocationException exception)
            {
                this.LogException(exception.InnerException ?? exception, false);
                info = null;
            }
            catch (Exception exception2)
            {
                this.LogException(exception2, false);
                info = null;
            }
            return info;
        }

        protected virtual IDbContainerInfo BuildCore(IDXTypeInfo type, IDbDescendantBuilder descendantBuilder)
        {
            object dbContextInstance = descendantBuilder.Build();
            if (dbContextInstance == null)
            {
                return null;
            }
            object objectContext = this.GetObjectContext(descendantBuilder.TypesCollector, dbContextInstance);
            if (objectContext == null)
            {
                return null;
            }
            this.CreateSampleQuery(objectContext);
            MetadataWorkspaceInfo mw = this.CreateMetadataWorkspaceInfo(objectContext);
            return this.GetDbContainerInfo(type, mw, this.GetMapper(descendantBuilder.TypesCollector, mw));
        }

        protected virtual DescendantBuilderFactoryBase CreateBuilderProviderFactory() => 
            new DescendantBuilderFactoryBase();

        private MetadataWorkspaceInfo CreateMetadataWorkspaceInfo(object objectContext)
        {
            object obj2 = PropertyAccessor.GetValue(objectContext, "MetadataWorkspace");
            return ((obj2 != null) ? new MetadataWorkspaceInfo(obj2) : null);
        }

        protected virtual void CreateSampleQuery(object objectContext)
        {
            try
            {
                MethodInfo method = objectContext.GetType().GetMethod("CreateQuery");
                if (method != null)
                {
                    Type[] typeArguments = new Type[] { typeof(object) };
                    method = method.MakeGenericMethod(typeArguments);
                    if (method != null)
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        if ((parameters != null) && (parameters.Length >= 2))
                        {
                            object[] args = new object[] { 0 };
                            object obj2 = Activator.CreateInstance(parameters[1].ParameterType, args);
                            object[] objArray2 = new object[] { "x", obj2 };
                            object obj3 = method.Invoke(objectContext, objArray2);
                            if (obj3 != null)
                            {
                                MethodInfo info2 = obj3.GetType().GetMethod("ToTraceString");
                                if (info2 != null)
                                {
                                    info2.Invoke(obj3, null);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void DeleteDatabase(object dbContextInstance)
        {
            try
            {
                object obj2 = PropertyAccessor.GetValue(dbContextInstance, "Database");
                if (obj2 != null)
                {
                    MethodInfo method = obj2.GetType().GetMethod("Delete", new Type[0]);
                    if (method != null)
                    {
                        method.Invoke(obj2, null);
                    }
                }
            }
            catch
            {
            }
        }

        private void DeleteTempFolder(string directoryPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(directoryPath) && Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }
            }
            catch
            {
            }
        }

        protected IEntityContainerInfo GetDbSets(TypesCollector typesCollector, object dbContextInstance)
        {
            try
            {
                if (typesCollector.DbSetFinder != null)
                {
                    return new EFCoreContainerInfo(Activator.CreateInstance(typesCollector.DbSetFinder.ResolveType()), dbContextInstance);
                }
            }
            catch (Exception exception)
            {
                if ((exception is TargetInvocationException) && (exception.InnerException != null))
                {
                    this.LogException(exception.InnerException, true);
                }
                else
                {
                    this.LogException(exception, true);
                }
            }
            return null;
        }

        protected internal virtual Mapper GetMapper(TypesCollector typesCollector, MetadataWorkspaceInfo mwInfo) => 
            new Mapper(mwInfo, typesCollector);

        private object GetObjectContext(TypesCollector typesCollector, object dbContextInstance)
        {
            object obj2 = null;
            try
            {
                if (typesCollector.IObjectContextAdapter != null)
                {
                    obj2 = GetProperty(dbContextInstance.GetType(), "ObjectContext").GetValue(dbContextInstance, null);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                if ((exception is TargetInvocationException) && (exception.InnerException != null))
                {
                    this.LogException(exception.InnerException, true);
                }
                else
                {
                    this.LogException(exception, true);
                }
            }
            return obj2;
        }

        private static PropertyInfo GetProperty(Type type, string name)
        {
            Func<Type, IEnumerable<PropertyInfo>> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<Type, IEnumerable<PropertyInfo>> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = (Func<Type, IEnumerable<PropertyInfo>>) (i => i.GetProperties());
            }
            return (from p in type.GetInterfaces().SelectMany<Type, PropertyInfo>(selector).Concat<PropertyInfo>(type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                where p.Name == name
                select p).First<PropertyInfo>();
        }

        protected virtual void LogException(Exception ex, bool display)
        {
        }

        public override DbContainerType BuilderType =>
            DbContainerType.EntityFramework;

        protected DescendantBuilderFactoryBase DescendantBuilderFactory
        {
            get
            {
                if (this.descendantBuilderFactory == null)
                {
                    this.descendantBuilderFactory = this.CreateBuilderProviderFactory();
                    this.descendantBuilderFactory.Initialize();
                }
                return this.descendantBuilderFactory;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EFContainerBuilderBase.<>c <>9 = new EFContainerBuilderBase.<>c();
            public static Func<Type, IEnumerable<PropertyInfo>> <>9__2_0;

            internal IEnumerable<PropertyInfo> <GetProperty>b__2_0(Type i) => 
                i.GetProperties();
        }
    }
}

