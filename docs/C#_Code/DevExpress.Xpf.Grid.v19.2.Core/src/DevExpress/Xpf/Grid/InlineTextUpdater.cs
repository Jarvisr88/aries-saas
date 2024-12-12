namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    internal class InlineTextUpdater
    {
        private bool AreInlinesAssignedFrom(IList<InlineInfo> infoList)
        {
            Inline[] inlineArray = this.TextBlock.Inlines.ToArray<Inline>();
            if (inlineArray.Length != infoList.Count)
            {
                return false;
            }
            for (int i = 0; i < inlineArray.Length; i++)
            {
                Inline inline = inlineArray[i];
                InlineInfo info = infoList[i];
                if (!IsInlineAssignedFrom(inline, info))
                {
                    return false;
                }
            }
            return true;
        }

        private System.Windows.Documents.Run CreateRun(InlineInfo source)
        {
            Style style = source.Style;
            System.Windows.Documents.Run run = new System.Windows.Documents.Run(source.DisplayText);
            if (style != null)
            {
                run.DataContext = source.Data;
                run.Style = style;
            }
            return run;
        }

        private static bool IsInlineAssignedFrom(Inline inline, InlineInfo info)
        {
            System.Windows.Documents.Run run = inline as System.Windows.Documents.Run;
            return ((run != null) && ((run.DataContext == info.Data) && (ReferenceEquals(run.Style, info.Style) && (run.Text == info.DisplayText))));
        }

        protected virtual void SetRuns(IEnumerable<System.Windows.Documents.Run> runs)
        {
            this.TextBlock.Text = string.Empty;
            this.TextBlock.Inlines.Clear();
            this.TextBlock.Inlines.AddRange(runs);
        }

        public void Update(InlineCollectionInfo info)
        {
            if (info != null)
            {
                if (this.UseInlines)
                {
                    this.UpdateInlines(info.InlineSource);
                }
                else
                {
                    this.UpdateText(info.TextSource);
                }
            }
        }

        private void UpdateInlines(IList<InlineInfo> newValues)
        {
            if (this.TextBlock != null)
            {
                newValues ??= new InlineInfo[0];
                if (!this.AreInlinesAssignedFrom(newValues))
                {
                    this.SetRuns(from x in newValues select this.CreateRun(x));
                }
            }
        }

        private void UpdateText(string value)
        {
            this.TextBlock.Do<System.Windows.Controls.TextBlock>(x => x.Text = value);
        }

        public System.Windows.Controls.TextBlock TextBlock { get; set; }

        public bool UseInlines { get; set; }
    }
}

