namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class GenerateBandWrapper : IGroupGenerator
    {
        private int propertyIndex;
        private readonly Func<GenerateBandWrapper, EditorsGeneratorBase> GetEditorsGenerator;
        private readonly GenerateBandWrapper ParentBandWrapper;

        public GenerateBandWrapper(GenerateBandWrapper parentBandWrapper, Func<GenerateBandWrapper, EditorsGeneratorBase> getEditorsGenerator)
        {
            this.ParentBandWrapper = parentBandWrapper;
            this.ColumnWrappers = new List<GenerateColumnWrapper>();
            this.BandWrappers = new List<GenerateBandWrapper>();
            this.GetEditorsGenerator = getEditorsGenerator;
        }

        public void ApplyGroupInfo(string name, GroupView view, Orientation orientation)
        {
            this.Header = name;
        }

        public IGroupGenerator CreateNestedGroup(string name, GroupView view, Orientation orientation)
        {
            GenerateBandWrapper wrapper1 = new GenerateBandWrapper(this, this.GetEditorsGenerator);
            wrapper1.Header = name;
            GenerateBandWrapper item = wrapper1;
            this.BandWrappers.Add(item);
            return item;
        }

        public List<GenerateColumnWrapper> GetAllColumnWrappers()
        {
            List<GenerateColumnWrapper> list = new List<GenerateColumnWrapper>();
            list.AddRange(this.ColumnWrappers);
            foreach (GenerateBandWrapper wrapper in this.BandWrappers)
            {
                list.AddRange(wrapper.GetAllColumnWrappers());
            }
            return list;
        }

        public int GetNextPropertyIndex()
        {
            GenerateBandWrapper rootBandWrapper = this.GetRootBandWrapper();
            int propertyIndex = rootBandWrapper.propertyIndex;
            rootBandWrapper.propertyIndex = propertyIndex + 1;
            return propertyIndex;
        }

        public GenerateBandWrapper GetRootBandWrapper() => 
            (this.ParentBandWrapper != null) ? this.ParentBandWrapper.GetRootBandWrapper() : this;

        public void MoveColumnWrappersDown()
        {
            if (this.BandWrappers.Count == 0)
            {
                this.ColumnWrappers.Sort(new GenerateColumnWrapperComparer());
            }
            else
            {
                GenerateBandWrapper wrapper = this.BandWrappers[0];
                while (this.ColumnWrappers.Count > 0)
                {
                    GenerateColumnWrapper item = this.ColumnWrappers[0];
                    wrapper.ColumnWrappers.Add(item);
                    this.ColumnWrappers.RemoveAt(0);
                }
                foreach (GenerateBandWrapper wrapper3 in this.BandWrappers)
                {
                    wrapper3.MoveColumnWrappersDown();
                }
            }
        }

        public void OnAfterGenerateContent()
        {
        }

        public string Header { get; set; }

        public List<GenerateColumnWrapper> ColumnWrappers { get; private set; }

        public List<GenerateBandWrapper> BandWrappers { get; private set; }

        public EditorsGeneratorBase EditorsGenerator =>
            this.GetEditorsGenerator(this);
    }
}

