﻿namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class MemberMetadataBuilderBase<T, TBuilder, TParent> : IPropertyMetadataBuilder, IAttributeBuilderInternal, IAttributeBuilderInternal<TBuilder> where TBuilder: MemberMetadataBuilderBase<T, TBuilder, TParent> where TParent: MetadataBuilderBase<T, TParent>
    {
        private readonly MemberMetadataStorage storage;
        protected internal readonly TParent parent;

        internal MemberMetadataBuilderBase(MemberMetadataStorage storage, TParent parent)
        {
            this.storage = storage;
            this.parent = parent;
        }

        internal TBuilder AddAttribute(Attribute attribute)
        {
            this.storage.AddAttribute(attribute);
            return (TBuilder) this;
        }

        internal TBuilder AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue = null) where TAttribute: Attribute, new()
        {
            this.storage.AddOrModifyAttribute<TAttribute>(setAttributeValue);
            return (TBuilder) this;
        }

        internal TBuilder AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute
        {
            this.storage.AddOrReplaceAttribute<TAttribute>(attribute);
            return (TBuilder) this;
        }

        protected TBuilder AutoGeneratedCore() => 
            DataAnnotationsAttributeHelper.AutoGeneratedCore<TBuilder>((TBuilder) this, true);

        protected TBuilder DescriptionCore(string description) => 
            DataAnnotationsAttributeHelper.DescriptionCore<TBuilder>((TBuilder) this, description);

        void IAttributeBuilderInternal.AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue) where TAttribute: Attribute, new()
        {
            this.AddOrModifyAttribute<TAttribute>(setAttributeValue);
        }

        void IAttributeBuilderInternal.AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute
        {
            this.AddOrReplaceAttribute<TAttribute>(attribute);
        }

        TBuilder IAttributeBuilderInternal<TBuilder>.AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue) where TAttribute: Attribute, new() => 
            this.AddOrModifyAttribute<TAttribute>(setAttributeValue);

        TBuilder IAttributeBuilderInternal<TBuilder>.AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute => 
            this.AddOrReplaceAttribute<TAttribute>(attribute);

        protected TBuilder DisplayNameCore(string name) => 
            DataAnnotationsAttributeHelper.DisplayNameCore<TBuilder>((TBuilder) this, name);

        protected TBuilder DisplayShortNameCore(string shortName) => 
            DataAnnotationsAttributeHelper.DisplayShortNameCore<TBuilder>((TBuilder) this, shortName);

        protected TBuilder DoNotScaffoldCore() => 
            DataAnnotationsAttributeHelper.DoNotScaffoldCore<TBuilder>((TBuilder) this);

        protected TBuilder DoNotScaffoldDetailCollectionCore() => 
            DataAnnotationsAttributeHelper.DoNotScaffoldDetailCollectionCore<TBuilder>((TBuilder) this);

        protected TBuilder ImageNameCore(string imageName) => 
            this.AddOrModifyAttribute<DXImageAttribute>(delegate (DXImageAttribute x) {
                x.ImageName = imageName;
            });

        protected TBuilder ImageUriCore(string imageUri) => 
            this.AddOrModifyAttribute<ImageAttribute>(delegate (ImageAttribute x) {
                x.ImageUri = imageUri;
            });

        protected TBuilder ImageUriLargeCore(string uri) => 
            this.AddOrModifyAttribute<DXImageAttribute>(delegate (DXImageAttribute x) {
                x.LargeImageUri = uri;
            });

        protected TBuilder ImageUriSmallCore(string uri) => 
            this.AddOrModifyAttribute<DXImageAttribute>(delegate (DXImageAttribute x) {
                x.SmallImageUri = uri;
            });

        protected TBuilder LocatedAtCore(int position, PropertyLocation propertyLocation = 0)
        {
            if ((position < 0) || (position >= 0x2710))
            {
                throw new ArgumentException("position should non-negative and less then " + PropertyLocation.AfterPropertiesWithoutSpecifiedLocation);
            }
            if (propertyLocation == PropertyLocation.AfterPropertiesWithoutSpecifiedLocation)
            {
                position += 0x2711;
            }
            return DataAnnotationsAttributeHelper.SetOrderCore<TBuilder>((TBuilder) this, position);
        }

        protected TBuilder NotAutoGeneratedCore() => 
            DataAnnotationsAttributeHelper.AutoGeneratedCore<TBuilder>((TBuilder) this, false);

        IEnumerable<Attribute> IPropertyMetadataBuilder.Attributes =>
            this.storage.GetAttributes();
    }
}

