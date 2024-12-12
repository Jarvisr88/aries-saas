namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class CleanupBandVisitorExt
    {
        public static DocumentBandCleanupStatus CleanupBands(this DocumentBand band, int[] bandIndexes, int bandLevel)
        {
            DocumentBandCleanupStatus status;
            if ((band.Bands == null) || (band.Bands.Count == 0))
            {
                return band.CleanupStatus;
            }
            int num = bandIndexes[bandLevel];
            if ((num == band.Bands.Count) && !band.IsRoot)
            {
                band.Bands.Clear();
                band.CleanupStatus = status = band.CleanupStatus | DocumentBandCleanupStatus.BandsCleaned;
                return status;
            }
            int index = (num < band.Bands.Count) ? num : (num - 1);
            while (true)
            {
                if (index >= 0)
                {
                    DocumentBand band2 = band.Bands[index];
                    if (band2 != null)
                    {
                        DocumentBandCleanupStatus cleanupStatus = band2.CleanupStatus;
                        if (!cleanupStatus.HasFlag(DocumentBandCleanupStatus.AllCleaned))
                        {
                            if (index == num)
                            {
                                cleanupStatus = band2.CleanupBands(bandIndexes, bandLevel + 1);
                            }
                            else if (index < num)
                            {
                                DocumentBandKind kind = band2.Kind;
                                bool flag = ((kind != DocumentBandKind.BottomMargin) && ((kind != DocumentBandKind.TopMargin) && ((kind != DocumentBandKind.ReportHeader) && ((kind != DocumentBandKind.ReportFooter) && (kind != DocumentBandKind.PageHeader))))) && (kind != DocumentBandKind.PageFooter);
                                bool flag2 = band.GetPath().SequenceEqual<int>(bandIndexes.Take<int>(bandIndexes.Length - 1));
                                if ((kind == DocumentBandKind.Header) & flag2)
                                {
                                    band.CleanupStatus = status = DocumentBandCleanupStatus.None;
                                    return status;
                                }
                                flag &= !((kind == DocumentBandKind.PageBreak) & flag2);
                                if (flag && !cleanupStatus.HasFlag(DocumentBandCleanupStatus.BricksCleaned))
                                {
                                    band2.Bricks.Clear();
                                    DocumentBandCleanupStatus status1 = cleanupStatus | DocumentBandCleanupStatus.BricksCleaned;
                                    band2.CleanupStatus = cleanupStatus = status1;
                                }
                                if (flag && !cleanupStatus.HasFlag(DocumentBandCleanupStatus.BandsCleaned))
                                {
                                    band2.Bands.Clear();
                                    DocumentBandCleanupStatus status3 = cleanupStatus | DocumentBandCleanupStatus.BandsCleaned;
                                    band2.CleanupStatus = cleanupStatus = status3;
                                }
                            }
                            if (cleanupStatus.HasFlag(DocumentBandCleanupStatus.AllCleaned))
                            {
                                band.RemoveChildBand(index);
                            }
                            index--;
                            continue;
                        }
                    }
                }
                return band.CleanupStatus;
            }
        }
    }
}

