namespace DevExpress.Xpf.Grid.Core.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class GridPropertySchemeController
    {
        private ObservablePropertySchemeNode[] scheme = new ObservablePropertySchemeNode[0];

        public GridPropertySchemeController(DataControlBase grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }
            this.<Grid>k__BackingField = grid;
        }

        private DataDependentEntityTree CreateDataDependentEntityGraph()
        {
            if (this.View == null)
            {
                return new DataDependentEntityTree();
            }
            List<DataDependentEntityNodeSource> source = new List<DataDependentEntityNodeSource>();
            List<DataDependentEntity> entities = new List<DataDependentEntity>();
            foreach (DataDependentEntityInfo info in this.View.GetDataDependentEntityInfo())
            {
                if (info.IsFixed)
                {
                    entities.Add(info.DataDependentEntity);
                    continue;
                }
                Func<string, bool> predicate = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<string, bool> local1 = <>c.<>9__19_0;
                    predicate = <>c.<>9__19_0 = path => !string.IsNullOrEmpty(path);
                }
                source.AddRange(from path in info.Path.Where<string>(predicate) select new DataDependentEntityNodeSource(path.Split(new char[] { '.' }), info.DataDependentEntity));
            }
            return new DataDependentEntityTree(CreateDataDependentEntityGraph(source), DataDependentEntity.Combine(entities));
        }

        private static DataDependentEntityNode[] CreateDataDependentEntityGraph(IEnumerable<DataDependentEntityNodeSource> source)
        {
            // Unresolved stack state at '00000087'
        }

        private ObservablePropertySchemeNode[] CreateScheme() => 
            this.Grid.ActualAllowComplexPropertyUpdates ? this.CreateScheme(this.DataDependentEntityGraph.Nodes) : new ObservablePropertySchemeNode[0];

        private ObservablePropertySchemeNode[] CreateScheme(DataDependentEntityNode[] dataDependentEntityNodes)
        {
            Func<DataDependentEntityNode, bool> predicate = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<DataDependentEntityNode, bool> local1 = <>c.<>9__18_0;
                predicate = <>c.<>9__18_0 = x => x.Children.Any<DataDependentEntityNode>();
            }
            return (from x in dataDependentEntityNodes.Where<DataDependentEntityNode>(predicate) select new ObservablePropertySchemeNode(x.PropertyName, this.CreateScheme(x.Children))).ToArray<ObservablePropertySchemeNode>();
        }

        public void OnDataSourceExtracted(BindingListAdapter bindingAdapter)
        {
            if (bindingAdapter != null)
            {
                bindingAdapter.ObservablePropertyScheme = this.Scheme;
            }
        }

        public void UpdateProperties()
        {
            if (this.Grid.IsInitialized)
            {
                this.DataDependentEntityGraph = this.CreateDataDependentEntityGraph();
                this.Scheme = this.CreateScheme();
            }
        }

        public DataControlBase Grid { get; }

        private DataViewBase View =>
            this.Grid.viewCore;

        public ObservablePropertySchemeNode[] Scheme
        {
            get => 
                this.scheme;
            private set
            {
                ObservablePropertySchemeNode[] nodeArray = value ?? new ObservablePropertySchemeNode[0];
                if (this.scheme != nodeArray)
                {
                    this.scheme = nodeArray;
                    if (this.View != null)
                    {
                        this.View.AttachObservablePropertyScheme(this.Scheme);
                    }
                }
            }
        }

        public DataDependentEntityTree DataDependentEntityGraph { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridPropertySchemeController.<>c <>9 = new GridPropertySchemeController.<>c();
            public static Func<DataDependentEntityNode, bool> <>9__18_0;
            public static Func<string, bool> <>9__19_0;
            public static Func<GridPropertySchemeController.DataDependentEntityNodeSource, bool> <>9__20_0;
            public static Func<GridPropertySchemeController.DataDependentEntityNodeSource, string> <>9__20_1;
            public static Func<GridPropertySchemeController.DataDependentEntityNodeSource, GridPropertySchemeController.DataDependentEntityNodeSource> <>9__20_2;
            public static Func<GridPropertySchemeController.DataDependentEntityNodeSource, DataDependentEntity> <>9__20_4;
            public static Func<IGrouping<string, GridPropertySchemeController.DataDependentEntityNodeSource>, DataDependentEntityNode> <>9__20_3;

            internal bool <CreateDataDependentEntityGraph>b__19_0(string path) => 
                !string.IsNullOrEmpty(path);

            internal bool <CreateDataDependentEntityGraph>b__20_0(GridPropertySchemeController.DataDependentEntityNodeSource sourceItem) => 
                sourceItem.Path.Any<string>();

            internal string <CreateDataDependentEntityGraph>b__20_1(GridPropertySchemeController.DataDependentEntityNodeSource sourceItem) => 
                sourceItem.Path.First<string>();

            internal GridPropertySchemeController.DataDependentEntityNodeSource <CreateDataDependentEntityGraph>b__20_2(GridPropertySchemeController.DataDependentEntityNodeSource sourceItem) => 
                new GridPropertySchemeController.DataDependentEntityNodeSource(sourceItem.Path.Skip<string>(1), sourceItem.Entity);

            internal DataDependentEntityNode <CreateDataDependentEntityGraph>b__20_3(IGrouping<string, GridPropertySchemeController.DataDependentEntityNodeSource> group)
            {
                Func<GridPropertySchemeController.DataDependentEntityNodeSource, DataDependentEntity> selector = <>9__20_4;
                if (<>9__20_4 == null)
                {
                    Func<GridPropertySchemeController.DataDependentEntityNodeSource, DataDependentEntity> local1 = <>9__20_4;
                    selector = <>9__20_4 = x => x.Entity;
                }
                return new DataDependentEntityNode(group.Key, DataDependentEntity.Combine(group.Select<GridPropertySchemeController.DataDependentEntityNodeSource, DataDependentEntity>(selector)), GridPropertySchemeController.CreateDataDependentEntityGraph(group));
            }

            internal DataDependentEntity <CreateDataDependentEntityGraph>b__20_4(GridPropertySchemeController.DataDependentEntityNodeSource x) => 
                x.Entity;

            internal bool <CreateScheme>b__18_0(DataDependentEntityNode x) => 
                x.Children.Any<DataDependentEntityNode>();
        }

        private class DataDependentEntityNodeSource
        {
            public DataDependentEntityNodeSource(IEnumerable<string> path, DataDependentEntity entity)
            {
                this.Path = path;
                this.Entity = entity;
            }

            public IEnumerable<string> Path { get; private set; }

            public DataDependentEntity Entity { get; private set; }
        }
    }
}

