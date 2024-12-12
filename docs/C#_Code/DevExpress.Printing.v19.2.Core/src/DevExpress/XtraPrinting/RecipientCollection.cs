namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Design;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    [ListBindable(BindableSupport.No), TypeConverter(typeof(CollectionTypeConverter))]
    public class RecipientCollection : Collection<Recipient>
    {
        public void AddRange(Recipient[] items)
        {
            foreach (Recipient recipient in items)
            {
                if (!recipient.EmptyNameAndAddress)
                {
                    base.Add(recipient);
                }
            }
        }

        internal void CopyFrom(RecipientCollection source)
        {
            base.Clear();
            this.AddRange(source.ToArray());
        }

        internal Recipient GetByAddress(string recipientAddress) => 
            base.Items.FirstOrDefault<Recipient>(recipient => recipient.Address == recipientAddress);

        internal List<Recipient> GetCorrectData()
        {
            List<Recipient> list = new List<Recipient>();
            foreach (Recipient recipient in this)
            {
                if (!recipient.EmptyNameAndAddress)
                {
                    Recipient item = recipient.Clone();
                    item.CorrectData();
                    list.Add(item);
                }
            }
            return list;
        }

        protected Recipient[] ToArray()
        {
            Recipient[] array = new Recipient[base.Count];
            base.CopyTo(array, 0);
            return array;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Recipient this[string recipientAddress] =>
            this.GetByAddress(recipientAddress);
    }
}

