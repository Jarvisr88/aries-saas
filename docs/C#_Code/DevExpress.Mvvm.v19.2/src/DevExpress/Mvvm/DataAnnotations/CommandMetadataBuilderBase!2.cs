﻿namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;

    public abstract class CommandMetadataBuilderBase<T, TBuilder> : MemberMetadataBuilderBase<T, TBuilder, ClassMetadataBuilder<T>> where TBuilder: CommandMetadataBuilderBase<T, TBuilder>
    {
        internal CommandMetadataBuilderBase(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent) : base(storage, parent)
        {
        }

        public TBuilder AutoGenerated() => 
            base.AutoGeneratedCore();

        public TBuilder Description(string description) => 
            base.DescriptionCore(description);

        public TBuilder DisplayName(string name) => 
            base.DisplayNameCore(name);

        public TBuilder DisplayShortName(string shortName) => 
            base.DisplayShortNameCore(shortName);

        public TBuilder DoNotScaffold() => 
            base.DoNotScaffoldCore();

        public TBuilder DoNotScaffoldDetailCollection() => 
            base.DoNotScaffoldDetailCollectionCore();

        public MetadataBuilder<T> EndCommand() => 
            (MetadataBuilder<T>) base.parent;

        public TBuilder ImageName(string imageName) => 
            base.ImageNameCore(imageName);

        public TBuilder ImageUri(string imageUri) => 
            base.ImageUriCore(imageUri);

        public TBuilder ImageUriLarge(string uri) => 
            base.ImageUriLargeCore(uri);

        public TBuilder ImageUriSmall(string uri) => 
            base.ImageUriSmallCore(uri);

        public TBuilder LocatedAt(int position, PropertyLocation propertyLocation = 0) => 
            base.LocatedAtCore(position, propertyLocation);

        public TBuilder NotAutoGenerated() => 
            base.NotAutoGeneratedCore();

        public TBuilder Parameter<TParameter>(Expression<Func<T, TParameter>> propertyExpression) => 
            base.AddOrReplaceAttribute<CommandParameterAttribute>(new CommandParameterAttribute(MetadataBuilderBase<T, ClassMetadataBuilder<T>>.GetPropertyName<TParameter>(propertyExpression)));
    }
}

