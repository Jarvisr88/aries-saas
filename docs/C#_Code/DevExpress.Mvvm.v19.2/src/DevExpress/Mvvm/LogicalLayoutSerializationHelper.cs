namespace DevExpress.Mvvm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    public static class LogicalLayoutSerializationHelper
    {
        private static LogicalNode BuildTree(IDocument document, ISupportLogicalLayout primaryViewModel, LogicalNode parent = null, bool includeNonSerializable = false)
        {
            List<object> viewModels = new List<object> {
                primaryViewModel
            };
            if (primaryViewModel.LookupViewModels != null)
            {
                viewModels.AddRange(primaryViewModel.LookupViewModels);
            }
            LogicalNode node = new LogicalNode(parent, document, primaryViewModel.DocumentManagerService, primaryViewModel);
            Func<LogicalNode, ISupportLogicalLayout> selector = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<LogicalNode, ISupportLogicalLayout> local1 = <>c.<>9__15_0;
                selector = <>c.<>9__15_0 = n => n.PrimaryViewModel;
            }
            if (!GetPath(node).Skip<LogicalNode>(1).Select<LogicalNode, ISupportLogicalLayout>(selector).ToList<ISupportLogicalLayout>().Contains(primaryViewModel))
            {
                var func2 = <>c.<>9__15_1;
                if (<>c.<>9__15_1 == null)
                {
                    var local2 = <>c.<>9__15_1;
                    func2 = <>c.<>9__15_1 = childDoc => new { 
                        childDoc = childDoc,
                        childViewModel = childDoc.Content as ISupportLogicalLayout
                    };
                }
                var predicate = <>c.<>9__15_3;
                if (<>c.<>9__15_3 == null)
                {
                    var local3 = <>c.<>9__15_3;
                    predicate = <>c.<>9__15_3 = <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.childViewModel != null;
                }
                node.Children.AddRange(from <>h__TransparentIdentifier0 in (from <>h__TransparentIdentifier0 in primaryViewModel.GetImmediateChildren(viewModels).Select(func2)
                    where <>h__TransparentIdentifier0.childDoc != document
                    select <>h__TransparentIdentifier0).Where(predicate)
                    where includeNonSerializable || <>h__TransparentIdentifier0.childViewModel.CanSerialize
                    select BuildTree(<>h__TransparentIdentifier0.childDoc, <>h__TransparentIdentifier0.childViewModel, node, includeNonSerializable));
            }
            return node;
        }

        private static IEnumerable<IDocument> CollectDocuments(LogicalNode tree)
        {
            List<IDocument> documents = new List<IDocument>();
            DepthFirstSearch(tree, delegate (LogicalNode n) {
                documents.Add(n.Document);
            });
            return documents;
        }

        private static void DepthFirstSearch(LogicalNode tree, Action<LogicalNode> action)
        {
            tree.Children.ForEach(c => DepthFirstSearch(c, action));
            action(tree);
        }

        public static Dictionary<string, string> Deserialize(string serialized)
        {
            List<StringPair> source = DeserializeDataContract<List<StringPair>>(serialized);
            if (source == null)
            {
                return new Dictionary<string, string>();
            }
            Func<StringPair, string> keySelector = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<StringPair, string> local1 = <>c.<>9__35_0;
                keySelector = <>c.<>9__35_0 = p => p.Key;
            }
            return source.ToDictionary<StringPair, string, string>(keySelector, (<>c.<>9__35_1 ??= p => p.Value));
        }

        private static T Deserialize<T>(XmlSerializer serializer, string value)
        {
            Func<XmlSerializer, Stream, object> deserialize = <>c__5<T>.<>9__5_0;
            if (<>c__5<T>.<>9__5_0 == null)
            {
                Func<XmlSerializer, Stream, object> local1 = <>c__5<T>.<>9__5_0;
                deserialize = <>c__5<T>.<>9__5_0 = (s, stream) => s.Deserialize(stream);
            }
            return Deserialize<XmlSerializer, T>(serializer, value, deserialize);
        }

        private static T Deserialize<S, T>(S serializer, string value, Func<S, Stream, object> deserialize)
        {
            T local;
            try
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
                {
                    local = (T) deserialize(serializer, stream);
                }
            }
            catch
            {
                local = default(T);
            }
            return local;
        }

        private static void Deserialize(object content, string state, string stateList)
        {
            List<string> list;
            Type[] iSupportLogicalLayout = GetISupportLogicalLayout(content);
            if (stateList != null)
            {
                list = Deserialize<List<string>>(new XmlSerializer(typeof(List<string>)), stateList);
            }
            else
            {
                List<string> list1 = new List<string>();
                list1.Add(state);
                list = list1;
            }
            if (iSupportLogicalLayout.Length == list.Count)
            {
                for (int i = 0; i < iSupportLogicalLayout.Length; i++)
                {
                    Deserialize(content, iSupportLogicalLayout[i], list[i]);
                }
            }
        }

        private static void Deserialize(object content, Type logicalLayoutType, string state)
        {
            object obj2 = Deserialize<object>(new XmlSerializer(logicalLayoutType.GetGenericArguments().Single<Type>()), state);
            object[] arguments = new object[] { obj2 };
            InvokeInterfaceMethod(content, logicalLayoutType, "RestoreState", arguments);
        }

        private static T DeserializeDataContract<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            Func<DataContractSerializer, Stream, object> deserialize = <>c__6<T>.<>9__6_0;
            if (<>c__6<T>.<>9__6_0 == null)
            {
                Func<DataContractSerializer, Stream, object> local1 = <>c__6<T>.<>9__6_0;
                deserialize = <>c__6<T>.<>9__6_0 = (s, stream) => s.ReadObject(stream);
            }
            return Deserialize<DataContractSerializer, T>(new DataContractSerializer(typeof(T)), value, deserialize);
        }

        private static IDocument GetDocument(object viewModel)
        {
            object parent = GetParent(viewModel);
            return ((parent != null) ? GetService(parent).Documents.FirstOrDefault<IDocument>(d => (d.Content == viewModel)) : null);
        }

        [IteratorStateMachine(typeof(<GetImmediateChildren>d__26))]
        public static IEnumerable<IDocument> GetImmediateChildren(this ISupportLogicalLayout parent, IEnumerable<object> viewModels = null)
        {
            <GetImmediateChildren>d__26 d__1 = new <GetImmediateChildren>d__26(-2);
            d__1.<>3__parent = parent;
            d__1.<>3__viewModels = viewModels;
            return d__1;
        }

        private static Type[] GetISupportLogicalLayout(object content)
        {
            if (content == null)
            {
                return null;
            }
            Func<Type, bool> predicate = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__25_0;
                predicate = <>c.<>9__25_0 = t => t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(ISupportLogicalLayout<>));
            }
            return content.GetType().GetInterfaces().Where<Type>(predicate).ToArray<Type>();
        }

        public static List<IDocument> GetOrphanedDocuments(this ISupportLogicalLayout viewModel)
        {
            Func<LogicalNode, IDocument> selector = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<LogicalNode, IDocument> local1 = <>c.<>9__18_0;
                selector = <>c.<>9__18_0 = n => n.Document;
            }
            return TrimLogicalTree(BuildTree(null, viewModel, null, true), viewModel).Select<LogicalNode, IDocument>(selector).ToList<IDocument>();
        }

        private static List<LogicalNode> GetOrphanedLeafs(LogicalNode root, ISupportLogicalLayout viewModel)
        {
            List<LogicalNode> orphans = new List<LogicalNode>();
            VisitOrphans(root, delegate (LogicalNode n) {
                if (n.Document != null)
                {
                    orphans.Add(n);
                }
            });
            return orphans;
        }

        private static object GetParent(object viewModel)
        {
            ISupportParentViewModel objA = viewModel as ISupportParentViewModel;
            return (((objA == null) || ReferenceEquals(objA, objA.ParentViewModel)) ? null : objA.ParentViewModel);
        }

        [IteratorStateMachine(typeof(<GetPath>d__14))]
        private static IEnumerable<LogicalNode> GetPath(LogicalNode node)
        {
            <GetPath>d__14 d__1 = new <GetPath>d__14(-2);
            d__1.<>3__node = node;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetPath>d__10))]
        private static IEnumerable<object> GetPath(object viewModel)
        {
            <GetPath>d__10 d__1 = new <GetPath>d__10(-2);
            d__1.<>3__viewModel = viewModel;
            return d__1;
        }

        private static IDocumentManagerService GetService(object viewModel)
        {
            IDocumentManagerService documentManagerService = null;
            while (documentManagerService == null)
            {
                ISupportLogicalLayout layout = viewModel as ISupportLogicalLayout;
                documentManagerService = layout.DocumentManagerService;
                viewModel = GetParent(viewModel);
            }
            return documentManagerService;
        }

        private static object InvokeInterfaceMethod(object obj, Type interfaceType, string name, params object[] arguments)
        {
            InterfaceMapping interfaceMap = obj.GetType().GetInterfaceMap(interfaceType);
            MethodInfo info = null;
            for (int i = 0; i < interfaceMap.InterfaceMethods.Length; i++)
            {
                if (interfaceMap.InterfaceMethods[i].Name == name)
                {
                    info = interfaceMap.TargetMethods[i];
                }
            }
            return info.Invoke(obj, arguments);
        }

        public static void RestoreDocumentManagerService(this ISupportLogicalLayout parent, string state)
        {
            List<SerializedDocument> children = DeserializeDataContract<List<SerializedDocument>>(state);
            if (children != null)
            {
                RestoreDocumentManagerService(children, parent);
            }
        }

        private static void RestoreDocumentManagerService(List<SerializedDocument> children, ISupportLogicalLayout rootViewModel)
        {
            if (children.Count == 1)
            {
                SerializedDocument document = children.First<SerializedDocument>();
                if ((document.DocumentType == null) && (document.Children != null))
                {
                    children = document.Children;
                    Deserialize(rootViewModel, document.ViewModelState, document.ViewModelStateList);
                }
            }
            foreach (IDocument document2 in rootViewModel.GetImmediateChildren(null).ToList<IDocument>())
            {
                document2.Close(true);
            }
            foreach (SerializedDocument document3 in children)
            {
                IDocument document4 = rootViewModel.DocumentManagerService.CreateDocument(document3.DocumentType, null, rootViewModel);
                document4.Id = document3.DocumentId;
                document4.Title = document3.DocumentTitle;
                if (document3.IsVisible)
                {
                    document4.DestroyOnClose = false;
                    document4.Show();
                }
                ISupportLogicalLayout content = document4.Content as ISupportLogicalLayout;
                if (content != null)
                {
                    Deserialize(document4.Content, document3.ViewModelState, document3.ViewModelStateList);
                    RestoreDocumentManagerService(document3.Children, content);
                }
            }
        }

        [Obsolete("Use the RestoreDocumentManagerService extension method instead.")]
        public static void RestoreDocumentManagerService(string state, ISupportLogicalLayout parent)
        {
            parent.RestoreDocumentManagerService(state);
        }

        public static string Serialize(this ISupportLogicalLayout content) => 
            SerializeLogicalLayout(content);

        public static string Serialize(Dictionary<string, string> dictionary)
        {
            Func<KeyValuePair<string, string>, StringPair> selector = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<KeyValuePair<string, string>, StringPair> local1 = <>c.<>9__34_0;
                selector = <>c.<>9__34_0 = delegate (KeyValuePair<string, string> p) {
                    StringPair pair1 = new StringPair();
                    pair1.Key = p.Key;
                    pair1.Value = p.Value;
                    return pair1;
                };
            }
            return SerializeDataContract<List<StringPair>>(dictionary.Select<KeyValuePair<string, string>, StringPair>(selector).ToList<StringPair>());
        }

        private static string Serialize<T>(XmlSerializer serializer, T value)
        {
            Action<XmlSerializer, Stream, T> serialize = <>c__3<T>.<>9__3_0;
            if (<>c__3<T>.<>9__3_0 == null)
            {
                Action<XmlSerializer, Stream, T> local1 = <>c__3<T>.<>9__3_0;
                serialize = <>c__3<T>.<>9__3_0 = delegate (XmlSerializer s, Stream stream, T v) {
                    s.Serialize(stream, v);
                };
            }
            return Serialize<XmlSerializer, T>(serializer, value, serialize);
        }

        private static string Serialize<S, T>(S serializer, T value, Action<S, Stream, T> serialize)
        {
            string str;
            using (MemoryStream stream = new MemoryStream())
            {
                serialize(serializer, stream, value);
                stream.Seek(0L, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        private static string SerializeDataContract<T>(T value)
        {
            Action<DataContractSerializer, Stream, T> serialize = <>c__4<T>.<>9__4_0;
            if (<>c__4<T>.<>9__4_0 == null)
            {
                Action<DataContractSerializer, Stream, T> local1 = <>c__4<T>.<>9__4_0;
                serialize = <>c__4<T>.<>9__4_0 = delegate (DataContractSerializer s, Stream stream, T v) {
                    s.WriteObject(stream, v);
                };
            }
            return Serialize<DataContractSerializer, T>(new DataContractSerializer(typeof(T)), value, serialize);
        }

        public static string SerializeDocumentManagerService(this ISupportLogicalLayout viewModel) => 
            ((viewModel == null) || (viewModel.DocumentManagerService == null)) ? string.Empty : SerializeDataContract<List<SerializedDocument>>(SerializeViews(viewModel));

        private static string SerializeLogicalLayout(object content)
        {
            List<string> list = new List<string>();
            foreach (Type type in GetISupportLogicalLayout(content))
            {
                object obj2 = InvokeInterfaceMethod(content, type, "SaveState", new object[0]);
                list.Add(Serialize<object>(new XmlSerializer(type.GetGenericArguments().Single<Type>()), obj2));
            }
            return Serialize<List<string>>(new XmlSerializer(typeof(List<string>)), list);
        }

        private static SerializedDocument SerializeTree(LogicalNode node)
        {
            string viewModelStateList = SerializeLogicalLayout(node.PrimaryViewModel);
            string documentId = (node.Document != null) ? (node.Document.Id as string) : "";
            SerializedDocument document = new SerializedDocument(node.DocumentType, null, viewModelStateList, documentId, (node.Document != null) ? (node.Document.Title as string) : "", node.IsVisible);
            Func<SerializedDocument, bool> predicate = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<SerializedDocument, bool> local1 = <>c.<>9__17_0;
                predicate = <>c.<>9__17_0 = child => child != null;
            }
            document.Children.AddRange(node.Children.Select<LogicalNode, SerializedDocument>(new Func<LogicalNode, SerializedDocument>(LogicalLayoutSerializationHelper.SerializeTree)).Where<SerializedDocument>(predicate));
            return document;
        }

        private static List<SerializedDocument> SerializeViews(ISupportLogicalLayout viewModel)
        {
            LogicalNode root = BuildTree(null, viewModel, null, false);
            TrimLogicalTree(root, viewModel);
            SerializedDocument[] source = new SerializedDocument[] { SerializeTree(root) };
            return source.ToList<SerializedDocument>();
        }

        private static List<LogicalNode> TrimLogicalTree(LogicalNode root, ISupportLogicalLayout viewModel)
        {
            List<LogicalNode> list2;
            List<LogicalNode> list = new List<LogicalNode>();
            while ((list2 = GetOrphanedLeafs(root, viewModel)).Any<LogicalNode>())
            {
                list.AddRange(list2);
                Action<LogicalNode> action = <>c.<>9__20_0;
                if (<>c.<>9__20_0 == null)
                {
                    Action<LogicalNode> local1 = <>c.<>9__20_0;
                    action = <>c.<>9__20_0 = delegate (LogicalNode n) {
                        n.Cull();
                    };
                }
                list2.ForEach(action);
            }
            return list;
        }

        private static void VisitOrphans(LogicalNode tree, Action<LogicalNode> action)
        {
            DepthFirstSearch(tree, delegate (LogicalNode n) {
                if (!n.IsVisible && !n.Children.Any<LogicalNode>())
                {
                    action(n);
                }
            });
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LogicalLayoutSerializationHelper.<>c <>9 = new LogicalLayoutSerializationHelper.<>c();
            public static Func<LogicalLayoutSerializationHelper.LogicalNode, ISupportLogicalLayout> <>9__15_0;
            public static Func<IDocument, <>f__AnonymousType1<IDocument, ISupportLogicalLayout>> <>9__15_1;
            public static Func<<>f__AnonymousType1<IDocument, ISupportLogicalLayout>, bool> <>9__15_3;
            public static Func<LogicalLayoutSerializationHelper.SerializedDocument, bool> <>9__17_0;
            public static Func<LogicalLayoutSerializationHelper.LogicalNode, IDocument> <>9__18_0;
            public static Action<LogicalLayoutSerializationHelper.LogicalNode> <>9__20_0;
            public static Func<Type, bool> <>9__25_0;
            public static Func<KeyValuePair<string, string>, LogicalLayoutSerializationHelper.StringPair> <>9__34_0;
            public static Func<LogicalLayoutSerializationHelper.StringPair, string> <>9__35_0;
            public static Func<LogicalLayoutSerializationHelper.StringPair, string> <>9__35_1;

            internal ISupportLogicalLayout <BuildTree>b__15_0(LogicalLayoutSerializationHelper.LogicalNode n) => 
                n.PrimaryViewModel;

            internal <>f__AnonymousType1<IDocument, ISupportLogicalLayout> <BuildTree>b__15_1(IDocument childDoc) => 
                new { 
                    childDoc = childDoc,
                    childViewModel = childDoc.Content as ISupportLogicalLayout
                };

            internal bool <BuildTree>b__15_3(<>f__AnonymousType1<IDocument, ISupportLogicalLayout> <>h__TransparentIdentifier0) => 
                <>h__TransparentIdentifier0.childViewModel != null;

            internal string <Deserialize>b__35_0(LogicalLayoutSerializationHelper.StringPair p) => 
                p.Key;

            internal string <Deserialize>b__35_1(LogicalLayoutSerializationHelper.StringPair p) => 
                p.Value;

            internal bool <GetISupportLogicalLayout>b__25_0(Type t) => 
                t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(ISupportLogicalLayout<>));

            internal IDocument <GetOrphanedDocuments>b__18_0(LogicalLayoutSerializationHelper.LogicalNode n) => 
                n.Document;

            internal LogicalLayoutSerializationHelper.StringPair <Serialize>b__34_0(KeyValuePair<string, string> p)
            {
                LogicalLayoutSerializationHelper.StringPair pair1 = new LogicalLayoutSerializationHelper.StringPair();
                pair1.Key = p.Key;
                pair1.Value = p.Value;
                return pair1;
            }

            internal bool <SerializeTree>b__17_0(LogicalLayoutSerializationHelper.SerializedDocument child) => 
                child != null;

            internal void <TrimLogicalTree>b__20_0(LogicalLayoutSerializationHelper.LogicalNode n)
            {
                n.Cull();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<T>
        {
            public static readonly LogicalLayoutSerializationHelper.<>c__3<T> <>9;
            public static Action<XmlSerializer, Stream, T> <>9__3_0;

            static <>c__3()
            {
                LogicalLayoutSerializationHelper.<>c__3<T>.<>9 = new LogicalLayoutSerializationHelper.<>c__3<T>();
            }

            internal void <Serialize>b__3_0(XmlSerializer s, Stream stream, T v)
            {
                s.Serialize(stream, v);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<T>
        {
            public static readonly LogicalLayoutSerializationHelper.<>c__4<T> <>9;
            public static Action<DataContractSerializer, Stream, T> <>9__4_0;

            static <>c__4()
            {
                LogicalLayoutSerializationHelper.<>c__4<T>.<>9 = new LogicalLayoutSerializationHelper.<>c__4<T>();
            }

            internal void <SerializeDataContract>b__4_0(DataContractSerializer s, Stream stream, T v)
            {
                s.WriteObject(stream, v);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T>
        {
            public static readonly LogicalLayoutSerializationHelper.<>c__5<T> <>9;
            public static Func<XmlSerializer, Stream, object> <>9__5_0;

            static <>c__5()
            {
                LogicalLayoutSerializationHelper.<>c__5<T>.<>9 = new LogicalLayoutSerializationHelper.<>c__5<T>();
            }

            internal object <Deserialize>b__5_0(XmlSerializer s, Stream stream) => 
                s.Deserialize(stream);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__6<T>
        {
            public static readonly LogicalLayoutSerializationHelper.<>c__6<T> <>9;
            public static Func<DataContractSerializer, Stream, object> <>9__6_0;

            static <>c__6()
            {
                LogicalLayoutSerializationHelper.<>c__6<T>.<>9 = new LogicalLayoutSerializationHelper.<>c__6<T>();
            }

            internal object <DeserializeDataContract>b__6_0(DataContractSerializer s, Stream stream) => 
                s.ReadObject(stream);
        }

        [CompilerGenerated]
        private sealed class <GetImmediateChildren>d__26 : IEnumerable<IDocument>, IEnumerable, IEnumerator<IDocument>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IDocument <>2__current;
            private int <>l__initialThreadId;
            private ISupportLogicalLayout parent;
            public ISupportLogicalLayout <>3__parent;
            private IEnumerable<object> viewModels;
            public IEnumerable<object> <>3__viewModels;
            private IEnumerator<IDocument> <>7__wrap1;

            [DebuggerHidden]
            public <GetImmediateChildren>d__26(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        if (this.parent != null)
                        {
                            IDocumentManagerService documentManagerService = this.parent.DocumentManagerService;
                            if (this.viewModels == null)
                            {
                                List<object> list1 = new List<object>();
                                list1.Add(this.parent);
                                this.viewModels = list1;
                            }
                            if (this.parent.DocumentManagerService != null)
                            {
                                this.<>7__wrap1 = documentManagerService.Documents.GetEnumerator();
                                this.<>1__state = -3;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            IDocument current = this.<>7__wrap1.Current;
                            ISupportParentViewModel content = current.Content as ISupportParentViewModel;
                            if ((content == null) || !this.viewModels.Contains<object>(content.ParentViewModel))
                            {
                                continue;
                            }
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<IDocument> IEnumerable<IDocument>.GetEnumerator()
            {
                LogicalLayoutSerializationHelper.<GetImmediateChildren>d__26 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LogicalLayoutSerializationHelper.<GetImmediateChildren>d__26(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.parent = this.<>3__parent;
                d__.viewModels = this.<>3__viewModels;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Mvvm.IDocument>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            IDocument IEnumerator<IDocument>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetPath>d__10 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private object viewModel;
            public object <>3__viewModel;

            [DebuggerHidden]
            public <GetPath>d__10(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.viewModel = LogicalLayoutSerializationHelper.GetParent(this.viewModel);
                }
                if (this.viewModel == null)
                {
                    return false;
                }
                this.<>2__current = this.viewModel;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                LogicalLayoutSerializationHelper.<GetPath>d__10 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LogicalLayoutSerializationHelper.<GetPath>d__10(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.viewModel = this.<>3__viewModel;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetPath>d__14 : IEnumerable<LogicalLayoutSerializationHelper.LogicalNode>, IEnumerable, IEnumerator<LogicalLayoutSerializationHelper.LogicalNode>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private LogicalLayoutSerializationHelper.LogicalNode <>2__current;
            private int <>l__initialThreadId;
            private LogicalLayoutSerializationHelper.LogicalNode node;
            public LogicalLayoutSerializationHelper.LogicalNode <>3__node;

            [DebuggerHidden]
            public <GetPath>d__14(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.node = this.node.Parent;
                }
                if (this.node == null)
                {
                    return false;
                }
                this.<>2__current = this.node;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<LogicalLayoutSerializationHelper.LogicalNode> IEnumerable<LogicalLayoutSerializationHelper.LogicalNode>.GetEnumerator()
            {
                LogicalLayoutSerializationHelper.<GetPath>d__14 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LogicalLayoutSerializationHelper.<GetPath>d__14(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.node = this.<>3__node;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Mvvm.LogicalLayoutSerializationHelper.LogicalNode>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            LogicalLayoutSerializationHelper.LogicalNode IEnumerator<LogicalLayoutSerializationHelper.LogicalNode>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class LogicalNode
        {
            private ISupportLogicalLayout primaryViewModel;

            public LogicalNode(LogicalLayoutSerializationHelper.LogicalNode parent, IDocument document, IDocumentManagerService service, ISupportLogicalLayout primaryViewModel = null)
            {
                this.Parent = parent;
                this.Document = document;
                this.Service = service;
                this.Children = new List<LogicalLayoutSerializationHelper.LogicalNode>();
                this.primaryViewModel = primaryViewModel;
            }

            public void Cull()
            {
                if (this.Parent != null)
                {
                    this.Parent.Children.Remove(this);
                }
            }

            public LogicalLayoutSerializationHelper.LogicalNode Parent { get; private set; }

            public IDocument Document { get; private set; }

            public IDocumentManagerService Service { get; private set; }

            public List<LogicalLayoutSerializationHelper.LogicalNode> Children { get; private set; }

            private IDocumentInfo DocumentInfo =>
                this.Document as IDocumentInfo;

            public string DocumentType =>
                this.DocumentInfo?.DocumentType;

            public bool IsVisible =>
                (this.DocumentInfo != null) && (this.DocumentInfo.State == DocumentState.Visible);

            public ISupportLogicalLayout PrimaryViewModel =>
                this.primaryViewModel ?? (this.Document.Content as ISupportLogicalLayout);
        }

        [DataContract]
        private class SerializedDocument
        {
            public SerializedDocument()
            {
            }

            public SerializedDocument(string documentType, string viewModelState, string viewModelStateList, string documentId, string documentTitle, bool isVisible)
            {
                this.DocumentType = documentType;
                this.ViewModelState = viewModelState;
                this.ViewModelStateList = viewModelStateList;
                this.DocumentId = documentId;
                this.DocumentTitle = documentTitle;
                this.IsVisible = isVisible;
                this.Children = new List<LogicalLayoutSerializationHelper.SerializedDocument>();
            }

            [DataMember]
            public string DocumentType { get; set; }

            [DataMember]
            public string ViewModelState { get; set; }

            [DataMember]
            public string ViewModelStateList { get; set; }

            [DataMember]
            public string DocumentId { get; set; }

            [DataMember]
            public string DocumentTitle { get; set; }

            [DataMember]
            public bool IsVisible { get; set; }

            [DataMember]
            public List<LogicalLayoutSerializationHelper.SerializedDocument> Children { get; set; }
        }

        [DataContract]
        private class StringPair
        {
            [DataMember]
            public string Key { get; set; }

            [DataMember]
            public string Value { get; set; }
        }
    }
}

