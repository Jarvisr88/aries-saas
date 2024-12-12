namespace DevExpress.Printing.Native.Preview
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public static class GraphicsExtensions
    {
        public static void ExecuteAndKeepState(this Graphics graph, Action action)
        {
            GraphicsState gstate = graph.Save();
            try
            {
                action();
            }
            finally
            {
                graph.Restore(gstate);
            }
        }
    }
}

