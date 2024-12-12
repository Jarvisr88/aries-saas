namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class Operand
    {
        public Operand(string path, RelativeSource source, string elementName, string resourceName, string referenceName, Type ancestorType, int ancestorLevel)
        {
            this.Path = path;
            this.Source = source;
            this.ElementName = elementName;
            this.ResourceName = resourceName;
            this.ReferenceName = referenceName;
            this.AncestorType = ancestorType;
            this.AncestorLevel = ancestorLevel;
            this.IsTwoWay = false;
        }

        internal static Operand CreateOperand(string path, NRelative n, Func<NType, Type> resolveType) => 
            ((path != null) || (n != null)) ? new Operand(((path != null) || (n == null)) ? path : string.Empty, GetRelativeSource(n), n?.ElementName, n?.ResourceName, n?.ReferenceName, GetAncestorType(n, resolveType), GetAncestorLevel(n)) : null;

        public override bool Equals(object obj)
        {
            Operand operand = obj as Operand;
            return ((operand != null) ? ((this.Path == operand.Path) && ((this.Source == operand.Source) && ((this.ElementName == operand.ElementName) && ((this.ResourceName == operand.ResourceName) && ((this.ReferenceName == operand.ReferenceName) && ((this.AncestorType == operand.AncestorType) && (this.AncestorLevel == operand.AncestorLevel))))))) : false);
        }

        private static int GetAncestorLevel(NRelative n)
        {
            if ((n == null) || (n.Kind != NRelative.NKind.Ancestor))
            {
                return 0;
            }
            int? ancestorLevel = n.AncestorLevel;
            return ((ancestorLevel != null) ? ancestorLevel.GetValueOrDefault() : 1);
        }

        private static Type GetAncestorType(NRelative n, Func<NType, Type> resolveType) => 
            ((n == null) || (n.Kind != NRelative.NKind.Ancestor)) ? null : resolveType(n.AncestorType);

        public override int GetHashCode() => 
            (((((((this.Path != null) ? this.Path.GetHashCode() : 7) ^ ((this.ElementName != null) ? this.ElementName.GetHashCode() : 13)) ^ ((this.ResourceName != null) ? this.ResourceName.GetHashCode() : 0x11)) ^ ((this.ReferenceName != null) ? this.ReferenceName.GetHashCode() : 0x17)) ^ ((this.AncestorType != null) ? this.AncestorType.GetHashCode() : 0x1b)) ^ this.AncestorLevel.GetHashCode()) ^ this.Source.GetHashCode();

        private static RelativeSource GetRelativeSource(NRelative n)
        {
            if (n == null)
            {
                return RelativeSource.Context;
            }
            switch (n.Kind)
            {
                case NRelative.NKind.Context:
                    return RelativeSource.Context;

                case NRelative.NKind.Self:
                    return RelativeSource.Self;

                case NRelative.NKind.Parent:
                    return RelativeSource.Parent;

                case NRelative.NKind.Element:
                    return RelativeSource.Element;

                case NRelative.NKind.Resource:
                    return RelativeSource.Resource;

                case NRelative.NKind.Reference:
                    return RelativeSource.Reference;

                case NRelative.NKind.Ancestor:
                    return RelativeSource.Ancestor;
            }
            throw new NotImplementedException();
        }

        internal void SetBackConverter(Func<object[], object> backConverter)
        {
            this.BackConverter = backConverter;
        }

        public void SetMode(bool isTwoWay)
        {
            this.IsTwoWay = isTwoWay;
        }

        public string Path { get; private set; }

        public RelativeSource Source { get; private set; }

        public string ElementName { get; private set; }

        public string ResourceName { get; private set; }

        public string ReferenceName { get; private set; }

        public Type AncestorType { get; private set; }

        public int AncestorLevel { get; private set; }

        public bool IsTwoWay { get; private set; }

        public Func<object[], object> BackConverter { get; private set; }

        public enum RelativeSource
        {
            Context,
            Self,
            Parent,
            Element,
            Resource,
            Reference,
            Ancestor
        }
    }
}

