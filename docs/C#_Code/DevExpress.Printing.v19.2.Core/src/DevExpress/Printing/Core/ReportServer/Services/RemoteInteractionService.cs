namespace DevExpress.Printing.Core.ReportServer.Services
{
    using DevExpress.Data;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.XtraPrinting.Native.Interaction;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class RemoteInteractionService : IInteractionService, IInteractionServiceBase
    {
        public RemoteInteractionService()
        {
            this.BandSorting = new Dictionary<string, List<SortingFieldInfoContract>>();
        }

        private static ColumnSortOrder ChangeSortOrder(ColumnSortOrder sortOrder) => 
            (sortOrder == ColumnSortOrder.Ascending) ? ColumnSortOrder.Descending : ColumnSortOrder.Ascending;

        void IInteractionService.AppendSorting(string sortData)
        {
            string[] values;
            List<SortingFieldInfoContract> list;
            if (TryParseData(sortData, out values) && this.BandSorting.TryGetValue(values[0], out list))
            {
                SortingFieldInfoContract contract = list.FirstOrDefault<SortingFieldInfoContract>(item => string.Equals(values[1], item.FieldName));
                if (contract != null)
                {
                    contract.SortOrder = ChangeSortOrder(contract.SortOrder);
                    list.Remove(contract);
                    list.Add(contract);
                }
            }
        }

        void IInteractionService.ApplySorting(string sortData)
        {
            string[] strArray;
            List<SortingFieldInfoContract> list;
            if (TryParseData(sortData, out strArray) && this.BandSorting.TryGetValue(strArray[0], out list))
            {
                foreach (SortingFieldInfoContract contract in list)
                {
                    contract.SortOrder = !string.Equals(strArray[1], contract.FieldName) ? ColumnSortOrder.None : ChangeSortOrder(contract.SortOrder);
                }
            }
        }

        void IInteractionService.RemoveSorting(string sortData)
        {
            string[] values;
            List<SortingFieldInfoContract> list;
            if (TryParseData(sortData, out values) && this.BandSorting.TryGetValue(values[0], out list))
            {
                SortingFieldInfoContract contract = list.FirstOrDefault<SortingFieldInfoContract>(item => string.Equals(values[1], item.FieldName));
                if (contract != null)
                {
                    contract.SortOrder = ColumnSortOrder.None;
                }
            }
        }

        bool IInteractionService.TryGetAppliedSorting(string sortData, out ColumnSortOrder sortOrder)
        {
            string[] values;
            List<SortingFieldInfoContract> list;
            if (string.IsNullOrEmpty(sortData) || (!TryParseData(sortData, out values) || !this.BandSorting.TryGetValue(values[0], out list)))
            {
                sortOrder = ColumnSortOrder.None;
                return false;
            }
            SortingFieldInfoContract contract = list.FirstOrDefault<SortingFieldInfoContract>(item => string.Equals(values[1], item.FieldName));
            if (contract != null)
            {
                sortOrder = contract.SortOrder;
                return true;
            }
            sortOrder = ColumnSortOrder.None;
            return false;
        }

        public void Reset()
        {
            this.BandSorting.Clear();
        }

        private static bool TryParseData(string s, out string[] values)
        {
            int length = s.LastIndexOf('.');
            if (length < 0)
            {
                values = null;
                return false;
            }
            string[] textArray1 = new string[] { s.Substring(0, length), s.Substring(length + 1, (s.Length - length) - 1) };
            values = textArray1;
            return true;
        }

        public Dictionary<string, List<SortingFieldInfoContract>> BandSorting { get; private set; }

        bool IInteractionServiceBase.IsInteracting
        {
            get => 
                false;
            set
            {
            }
        }
    }
}

