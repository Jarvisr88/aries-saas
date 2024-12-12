namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class DefaultEFCoreDescendantBuilder : DescendantBuilderBase, IDbDescendantBuilder, IDisposable
    {
        private readonly Type descendant;
        private Type dbContext;
        private readonly DevExpress.Entity.Model.TypesCollector typesCollector;
        private string tempFolder;

        public DefaultEFCoreDescendantBuilder(DevExpress.Entity.Model.TypesCollector typesCollector)
        {
            this.descendant = typesCollector.DbDescendantType;
            this.dbContext = typesCollector.DbContextType;
            this.typesCollector = typesCollector;
        }

        public virtual object Build()
        {
            object obj2;
            IDXTypeInfo dbDescendantInfo = this.TypesCollector.DbDescendantInfo;
            if (dbDescendantInfo == null)
            {
                return null;
            }
            base.InstanceActivator = null;
            try
            {
                this.BuildCore(dbDescendantInfo);
                return base.DescendantInstance;
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
                obj2 = null;
            }
            catch (Exception)
            {
                if (!this.SuppressExceptions)
                {
                    throw;
                }
                obj2 = null;
            }
            return obj2;
        }

        protected virtual void BuildCore(IDXTypeInfo typeInfo)
        {
            Type contextType = typeInfo.ResolveType();
            base.InstanceActivator = this.CreateContextInstance(contextType);
        }

        protected DescendantInstanceActivator CreateContextInstance(Type contextType)
        {
            Func<ConstructorInfo, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = c => c.GetParameters().Length == 0;
            }
            ConstructorInfo constructor = contextType.GetConstructors().FirstOrDefault<ConstructorInfo>(predicate);
            if (constructor != null)
            {
                return new DescendantInstanceActivator(null, Expression.New(constructor));
            }
            TypeBuilder newContextTypeBuilder = this.CreateDynamicAssembly().DefineType(contextType.FullName, TypeAttributes.Public, contextType);
            this.CreateDefaultConstructor(contextType, newContextTypeBuilder);
            Type type = newContextTypeBuilder.CreateType();
            return new DescendantInstanceActivator(this.TempFolder, Expression.New(type));
        }

        protected void CreateDefaultConstructor(Type contextType, TypeBuilder newContextTypeBuilder)
        {
            Type[] typeArguments = new Type[] { contextType };
            ConstructorInfo constructor = this.TypesCollector.DbContextOptionsT.MakeGenericType(typeArguments).GetConstructor(new Type[0]);
            ConstructorInfo con = this.TypesCollector.DbContextType.GetConstructors().FirstOrDefault<ConstructorInfo>(c => (c.GetParameters().Length == 1) && (c.GetParameters()[0].ParameterType == this.TypesCollector.DbContextOptions));
            ILGenerator iLGenerator = newContextTypeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[0]).GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Newobj, constructor);
            iLGenerator.Emit(OpCodes.Call, con);
            iLGenerator.Emit(OpCodes.Nop);
            iLGenerator.Emit(OpCodes.Nop);
            iLGenerator.Emit(OpCodes.Ret);
        }

        protected ModuleBuilder CreateDynamicAssembly()
        {
            AssemblyName dynamicAssemblyName = this.GetDynamicAssemblyName();
            string fileName = dynamicAssemblyName.Name + ".dll";
            return AppDomain.CurrentDomain.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.RunAndSave, this.TempFolder).DefineDynamicModule(dynamicAssemblyName.Name, fileName);
        }

        protected AssemblyName GetDynamicAssemblyName() => 
            new AssemblyName("Assembly" + this.descendant.Name);

        public DevExpress.Entity.Model.TypesCollector TypesCollector =>
            this.typesCollector;

        public bool SuppressExceptions =>
            false;

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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultEFCoreDescendantBuilder.<>c <>9 = new DefaultEFCoreDescendantBuilder.<>c();
            public static Func<ConstructorInfo, bool> <>9__13_0;

            internal bool <CreateContextInstance>b__13_0(ConstructorInfo c) => 
                c.GetParameters().Length == 0;
        }
    }
}

