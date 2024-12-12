namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class EFCoreContainerInfo : RuntimeWrapper, IEntityContainerInfo
    {
        private readonly string name;
        private readonly object dbContextInstance;
        private List<IEntitySetInfo> entitySets;
        private readonly IEnumerable<IEntityFunctionInfo> entityFunctions;

        public EFCoreContainerInfo(object finder, object dbContextInstance) : base("Microsoft.Data.Entity.Internal.DbSetFinder", finder)
        {
            this.entityFunctions = Enumerable.Empty<IEntityFunctionInfo>();
            this.name = dbContextInstance.GetType().Name;
            this.dbContextInstance = dbContextInstance;
        }

        private void InitEntitySets()
        {
            Type type1;
            MethodInfo local3;
            MethodAccessor methodAccessor = base.GetMethodAccessor("FindSets");
            object obj1 = base.Value;
            if (obj1 != null)
            {
                type1 = obj1.GetType();
            }
            else
            {
                object local1 = obj1;
                type1 = null;
            }
            Type type = type1;
            if (type == null)
            {
                local3 = null;
            }
            else
            {
                Func<MethodInfo, bool> predicate = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<MethodInfo, bool> local2 = <>c.<>9__11_0;
                    predicate = <>c.<>9__11_0 = x => (x.Name == "FindSets") && !x.IsGenericMethod;
                }
                local3 = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase).FirstOrDefault<MethodInfo>(predicate);
            }
            MethodInfo info = local3;
            IEnumerable enumerable = null;
            enumerable = ((info == null) || !(info.GetParameters().FirstOrDefault<ParameterInfo>().ParameterType == typeof(Type))) ? (methodAccessor.Invoke(base.Value, () => new object[] { this.dbContextInstance }) as IEnumerable) : (methodAccessor.Invoke(base.Value, (Func<object[]>) (() => new Type[] { this.dbContextInstance.GetType() })) as IEnumerable);
            if (enumerable != null)
            {
                this.entitySets = new List<IEntitySetInfo>();
                foreach (object obj2 in enumerable)
                {
                    this.entitySets.Add(new DbSetPropertyInfo(obj2, this));
                }
            }
        }

        public string Name =>
            this.name;

        public IEnumerable<IEntitySetInfo> EntitySets
        {
            get
            {
                if (this.entitySets == null)
                {
                    this.InitEntitySets();
                }
                return this.entitySets;
            }
        }

        public IEnumerable<IEntityFunctionInfo> EntityFunctions =>
            this.entityFunctions;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EFCoreContainerInfo.<>c <>9 = new EFCoreContainerInfo.<>c();
            public static Func<MethodInfo, bool> <>9__11_0;

            internal bool <InitEntitySets>b__11_0(MethodInfo x) => 
                (x.Name == "FindSets") && !x.IsGenericMethod;
        }
    }
}

