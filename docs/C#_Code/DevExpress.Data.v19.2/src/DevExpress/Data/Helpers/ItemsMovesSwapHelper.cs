namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public static class ItemsMovesSwapHelper
    {
        public static void SwapMoves(ref int from1, ref int to1, ref int from2, ref int to2);
        private static void SwapMoves(int from1, int to1, int from2, int to2, out int newFrom1, out int newTo1, out int newFrom2, out int newTo2);
        public static void SwapMovesJustSecond(int from1, int to1, int from2, int to2, out int newFrom1, out int newTo1);
    }
}

