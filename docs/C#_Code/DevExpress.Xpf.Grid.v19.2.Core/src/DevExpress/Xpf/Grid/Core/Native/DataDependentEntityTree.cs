namespace DevExpress.Xpf.Grid.Core.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class DataDependentEntityTree
    {
        private readonly DataDependentEntityNode[] nodes;
        private readonly DataDependentEntity fixedEntity;
        private Dictionary<string, DataDependentEntity> propertyChangeDependencyCache;

        public DataDependentEntityTree() : this(new DataDependentEntityNode[0])
        {
        }

        public DataDependentEntityTree(DataDependentEntityNode[] nodes) : this(nodes, new DataDependentEntity())
        {
        }

        public DataDependentEntityTree(DataDependentEntityNode[] nodes, DataDependentEntity fixedEntity)
        {
            this.propertyChangeDependencyCache = new Dictionary<string, DataDependentEntity>();
            this.nodes = nodes;
            this.fixedEntity = fixedEntity;
        }

        private static void CollectDependentEntities(List<DataDependentEntity> dependentEntities, DataDependentEntityNode node)
        {
            dependentEntities.Add(node.Entity);
            foreach (DataDependentEntityNode node2 in node.Children)
            {
                CollectDependentEntities(dependentEntities, node2);
            }
        }

        private static DataDependentEntityNode FindPropertyDependentNode(string propertyName, DataDependentEntityNode[] nodes)
        {
            char[] separator = new char[] { '.' };
            return FindPropertyDependentNode(propertyName.Split(separator), 0, nodes);
        }

        private static DataDependentEntityNode FindPropertyDependentNode(string[] descriptorNames, int level, DataDependentEntityNode[] nodes)
        {
            string str = SafeGetDescriptorName(descriptorNames, level);
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            DataDependentEntityNode node = null;
            int index = 0;
            while (true)
            {
                if (index < nodes.Length)
                {
                    if (nodes[index].PropertyName != str)
                    {
                        index++;
                        continue;
                    }
                    node = nodes[index];
                }
                return ((node != null) ? ((level == (descriptorNames.Length - 1)) ? node : FindPropertyDependentNode(descriptorNames, level + 1, node.Children)) : null);
            }
        }

        private static DataDependentEntityNode[] FindPropertyDependentNodes(string changedPropertyName, DataDependentEntityNode[] rootNodes)
        {
            if (string.IsNullOrEmpty(changedPropertyName))
            {
                return rootNodes;
            }
            Func<DataDependentEntityNode, DataDependentEntityNode[]> evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<DataDependentEntityNode, DataDependentEntityNode[]> local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = x => x.YieldToArray<DataDependentEntityNode>();
            }
            return FindPropertyDependentNode(changedPropertyName, rootNodes).Return<DataDependentEntityNode, DataDependentEntityNode[]>(evaluator, (<>c.<>9__11_1 ??= () => new DataDependentEntityNode[0]));
        }

        public DataDependentEntity GetPropertyChangeDependency(string changedPropertyName)
        {
            string key = changedPropertyName;
            if (changedPropertyName == null)
            {
                string local1 = changedPropertyName;
                key = string.Empty;
            }
            return this.propertyChangeDependencyCache.GetOrAdd<string, DataDependentEntity>(key, delegate {
                List<DataDependentEntity> dependentEntities = new List<DataDependentEntity>();
                foreach (DataDependentEntityNode node in FindPropertyDependentNodes(changedPropertyName, this.Nodes))
                {
                    CollectDependentEntities(dependentEntities, node);
                }
                dependentEntities.Add(this.FixedEntity);
                return DataDependentEntity.Combine(dependentEntities);
            });
        }

        private static string SafeGetDescriptorName(string[] descriptorNames, int level) => 
            ((level >= descriptorNames.Length) || (level < 0)) ? null : descriptorNames[level];

        public DataDependentEntityNode[] Nodes =>
            this.nodes;

        public DataDependentEntity FixedEntity =>
            this.fixedEntity;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataDependentEntityTree.<>c <>9 = new DataDependentEntityTree.<>c();
            public static Func<DataDependentEntityNode, DataDependentEntityNode[]> <>9__11_0;
            public static Func<DataDependentEntityNode[]> <>9__11_1;

            internal DataDependentEntityNode[] <FindPropertyDependentNodes>b__11_0(DataDependentEntityNode x) => 
                x.YieldToArray<DataDependentEntityNode>();

            internal DataDependentEntityNode[] <FindPropertyDependentNodes>b__11_1() => 
                new DataDependentEntityNode[0];
        }
    }
}

