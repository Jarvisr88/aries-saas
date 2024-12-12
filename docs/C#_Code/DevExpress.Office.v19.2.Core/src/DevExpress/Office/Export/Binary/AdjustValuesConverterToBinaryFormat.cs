namespace DevExpress.Office.Export.Binary
{
    using DevExpress.Office.Drawing;
    using System;

    public class AdjustValuesConverterToBinaryFormat
    {
        private static void CalculateArrowCalloutCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Max, int odrawAdj2, int odrawAdj2Max, int odrawAdj3, int odrawAdj3Max, int odrawAdj4, int odrawAdj4Max, int maxAdj1, int maxAdj2, int maxAdj3, int maxAdj4, bool reverseAdj1, bool reverseAdj2, bool reverseAdj3, bool reverseAdj4)
        {
            if ((adjustValues[0] != null) && ((adjustValues[1] != null) && ((adjustValues[2] != null) && (adjustValues[3] != null))))
            {
                int? nullable;
                int? nullable1;
                int? nullable2;
                int? nullable3;
                int? nullable4;
                int num = (int) Math.Round((double) ((((double) adjustValues[0].Value) / ((double) maxAdj1)) * odrawAdj4Max));
                if (reverseAdj1)
                {
                    num = odrawAdj4Max - num;
                }
                int num2 = (int) Math.Round((double) ((((double) adjustValues[1].Value) / ((double) maxAdj2)) * odrawAdj2Max));
                if (reverseAdj2)
                {
                    num2 = odrawAdj2Max - num2;
                }
                int num3 = (int) Math.Round((double) ((((double) adjustValues[2].Value) / ((double) maxAdj3)) * odrawAdj3Max));
                if (reverseAdj3)
                {
                    num3 = odrawAdj3Max - num3;
                }
                int num4 = (int) Math.Round((double) ((((double) adjustValues[3].Value) / ((double) maxAdj4)) * odrawAdj1Max));
                if (reverseAdj4)
                {
                    num4 = odrawAdj1Max - num4;
                }
                if (num4 != odrawAdj1)
                {
                    nullable1 = new int?(num4);
                }
                else
                {
                    nullable = null;
                    nullable1 = nullable;
                }
                adjustValues[0] = nullable1;
                if (num2 != odrawAdj2)
                {
                    nullable2 = new int?(num2);
                }
                else
                {
                    nullable = null;
                    nullable2 = nullable;
                }
                adjustValues[1] = nullable2;
                if (num3 != odrawAdj3)
                {
                    nullable3 = new int?(num3);
                }
                else
                {
                    nullable = null;
                    nullable3 = nullable;
                }
                adjustValues[2] = nullable3;
                if (num != odrawAdj4)
                {
                    nullable4 = new int?(num);
                }
                else
                {
                    nullable = null;
                    nullable4 = nullable;
                }
                adjustValues[3] = nullable4;
            }
        }

        private static void CalculateArrowCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Max, int odrawAdj2, int odrawAdj2Max, int maxAdj1, int maxAdj2, bool reverseAdj1, bool reverseAdj2)
        {
            if ((adjustValues[0] != null) && (adjustValues[1] != null))
            {
                int? nullable;
                int? nullable1;
                int? nullable2;
                int num = (int) Math.Round((double) ((((double) adjustValues[0].Value) / ((double) maxAdj1)) * odrawAdj2Max));
                int num2 = reverseAdj1 ? (odrawAdj2Max - num) : num;
                num = (int) Math.Round((double) ((((double) adjustValues[1].Value) / ((double) maxAdj2)) * odrawAdj1Max));
                int num3 = reverseAdj2 ? (odrawAdj1Max - num) : num;
                if (num3 != odrawAdj1)
                {
                    nullable1 = new int?(num3);
                }
                else
                {
                    nullable = null;
                    nullable1 = nullable;
                }
                adjustValues[0] = nullable1;
                if (num2 != odrawAdj2)
                {
                    nullable2 = new int?(num2);
                }
                else
                {
                    nullable = null;
                    nullable2 = nullable;
                }
                adjustValues[1] = nullable2;
            }
        }

        private static void CalculateBentConnector3(int?[] adjustValues)
        {
            CalculateBentConnectorCore(adjustValues, 1);
        }

        private static void CalculateBentConnector4(int?[] adjustValues)
        {
            CalculateBentConnectorCore(adjustValues, 2);
        }

        private static void CalculateBentConnector5(int?[] adjustValues)
        {
            CalculateBentConnectorCore(adjustValues, 3);
        }

        private static void CalculateBentConnectorCore(int?[] adjustValues, int adjCount)
        {
            CalculateConnectorCore(adjustValues, 0x2a30, adjCount);
        }

        private static void CalculateCallout(int?[] adjustValues)
        {
            CalculateCalloutCore(adjustValues, 0x546, 0x6540);
        }

        private static void CalculateCalloutCore(int?[] adjustValues, int odrawAdj1, int odrawAdj2)
        {
            double num = 4.62962962962963;
            if ((adjustValues[0] != null) && (adjustValues[1] != null))
            {
                int? nullable;
                int? nullable1;
                int? nullable2;
                int num2 = ((int) Math.Round((double) (((double) adjustValues[0].Value) / num))) + 0x2a30;
                int num3 = ((int) Math.Round((double) (((double) adjustValues[1].Value) / num))) + 0x2a30;
                if (num2 != odrawAdj1)
                {
                    nullable1 = new int?(num2);
                }
                else
                {
                    nullable = null;
                    nullable1 = nullable;
                }
                adjustValues[0] = nullable1;
                if (num3 != odrawAdj2)
                {
                    nullable2 = new int?(num3);
                }
                else
                {
                    nullable = null;
                    nullable2 = nullable;
                }
                adjustValues[1] = nullable2;
                adjustValues[2] = null;
            }
        }

        private static void CalculateConnectorCore(int?[] adjustValues, int odrawAdj, int adjCount)
        {
            double num = 4.62962962962963;
            for (int i = 0; i < adjCount; i++)
            {
                if (adjustValues[i] != null)
                {
                    int? nullable1;
                    int num3 = (int) Math.Round((double) (((double) adjustValues[i].Value) / num));
                    if (num3 != odrawAdj)
                    {
                        nullable1 = new int?(num3);
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    adjustValues[i] = nullable1;
                }
            }
        }

        private static void CalculateCurvedArrowAdj2(int?[] adjustValues, int odrawWidth, int adj1, int odrawAdj2Max, int odrawAdjDefault, int openXmlAdj1, int openXmlAdj1Max)
        {
            int? nullable1;
            int num = (int) Math.Round((double) (((double) (odrawWidth + adj1)) / 2.0));
            int num2 = odrawAdj2Max - num;
            int num3 = ((int) Math.Round((double) ((((double) openXmlAdj1) / ((double) openXmlAdj1Max)) * num2))) + num;
            if (num3 != odrawAdjDefault)
            {
                nullable1 = new int?(num3);
            }
            else
            {
                nullable1 = null;
            }
            adjustValues[1] = nullable1;
        }

        private static void CalculateCurvedArrowAdj3(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide, int openXmlAdj1, int openXmlAdj1Max, int openXmlAdj3, int odrawAdjDefault, bool vertical, bool reverseAdj)
        {
            int num = Pin(0, openXmlAdj1, openXmlAdj1Max);
            double num3 = (minSide * num) / 100000.0;
            double num6 = (((vertical ? ((double) shapeWidth) : ((double) shapeHeight)) / 2.0) - ((num3 + ((minSide * openXmlAdj1Max) / 100000.0)) / 4.0)) * 2.0;
            double num8 = Math.Sqrt(Math.Abs((double) ((num6 * num6) - (num3 * num3))));
            if (num6 != 0.0)
            {
                double num9 = (num8 * (vertical ? ((double) shapeHeight) : ((double) shapeWidth))) / num6;
                int num10 = (int) Math.Round((double) ((100000.0 * num9) / ((double) minSide)));
                int num11 = (int) Math.Round((double) ((21600.0 * num9) / (vertical ? ((double) shapeHeight) : ((double) shapeWidth))));
                int num12 = reverseAdj ? 0x5460 : num11;
                int num14 = num12 - (reverseAdj ? (0x5460 - num11) : 0);
                if (num10 != 0)
                {
                    int? nullable1;
                    int num15 = (int) Math.Round((double) ((((double) openXmlAdj3) / ((double) num10)) * num14));
                    if (reverseAdj)
                    {
                        num15 = num12 - num15;
                    }
                    if (num15 != odrawAdjDefault)
                    {
                        nullable1 = new int?(num15);
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    adjustValues[2] = nullable1;
                }
            }
        }

        private static void CalculateCurvedArrowCore(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide, int odrawAdj1Default, int odrawAdj2Default, int odrawAdj3Default, bool vertical, bool reverseAdj3)
        {
            if ((adjustValues[0] != null) && ((adjustValues[1] != null) && (adjustValues[2] != null)))
            {
                int num = adjustValues[0].Value;
                int y = adjustValues[1].Value;
                int num3 = adjustValues[2].Value;
                int z = (int) Math.Round((double) ((50000.0 * (vertical ? ((double) shapeWidth) : ((double) shapeHeight))) / ((double) minSide)));
                if (z != 0)
                {
                    int num5 = Pin(0, y, z);
                    if (num5 != 0)
                    {
                        int? nullable1;
                        int num6 = 0x5460 - ((int) Math.Round((double) ((((double) y) / ((double) z)) * 10800.0)));
                        if (num6 != odrawAdj1Default)
                        {
                            nullable1 = new int?(num6);
                        }
                        else
                        {
                            nullable1 = null;
                        }
                        adjustValues[0] = nullable1;
                        CalculateCurvedArrowAdj2(adjustValues, 0x5460, num6, 0x5460, odrawAdj2Default, num, num5);
                        CalculateCurvedArrowAdj3(adjustValues, shapeWidth, shapeHeight, minSide, num, num5, num3, odrawAdj3Default, vertical, reverseAdj3);
                    }
                }
            }
        }

        private static void CalculateCurvedConnector3(int?[] adjustValues)
        {
            CalculateCurvedConnectorCore(adjustValues, 1);
        }

        private static void CalculateCurvedConnector4(int?[] adjustValues)
        {
            CalculateCurvedConnectorCore(adjustValues, 2);
        }

        private static void CalculateCurvedConnector5(int?[] adjustValues)
        {
            CalculateCurvedConnectorCore(adjustValues, 3);
        }

        private static void CalculateCurvedConnectorCore(int?[] adjustValues, int adjCount)
        {
            CalculateConnectorCore(adjustValues, 0, adjCount);
        }

        private static void CalculateCurvedDownArrow(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            CalculateCurvedArrowCore(adjustValues, shapeWidth, shapeHeight, minSide, 0x32a0, 0x4bf0, 0x3840, true, true);
        }

        private static void CalculateCurvedDownRibbon(int?[] adjustValues)
        {
            if ((adjustValues[0] != null) && ((adjustValues[1] != null) && (adjustValues[2] != null)))
            {
                int num = 0xa8c;
                int num3 = (int) Math.Round((double) ((((double) adjustValues[0].Value) / 100000.0) * 21600.0));
                int num4 = 0x1fa0 - num;
                int num5 = 0xc32b;
                if (num5 != 0)
                {
                    int num6 = (num4 - ((int) Math.Round((double) ((((double) (adjustValues[1].Value - 0x61cd)) / ((double) num5)) * num4)))) + num;
                    int num7 = Pin(0, adjustValues[0].Value, 0x186a0);
                    if (num7 != 0)
                    {
                        int? nullable;
                        int? nullable1;
                        int? nullable2;
                        int? nullable3;
                        int num8 = 0x5460 - ((int) Math.Round((double) ((((double) adjustValues[2].Value) / ((double) num7)) * num3)));
                        if (num6 != 0x1518)
                        {
                            nullable1 = new int?(num6);
                        }
                        else
                        {
                            nullable = null;
                            nullable1 = nullable;
                        }
                        adjustValues[0] = nullable1;
                        if (num3 != 0x1518)
                        {
                            nullable2 = new int?(num3);
                        }
                        else
                        {
                            nullable = null;
                            nullable2 = nullable;
                        }
                        adjustValues[1] = nullable2;
                        if (num8 != 0x49d4)
                        {
                            nullable3 = new int?(num8);
                        }
                        else
                        {
                            nullable = null;
                            nullable3 = nullable;
                        }
                        adjustValues[2] = nullable3;
                    }
                }
            }
        }

        private static void CalculateCurvedLeftArrow(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            CalculateCurvedArrowCore(adjustValues, shapeWidth, shapeHeight, minSide, 0x32a0, 0x4bf0, 0x1c20, false, false);
        }

        private static void CalculateCurvedRightArrow(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            CalculateCurvedArrowCore(adjustValues, shapeWidth, shapeHeight, minSide, 0x32a0, 0x4bf0, 0x3840, false, true);
        }

        private static void CalculateCurvedUpArrow(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            CalculateCurvedArrowCore(adjustValues, shapeWidth, shapeHeight, minSide, 0x32a0, 0x4bf0, 0x1c20, true, false);
        }

        private static void CalculateCurvedUpRibbon(int?[] adjustValues)
        {
            if ((adjustValues[0] != null) && ((adjustValues[1] != null) && (adjustValues[2] != null)))
            {
                int num = 0xa8c;
                int num3 = 0x1fa0 - num;
                if (num3 != 0)
                {
                    int num4 = 0x5460 - ((int) Math.Round((double) ((((double) adjustValues[0].Value) / 100000.0) * 21600.0)));
                    int num5 = 0xc32b;
                    if (num5 != 0)
                    {
                        int num6 = (num3 - ((int) Math.Round((double) ((((double) (adjustValues[1].Value - 0x61cd)) / ((double) num5)) * num3)))) + num;
                        int num7 = Pin(0, adjustValues[0].Value, 0x186a0);
                        if (num7 != 0)
                        {
                            int? nullable;
                            int? nullable1;
                            int? nullable2;
                            int? nullable3;
                            int num8 = (int) Math.Round((double) ((((double) adjustValues[2].Value) / ((double) num7)) * (0x5460 - num4)));
                            if (num6 != 0x1518)
                            {
                                nullable1 = new int?(num6);
                            }
                            else
                            {
                                nullable = null;
                                nullable1 = nullable;
                            }
                            adjustValues[0] = nullable1;
                            if (num4 != 0x3f48)
                            {
                                nullable2 = new int?(num4);
                            }
                            else
                            {
                                nullable = null;
                                nullable2 = nullable;
                            }
                            adjustValues[1] = nullable2;
                            if (num8 != 0xa8c)
                            {
                                nullable3 = new int?(num8);
                            }
                            else
                            {
                                nullable = null;
                                nullable3 = nullable;
                            }
                            adjustValues[2] = nullable3;
                        }
                    }
                }
            }
        }

        private static void CalculateDoubleWave(int?[] adjustValues)
        {
            CalculateWaveCore(adjustValues, 0x57c, 0xa8c, 0x2a30, 0x30d4);
        }

        private static void CalculateDownArrow(int?[] adjustValues, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((100000.0 * shapeHeight) / ((double) minSide)));
            CalculateArrowCore(adjustValues, 0x3f48, 0x5460, 0x1518, 0x2a30, 0x186a0, num, true, true);
        }

        private static void CalculateDownArrowCallout(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((50000.0 * shapeWidth) / ((double) minSide)));
            CalculateDownCalloutCore(adjustValues, 0x3840, 0x1518, 0x4650, 0x1fa4, 0x5460, 0x2a30, 0x5460, 0x2a30, num, (int) Math.Round((double) ((100000.0 * shapeHeight) / ((double) minSide))), shapeHeight, minSide, true, true, true, false);
        }

        private static void CalculateDownCalloutCore(int?[] adjustValues, int odrawAdj1, int odrawAdj2, int odrawAdj3, int odrawAdj4, int odrawAdj1Max, int odrawAdj2Max, int odrawAdj3Max, int odrawAdj4Max, int maxAdj2, int maxAdj3, int q2Divider, int minSide, bool reverseAdj1, bool reverseAdj2, bool reverseAdj3, bool reverseAdj4)
        {
            int num = (int) Math.Round((double) ((((double) adjustValues[1].Value) / ((double) maxAdj2)) * odrawAdj2Max));
            int num2 = Pin(0, adjustValues[1].Value, maxAdj2) * 2;
            if (num2 != 0)
            {
                int num3 = (int) Math.Round((double) ((((double) adjustValues[0].Value) / ((double) num2)) * num));
                if (reverseAdj1)
                {
                    num3 = odrawAdj4Max - num3;
                }
                if (reverseAdj2)
                {
                    num = odrawAdj2Max - num;
                }
                int num4 = (int) Math.Round((double) ((((double) adjustValues[2].Value) / ((double) maxAdj3)) * odrawAdj3Max));
                if (reverseAdj3)
                {
                    num4 = odrawAdj3Max - num4;
                }
                int num5 = (int) Math.Round((double) (100000.0 - ((Pin(0, adjustValues[2].Value, maxAdj3) * minSide) / ((double) q2Divider))));
                if (num5 != 0)
                {
                    int? nullable;
                    int? nullable1;
                    int? nullable2;
                    int? nullable3;
                    int? nullable4;
                    int num6 = (int) Math.Round((double) ((((double) adjustValues[3].Value) / ((double) num5)) * (!reverseAdj3 ? ((double) (odrawAdj3Max - num4)) : ((double) num4))));
                    if (reverseAdj4)
                    {
                        num6 = odrawAdj1Max - num6;
                    }
                    if (num6 != odrawAdj1)
                    {
                        nullable1 = new int?(num6);
                    }
                    else
                    {
                        nullable = null;
                        nullable1 = nullable;
                    }
                    adjustValues[0] = nullable1;
                    if (num != odrawAdj2)
                    {
                        nullable2 = new int?(num);
                    }
                    else
                    {
                        nullable = null;
                        nullable2 = nullable;
                    }
                    adjustValues[1] = nullable2;
                    if (num4 != odrawAdj3)
                    {
                        nullable3 = new int?(num4);
                    }
                    else
                    {
                        nullable = null;
                        nullable3 = nullable;
                    }
                    adjustValues[2] = nullable3;
                    if (num3 != odrawAdj4)
                    {
                        nullable4 = new int?(num3);
                    }
                    else
                    {
                        nullable = null;
                        nullable4 = nullable;
                    }
                    adjustValues[3] = nullable4;
                }
            }
        }

        private static void CalculateDownRibbon(int?[] adjustValues)
        {
            CalculateRibbonCore(adjustValues, 0x1518, 0xa8c, 0x1fa4, 0xa8c, 0, 0x1c20, 0x8235, 0x61a8, 0x124f8, false);
        }

        private static void CalculateHomePlate(int?[] adjustValues, int shapeWidth, int minSide)
        {
            if (adjustValues[0] != null)
            {
                int? nullable1;
                int num = (int) Math.Round((double) ((((double) shapeWidth) / ((double) minSide)) * 100000.0));
                int num3 = 0x5460 - ((int) Math.Round((double) ((((double) adjustValues[0].Value) / ((double) num)) * 21600.0)));
                if (num3 != 0x3f48)
                {
                    nullable1 = new int?(num3);
                }
                else
                {
                    nullable1 = null;
                }
                adjustValues[0] = nullable1;
            }
        }

        private static void CalculateLeftArrow(int?[] adjustValues, int shapeWidth, int minSide)
        {
            int num = (int) Math.Round((double) ((100000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCore(adjustValues, 0x1518, 0x5460, 0x1518, 0x2a30, 0x186a0, num, true, false);
        }

        private static void CalculateLeftArrowCallout(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((50000.0 * shapeHeight) / ((double) minSide)));
            int num2 = num * 2;
            int num3 = (int) Math.Round((double) ((100000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCalloutCore(adjustValues, 0x1c20, 0x5460, 0x1518, 0x2a30, 0xe10, 0x5460, 0x1fa4, 0x2a30, num2, num, num3, (int) Math.Round((double) ((num3 * minSide) / ((double) shapeWidth))), true, true, false, true);
        }

        private static void CalculateLeftRightArrow(int?[] adjustValues, int shapeWidth, int minSide)
        {
            int num = (int) Math.Round((double) ((50000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCore(adjustValues, 0x10e0, 0x2a30, 0x1518, 0x2a30, 0x186a0, num, true, false);
        }

        private static void CalculateLeftRightArrowCallout(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((50000.0 * shapeHeight) / ((double) minSide)));
            int num2 = num * 2;
            int num3 = (int) Math.Round((double) ((100000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCalloutCore(adjustValues, 0x1518, 0x2a30, 0x1518, 0x2a30, 0xa8c, 0x5460, 0x1fa4, 0x2a30, num2, num, num3, (int) Math.Round((double) ((num3 * minSide) / ((double) shapeWidth))), true, true, false, true);
        }

        private static void CalculateLineCallout1(int?[] adjustValues)
        {
            int[] odrawValues = new int[] { -8280, 0x5eec, -1800, 0xfd2 };
            CalculateLineCalloutCore(adjustValues, odrawValues);
        }

        private static void CalculateLineCallout2(int?[] adjustValues)
        {
            int[] odrawValues = new int[] { -10080, 0x5eec, -3600, 0xfd2, -1800, 0xfd2 };
            CalculateLineCalloutCore(adjustValues, odrawValues);
        }

        private static void CalculateLineCallout3(int?[] adjustValues)
        {
            int[] odrawValues = new int[] { 0x5b68, 0x5f50, 0x6270, 0x5460, 0x6270, 0xfd2, 0x5b68, 0xfd2 };
            CalculateLineCalloutCore(adjustValues, odrawValues);
        }

        private static void CalculateLineCalloutCore(int?[] adjustValues, params int[] odrawValues)
        {
            double num = 4.62962962962963;
            if (odrawValues.Length <= adjustValues.Length)
            {
                int index = odrawValues.Length - 1;
                int?[] nullableArray = new int?[8];
                for (int i = 0; (i < adjustValues.Length) && (adjustValues[i] != null); i++)
                {
                    int? nullable1;
                    int num4 = (int) Math.Round((double) (((double) adjustValues[i].Value) / num));
                    if (num4 != odrawValues[index])
                    {
                        nullable1 = new int?(num4);
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    nullableArray[index] = nullable1;
                    index--;
                }
                nullableArray.CopyTo(adjustValues, 0);
            }
        }

        private static void CalculateNotchedRightArrow(int?[] adjustValues, int shapeWidth, int minSide)
        {
            int num = (int) Math.Round((double) ((100000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCore(adjustValues, 0x3f48, 0x5460, 0x1518, 0x2a30, 0x186a0, num, true, true);
        }

        private static void CalculateProportionalValues(int?[] adjustValues, double value)
        {
            for (int i = 0; i < adjustValues.Length; i++)
            {
                double? nullable3;
                double? nullable1;
                double? nullable4;
                if (adjustValues[i] == null)
                {
                    return;
                }
                int? nullable2 = adjustValues[i];
                if (nullable2 != null)
                {
                    nullable1 = new double?((double) nullable2.GetValueOrDefault());
                }
                else
                {
                    nullable3 = null;
                    nullable1 = nullable3;
                }
                double? nullable = nullable1;
                double num2 = value;
                if (nullable != null)
                {
                    nullable4 = new double?(nullable.GetValueOrDefault() / num2);
                }
                else
                {
                    nullable3 = null;
                    nullable4 = nullable3;
                }
                adjustValues[i] = new int?((int) nullable4.Value);
            }
        }

        private static int CalculateRangeCore(int value, int odrawMinValue, int odrawMaxValue, int openXmlMaxValue)
        {
            int num = (odrawMaxValue - odrawMinValue) / 2;
            int num2 = odrawMinValue + num;
            double num3 = ((double) openXmlMaxValue) / ((double) num);
            if (value == 0)
            {
                return num2;
            }
            value = (int) Math.Round((value > 0) ? (num2 + (((double) value) / num3)) : (num2 - (((double) Math.Abs(value)) / num3)));
            if (value < odrawMinValue)
            {
                value = odrawMinValue;
            }
            if (value > odrawMaxValue)
            {
                value = odrawMaxValue;
            }
            return value;
        }

        private static void CalculateRibbonCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Min, int odrawAdj1Max, int odrawAdj2, int odrawAdj2Min, int odrawAdj2Max, int openXmlAdj1Max, int openXmlAdj2Min, int openXmlAdj2Max, bool reverseAdj1)
        {
            int num = odrawAdj1Max - odrawAdj1Min;
            int num2 = odrawAdj2Max - odrawAdj2Min;
            if ((adjustValues[0] != null) && (adjustValues[1] != null))
            {
                int? nullable;
                int? nullable1;
                int? nullable2;
                int num3 = openXmlAdj2Max - openXmlAdj2Min;
                int num4 = (int) Math.Round((double) ((((double) adjustValues[0].Value) / ((double) openXmlAdj1Max)) * num2));
                int num5 = (int) Math.Round((double) ((((double) (adjustValues[1].Value - openXmlAdj2Min)) / ((double) num3)) * num));
                if (reverseAdj1)
                {
                    num4 = (num2 - num4) + odrawAdj2Min;
                }
                num5 = (num - num5) + odrawAdj1Min;
                if (num5 != odrawAdj1)
                {
                    nullable1 = new int?(num5);
                }
                else
                {
                    nullable = null;
                    nullable1 = nullable;
                }
                adjustValues[0] = nullable1;
                if (num4 != odrawAdj2)
                {
                    nullable2 = new int?(num4);
                }
                else
                {
                    nullable = null;
                    nullable2 = nullable;
                }
                adjustValues[1] = nullable2;
            }
        }

        private static void CalculateRightArrow(int?[] adjustValues, int shapeWidth, int minSide)
        {
            int num = (int) Math.Round((double) ((100000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCore(adjustValues, 0x3f48, 0x5460, 0x1518, 0x2a30, 0x186a0, num, true, true);
        }

        private static void CalculateRightArrowCallout(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((50000.0 * shapeHeight) / ((double) minSide)));
            int num2 = num * 2;
            int num3 = (int) Math.Round((double) ((100000.0 * shapeWidth) / ((double) minSide)));
            CalculateArrowCalloutCore(adjustValues, 0x3840, 0x5460, 0x1518, 0x2a30, 0x4650, 0x5460, 0x1fa4, 0x2a30, num2, num, num3, (int) Math.Round((double) ((num3 * minSide) / ((double) shapeWidth))), true, true, true, false);
        }

        private static void CalculateSmileyFace(int?[] adjustValues)
        {
            if (adjustValues[0] != null)
            {
                int? nullable1;
                int num = CalculateRangeCore(adjustValues[0].Value, 0x3c96, 0x4470, 0x122d);
                if (num != 0x4470)
                {
                    nullable1 = new int?(num);
                }
                else
                {
                    nullable1 = null;
                }
                adjustValues[0] = nullable1;
            }
        }

        private static void CalculateStar16(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0xa8c, 0x927c);
        }

        private static void CalculateStar24(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0xa8c, 0x927c);
        }

        private static void CalculateStar32(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0xa8c, 0x927c);
        }

        private static void CalculateStar4(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0x1fa4, 0x30d4);
        }

        private static void CalculateStar8(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0x9ea, 0x927c);
        }

        private static void CalculateStarCore(int?[] adjustValues, int odrawAdj1, int openXmlAdj1)
        {
            int? nullable1;
            int? nullable = adjustValues[0];
            int num = 0x2a30 - ((int) Math.Round((double) ((((nullable != null) ? ((double) nullable.GetValueOrDefault()) : ((double) openXmlAdj1)) / 50000.0) * 10800.0)));
            if (num != odrawAdj1)
            {
                nullable1 = new int?(num);
            }
            else
            {
                nullable = null;
                nullable1 = nullable;
            }
            0[adjustValues] = (int) nullable1;
        }

        private static void CalculateUpArrow(int?[] adjustValues, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((100000.0 * shapeHeight) / ((double) minSide)));
            CalculateArrowCore(adjustValues, 0x1518, 0x5460, 0x1518, 0x2a30, 0x186a0, num, true, false);
        }

        private static void CalculateUpArrowCallout(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((50000.0 * shapeWidth) / ((double) minSide)));
            int num2 = num * 2;
            int num3 = (int) Math.Round((double) ((100000.0 * shapeHeight) / ((double) minSide)));
            CalculateArrowCalloutCore(adjustValues, 0x1c20, 0x5460, 0x1518, 0x2a30, 0xe10, 0x5460, 0x1fa4, 0x2a30, num2, num, num3, (int) Math.Round((double) ((num3 * minSide) / ((double) shapeHeight))), true, true, false, true);
        }

        private static void CalculateUpDownArrow(int?[] adjustValues, int shapeHeight, int minSide)
        {
            int? nullable;
            int? nullable1;
            int? nullable2;
            int num = (int) Math.Round((double) ((50000.0 * shapeHeight) / ((double) minSide)));
            int num2 = 0x2a30 - ((int) Math.Round((double) ((((double) adjustValues[0].Value) / 100000.0) * 10800.0)));
            int num3 = (int) Math.Round((double) ((((double) adjustValues[1].Value) / ((double) num)) * 10800.0));
            if (num2 != 0x1518)
            {
                nullable1 = new int?(num2);
            }
            else
            {
                nullable = null;
                nullable1 = nullable;
            }
            adjustValues[0] = nullable1;
            if (num3 != 0x10e0)
            {
                nullable2 = new int?(num3);
            }
            else
            {
                nullable = null;
                nullable2 = nullable;
            }
            adjustValues[1] = nullable2;
        }

        private static void CalculateUpDownArrowCallout(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide)
        {
            int num = (int) Math.Round((double) ((((double) shapeWidth) / ((double) minSide)) * 50000.0));
            CalculateDownCalloutCore(adjustValues, 0x1518, 0x1518, 0xa8c, 0x1fa4, 0x2a30, 0x2a30, 0x2a30, 0x2a30, num, (int) Math.Round((double) ((((double) shapeHeight) / ((double) minSide)) * 50000.0)), shapeHeight / 2, minSide, true, true, false, true);
        }

        private static void CalculateUpRibbon(int?[] adjustValues)
        {
            CalculateRibbonCore(adjustValues, 0x1518, 0xa8c, 0x1fa4, 0x49d4, 0x3840, 0x5460, 0x8235, 0x61a8, 0x124f8, true);
        }

        private static void CalculateWave(int?[] adjustValues)
        {
            CalculateWaveCore(adjustValues, 0xaf9, 0x10e0, 0x2a30, 0x4e20);
        }

        private static void CalculateWaveCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Max, int odrawAdj2, int openXmlAdj1Max)
        {
            if ((adjustValues[0] != null) && (adjustValues[1] != null))
            {
                int num;
                int? nullable1;
                int? nullable2;
                if (((int) Math.Round((double) ((((double) adjustValues[0].Value) / ((double) openXmlAdj1Max)) * odrawAdj1Max))) > odrawAdj1Max)
                {
                    num = odrawAdj1Max;
                }
                int? nullable = adjustValues[1];
                int num2 = CalculateRangeCore((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2, 0x21c0, 0x32a0, 0x2710);
                if (num != odrawAdj1)
                {
                    nullable1 = new int?(num);
                }
                else
                {
                    nullable = null;
                    nullable1 = nullable;
                }
                0[adjustValues] = (int) nullable1;
                if (num2 != odrawAdj2)
                {
                    nullable2 = new int?(num2);
                }
                else
                {
                    nullable = null;
                    nullable2 = nullable;
                }
                adjustValues[1] = nullable2;
            }
        }

        public static void Convert(int shapeWidth, int shapeHeight, ShapePreset shapeType, int?[] adjustValues)
        {
            if ((shapeHeight != 0) && (shapeWidth != 0))
            {
                int minSide = Math.Min(shapeWidth, shapeHeight);
                if (shapeType == ShapePreset.Star4)
                {
                    CalculateStar4(adjustValues);
                }
                else
                {
                    switch (shapeType)
                    {
                        case ShapePreset.Star8:
                            CalculateStar8(adjustValues);
                            return;

                        case ShapePreset.Star10:
                        case ShapePreset.Star12:
                            break;

                        case ShapePreset.Star16:
                            CalculateStar16(adjustValues);
                            return;

                        case ShapePreset.Star24:
                            CalculateStar24(adjustValues);
                            return;

                        case ShapePreset.Star32:
                            CalculateStar32(adjustValues);
                            return;

                        default:
                            switch (shapeType)
                            {
                                case ShapePreset.HomePlate:
                                case ShapePreset.Chevron:
                                    CalculateHomePlate(adjustValues, shapeWidth, minSide);
                                    return;

                                case ShapePreset.RightArrow:
                                    CalculateRightArrow(adjustValues, shapeWidth, minSide);
                                    return;

                                case ShapePreset.LeftArrow:
                                    CalculateLeftArrow(adjustValues, shapeWidth, minSide);
                                    return;

                                case ShapePreset.UpArrow:
                                    CalculateUpArrow(adjustValues, shapeHeight, minSide);
                                    return;

                                case ShapePreset.DownArrow:
                                    CalculateDownArrow(adjustValues, shapeHeight, minSide);
                                    return;

                                case ShapePreset.NotchedRightArrow:
                                    CalculateNotchedRightArrow(adjustValues, shapeWidth, minSide);
                                    return;

                                case ShapePreset.LeftRightArrow:
                                    CalculateLeftRightArrow(adjustValues, shapeWidth, minSide);
                                    return;

                                case ShapePreset.UpDownArrow:
                                    CalculateUpDownArrow(adjustValues, shapeHeight, minSide);
                                    return;

                                case ShapePreset.LeftArrowCallout:
                                    CalculateLeftArrowCallout(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.RightArrowCallout:
                                    CalculateRightArrowCallout(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.UpArrowCallout:
                                    CalculateUpArrowCallout(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.DownArrowCallout:
                                    CalculateDownArrowCallout(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.LeftRightArrowCallout:
                                    CalculateLeftRightArrowCallout(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.UpDownArrowCallout:
                                    CalculateUpDownArrowCallout(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.CurvedRightArrow:
                                    CalculateCurvedRightArrow(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.CurvedLeftArrow:
                                    CalculateCurvedLeftArrow(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.CurvedUpArrow:
                                    CalculateCurvedUpArrow(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.CurvedDownArrow:
                                    CalculateCurvedDownArrow(adjustValues, shapeWidth, shapeHeight, minSide);
                                    return;

                                case ShapePreset.SmileyFace:
                                    CalculateSmileyFace(adjustValues);
                                    return;

                                case ShapePreset.BentConnector3:
                                    CalculateBentConnector3(adjustValues);
                                    return;

                                case ShapePreset.BentConnector4:
                                    CalculateBentConnector4(adjustValues);
                                    return;

                                case ShapePreset.BentConnector5:
                                    CalculateBentConnector5(adjustValues);
                                    return;

                                case ShapePreset.CurvedConnector3:
                                    CalculateCurvedConnector3(adjustValues);
                                    return;

                                case ShapePreset.CurvedConnector4:
                                    CalculateCurvedConnector4(adjustValues);
                                    return;

                                case ShapePreset.CurvedConnector5:
                                    CalculateCurvedConnector5(adjustValues);
                                    return;

                                case ShapePreset.Callout1:
                                case ShapePreset.AccentCallout1:
                                case ShapePreset.BorderCallout1:
                                case ShapePreset.AccentBorderCallout1:
                                    CalculateLineCallout1(adjustValues);
                                    return;

                                case ShapePreset.Callout2:
                                case ShapePreset.AccentCallout2:
                                case ShapePreset.BorderCallout2:
                                case ShapePreset.AccentBorderCallout2:
                                    CalculateLineCallout2(adjustValues);
                                    return;

                                case ShapePreset.Callout3:
                                case ShapePreset.AccentCallout3:
                                case ShapePreset.BorderCallout3:
                                case ShapePreset.AccentBorderCallout3:
                                    CalculateLineCallout3(adjustValues);
                                    return;

                                case ShapePreset.WedgeRectCallout:
                                case ShapePreset.WedgeRoundRectCallout:
                                case ShapePreset.WedgeEllipseCallout:
                                case ShapePreset.CloudCallout:
                                    CalculateCallout(adjustValues);
                                    return;

                                case ShapePreset.Ribbon:
                                    CalculateDownRibbon(adjustValues);
                                    return;

                                case ShapePreset.Ribbon2:
                                    CalculateUpRibbon(adjustValues);
                                    return;

                                case ShapePreset.EllipseRibbon:
                                    CalculateCurvedDownRibbon(adjustValues);
                                    return;

                                case ShapePreset.EllipseRibbon2:
                                    CalculateCurvedUpRibbon(adjustValues);
                                    return;

                                case ShapePreset.Wave:
                                    CalculateWave(adjustValues);
                                    return;

                                case ShapePreset.DoubleWave:
                                    CalculateDoubleWave(adjustValues);
                                    return;

                                default:
                                    break;
                            }
                            break;
                    }
                    double num2 = 4.62962962962963;
                    CalculateProportionalValues(adjustValues, num2);
                }
            }
        }

        public static void ConvertWordArt(int widthEmu, int heightEmu, DrawingPresetTextWarp presetTextWarp, int?[] adjustValues)
        {
            switch (presetTextWarp)
            {
                case DrawingPresetTextWarp.NoShape:
                    return;

                case DrawingPresetTextWarp.ArchDown:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    return;

                case DrawingPresetTextWarp.ArchDownPour:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    adjustValues[1] = new int?(ConvertWordArtAdj1(adjustValues[1]));
                    return;

                case DrawingPresetTextWarp.ArchUp:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    return;

                case DrawingPresetTextWarp.ArchUpPour:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    adjustValues[1] = new int?(ConvertWordArtAdj1(adjustValues[1]));
                    return;

                case DrawingPresetTextWarp.Button:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    return;

                case DrawingPresetTextWarp.ButtonPour:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    adjustValues[1] = new int?(ConvertWordArtAdj1(adjustValues[1]));
                    return;

                case DrawingPresetTextWarp.CanDown:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.CanUp:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.CascadeDown:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.CascadeUp:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Chevron:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.ChevronInverted:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Circle:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    return;

                case DrawingPresetTextWarp.CirclePour:
                    adjustValues[0] = new int?(ConvertWordArtAdj5(adjustValues[0], (double) widthEmu, (double) heightEmu));
                    adjustValues[1] = new int?(ConvertWordArtAdj1(adjustValues[1]));
                    return;

                case DrawingPresetTextWarp.CurveDown:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.CurveUp:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Deflate:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.DeflateBottom:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.DeflateInflate:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.InflateDeflate:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.DeflateTop:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.DoubleWave1:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    adjustValues[1] = new int?(ConvertWordArtAdj2(adjustValues[1]));
                    return;

                case DrawingPresetTextWarp.FadeDown:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.FadeLeft:
                case DrawingPresetTextWarp.FadeRight:
                case DrawingPresetTextWarp.FadeUp:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Inflate:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.InflateBottom:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.InflateTop:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Plain:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.RingInside:
                case DrawingPresetTextWarp.RingOutside:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.SlantDown:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.SlantUp:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Stop:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Triangle:
                case DrawingPresetTextWarp.TriangleInverted:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    return;

                case DrawingPresetTextWarp.Wave1:
                case DrawingPresetTextWarp.Wave2:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    adjustValues[1] = new int?(ConvertWordArtAdj2(adjustValues[1]));
                    return;

                case DrawingPresetTextWarp.Wave4:
                    adjustValues[0] = new int?(ConvertWordArtAdj3(adjustValues[0]));
                    adjustValues[1] = new int?(ConvertWordArtAdj2(adjustValues[1]));
                    return;
            }
            throw new ArgumentOutOfRangeException("presetTextWarp", presetTextWarp, null);
        }

        private static int ConvertWordArtAdj1(int? value)
        {
            double? nullable1;
            int? nullable = value;
            if (nullable != null)
            {
                nullable1 = new double?(((double) nullable.GetValueOrDefault()) / 9.259);
            }
            else
            {
                nullable1 = null;
            }
            return (int) nullable1.Value;
        }

        private static int ConvertWordArtAdj2(int? value)
        {
            double? nullable3;
            double? nullable1;
            double? nullable4;
            int? nullable2 = value;
            if (nullable2 != null)
            {
                nullable1 = new double?(((double) nullable2.GetValueOrDefault()) * 0.216);
            }
            else
            {
                nullable3 = null;
                nullable1 = nullable3;
            }
            double? nullable = nullable1;
            double num = 0x2a30;
            if (nullable != null)
            {
                nullable4 = new double?(nullable.GetValueOrDefault() + num);
            }
            else
            {
                nullable3 = null;
                nullable4 = nullable3;
            }
            return (int) nullable4.Value;
        }

        private static int ConvertWordArtAdj3(int? value)
        {
            double? nullable1;
            int? nullable = value;
            if (nullable != null)
            {
                nullable1 = new double?(((double) nullable.GetValueOrDefault()) / 4.63);
            }
            else
            {
                nullable1 = null;
            }
            return (int) nullable1.Value;
        }

        private static int ConvertWordArtAdj5(int? value, double width, double height)
        {
            double num = (((double) value.Value) / 60000.0) % 360.0;
            if (num < 0.0)
            {
                num += 360.0;
            }
            double num3 = (Math.Atan((Math.Tan((num / 180.0) * 3.1415926535897931) * width) / height) / 3.1415926535897931) * 180.0;
            return (((90.0 >= num) || (num > 270.0)) ? ((int) (((360.0 + num3) % 360.0) * 65536.0)) : ((int) (((180.0 + num3) % 360.0) * 65536.0)));
        }

        private static int Pin(int x, int y, int z) => 
            (y < x) ? x : ((y > z) ? z : y);
    }
}

