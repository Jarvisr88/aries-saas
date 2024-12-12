namespace DevExpress.Data.Helpers
{
    using System;

    public class IndexRenumber
    {
        public static void RenumberIndexes(IIndexRenumber list, int listSourceRow, bool increment);
        public static void RenumberIndexes(IIndexRenumber list, int oldListSourceRow, int newListSourceRow);
        public static void RenumberIndexes(IIndexRenumber list, int listSourceRow, bool increment, int maxIndex);
    }
}

