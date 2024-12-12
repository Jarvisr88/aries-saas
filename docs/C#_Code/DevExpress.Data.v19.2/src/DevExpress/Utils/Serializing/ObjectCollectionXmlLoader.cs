namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections;
    using System.Xml;

    public abstract class ObjectCollectionXmlLoader : ObjectXmlLoader
    {
        protected ObjectCollectionXmlLoader(System.Xml.XmlNode root) : base(root)
        {
        }

        protected abstract void AddObjectToCollection(object obj);
        protected abstract void ClearCollectionObjects();
        protected abstract object LoadObject(System.Xml.XmlNode root);
        public override object ObjectFromXml()
        {
            this.ClearCollectionObjects();
            DXXmlNodeCollection childNodes = base.GetChildNodes(this.XmlCollectionName);
            if (childNodes.Count > 0)
            {
                foreach (System.Xml.XmlNode node in childNodes[0].ChildNodes)
                {
                    object obj2 = this.LoadObject(node);
                    if (obj2 != null)
                    {
                        this.AddObjectToCollection(obj2);
                    }
                }
            }
            return this.Collection;
        }

        protected abstract ICollection Collection { get; }

        protected abstract string XmlCollectionName { get; }
    }
}

