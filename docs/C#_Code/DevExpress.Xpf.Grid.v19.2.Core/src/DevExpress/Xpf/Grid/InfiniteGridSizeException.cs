namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows.Controls;

    [Serializable]
    public class InfiniteGridSizeException : ApplicationException
    {
        internal const string InfiniteWidthMessage = "By default, an infinite grid width is not allowed since all grid cards will be rendered and, hence, the grid will work very slowly. To fix this issue, place the grid into a container that will give a finite width to the grid, or manually specify the grid's Width or MaxWidth. Note that you can also avoid this exception by setting the {0}.AllowInfiniteGridSize static property to True, but in that case, the grid will run slowly.";
        internal const string InfiniteHeightMessage = "By default, an infinite grid height is not allowed since all grid rows will be rendered and, hence, the grid will work very slowly. To fix this issue, place the grid into a container that will give a finite height to the grid, or manually specify the grid's Height or MaxHeight. Note that you can also avoid this exception by setting the {0}.AllowInfiniteGridSize static property to True, but in that case, the grid will run slowly.";

        public InfiniteGridSizeException(string message) : base(message)
        {
        }

        internal static void ValidateDefineSize(double defineSize, Orientation orientation, string DataControlTypeName)
        {
            if (!DataControlBase.AllowInfiniteGridSize && double.IsPositiveInfinity(defineSize))
            {
                throw new InfiniteGridSizeException(string.Format((orientation == Orientation.Horizontal) ? "By default, an infinite grid width is not allowed since all grid cards will be rendered and, hence, the grid will work very slowly. To fix this issue, place the grid into a container that will give a finite width to the grid, or manually specify the grid's Width or MaxWidth. Note that you can also avoid this exception by setting the {0}.AllowInfiniteGridSize static property to True, but in that case, the grid will run slowly." : "By default, an infinite grid height is not allowed since all grid rows will be rendered and, hence, the grid will work very slowly. To fix this issue, place the grid into a container that will give a finite height to the grid, or manually specify the grid's Height or MaxHeight. Note that you can also avoid this exception by setting the {0}.AllowInfiniteGridSize static property to True, but in that case, the grid will run slowly.", DataControlTypeName));
            }
        }
    }
}

