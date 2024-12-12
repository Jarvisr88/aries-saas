namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class BuildInfoContainer
    {
        private Dictionary<int, int> buildInfoDictionary;
        private Dictionary<int, ProcessState> processStateDictionary;
        private Dictionary<int, float> negativeOffsets;

        public BuildInfoContainer();
        public int GetBuildInfo(DocumentBand band);
        public DocumentBand GetDetailContainer(DocumentBand rootBand, PageRowBuilderBase pageRowBuilderBase, RectangleF bounds);
        public DocumentBand GetPrintingDetail(DocumentBand detailContainer);
        public ProcessState GetProcessState(DocumentBand rootBand);
        private int GetStartBandIndex(DocumentBand rootBand);
        public void SetBuildInfo(DocumentBand band, int value);
        public void SetProcessState(DocumentBand rootBand, ProcessState processState);

        public Dictionary<int, float> NegativeOffsets { get; }
    }
}

