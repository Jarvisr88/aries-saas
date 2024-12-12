namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.Model.Metadata;
    using DevExpress.Entity.ProjectModel;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public abstract class DbDescendantBuilder : DescendantBuilderBase, IDbDescendantBuilder, IDisposable
    {
        protected Type dbContext;
        protected Type descendant;
        private string tempFolder;
        private DevExpress.Entity.Model.TypesCollector typesCollector;

        protected DbDescendantBuilder(DevExpress.Entity.Model.TypesCollector typesCollector)
        {
            this.descendant = typesCollector.DbDescendantType;
            this.dbContext = typesCollector.DbContextType;
            this.typesCollector = typesCollector;
        }

        public virtual object Build()
        {
            object descendantInstance;
            base.InstanceActivator = null;
            IDXTypeInfo dbDescendantInfo = this.TypesCollector.DbDescendantInfo;
            if (dbDescendantInfo == null)
            {
                return null;
            }
            Expression connection = null;
            EdmxResource edmxResource = EdmxResource.GetEdmxResource(dbDescendantInfo);
            bool isModelFirst = edmxResource != null;
            connection = this.CreateDbConnection(this.dbContext, isModelFirst, this.typesCollector);
            if (connection == null)
            {
                return null;
            }
            try
            {
                Tuple<ConstructorInfo, Type[]> dbContextConstructor = this.GetDbContextConstructor(connection, this.dbContext);
                if (dbContextConstructor == null)
                {
                    descendantInstance = null;
                }
                else
                {
                    Type resultType = this.EmitDbDescendant(this.typesCollector, dbContextConstructor, this.CreateDynamicAssembly(), isModelFirst);
                    if (isModelFirst)
                    {
                        this.PrepareEdmx(edmxResource);
                    }
                    base.InstanceActivator = base.CreateDescendantInstanceActivator(resultType, dbContextConstructor.Item2, connection, this.TempFolder);
                    descendantInstance = base.DescendantInstance;
                }
            }
            catch (TargetInvocationException exception)
            {
                if (!this.SuppressExceptions)
                {
                    if (exception.InnerException != null)
                    {
                        throw exception.InnerException;
                    }
                    throw;
                }
                descendantInstance = null;
            }
            catch (Exception)
            {
                if (!this.SuppressExceptions)
                {
                    throw;
                }
                descendantInstance = null;
            }
            return descendantInstance;
        }

        protected virtual void Clear()
        {
            this.DeleteDatabase(base.DescendantInstance);
            this.DeleteTempFolder();
            this.typesCollector = null;
        }

        protected virtual Expression CreateDbConnection(Type dbContextType, bool isModelFirst, DevExpress.Entity.Model.TypesCollector typesCollector) => 
            !isModelFirst ? this.CreateDefaultDbConnection(dbContextType, typesCollector) : this.CreateModelFirstDbConnection(typesCollector);

        protected abstract Expression CreateDefaultDbConnection(Type dbContextType, DevExpress.Entity.Model.TypesCollector typesCollector);
        protected ModuleBuilder CreateDynamicAssembly()
        {
            AssemblyName dynamicAssemblyName = this.GetDynamicAssemblyName();
            string fileName = dynamicAssemblyName.Name + ".dll";
            return AppDomain.CurrentDomain.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.RunAndSave, this.TempFolder).DefineDynamicModule(dynamicAssemblyName.Name, fileName);
        }

        protected abstract Expression CreateModelFirstDbConnection(DevExpress.Entity.Model.TypesCollector typesCollector);
        protected void CreateProperty(TypeBuilder tb, PropertyInfo pi, DevExpress.Entity.Model.TypesCollector typesCollector)
        {
            if ((tb != null) && ((pi != null) && ((typesCollector != null) && (typesCollector.DbSet != null))))
            {
                Type propertyType = pi.PropertyType;
                if (propertyType != null)
                {
                    Type[] genericArguments = propertyType.GetGenericArguments();
                    if ((genericArguments != null) && (genericArguments.Length != 0))
                    {
                        Type type2 = genericArguments[0];
                        if (type2 != null)
                        {
                            Type type3 = typesCollector.DbSet.ResolveType();
                            if (type3 != null)
                            {
                                Type[] typeArguments = new Type[] { type2 };
                                propertyType = type3.MakeGenericType(typeArguments);
                                FieldBuilder field = tb.DefineField("f_" + pi.Name, propertyType, FieldAttributes.Private);
                                MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Public;
                                MethodBuilder mdBuilder = tb.DefineMethod("get_" + pi.Name, attributes, propertyType, Type.EmptyTypes);
                                ILGenerator iLGenerator = mdBuilder.GetILGenerator();
                                iLGenerator.Emit(OpCodes.Ldarg_0);
                                iLGenerator.Emit(OpCodes.Ldfld, field);
                                iLGenerator.Emit(OpCodes.Ret);
                                Type[] parameterTypes = new Type[] { propertyType };
                                MethodBuilder builder3 = tb.DefineMethod("set_" + pi.Name, attributes, null, parameterTypes);
                                ILGenerator generator2 = builder3.GetILGenerator();
                                generator2.Emit(OpCodes.Ldarg_0);
                                generator2.Emit(OpCodes.Ldarg_1);
                                generator2.Emit(OpCodes.Stfld, field);
                                generator2.Emit(OpCodes.Ret);
                                PropertyBuilder builder4 = tb.DefineProperty(pi.Name, PropertyAttributes.HasDefault, propertyType, null);
                                builder4.SetGetMethod(mdBuilder);
                                builder4.SetSetMethod(builder3);
                            }
                        }
                    }
                }
            }
        }

        protected void DeleteDatabase(object dbContextInstance)
        {
            if (dbContextInstance != null)
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
        }

        protected void DeleteTempFolder()
        {
            if (!string.IsNullOrEmpty(this.tempFolder) && Directory.Exists(this.tempFolder))
            {
                try
                {
                    string[] files = Directory.GetFiles(this.tempFolder);
                    int index = 0;
                    while (true)
                    {
                        if (index >= files.Length)
                        {
                            Directory.Delete(this.tempFolder, true);
                            break;
                        }
                        string path = files[index];
                        File.Delete(path);
                        index++;
                    }
                }
                catch
                {
                }
                finally
                {
                    this.tempFolder = null;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.Clear();
            }
        }

        protected virtual Type EmitDbDescendant(DevExpress.Entity.Model.TypesCollector typesCollector, Tuple<ConstructorInfo, Type[]> ctorTuple, ModuleBuilder mb, bool isModelFirst)
        {
            TypeBuilder tb = mb.DefineType(this.descendant.FullName, TypeAttributes.Public, !isModelFirst ? this.descendant : this.dbContext);
            ILGenerator iLGenerator = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctorTuple.Item2).GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (ctorTuple.Item1.GetParameters().Length == 2)
            {
                iLGenerator.Emit(OpCodes.Ldarg_2);
            }
            iLGenerator.Emit(OpCodes.Call, ctorTuple.Item1);
            iLGenerator.Emit(OpCodes.Ret);
            if (isModelFirst)
            {
                foreach (PropertyInfo info in this.GetDbSetProperties(this.descendant))
                {
                    this.CreateProperty(tb, info, typesCollector);
                }
            }
            return tb.CreateType();
        }

        protected Type FindEFType(string typeName) => 
            this.dbContext.GetAssembly().GetTypes().FirstOrDefault<Type>(x => x.FullName.StartsWith(typeName));

        protected Tuple<ConstructorInfo, Type[]> GetDbContextConstructor(Expression connection, Type dbContextType)
        {
            if ((connection != null) && (connection.Type == typeof(string)))
            {
                Func<ConstructorInfo, bool> func1 = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<ConstructorInfo, bool> local1 = <>c.<>9__14_0;
                    func1 = <>c.<>9__14_0 = a => (a.GetParameters().Length == 1) && (a.GetParameters()[0].ParameterType == typeof(string));
                }
                Type[] typeArray1 = new Type[] { typeof(string) };
                return new Tuple<ConstructorInfo, Type[]>(dbContextType.GetConstructors().Where<ConstructorInfo>(func1).First<ConstructorInfo>(), typeArray1);
            }
            if ((connection != null) && !typeof(DbConnection).IsAssignableFrom(connection.Type))
            {
                return null;
            }
            Func<ConstructorInfo, bool> predicate = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                Func<ConstructorInfo, bool> local2 = <>c.<>9__14_1;
                predicate = <>c.<>9__14_1 = a => (a.GetParameters().Length == 2) && ((a.GetParameters()[0].ParameterType == typeof(DbConnection)) && (a.GetParameters()[1].ParameterType == typeof(bool)));
            }
            Type[] typeArray2 = new Type[] { typeof(DbConnection), typeof(bool) };
            return new Tuple<ConstructorInfo, Type[]>(dbContextType.GetConstructors().Where<ConstructorInfo>(predicate).First<ConstructorInfo>(), typeArray2);
        }

        protected IEnumerable<PropertyInfo> GetDbSetProperties(Type type)
        {
            if (type == null)
            {
                return null;
            }
            PropertyInfo[] properties = type.GetProperties();
            if (properties == null)
            {
                return null;
            }
            Func<PropertyInfo, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<PropertyInfo, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = delegate (PropertyInfo p) {
                    if (!p.PropertyType.IsGenericType || !p.PropertyType.FullName.StartsWith("System.Data.Entity.DbSet`1"))
                    {
                        return false;
                    }
                    Type[] genericArguments = p.PropertyType.GetGenericArguments();
                    return (genericArguments != null) && (genericArguments.Length == 1);
                };
            }
            return properties.Where<PropertyInfo>(predicate);
        }

        protected AssemblyName GetDynamicAssemblyName() => 
            new AssemblyName("Assembly" + this.descendant.Name);

        protected virtual void PrepareEdmx(EdmxResource edmxResource)
        {
            if (edmxResource != null)
            {
                edmxResource.WriteResources(this.TempFolder);
            }
        }

        public string TempFolder
        {
            get
            {
                if (base.IsDisposed)
                {
                    throw new ObjectDisposedException(typeof(DbDescendantBuilder).Name);
                }
                if (string.IsNullOrEmpty(this.tempFolder))
                {
                    this.tempFolder = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())).FullName;
                }
                return this.tempFolder;
            }
        }

        public DevExpress.Entity.Model.TypesCollector TypesCollector =>
            this.typesCollector;

        public abstract bool SuppressExceptions { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DbDescendantBuilder.<>c <>9 = new DbDescendantBuilder.<>c();
            public static Func<PropertyInfo, bool> <>9__13_0;
            public static Func<ConstructorInfo, bool> <>9__14_0;
            public static Func<ConstructorInfo, bool> <>9__14_1;

            internal bool <GetDbContextConstructor>b__14_0(ConstructorInfo a) => 
                (a.GetParameters().Length == 1) && (a.GetParameters()[0].ParameterType == typeof(string));

            internal bool <GetDbContextConstructor>b__14_1(ConstructorInfo a) => 
                (a.GetParameters().Length == 2) && ((a.GetParameters()[0].ParameterType == typeof(DbConnection)) && (a.GetParameters()[1].ParameterType == typeof(bool)));

            internal bool <GetDbSetProperties>b__13_0(PropertyInfo p)
            {
                if (!p.PropertyType.IsGenericType || !p.PropertyType.FullName.StartsWith("System.Data.Entity.DbSet`1"))
                {
                    return false;
                }
                Type[] genericArguments = p.PropertyType.GetGenericArguments();
                return ((genericArguments != null) && (genericArguments.Length == 1));
            }
        }
    }
}

