namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections;
    using System.Xml;

    public abstract class CollectionXmlPersistenceHelper : XmlPersistenceHelper
    {
        private ICollection collection;

        protected CollectionXmlPersistenceHelper(ICollection collection)
        {
            this.collection = collection;
        }

        protected virtual void AddItemToContext(object item, IXmlContext context)
        {
            ((DevExpress.Utils.Serializing.XmlContext) context).Elements.Add(this.CreateXmlContextItem(item));
        }

        protected abstract ObjectCollectionXmlLoader CreateObjectCollectionXmlLoader(System.Xml.XmlNode root);
        public override ObjectXmlLoader CreateObjectXmlLoader(System.Xml.XmlNode root) => 
            this.CreateObjectCollectionXmlLoader(root);

        protected virtual IXmlContext CreateXmlContext() => 
            new DevExpress.Utils.Serializing.XmlContext(this.XmlCollectionName);

        protected abstract IXmlContextItem CreateXmlContextItem(object obj);
        protected override IXmlContext GetXmlContext()
        {
            IXmlContext context = this.CreateXmlContext();
            this.InitXmlContext(context);
            return context;
        }

        protected virtual void InitXmlContext(IXmlContext context)
        {
            foreach (object obj2 in this.Collection)
            {
                this.AddItemToContext(obj2, context);
            }
        }

        protected ICollection Collection =>
            this.collection;

        protected abstract string XmlCollectionName { get; }
    }
}

