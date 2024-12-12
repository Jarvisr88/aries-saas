namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public class XlPtgBinaryOperator : XlPtgOperator
    {
        private static readonly Dictionary<int, string> typeCodeToOperatorTextConversionTable = CreateTypeCodeToOperatorTextConvetsionTable();

        public XlPtgBinaryOperator(int typeCode) : base(typeCode)
        {
        }

        private static Dictionary<int, string> CreateTypeCodeToOperatorTextConvetsionTable() => 
            new Dictionary<int, string> { 
                { 
                    3,
                    "+"
                },
                { 
                    4,
                    "-"
                },
                { 
                    5,
                    "*"
                },
                { 
                    6,
                    "/"
                },
                { 
                    7,
                    "^"
                },
                { 
                    8,
                    "&"
                },
                { 
                    9,
                    "<"
                },
                { 
                    10,
                    "<="
                },
                { 
                    11,
                    "="
                },
                { 
                    12,
                    ">="
                },
                { 
                    13,
                    ">"
                },
                { 
                    14,
                    "<>"
                },
                { 
                    15,
                    " "
                },
                { 
                    0x10,
                    ","
                },
                { 
                    0x11,
                    ":"
                }
            };

        protected override bool IsValidTypeCode(int typeCode) => 
            (typeCode >= 3) && (typeCode <= 0x11);

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string OperatorText =>
            typeCodeToOperatorTextConversionTable[this.TypeCode];
    }
}

