namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;

    public class GenerateColumnWrapperComparer : IComparer<GenerateColumnWrapper>
    {
        public int Compare(GenerateColumnWrapper x, GenerateColumnWrapper y) => 
            (x.Index != y.Index) ? (x.Index - y.Index) : 0;
    }
}

