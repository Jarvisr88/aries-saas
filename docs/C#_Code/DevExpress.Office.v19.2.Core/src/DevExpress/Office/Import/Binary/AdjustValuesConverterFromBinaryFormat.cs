namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    public class AdjustValuesConverterFromBinaryFormat
    {
        private static void CalculateArrowCalloutCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Max, int odrawAdj2, int odrawAdj2Max, int odrawAdj3, int odrawAdj3Max, int odrawAdj4, int odrawAdj4Max, int maxAdj1, int maxAdj2, int maxAdj3, int maxAdj4, bool reverseAdj1, bool reverseAdj2, bool reverseAdj3, bool reverseAdj4)
        {
            int? nullable;
            int num1;
            int num9;
            int num10;
            int num11;
            if (!reverseAdj1)
            {
                nullable = adjustValues[3];
                num1 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj4;
            }
            else
            {
                nullable = adjustValues[3];
                num1 = odrawAdj4Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj4);
            }
            int num = num1;
            if (!reverseAdj2)
            {
                nullable = adjustValues[1];
                num9 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2;
            }
            else
            {
                nullable = adjustValues[1];
                num9 = odrawAdj2Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2);
            }
            int num2 = num9;
            if (!reverseAdj3)
            {
                nullable = adjustValues[2];
                num10 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj3;
            }
            else
            {
                nullable = adjustValues[2];
                num10 = odrawAdj3Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj3);
            }
            int num3 = num10;
            if (!reverseAdj4)
            {
                nullable = adjustValues[0];
                num11 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1;
            }
            else
            {
                nullable = adjustValues[0];
                num11 = odrawAdj1Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1);
            }
            int num4 = num11;
            int num5 = (int) Math.Round((double) (maxAdj1 * (((double) num) / ((double) odrawAdj4Max))));
            int num6 = (int) Math.Round((double) (maxAdj2 * (((double) num2) / ((double) odrawAdj2Max))));
            int num7 = (int) Math.Round((double) (maxAdj3 * (((double) num3) / ((double) odrawAdj3Max))));
            int num8 = (int) Math.Round((double) (maxAdj4 * (((double) num4) / ((double) odrawAdj1Max))));
            adjustValues[0] = new int?(num5);
            adjustValues[1] = new int?(num6);
            adjustValues[2] = new int?(num7);
            adjustValues[3] = new int?(num8);
        }

        private static void CalculateArrowCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Max, int odrawAdj2, int odrawAdj2Max, int maxAdj1, int maxAdj2, bool reverseAdj1, bool reverseAdj2)
        {
            int? nullable;
            int num1;
            int num5;
            if (!reverseAdj1)
            {
                nullable = adjustValues[1];
                num1 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2;
            }
            else
            {
                nullable = adjustValues[1];
                num1 = odrawAdj2Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2);
            }
            int num = num1;
            if (!reverseAdj2)
            {
                nullable = adjustValues[0];
                num5 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1;
            }
            else
            {
                nullable = adjustValues[0];
                num5 = odrawAdj1Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1);
            }
            int num2 = num5;
            int num3 = (int) Math.Round((double) (maxAdj1 * (((double) num) / ((double) odrawAdj2Max))));
            int num4 = (int) Math.Round((double) (maxAdj2 * (((double) num2) / ((double) odrawAdj1Max))));
            adjustValues[0] = new int?(num3);
            adjustValues[1] = new int?(num4);
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
            int? nullable = adjustValues[0];
            int num2 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1;
            nullable = adjustValues[1];
            int num4 = (int) Math.Round((double) ((num2 - 0x2a30) * num));
            int num5 = (int) Math.Round((double) (((double) (((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2) - 0x2a30)) * num));
            adjustValues[0] = new int?(num4);
            adjustValues[1] = new int?(num5);
        }

        private static void CalculateConnectorCore(int?[] adjustValues, int odrawAdj, int adjCount)
        {
            double num = 4.62962962962963;
            for (int i = 0; i < adjCount; i++)
            {
                int? nullable = adjustValues[i];
                adjustValues[i] = new int?((int) Math.Round((double) (((nullable != null) ? ((double) nullable.GetValueOrDefault()) : ((double) odrawAdj)) * num)));
            }
        }

        private static int CalculateCurvedArrowAdj3(int shapeWidth, int shapeHeight, int minSide, bool vertical, bool reverseAdj3, int adj3In, int adj1, int openXmlAdj1Max)
        {
            int num = Pin(0, adj1, openXmlAdj1Max);
            double num3 = (minSide * num) / 100000.0;
            double num6 = (((vertical ? ((double) shapeWidth) : ((double) shapeHeight)) / 2.0) - ((num3 + ((minSide * openXmlAdj1Max) / 100000.0)) / 4.0)) * 2.0;
            double num8 = Math.Sqrt(Math.Abs((double) ((num6 * num6) - (num3 * num3))));
            if (num6 == 0.0)
            {
                return 0xc350;
            }
            double num9 = (num8 * (vertical ? ((double) shapeHeight) : ((double) shapeWidth))) / num6;
            int num10 = (int) Math.Round((double) ((100000.0 * num9) / ((double) minSide)));
            int num11 = (int) Math.Round((double) ((21600.0 * num9) / (vertical ? ((double) shapeHeight) : ((double) shapeWidth))));
            int num12 = reverseAdj3 ? 0x5460 : num11;
            int num13 = reverseAdj3 ? (num12 - adj3In) : adj3In;
            int num15 = num12 - (reverseAdj3 ? (0x5460 - num11) : 0);
            return ((num15 != 0) ? ((int) Math.Round((double) (num10 * (((double) num13) / ((double) num15))))) : 0xc350);
        }

        private static void CalculateCurvedArrowCore(int?[] adjustValues, int shapeWidth, int shapeHeight, int minSide, int odrawAdj1, int odrawAdj2, int odrawAdj3, bool vertical, bool reverseAdj3)
        {
            int? nullable = adjustValues[0];
            int num = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1;
            nullable = adjustValues[1];
            int num2 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2;
            nullable = adjustValues[2];
            int num3 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj3;
            int num4 = (int) Math.Round((double) (((double) (0x5460 + num)) / 2.0));
            int z = (int) Math.Round((double) ((50000.0 * (vertical ? ((double) shapeWidth) : ((double) shapeHeight))) / ((double) minSide)));
            int num6 = 0x5460 - num;
            int y = (int) Math.Round((double) (z * (((double) num6) / 10800.0)));
            int num8 = Pin(0, y, z);
            int num9 = 0x5460 - num4;
            if (num9 != 0)
            {
                int num10 = (int) Math.Round((double) (num8 * (((double) (num2 - num4)) / ((double) num9))));
                int num11 = CalculateCurvedArrowAdj3(shapeWidth, shapeHeight, minSide, vertical, reverseAdj3, num3, num10, num8);
                adjustValues[0] = new int?(num10);
                adjustValues[1] = new int?(y);
                adjustValues[2] = new int?(num11);
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
            int? nullable = adjustValues[0];
            int num = (nullable != null) ? nullable.GetValueOrDefault() : 0x1518;
            nullable = adjustValues[1];
            int num2 = (nullable != null) ? nullable.GetValueOrDefault() : 0x1518;
            nullable = adjustValues[2];
            int num3 = (nullable != null) ? nullable.GetValueOrDefault() : 0x49d4;
            int num4 = 0xa8c;
            int num6 = 0x5460 - num2;
            int y = (int) Math.Round((double) (100000.0 * (((double) num2) / 21600.0)));
            int num8 = 0x1fa0 - num4;
            if (num8 != 0)
            {
                int num10 = num8 - (num - num4);
                int num11 = ((int) Math.Round((double) (0xc32b * (((double) num10) / ((double) num8))))) + 0x61cd;
                int num12 = Pin(0, y, 0x186a0);
                int num13 = 0x5460 - num3;
                int num14 = 0x5460 - num6;
                if (num14 != 0)
                {
                    int num15 = (int) Math.Round((double) (num12 * (((double) num13) / ((double) num14))));
                    adjustValues[0] = new int?(y);
                    adjustValues[1] = new int?(num11);
                    adjustValues[2] = new int?(num15);
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
            int num = 0xa8c;
            int num2 = 0x1fa0;
            int? nullable = adjustValues[0];
            int num3 = (nullable != null) ? nullable.GetValueOrDefault() : 0x1518;
            nullable = adjustValues[1];
            int num4 = (nullable != null) ? nullable.GetValueOrDefault() : 0x3f48;
            nullable = adjustValues[2];
            int num5 = (nullable != null) ? nullable.GetValueOrDefault() : 0xa8c;
            int num6 = 0x5460 - num4;
            int num7 = num2 - num;
            if ((num7 != 0) && (num6 != 0))
            {
                int y = (int) Math.Round((double) (100000.0 * (((double) (0x5460 - num4)) / 21600.0)));
                int num11 = num7 - (num3 - num);
                int num12 = ((int) Math.Round((double) (0xc32b * (((double) num11) / ((double) num7))))) + 0x61cd;
                int num14 = (int) Math.Round((double) (Pin(0, y, 0x186a0) * (((double) num5) / ((double) num6))));
                adjustValues[0] = new int?(y);
                adjustValues[1] = new int?(num12);
                adjustValues[2] = new int?(num14);
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
            int? nullable;
            int num1;
            int num13;
            int num14;
            int num15;
            if (!reverseAdj1)
            {
                nullable = adjustValues[3];
                num1 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj4;
            }
            else
            {
                nullable = adjustValues[3];
                num1 = odrawAdj4Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj4);
            }
            int num = num1;
            if (!reverseAdj2)
            {
                nullable = adjustValues[1];
                num13 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2;
            }
            else
            {
                nullable = adjustValues[1];
                num13 = odrawAdj2Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2);
            }
            int num2 = num13;
            if (!reverseAdj3)
            {
                nullable = adjustValues[2];
                num14 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj3;
            }
            else
            {
                nullable = adjustValues[2];
                num14 = odrawAdj3Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj3);
            }
            int num3 = num14;
            if (!reverseAdj4)
            {
                nullable = adjustValues[0];
                num15 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1;
            }
            else
            {
                nullable = adjustValues[0];
                num15 = odrawAdj1Max - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1);
            }
            int num4 = num15;
            if ((num2 != 0) && (num3 != odrawAdj3Max))
            {
                int y = (int) Math.Round((double) (maxAdj2 * (((double) num2) / ((double) odrawAdj2Max))));
                int num7 = (int) Math.Round((double) ((Pin(0, y, maxAdj2) * 2) * (((double) num) / ((double) num2))));
                int num8 = (int) Math.Round((double) (maxAdj3 * (((double) num3) / ((double) odrawAdj3Max))));
                int num12 = (int) Math.Round((double) ((0x186a0 - ((int) Math.Round((double) ((Pin(0, num8, maxAdj3) * minSide) / ((double) q2Divider))))) * (((double) num4) / ((double) (odrawAdj3Max - num3)))));
                adjustValues[0] = new int?(num7);
                adjustValues[1] = new int?(y);
                adjustValues[2] = new int?(num8);
                adjustValues[3] = new int?(num12);
            }
        }

        private static void CalculateDownRibbon(int?[] adjustValues)
        {
            CalculateRibbonCore(adjustValues, 0x1518, 0xa8c, 0x1fa4, 0xa8c, 0, 0x1c20, 0x8235, 0x61a8, 0x124f8, false);
        }

        private static void CalculateHomePlate(int?[] adjustValues, int shapeWidth, int minSide)
        {
            int num = (int) Math.Round((double) ((((double) shapeWidth) / ((double) minSide)) * 100000.0));
            int? nullable = adjustValues[0];
            int num2 = 0x5460 - ((nullable != null) ? nullable.GetValueOrDefault() : 0x3f48);
            num2 = (int) Math.Round((double) (num * (((double) num2) / 21600.0)));
            adjustValues[0] = new int?(num2);
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
                for (int i = 0; i < odrawValues.Length; i++)
                {
                    int? nullable = adjustValues[i];
                    nullableArray[index] = new int?((int) Math.Round((double) (((nullable != null) ? ((double) nullable.GetValueOrDefault()) : ((double) odrawValues[i])) * num)));
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
                    nullable4 = new double?(nullable.GetValueOrDefault() * num2);
                }
                else
                {
                    nullable3 = null;
                    nullable4 = nullable3;
                }
                adjustValues[i] = new int?((int) nullable4.Value);
            }
        }

        private static int CalculateRangeCore(int value, int odrawMinValue, int odrawMaxValue, int openXmlMinValue, int openXmlMaxValue)
        {
            int num = (odrawMaxValue - odrawMinValue) / 2;
            int num2 = odrawMinValue + num;
            double num3 = ((double) openXmlMaxValue) / ((double) num);
            if (value == num2)
            {
                return 0;
            }
            value = (value <= num2) ? ((int) Math.Round((double) ((value - num2) * num3))) : ((int) Math.Round((double) (openXmlMaxValue - ((odrawMaxValue - value) * num3))));
            if (value < openXmlMinValue)
            {
                value = openXmlMinValue;
            }
            if (value > openXmlMaxValue)
            {
                value = openXmlMaxValue;
            }
            return value;
        }

        private static void CalculateRibbonCore(int?[] adjustValues, int odrawAdj1, int odrawAdj1Min, int odrawAdj1Max, int odrawAdj2, int odrawAdj2Min, int odrawAdj2Max, int openXmlAdj1Max, int openXmlAdj2Min, int openXmlAdj2Max, bool reverseAdj1)
        {
            int? nullable;
            int num1;
            int num = odrawAdj1Max - odrawAdj1Min;
            int num2 = odrawAdj2Max - odrawAdj2Min;
            if (!reverseAdj1)
            {
                nullable = adjustValues[1];
                num1 = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2;
            }
            else
            {
                nullable = adjustValues[1];
                num1 = num2 - (((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2) - odrawAdj2Min);
            }
            int num3 = num1;
            nullable = adjustValues[0];
            int num4 = num - (((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1) - odrawAdj1Min);
            num3 = (int) Math.Round((double) (openXmlAdj1Max * (((double) num3) / ((double) num2))));
            num4 = ((int) Math.Round((double) ((openXmlAdj2Max - openXmlAdj2Min) * (((double) num4) / ((double) num))))) + openXmlAdj2Min;
            adjustValues[0] = new int?(num3);
            adjustValues[1] = new int?(num4);
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
            int? nullable = adjustValues[0];
            int num = CalculateRangeCore((nullable != null) ? nullable.GetValueOrDefault() : 0x4470, 0x3c96, 0x4470, -4653, 0x122d);
            adjustValues[0] = new int?(num);
        }

        private static void CalculateStar16(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0xa8c);
        }

        private static void CalculateStar24(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0xa8c);
        }

        private static void CalculateStar32(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0xa8c);
        }

        private static void CalculateStar4(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0x1fa4);
        }

        private static void CalculateStar8(int?[] adjustValues)
        {
            CalculateStarCore(adjustValues, 0x9ea);
        }

        private static void CalculateStarCore(int?[] adjustValues, int odrawAdj1)
        {
            int? nullable = adjustValues[0];
            int num = (int) Math.Round((double) (50000.0 * (((double) (0x2a30 - ((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1))) / 10800.0)));
            adjustValues[0] = new int?(num);
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
            int num = (int) Math.Round((double) ((50000.0 * shapeHeight) / ((double) minSide)));
            int? nullable = adjustValues[0];
            int num2 = 0x2a30 - ((nullable != null) ? nullable.GetValueOrDefault() : 0x1518);
            nullable = adjustValues[1];
            int num3 = (nullable != null) ? nullable.GetValueOrDefault() : 0x10e0;
            num2 = (int) Math.Round((double) (100000.0 * (((double) num2) / 10800.0)));
            num3 = (int) Math.Round((double) (num * (((double) num3) / 10800.0)));
            adjustValues[0] = new int?(num2);
            adjustValues[1] = new int?(num3);
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
            int? nullable = adjustValues[0];
            int num = (nullable != null) ? nullable.GetValueOrDefault() : odrawAdj1;
            if (num > odrawAdj1Max)
            {
                num = odrawAdj1Max;
            }
            num = (int) Math.Round((double) (openXmlAdj1Max * (((double) num) / ((double) odrawAdj1Max))));
            nullable = adjustValues[1];
            int num2 = CalculateRangeCore((nullable != null) ? nullable.GetValueOrDefault() : odrawAdj2, 0x21c0, 0x32a0, -10000, 0x2710);
            adjustValues[0] = new int?(num);
            adjustValues[1] = new int?(num2);
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

        private static long ConvertWordArtAdj1(int? value, long defaultValue)
        {
            double? nullable1;
            if (value == null)
            {
                return defaultValue;
            }
            int? nullable = value;
            if (nullable != null)
            {
                nullable1 = new double?(((double) nullable.GetValueOrDefault()) * 9.259);
            }
            else
            {
                nullable1 = null;
            }
            return (long) nullable1.Value;
        }

        private static long ConvertWordArtAdj2(int? value, long defaultValue)
        {
            double? nullable1;
            if (value == null)
            {
                return defaultValue;
            }
            int? nullable = value;
            if (nullable != null)
            {
                nullable1 = new double?(((double) (nullable.GetValueOrDefault() - 0x2a30)) / 0.216);
            }
            else
            {
                nullable1 = null;
            }
            return (long) nullable1.Value;
        }

        private static long ConvertWordArtAdj3(int? value, long defaultValue)
        {
            double? nullable1;
            if (value == null)
            {
                return defaultValue;
            }
            int? nullable = value;
            if (nullable != null)
            {
                nullable1 = new double?(((double) nullable.GetValueOrDefault()) * 4.63);
            }
            else
            {
                nullable1 = null;
            }
            return (long) nullable1.Value;
        }

        private static long ConvertWordArtAdj5(int? value, double width, double height, long defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }
            double num = (((double) value.Value) / 65536.0) % 360.0;
            if (num < 0.0)
            {
                num += 360.0;
            }
            double num3 = (Math.Atan((Math.Tan((num / 180.0) * 3.1415926535897931) * height) / width) / 3.1415926535897931) * 180.0;
            return (((90.0 >= num) || (num > 270.0)) ? ((long) (((360.0 + num3) % 360.0) * 60000.0)) : ((long) (((180.0 + num3) % 360.0) * 60000.0)));
        }

        public static List<long> GetWordArtAdjustValues(DrawingPresetTextWarp presetTextWarp, float shapeWidth, float shapeHeight, int?[] adjs)
        {
            int? nullable = adjs[0];
            int? nullable2 = adjs[1];
            List<long> list = new List<long>();
            switch (presetTextWarp)
            {
                case DrawingPresetTextWarp.NoShape:
                    break;

                case DrawingPresetTextWarp.ArchDown:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0L));
                    break;

                case DrawingPresetTextWarp.ArchDownPour:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0L));
                    list.Add(ConvertWordArtAdj1(nullable2, 0xc350L));
                    break;

                case DrawingPresetTextWarp.ArchUp:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0xa4cb80L));
                    break;

                case DrawingPresetTextWarp.ArchUpPour:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0xa4cb80L));
                    list.Add(ConvertWordArtAdj1(nullable2, 0xc350L));
                    break;

                case DrawingPresetTextWarp.Button:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0xa4cb80L));
                    break;

                case DrawingPresetTextWarp.ButtonPour:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0xa4cb80L));
                    list.Add(ConvertWordArtAdj1(nullable2, 0xc350L));
                    break;

                case DrawingPresetTextWarp.CanDown:
                    list.Add(ConvertWordArtAdj3(nullable, 0x37cfL));
                    break;

                case DrawingPresetTextWarp.CanUp:
                    list.Add(ConvertWordArtAdj3(nullable, 0x14ed1L));
                    break;

                case DrawingPresetTextWarp.CascadeDown:
                    list.Add(ConvertWordArtAdj3(nullable, 0xad9cL));
                    break;

                case DrawingPresetTextWarp.CascadeUp:
                    list.Add(ConvertWordArtAdj3(nullable, 0xad9cL));
                    break;

                case DrawingPresetTextWarp.Chevron:
                    list.Add(ConvertWordArtAdj3(nullable, 0x61a8L));
                    break;

                case DrawingPresetTextWarp.ChevronInverted:
                    list.Add(ConvertWordArtAdj3(nullable, 0x124f8L));
                    break;

                case DrawingPresetTextWarp.Circle:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0xa4f3a9L));
                    break;

                case DrawingPresetTextWarp.CirclePour:
                    list.Add(ConvertWordArtAdj5(nullable, (double) shapeWidth, (double) shapeHeight, 0xa4f3a9L));
                    list.Add(ConvertWordArtAdj1(nullable2, 0xc350L));
                    break;

                case DrawingPresetTextWarp.CurveDown:
                    list.Add(ConvertWordArtAdj3(nullable, 0xa9d5L));
                    break;

                case DrawingPresetTextWarp.CurveUp:
                    list.Add(ConvertWordArtAdj3(nullable, 0xb399L));
                    break;

                case DrawingPresetTextWarp.Deflate:
                    list.Add(ConvertWordArtAdj3(nullable, 0x493eL));
                    break;

                case DrawingPresetTextWarp.DeflateBottom:
                    list.Add(ConvertWordArtAdj3(nullable, 0xcf85L));
                    break;

                case DrawingPresetTextWarp.DeflateInflate:
                    list.Add(ConvertWordArtAdj3(nullable, 0x6d7cL));
                    break;

                case DrawingPresetTextWarp.InflateDeflate:
                    list.Add(ConvertWordArtAdj3(nullable, 0x6d7cL));
                    break;

                case DrawingPresetTextWarp.DeflateTop:
                    list.Add(ConvertWordArtAdj3(nullable, 0xb71bL));
                    break;

                case DrawingPresetTextWarp.DoubleWave1:
                    list.Add(ConvertWordArtAdj3(nullable, 0x1964L));
                    list.Add(ConvertWordArtAdj2(nullable2, 0L));
                    break;

                case DrawingPresetTextWarp.FadeDown:
                    list.Add(ConvertWordArtAdj3(nullable, 0x8235L));
                    break;

                case DrawingPresetTextWarp.FadeLeft:
                case DrawingPresetTextWarp.FadeRight:
                case DrawingPresetTextWarp.FadeUp:
                    list.Add(ConvertWordArtAdj3(nullable, 0x8235L));
                    break;

                case DrawingPresetTextWarp.Inflate:
                    list.Add(ConvertWordArtAdj3(nullable, 0x3542L));
                    break;

                case DrawingPresetTextWarp.InflateBottom:
                    list.Add(ConvertWordArtAdj3(nullable, 0x109f3L));
                    break;

                case DrawingPresetTextWarp.InflateTop:
                    list.Add(ConvertWordArtAdj3(nullable, 0x7cadL));
                    break;

                case DrawingPresetTextWarp.Plain:
                    list.Add(ConvertWordArtAdj3(nullable, 0xc350L));
                    break;

                case DrawingPresetTextWarp.RingInside:
                case DrawingPresetTextWarp.RingOutside:
                    list.Add(ConvertWordArtAdj3(nullable, 0xf424L));
                    break;

                case DrawingPresetTextWarp.SlantDown:
                    list.Add(ConvertWordArtAdj3(nullable, 0xad9cL));
                    break;

                case DrawingPresetTextWarp.SlantUp:
                    list.Add(ConvertWordArtAdj3(nullable, 0xd904L));
                    break;

                case DrawingPresetTextWarp.Stop:
                    list.Add(ConvertWordArtAdj3(nullable, 0x56ceL));
                    break;

                case DrawingPresetTextWarp.Triangle:
                case DrawingPresetTextWarp.TriangleInverted:
                    list.Add(ConvertWordArtAdj3(nullable, 0xc350L));
                    break;

                case DrawingPresetTextWarp.Wave1:
                case DrawingPresetTextWarp.Wave2:
                    list.Add(ConvertWordArtAdj3(nullable, 0x32cdL));
                    list.Add(ConvertWordArtAdj2(nullable2, 0L));
                    break;

                case DrawingPresetTextWarp.Wave4:
                    list.Add(ConvertWordArtAdj3(nullable, 0x1964L));
                    list.Add(ConvertWordArtAdj2(nullable2, 0L));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return list;
        }

        private static int Pin(int x, int y, int z) => 
            (y < x) ? x : ((y > z) ? z : y);
    }
}

