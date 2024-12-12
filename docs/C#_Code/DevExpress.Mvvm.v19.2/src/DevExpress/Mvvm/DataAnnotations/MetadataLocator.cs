namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class MetadataLocator : IMetadataLocator
    {
        public static IMetadataLocator Default;
        private readonly IEnumerable<Tuple<Type, Type>> infoList;
        private IDictionary<Type, Type[]> dictionary;

        private MetadataLocator(IEnumerable<Tuple<Type, Type>> infoList)
        {
            this.infoList = infoList;
            MetadataHelper.RegisterLocator(this);
        }

        public MetadataLocator AddMetadata<TMetadata>() => 
            this.AddMetadata(typeof(TMetadata));

        public MetadataLocator AddMetadata<T, TMetadata>() => 
            this.AddMetadata(typeof(T), typeof(TMetadata));

        private MetadataLocator AddMetadata(IEnumerable<Tuple<Type, Type>> newInfoList)
        {
            MetadataHelper.CheckMetadata(newInfoList);
            return new MetadataLocator(this.infoList.Union<Tuple<Type, Type>>(newInfoList));
        }

        public MetadataLocator AddMetadata(Type metadataType) => 
            this.AddMetadata(MetadataHelper.GetMetadataInfoList(metadataType));

        public MetadataLocator AddMetadata(Type type, Type metadataType)
        {
            Tuple<Type, Type> tuple = new Tuple<Type, Type>(type, metadataType);
            Tuple<Type, Type>[] newInfoList = new Tuple<Type, Type>[] { tuple };
            return this.AddMetadata(newInfoList);
        }

        public static MetadataLocator Create() => 
            new MetadataLocator(Enumerable.Empty<Tuple<Type, Type>>());

        Type[] IMetadataLocator.GetMetadataTypes(Type type)
        {
            Type[] typeArray;
            this.Dictionary.TryGetValue(type, out typeArray);
            return typeArray;
        }

        internal void Update()
        {
            this.dictionary = null;
        }

        private IDictionary<Type, Type[]> Dictionary
        {
            get
            {
                IDictionary<Type, Type[]> dictionary = this.dictionary;
                if (this.dictionary == null)
                {
                    IDictionary<Type, Type[]> local1 = this.dictionary;
                    Func<Tuple<Type, Type>, Type> keySelector = <>c.<>9__5_0;
                    if (<>c.<>9__5_0 == null)
                    {
                        Func<Tuple<Type, Type>, Type> local2 = <>c.<>9__5_0;
                        keySelector = <>c.<>9__5_0 = x => x.Item1;
                    }
                    IEnumerable<IGrouping<Type, Type>> enumerable1 = MetadataHelper.InternalMetadataProviders.Union<Tuple<Type, Type>>(this.infoList).GroupBy<Tuple<Type, Type>, Type, Type>(keySelector, <>c.<>9__5_1 ??= x => x.Item2);
                    if (<>c.<>9__5_2 == null)
                    {
                        IEnumerable<IGrouping<Type, Type>> local4 = MetadataHelper.InternalMetadataProviders.Union<Tuple<Type, Type>>(this.infoList).GroupBy<Tuple<Type, Type>, Type, Type>(keySelector, <>c.<>9__5_1 ??= x => x.Item2);
                        enumerable1 = (IEnumerable<IGrouping<Type, Type>>) (<>c.<>9__5_2 = x => x.Key);
                    }
                    dictionary = MetadataHelper.InternalMetadataProviders.Union<Tuple<Type, Type>>(this.infoList).GroupBy<Tuple<Type, Type>, Type, Type>(keySelector, (<>c.<>9__5_1 ??= x => x.Item2)).dictionary = MetadataHelper.InternalMetadataProviders.Union<Tuple<Type, Type>>(this.infoList).GroupBy<Tuple<Type, Type>, Type, Type>(keySelector, (<>c.<>9__5_1 ??= x => x.Item2)).ToDictionary<IGrouping<Type, Type>, Type, Type[]>((Func<IGrouping<Type, Type>, Type>) enumerable1, <>c.<>9__5_3 ??= x => x.ToArray<Type>());
                }
                return dictionary;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MetadataLocator.<>c <>9 = new MetadataLocator.<>c();
            public static Func<Tuple<Type, Type>, Type> <>9__5_0;
            public static Func<Tuple<Type, Type>, Type> <>9__5_1;
            public static Func<IGrouping<Type, Type>, Type> <>9__5_2;
            public static Func<IGrouping<Type, Type>, Type[]> <>9__5_3;

            internal Type <get_Dictionary>b__5_0(Tuple<Type, Type> x) => 
                x.Item1;

            internal Type <get_Dictionary>b__5_1(Tuple<Type, Type> x) => 
                x.Item2;

            internal Type <get_Dictionary>b__5_2(IGrouping<Type, Type> x) => 
                x.Key;

            internal Type[] <get_Dictionary>b__5_3(IGrouping<Type, Type> x) => 
                x.ToArray<Type>();
        }
    }
}

