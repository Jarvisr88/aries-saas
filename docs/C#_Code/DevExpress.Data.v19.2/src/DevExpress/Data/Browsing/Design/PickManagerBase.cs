namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public abstract class PickManagerBase
    {
        private readonly ActionExecutor executor;

        protected PickManagerBase();
        protected virtual void AddChildNodes(IList<IPropertyDescriptor> properties, object dataSource, string dataMember, IList nodes);
        public bool AreContainDummyNode(IList nodes);
        public static bool ContainsDummyNode(IList list);
        private INode CreateChildNode(object dataSource, string dataMember, string displayName, object owner, IPropertyDescriptor property);
        protected abstract INode CreateDataMemberNode(object dataSource, string dataMember, string displayName, bool isList, object owner, IPropertyDescriptor property);
        protected abstract INode CreateDataSourceNode(object dataSource, string dataMember, string name, object owner);
        protected abstract INode CreateDummyNode(object owner);
        protected abstract object CreateNoneNode(object owner);
        protected abstract IPropertiesProvider CreateProvider();
        protected static void DisposeObject(object obj);
        public void FillContent(IList nodes, IList dataSources, bool addNoneNode);
        public virtual void FillContent(IList nodes, Collection<Pair<object, string>> dataSources, bool addNoneNode);
        private void FillContentCore(IList nodes, object dataSource, string dataMember);
        public virtual void FillNodes(object dataSource, string dataMember, IList nodes);
        protected virtual Collection<Pair<object, string>> FilterDataSources(Collection<Pair<object, string>> dataSources);
        public virtual INode FindDataMemberNode(IList nodes, string dataMember);
        public object FindDataMemberNode(IList nodes, object dataSource, string dataMember);
        public virtual void FindDataMemberNode(IList nodes, string dataMember, Action<INode> callback);
        protected void FindDataMemberNodeCore(IList nodes, string dataMember, int i, Action<INode> callback, bool joinItems);
        protected INode FindNode(IList nodes, string dataMember);
        public object FindNoneNode(IList nodes);
        public INode FindSourceNode(IList nodes, object dataSource);
        protected virtual string GetDataMember(INode node);
        public virtual void GetDataSourceName(object dataSource, string dataMember, IPropertiesProvider provider, EventHandler<GetDataSourceDisplayNameEventArgs> callback);
        public INode GetDataSourceNode(INode node);
        protected string GetFullName(string dataMember, string name);
        private static void InvokeCallback(Action<INode> callback, INode node);
        private static bool IsListSource(object obj);
        protected abstract bool NodeIsEmpty(INode node);
        public void OnNodeExpand(object dataSource, INode node);
        protected virtual bool ShouldAddDummyNode(IPropertyDescriptor property);

        public ActionExecutor Executor { get; }
    }
}

