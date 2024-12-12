namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomBestFitEventArgsBase : RoutedEventArgs
    {
        private DevExpress.Xpf.Core.BestFitMode bestFitMode;

        public CustomBestFitEventArgsBase(ColumnBase column, DevExpress.Xpf.Core.BestFitMode bestFitMode)
        {
            this.BestFitMode = bestFitMode;
            this.ColumnCore = column;
        }

        public DevExpress.Xpf.Core.BestFitMode BestFitMode
        {
            get => 
                this.bestFitMode;
            set
            {
                if ((value == DevExpress.Xpf.Core.BestFitMode.Smart) || (value == DevExpress.Xpf.Core.BestFitMode.Default))
                {
                    throw new IncorrectBestFitModeException();
                }
                this.bestFitMode = value;
            }
        }

        public IEnumerable<int> BestFitRows { get; set; }

        protected ColumnBase ColumnCore { get; private set; }
    }
}

