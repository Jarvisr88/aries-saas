namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class BarItemLinkInfoCollection : BarItemLinkInfoCollectionGeneric<BarItemLinkInfo>
    {
        public static readonly DependencyProperty SkipFromVisibleInfosCalculationProperty;

        static BarItemLinkInfoCollection();
        public BarItemLinkInfoCollection(params IList<BarItemLinkBase>[] sourceCollection);
        public BarItemLinkInfoCollection(bool expandInplaceLinkHolders, params IList<BarItemLinkBase>[] sourceCollection);
        public BarItemLinkInfoCollection(bool expandInplaceLinkHolders, IList<BarItemLinkBase>[] sourceCollection, BarItemLinkInfoFactory factory, bool allowRecycling);
        public static bool GetSkipFromVisibleInfosCalculation(DependencyObject element);
        public static void SetSkipFromVisibleInfosCalculation(DependencyObject element, bool value);
    }
}

