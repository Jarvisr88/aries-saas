﻿namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class GenerateColumnMerger
    {
        private readonly Type ColumnType;
        private readonly Type BandType;
        private readonly bool canCreateColumns;
        private readonly bool forcePopulateSmartAttributes;
        private readonly bool canCreateBands;
        private readonly bool generateSmartProperties;
        private readonly bool removeOldColumns;
        private readonly GenerateBandWrapper BandRootWrapper;
        private readonly ModelItemCollectionWrapperHelper ColumnCollectionHelper;

        public GenerateColumnMerger(DataControlBase dataControl, GenerateBandWrapper bandRootWrapper, bool forceClearOldColumns, bool forcePopulateColumns, bool ignoreKeepOld)
        {
            this.ColumnType = dataControl.ColumnType;
            this.BandType = dataControl.BandType;
            this.BandRootWrapper = bandRootWrapper;
            switch (dataControl.AutoGenerateColumns)
            {
                case AutoGenerateColumnsMode.None:
                    this.removeOldColumns = forceClearOldColumns;
                    break;

                case AutoGenerateColumnsMode.KeepOld:
                    this.canCreateColumns = ignoreKeepOld || (dataControl.ColumnsCore.Count == 0);
                    break;

                case AutoGenerateColumnsMode.AddNew:
                    this.canCreateColumns = true;
                    break;

                case AutoGenerateColumnsMode.RemoveOld:
                    this.removeOldColumns = true;
                    this.canCreateColumns = true;
                    break;

                default:
                    break;
            }
            this.canCreateBands = dataControl.EnableSmartColumnsGeneration;
            if (forcePopulateColumns)
            {
                this.canCreateColumns = true;
            }
            this.forcePopulateSmartAttributes = forcePopulateColumns;
            this.generateSmartProperties = dataControl.EnableSmartColumnsGeneration;
            this.ColumnCollectionHelper = new ModelItemCollectionWrapperHelper();
        }

        private void AddColumnInCollection(IModelItem dataControlModel, IModelItem columnsOwnerModel, IModelItem columnModel)
        {
            if (this.IsAutoGenerated(columnModel) && this.CanAddColumn(dataControlModel, columnModel))
            {
                this.ColumnCollectionHelper.GetColumnCollection(columnsOwnerModel).Add(columnModel);
            }
        }

        private bool CanAddColumn(IModelItem dataControlModel, IModelItem columnModel) => 
            ((DataControlBase) dataControlModel.GetCurrentValue()).RaiseAutoGeneratingColumn((ColumnBase) columnModel.GetCurrentValue());

        public void FillColumn(IModelItem dataControlModel, IModelItem columnModel, bool ignoreSmartProperty)
        {
            string currentValue = (string) columnModel.Properties[ColumnBase.FieldNameProperty.Name].Value.GetCurrentValue();
            GenerateColumnWrapper columnWrapper = GenerateColumnWrapperHelper.FindColumnWrapper(this.BandRootWrapper, currentValue);
            if (columnWrapper != null)
            {
                this.FillColumn(columnModel, columnWrapper, ignoreSmartProperty);
                this.AddColumnInCollection(dataControlModel, dataControlModel, columnModel);
            }
        }

        private void FillColumn(IModelItem columnModel, GenerateColumnWrapper columnWrapper, bool ignoreSmartProperty)
        {
            RuntimeColumnPropertiesPopulator populator = new RuntimeColumnPropertiesPopulator(columnModel, columnWrapper);
            populator.ApplyRuntimeProperties();
            populator.ApplyFieldName();
            populator.ApplyDefaultProperties(this.IsAutoGenerated(columnModel) ? this.generateSmartProperties : false);
            populator.ApplySmartProperties(ignoreSmartProperty);
            populator.ApplyPropertiesFromResource();
            if (this.IsSmartColumn(columnModel))
            {
                IModelItem editSettings = populator.CreateEditSettings();
                populator.ApplyEditSettingsProperties(editSettings);
                populator.ApplyInitializer(editSettings);
            }
        }

        public void FillColumns(IModelItem dataControlModel)
        {
            this.RemoveOldElements(dataControlModel);
            this.MergeBands(dataControlModel, dataControlModel, this.BandRootWrapper);
            this.MergeColumns(dataControlModel, dataControlModel, this.BandRootWrapper);
            this.MoveExistingColumnsDown(dataControlModel, dataControlModel, null);
        }

        private void FillColumnsInCollection(IModelItem dataControlModel, IModelItem columnsOwnerModel, List<GenerateColumnWrapper> columnWrappers)
        {
            foreach (GenerateColumnWrapper wrapper in columnWrappers)
            {
                bool flag;
                IModelItem columnModel = this.FindOrCreateColumn(dataControlModel, columnsOwnerModel, wrapper, out flag);
                if (columnModel != null)
                {
                    this.FillColumn(columnModel, wrapper, this.forcePopulateSmartAttributes);
                    if (flag)
                    {
                        this.AddColumnInCollection(dataControlModel, columnsOwnerModel, columnModel);
                    }
                }
            }
        }

        private IModelItem FindBand(IModelItem bandOwnerModel, string header)
        {
            IModelItem item2;
            using (List<IModelItem>.Enumerator enumerator = this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Collection.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IModelItem current = enumerator.Current;
                        if (current.Properties["Header"].Value.GetCurrentValue().ToString() != header)
                        {
                            continue;
                        }
                        item2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item2;
        }

        private ColumnAndParent FindColumn(IModelItem dataControlModel, IModelItem bandOwnerModel, string fieldName)
        {
            ColumnAndParent parent2;
            if (this.removeOldColumns)
            {
                return null;
            }
            if (!ReferenceEquals(dataControlModel, bandOwnerModel) || (this.ColumnCollectionHelper.GetBandCollection(dataControlModel).Count == 0))
            {
                ModelItemCollectionWrapper columnCollection = this.ColumnCollectionHelper.GetColumnCollection(bandOwnerModel);
                IModelItem columnModel = columnCollection.FirstOrDefault(fieldName);
                columnModel ??= this.TryFindSimpleBindingColumn(columnCollection, fieldName);
                if (columnModel != null)
                {
                    return new ColumnAndParent(columnModel, bandOwnerModel);
                }
            }
            using (List<IModelItem>.Enumerator enumerator = this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Collection.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IModelItem current = enumerator.Current;
                        ColumnAndParent parent = this.FindColumn(dataControlModel, current, fieldName);
                        if (parent == null)
                        {
                            continue;
                        }
                        parent2 = parent;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return parent2;
        }

        private IModelItem FindLeftBottomBand(IModelItem bandRootModel) => 
            (this.ColumnCollectionHelper.GetBandCollection(bandRootModel).Count != 0) ? this.FindLeftBottomBand(this.ColumnCollectionHelper.GetBandCollection(bandRootModel).Collection[0]) : bandRootModel;

        private IModelItem FindOrCreateBand(IModelItem bandOwnerModel, GenerateBandWrapper wrapper)
        {
            IModelItem modelItem = this.FindBand(bandOwnerModel, wrapper.Header);
            if (modelItem == null)
            {
                if (!this.canCreateBands)
                {
                    return null;
                }
                modelItem = bandOwnerModel.Context.CreateItem(this.BandType);
                this.SetIsAutoGenerated(modelItem);
                modelItem.Properties[BaseColumn.HeaderProperty.Name].SetValue(wrapper.Header);
            }
            return modelItem;
        }

        private IModelItem FindOrCreateColumn(IModelItem dataControlModel, IModelItem columnsOwnerModel, GenerateColumnWrapper wrapper, out bool isNewColumn)
        {
            isNewColumn = false;
            ColumnAndParent columnAndParent = this.FindColumn(dataControlModel, dataControlModel, wrapper.FieldName);
            if (columnAndParent != null)
            {
                if (!ReferenceEquals(columnAndParent.OwnerModel, dataControlModel) && (this.IsSmartColumn(columnAndParent.ColumnModel) && ((bool) columnAndParent.OwnerModel.Properties[BaseColumn.IsAutoGeneratedProperty.Name].Value.GetCurrentValue())))
                {
                    this.MoveExistingColumnToBand(dataControlModel, columnsOwnerModel, columnAndParent);
                }
                return columnAndParent.ColumnModel;
            }
            if (!this.canCreateColumns)
            {
                return null;
            }
            DataControlBase currentValue = dataControlModel.GetCurrentValue() as DataControlBase;
            ColumnBase element = this.GetColumnFromEditorResourceKey(dataControlModel, wrapper) ?? currentValue.CreateColumnFromColumnGenerator(wrapper.PropertyDescriptor);
            IModelItem modelItem = (element == null) ? dataControlModel.Context.CreateItem(this.ColumnType) : new RuntimeModelItem((RuntimeEditingContext) dataControlModel.Context, element, dataControlModel.Context.GetRoot());
            this.SetIsAutoGenerated(modelItem);
            isNewColumn = true;
            return modelItem;
        }

        private ColumnBase GetColumnFromEditorResourceKey(IModelItem dataControlModel, GenerateColumnWrapper wrapper)
        {
            if (wrapper.EditorResourceKey == null)
            {
                return null;
            }
            DataTemplate resourceTemplate = wrapper.Generator.GetResourceTemplate(dataControlModel, wrapper.EditorResourceKey);
            return (wrapper.Generator.GetResourceContentFromTemplate(resourceTemplate) as ColumnBase);
        }

        private bool IsAutoGenerated(IModelItem model) => 
            (bool) model.Properties[BaseColumn.IsAutoGeneratedProperty.Name].Value.GetCurrentValue();

        private bool IsSmartColumn(IModelItem columnModel) => 
            (bool) columnModel.Properties[ColumnBase.IsSmartProperty.Name].Value.GetCurrentValue();

        private void MergeBands(IModelItem dataControlModel, IModelItem bandRootModel, GenerateBandWrapper bandRootWrapper)
        {
            if (bandRootWrapper.BandWrappers.Count != 0)
            {
                foreach (GenerateBandWrapper wrapper in bandRootWrapper.BandWrappers)
                {
                    IModelItem model = this.FindOrCreateBand(bandRootModel, wrapper);
                    if (model != null)
                    {
                        if (this.IsAutoGenerated(model))
                        {
                            if (ReferenceEquals(bandRootModel, dataControlModel) && (this.ColumnCollectionHelper.GetBandCollection(dataControlModel).Count == 0))
                            {
                                this.MoveColumnsInNewBand(dataControlModel, model);
                            }
                            this.ColumnCollectionHelper.GetBandCollection(bandRootModel).Add(model);
                        }
                        this.MergeBands(dataControlModel, model, wrapper);
                    }
                }
            }
        }

        private void MergeColumns(IModelItem dataControlModel, IModelItem bandRootModel, GenerateBandWrapper bandRootWrapper)
        {
            if (bandRootWrapper.BandWrappers.Count == 0)
            {
                this.FillColumnsInCollection(dataControlModel, bandRootModel, bandRootWrapper.ColumnWrappers);
            }
            else
            {
                foreach (GenerateBandWrapper wrapper in bandRootWrapper.BandWrappers)
                {
                    IModelItem item = this.FindBand(bandRootModel, wrapper.Header);
                    item ??= this.FindLeftBottomBand(dataControlModel);
                    this.MergeColumns(dataControlModel, item, wrapper);
                }
            }
        }

        private void MoveColumnsInNewBand(IModelItem dataControlModel, IModelItem newBandModel)
        {
            foreach (IModelItem item in this.ColumnCollectionHelper.GetColumnCollection(dataControlModel).FindColumns())
            {
                if (this.IsSmartColumn(item))
                {
                    this.ColumnCollectionHelper.GetColumnCollection(dataControlModel).Remove(item);
                    this.ColumnCollectionHelper.GetColumnCollection(newBandModel).Add(item);
                }
            }
        }

        private void MoveExistingColumnsDown(IModelItem dataControlModel, IModelItem bandOwnerModel, List<ColumnAndParent> collectedColumns = null)
        {
            if (this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Count == 0)
            {
                if (collectedColumns != null)
                {
                    foreach (ColumnAndParent parent in collectedColumns)
                    {
                        if (this.IsSmartColumn(parent.ColumnModel) || this.IsAutoGenerated(parent.ColumnModel))
                        {
                            this.ColumnCollectionHelper.GetColumnCollection(parent.OwnerModel).Remove(parent.ColumnModel);
                            this.ColumnCollectionHelper.GetColumnCollection(bandOwnerModel).Add(parent.ColumnModel);
                        }
                    }
                }
            }
            else
            {
                collectedColumns ??= new List<ColumnAndParent>();
                if (!ReferenceEquals(dataControlModel, bandOwnerModel))
                {
                    foreach (IModelItem item3 in this.ColumnCollectionHelper.GetColumnCollection(bandOwnerModel).FindColumns())
                    {
                        collectedColumns.Add(new ColumnAndParent(item3, bandOwnerModel));
                    }
                }
                else
                {
                    foreach (IModelItem item in this.ColumnCollectionHelper.GetColumnCollection(dataControlModel).FindColumns())
                    {
                        bool flag = false;
                        foreach (IModelItem item2 in this.ColumnCollectionHelper.GetBandCollection(dataControlModel).Collection)
                        {
                            Func<object, string> evaluator = <>c.<>9__22_0;
                            if (<>c.<>9__22_0 == null)
                            {
                                Func<object, string> local1 = <>c.<>9__22_0;
                                evaluator = <>c.<>9__22_0 = x => x.ToString();
                            }
                            if (this.FindColumn(dataControlModel, item2, item.Properties[ColumnBase.FieldNameProperty.Name].Value.GetCurrentValue().With<object, string>(evaluator)) != null)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            collectedColumns.Add(new ColumnAndParent(item, bandOwnerModel));
                        }
                    }
                }
                for (int i = 0; i < this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Count; i++)
                {
                    this.MoveExistingColumnsDown(dataControlModel, this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Collection[i], (i == 0) ? collectedColumns : null);
                }
            }
        }

        private void MoveExistingColumnToBand(IModelItem dataControlModel, IModelItem columnsOwnerModel, ColumnAndParent columnAndParent)
        {
            if (!ReferenceEquals(dataControlModel, columnsOwnerModel))
            {
                this.ColumnCollectionHelper.GetColumnCollection(columnAndParent.OwnerModel).Remove(columnAndParent.ColumnModel);
                this.ColumnCollectionHelper.GetColumnCollection(columnsOwnerModel).Add(columnAndParent.ColumnModel);
            }
        }

        private void MoveNonAutoGeneratedBandsToParent(IModelItem parentBandModel, IModelItem childBandModel)
        {
            List<IModelItem> list = new List<IModelItem>();
            foreach (IModelItem item in this.ColumnCollectionHelper.GetBandCollection(childBandModel).Collection)
            {
                list.Add(item);
            }
            foreach (IModelItem item2 in list)
            {
                this.MoveNonAutoGeneratedBandsToParent(childBandModel, item2);
            }
            if ((parentBandModel != null) && this.IsAutoGenerated(childBandModel))
            {
                this.MoveNonAutoGeneratedColumnsToParent(parentBandModel, this.ColumnCollectionHelper.GetColumnCollection(childBandModel));
                List<IModelItem> list2 = new List<IModelItem>();
                foreach (IModelItem item3 in this.ColumnCollectionHelper.GetBandCollection(childBandModel).Collection)
                {
                    if (!this.IsAutoGenerated(item3))
                    {
                        list2.Add(item3);
                    }
                }
                foreach (IModelItem item4 in list2)
                {
                    this.ColumnCollectionHelper.GetBandCollection(childBandModel).Remove(item4);
                    this.ColumnCollectionHelper.GetBandCollection(parentBandModel).Add(item4);
                }
            }
        }

        private void MoveNonAutoGeneratedColumnsToParent(IModelItem parentModel, ModelItemCollectionWrapper collection)
        {
            foreach (IModelItem item in collection.FindColumns(columnModel => !this.IsAutoGenerated(columnModel)))
            {
                collection.Remove(item);
                this.ColumnCollectionHelper.GetColumnCollection(parentModel).Add(item);
            }
        }

        private void RemoveAutoGeneratedBands(IModelItem bandOwnerModel)
        {
            List<IModelItem> list = new List<IModelItem>();
            foreach (IModelItem item in this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Collection)
            {
                if (this.IsAutoGenerated(item))
                {
                    list.Add(item);
                    continue;
                }
                this.RemoveAutoGeneratedBands(item);
            }
            foreach (IModelItem item2 in list)
            {
                this.ColumnCollectionHelper.GetBandCollection(bandOwnerModel).Remove(item2);
            }
            foreach (IModelItem item3 in this.ColumnCollectionHelper.GetColumnCollection(bandOwnerModel).FindColumns(columnModel => this.removeOldColumns || this.IsAutoGenerated(columnModel)))
            {
                this.ColumnCollectionHelper.GetColumnCollection(bandOwnerModel).Remove(item3);
            }
        }

        private void RemoveOldElements(IModelItem dataControlModel)
        {
            if (this.canCreateColumns)
            {
                this.MoveNonAutoGeneratedBandsToParent(null, dataControlModel);
                this.RemoveAutoGeneratedBands(dataControlModel);
            }
        }

        private void SetIsAutoGenerated(IModelItem modelItem)
        {
            (modelItem.GetCurrentValue() as BaseColumn).IsAutoGenerated = true;
        }

        private IModelItem TryFindSimpleBindingColumn(ModelItemCollectionWrapper columnsModelCollection, string fieldName) => 
            columnsModelCollection.FirstOrDefault("$simpleBinding_" + fieldName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GenerateColumnMerger.<>c <>9 = new GenerateColumnMerger.<>c();
            public static Func<object, string> <>9__22_0;

            internal string <MoveExistingColumnsDown>b__22_0(object x) => 
                x.ToString();
        }
    }
}

