namespace DevExpress.Printing.Core.Native.Navigation
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class NavigationBuilderBase : INavigationBuilder
    {
        private Dictionary<IBrickOwner, BrickPagePair> navigationTargets = new Dictionary<IBrickOwner, BrickPagePair>();
        private Dictionary<IBrickOwner, List<VisualBrick>> navigationLinks = new Dictionary<IBrickOwner, List<VisualBrick>>();
        private Hashtable brickOwners = new Hashtable();
        private PageList pageList;

        public NavigationBuilderBase(PageList pageList)
        {
            this.pageList = pageList;
        }

        public void FinalizeBuild()
        {
            this.navigationTargets.Clear();
            this.navigationLinks.Clear();
            this.brickOwners.Clear();
        }

        protected abstract IBrickOwner GetNavigationTarget(VisualBrick brick);
        protected abstract VisualBrick GetVisualBrick(BrickPagePair brickPagePair, PageList pageList);
        public virtual void SetNavigationPairs(BrickPagePairCollection bpPairs)
        {
            for (int i = 0; i < bpPairs.Count; i++)
            {
                BrickPagePair brickPagePair = bpPairs[i];
                VisualBrick visualBrick = this.GetVisualBrick(brickPagePair, this.pageList);
                if (visualBrick != null)
                {
                    if (visualBrick.BrickOwner.IsNavigationLink)
                    {
                        IBrickOwner navigationTarget = this.GetNavigationTarget(visualBrick);
                        if (navigationTarget != null)
                        {
                            BrickPagePair pair2;
                            if (this.navigationTargets.TryGetValue(navigationTarget, out pair2) && !this.brickOwners.ContainsKey(visualBrick.BrickOwner))
                            {
                                visualBrick.NavigationPair = pair2;
                            }
                            else
                            {
                                List<VisualBrick> list;
                                this.brickOwners[visualBrick.BrickOwner] = true;
                                if (!this.navigationLinks.TryGetValue(navigationTarget, out list))
                                {
                                    list = new List<VisualBrick>();
                                    this.navigationLinks.Add(navigationTarget, list);
                                }
                                list.Add(visualBrick);
                            }
                        }
                    }
                    if (visualBrick.BrickOwner.IsNavigationTarget)
                    {
                        List<VisualBrick> list2;
                        if (this.navigationLinks.TryGetValue(visualBrick.BrickOwner, out list2))
                        {
                            foreach (VisualBrick brick2 in list2)
                            {
                                brick2.NavigationPair = brickPagePair;
                            }
                            this.navigationLinks.Remove(visualBrick.BrickOwner);
                        }
                        this.navigationTargets[visualBrick.BrickOwner] = brickPagePair;
                    }
                }
            }
            foreach (IBrickOwner owner2 in this.navigationLinks.Keys)
            {
                BrickPagePair pair3;
                if (this.navigationTargets.TryGetValue(owner2, out pair3))
                {
                    foreach (VisualBrick brick3 in this.navigationLinks[owner2])
                    {
                        brick3.NavigationPair = pair3;
                    }
                }
            }
        }
    }
}

