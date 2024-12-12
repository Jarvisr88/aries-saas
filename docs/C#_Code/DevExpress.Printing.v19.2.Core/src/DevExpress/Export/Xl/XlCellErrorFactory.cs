namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class XlCellErrorFactory
    {
        private static readonly Dictionary<XlCellErrorType, XlVariantValue> errorConversionTable = CreateErrorConversionTable();
        private static readonly Dictionary<string, IXlCellError> errorByInvariantNameTable = CreateErrorByInvariantNameTable();

        public static XlVariantValue CreateError(XlCellErrorType errorType) => 
            errorConversionTable[errorType];

        private static Dictionary<string, IXlCellError> CreateErrorByInvariantNameTable() => 
            new Dictionary<string, IXlCellError>(StringExtensions.ComparerInvariantCultureIgnoreCase) { 
                { 
                    "#NULL!",
                    NullIntersectionError.Instance
                },
                { 
                    "#DIV/0!",
                    DivisionByZeroError.Instance
                },
                { 
                    "#VALUE!",
                    InvalidValueInFunctionError.Instance
                },
                { 
                    "#REF!",
                    ReferenceError.Instance
                },
                { 
                    "#NAME?",
                    NameError.Instance
                },
                { 
                    "#NUM!",
                    NumberError.Instance
                },
                { 
                    "#N/A",
                    ValueNotAvailableError.Instance
                }
            };

        private static Dictionary<XlCellErrorType, XlVariantValue> CreateErrorConversionTable() => 
            new Dictionary<XlCellErrorType, XlVariantValue> { 
                { 
                    XlCellErrorType.DivisionByZero,
                    XlVariantValue.ErrorDivisionByZero
                },
                { 
                    XlCellErrorType.Name,
                    XlVariantValue.ErrorName
                },
                { 
                    XlCellErrorType.NotAvailable,
                    XlVariantValue.ErrorValueNotAvailable
                },
                { 
                    XlCellErrorType.Null,
                    XlVariantValue.ErrorNullIntersection
                },
                { 
                    XlCellErrorType.Number,
                    XlVariantValue.ErrorNumber
                },
                { 
                    XlCellErrorType.Reference,
                    XlVariantValue.ErrorReference
                },
                { 
                    XlCellErrorType.Value,
                    XlVariantValue.ErrorInvalidValueInFunction
                }
            };

        public static bool TryCreateErrorByInvariantName(string name, out IXlCellError error)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return errorByInvariantNameTable.TryGetValue(name, out error);
            }
            error = null;
            return false;
        }
    }
}

