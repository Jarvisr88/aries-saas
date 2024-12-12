namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Windows.Controls;

    public sealed class LayoutGroupGenerator : IGroupGenerator
    {
        private readonly IModelItem group;
        private readonly EditorsGeneratorBase itemGenerator;
        private readonly Func<IModelItem, IModelItem> createChildGroupCallback;
        private readonly Action<IModelItem> onAfterGenerateContentCallback;
        private readonly Func<IModelItem, EditorsGeneratorBase> createGeneratorCallback;

        public LayoutGroupGenerator(IModelItem group, Func<IModelItem, IModelItem> createChildGroupCallback, Action<IModelItem> onAfterGenerateContentCallback, Func<IModelItem, EditorsGeneratorBase> createGeneratorCallback)
        {
            this.group = group;
            this.createChildGroupCallback = createChildGroupCallback;
            this.onAfterGenerateContentCallback = onAfterGenerateContentCallback;
            this.createGeneratorCallback = createGeneratorCallback;
            this.itemGenerator = createGeneratorCallback(group);
        }

        private void AddChild(IModelItem childGroup)
        {
            this.group.Properties["Children"].Collection.Add(childGroup);
        }

        void IGroupGenerator.ApplyGroupInfo(string name, GroupView view, Orientation orientation)
        {
            IModelProperty property = this.group.Properties["View"];
            property.SetValueIfNotSet(Enum.ToObject(property.PropertyType, view));
            this.group.Properties["Orientation"].SetValueIfNotSet(orientation);
            this.group.Properties["Header"].SetValueIfNotSet(name);
        }

        IGroupGenerator IGroupGenerator.CreateNestedGroup(string name, GroupView view, Orientation orientation)
        {
            IModelItem item = this.createChildGroupCallback(this.group);
            this.group.Properties["Children"].Collection.Add(item);
            return new LayoutGroupGenerator(item, this.createChildGroupCallback, this.onAfterGenerateContentCallback, this.createGeneratorCallback);
        }

        void IGroupGenerator.OnAfterGenerateContent()
        {
            this.onAfterGenerateContentCallback(this.group);
        }

        EditorsGeneratorBase IGroupGenerator.EditorsGenerator =>
            this.itemGenerator;
    }
}

