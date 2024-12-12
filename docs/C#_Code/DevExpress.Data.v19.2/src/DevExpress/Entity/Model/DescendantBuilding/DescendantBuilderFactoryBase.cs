namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Collections.Generic;

    public class DescendantBuilderFactoryBase
    {
        private List<DescendantBuilderProvider> providers = new List<DescendantBuilderProvider>();

        protected void Add(DescendantBuilderProvider provider)
        {
            this.providers.Add(provider);
        }

        public virtual IDbDescendantBuilder GetDbDescendantBuilder(IDXTypeInfo dXTypeInfo, ISolutionTypesProvider typesProvider)
        {
            IDbDescendantBuilder builder;
            DXTypeInfo typeInfo = dXTypeInfo as DXTypeInfo;
            if (typeInfo == null)
            {
                return null;
            }
            TypesCollector typesCollector = new TypesCollector(typeInfo);
            if ((typesCollector.DbContextType == null) || (typesCollector.DbDescendantType == null))
            {
                return null;
            }
            using (List<DescendantBuilderProvider>.Enumerator enumerator = this.providers.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DescendantBuilderProvider current = enumerator.Current;
                        if (!current.Available(typesCollector.DbContextType, typesCollector.DbDescendantInfo, typesProvider))
                        {
                            continue;
                        }
                        builder = current.GetBuilder(typesCollector, typesProvider);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return builder;
        }

        public void Initialize()
        {
            this.InitializeProviders();
        }

        protected virtual void InitializeProviders()
        {
            this.Add(new DefaultDescendantBuilderProvider());
            this.Add(new SqlCeDescendantBuilderProvider());
            this.Add(new SqlClientDescendantBuilderProvider());
        }
    }
}

