namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleIconSet : XlCondFmtRuleWithGuid
    {
        private static Dictionary<XlCondFmtIconSetType, int> iconSetCountTable = CreateIconSetCountTable();
        private XlCondFmtIconSetType iconSetType;
        private readonly List<XlCondFmtValueObject> values;
        private readonly List<XlCondFmtCustomIcon> customIcons;

        public XlCondFmtRuleIconSet() : base(XlCondFmtType.IconSet)
        {
            this.values = new List<XlCondFmtValueObject>();
            this.customIcons = new List<XlCondFmtCustomIcon>();
            this.iconSetType = XlCondFmtIconSetType.TrafficLights3;
            this.Percent = true;
            this.ShowValues = true;
            this.SetupValues();
        }

        private static Dictionary<XlCondFmtIconSetType, int> CreateIconSetCountTable() => 
            new Dictionary<XlCondFmtIconSetType, int> { 
                { 
                    XlCondFmtIconSetType.Arrows3,
                    3
                },
                { 
                    XlCondFmtIconSetType.ArrowsGray3,
                    3
                },
                { 
                    XlCondFmtIconSetType.Flags3,
                    3
                },
                { 
                    XlCondFmtIconSetType.TrafficLights3,
                    3
                },
                { 
                    XlCondFmtIconSetType.TrafficLights3Black,
                    3
                },
                { 
                    XlCondFmtIconSetType.Signs3,
                    3
                },
                { 
                    XlCondFmtIconSetType.Symbols3,
                    3
                },
                { 
                    XlCondFmtIconSetType.Symbols3Circled,
                    3
                },
                { 
                    XlCondFmtIconSetType.Stars3,
                    3
                },
                { 
                    XlCondFmtIconSetType.Triangles3,
                    3
                },
                { 
                    XlCondFmtIconSetType.Arrows4,
                    4
                },
                { 
                    XlCondFmtIconSetType.ArrowsGray4,
                    4
                },
                { 
                    XlCondFmtIconSetType.RedToBlack4,
                    4
                },
                { 
                    XlCondFmtIconSetType.Rating4,
                    4
                },
                { 
                    XlCondFmtIconSetType.TrafficLights4,
                    4
                },
                { 
                    XlCondFmtIconSetType.Arrows5,
                    5
                },
                { 
                    XlCondFmtIconSetType.ArrowsGray5,
                    5
                },
                { 
                    XlCondFmtIconSetType.Rating5,
                    5
                },
                { 
                    XlCondFmtIconSetType.Quarters5,
                    5
                },
                { 
                    XlCondFmtIconSetType.Boxes5,
                    5
                },
                { 
                    XlCondFmtIconSetType.NoIcons,
                    1
                }
            };

        private void SetupValues()
        {
            int num = iconSetCountTable[this.iconSetType];
            this.Values.Clear();
            if (num == 3)
            {
                XlCondFmtValueObject item = new XlCondFmtValueObject();
                item.ObjectType = XlCondFmtValueObjectType.Percent;
                item.Value = 0.0;
                this.Values.Add(item);
                XlCondFmtValueObject obj2 = new XlCondFmtValueObject();
                obj2.ObjectType = XlCondFmtValueObjectType.Percent;
                obj2.Value = 33.0;
                this.Values.Add(obj2);
                XlCondFmtValueObject obj3 = new XlCondFmtValueObject();
                obj3.ObjectType = XlCondFmtValueObjectType.Percent;
                obj3.Value = 67.0;
                this.Values.Add(obj3);
            }
            else if (num == 4)
            {
                XlCondFmtValueObject item = new XlCondFmtValueObject();
                item.ObjectType = XlCondFmtValueObjectType.Percent;
                item.Value = 0.0;
                this.Values.Add(item);
                XlCondFmtValueObject obj5 = new XlCondFmtValueObject();
                obj5.ObjectType = XlCondFmtValueObjectType.Percent;
                obj5.Value = 25.0;
                this.Values.Add(obj5);
                XlCondFmtValueObject obj6 = new XlCondFmtValueObject();
                obj6.ObjectType = XlCondFmtValueObjectType.Percent;
                obj6.Value = 50.0;
                this.Values.Add(obj6);
                XlCondFmtValueObject obj7 = new XlCondFmtValueObject();
                obj7.ObjectType = XlCondFmtValueObjectType.Percent;
                obj7.Value = 75.0;
                this.Values.Add(obj7);
            }
            else if (num == 5)
            {
                XlCondFmtValueObject item = new XlCondFmtValueObject();
                item.ObjectType = XlCondFmtValueObjectType.Percent;
                item.Value = 0.0;
                this.Values.Add(item);
                XlCondFmtValueObject obj9 = new XlCondFmtValueObject();
                obj9.ObjectType = XlCondFmtValueObjectType.Percent;
                obj9.Value = 20.0;
                this.Values.Add(obj9);
                XlCondFmtValueObject obj10 = new XlCondFmtValueObject();
                obj10.ObjectType = XlCondFmtValueObjectType.Percent;
                obj10.Value = 40.0;
                this.Values.Add(obj10);
                XlCondFmtValueObject obj11 = new XlCondFmtValueObject();
                obj11.ObjectType = XlCondFmtValueObjectType.Percent;
                obj11.Value = 60.0;
                this.Values.Add(obj11);
                XlCondFmtValueObject obj12 = new XlCondFmtValueObject();
                obj12.ObjectType = XlCondFmtValueObjectType.Percent;
                obj12.Value = 80.0;
                this.Values.Add(obj12);
            }
        }

        internal static Dictionary<XlCondFmtIconSetType, int> IconSetCountTable =>
            iconSetCountTable;

        public XlCondFmtIconSetType IconSetType
        {
            get => 
                this.iconSetType;
            set
            {
                if (this.iconSetType != value)
                {
                    if (value == XlCondFmtIconSetType.NoIcons)
                    {
                        throw new ArgumentException("Invalid icon set type!");
                    }
                    this.iconSetType = value;
                    this.customIcons.Clear();
                    this.SetupValues();
                }
            }
        }

        public bool Percent { get; set; }

        public bool Reverse { get; set; }

        public bool ShowValues { get; set; }

        public IList<XlCondFmtValueObject> Values =>
            this.values;

        public IList<XlCondFmtCustomIcon> CustomIcons =>
            this.customIcons;

        public bool IsCustom =>
            this.customIcons.Count > 0;
    }
}

