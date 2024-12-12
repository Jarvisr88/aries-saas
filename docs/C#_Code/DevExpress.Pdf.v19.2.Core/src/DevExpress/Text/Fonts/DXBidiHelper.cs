namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;

    public static class DXBidiHelper
    {
        public static void Reorder(IList<DXCluster> clusters)
        {
            int count = clusters.Count;
            if (count > 0)
            {
                int num2 = 0;
                int num3 = 0;
                while (true)
                {
                    DXCluster cluster;
                    if (num3 >= count)
                    {
                        int num4 = num2;
                        while (num4 > 0)
                        {
                            int num5 = 0;
                            while (true)
                            {
                                if (num5 >= count)
                                {
                                    num4--;
                                    break;
                                }
                                if (clusters[num5].BidiLevel >= num4)
                                {
                                    int num6 = num5;
                                    while (true)
                                    {
                                        if (num5 < count)
                                        {
                                            cluster = clusters[num5];
                                            if (cluster.BidiLevel >= num4)
                                            {
                                                num5++;
                                                continue;
                                            }
                                        }
                                        for (int i = num5 - 1; num6 <= i; i--)
                                        {
                                            DXCluster cluster2 = clusters[num6];
                                            clusters[num6] = clusters[i];
                                            clusters[i] = cluster2;
                                            num6++;
                                        }
                                        break;
                                    }
                                }
                                num5++;
                            }
                        }
                        break;
                    }
                    cluster = clusters[num3];
                    num2 = Math.Max(cluster.BidiLevel, num2);
                    num3++;
                }
            }
        }
    }
}

