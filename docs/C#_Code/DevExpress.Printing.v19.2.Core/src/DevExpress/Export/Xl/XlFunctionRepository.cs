namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    internal static class XlFunctionRepository
    {
        public const int UnknownFunction = 0xff;
        private static readonly Dictionary<int, XlFunctionInfo> functionCodeToFunctionInfoConversionTable = CreateFunctionCodeToNameConversionTable();

        private static Dictionary<int, XlFunctionInfo> CreateFunctionCodeToNameConversionTable() => 
            new Dictionary<int, XlFunctionInfo> { 
                { 
                    270,
                    new XlFunctionInfo("BETADIST", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0x110,
                    new XlFunctionInfo("BETAINV", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0x111,
                    new XlFunctionInfo("BINOMDIST", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x112,
                    new XlFunctionInfo("CHIDIST", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x113,
                    new XlFunctionInfo("CHIINV", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x132,
                    new XlFunctionInfo("CHITEST", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x115,
                    new XlFunctionInfo("CONFIDENCE", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x134,
                    new XlFunctionInfo("COVAR", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x116,
                    new XlFunctionInfo("CRITBINOM", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    280,
                    new XlFunctionInfo("EXPONDIST", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x119,
                    new XlFunctionInfo("FDIST", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x11a,
                    new XlFunctionInfo("FINV", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    310,
                    new XlFunctionInfo("FTEST", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x11e,
                    new XlFunctionInfo("GAMMADIST", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x11f,
                    new XlFunctionInfo("GAMMAINV", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x121,
                    new XlFunctionInfo("HYPGEOMDIST", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x123,
                    new XlFunctionInfo("LOGINV", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    290,
                    new XlFunctionInfo("LOGNORMDIST", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    330,
                    new XlFunctionInfo("MODE", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x124,
                    new XlFunctionInfo("NEGBINOMDIST", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x125,
                    new XlFunctionInfo("NORMDIST", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x127,
                    new XlFunctionInfo("NORMINV", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x126,
                    new XlFunctionInfo("NORMSDIST", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x128,
                    new XlFunctionInfo("NORMSINV", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x148,
                    new XlFunctionInfo("PERCENTILE", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x149,
                    new XlFunctionInfo("PERCENTRANK", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    300,
                    new XlFunctionInfo("POISSON", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x147,
                    new XlFunctionInfo("QUARTILE", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xd8,
                    new XlFunctionInfo("RANK", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    12,
                    new XlFunctionInfo("STDEV", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0xc1,
                    new XlFunctionInfo("STDEVP", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x12d,
                    new XlFunctionInfo("TDIST", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x14c,
                    new XlFunctionInfo("TINV", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x13c,
                    new XlFunctionInfo("TTEST", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x2e,
                    new XlFunctionInfo("VAR", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0xc2,
                    new XlFunctionInfo("VARP", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x12e,
                    new XlFunctionInfo("WEIBULL", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x144,
                    new XlFunctionInfo("ZTEST", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x2a,
                    new XlFunctionInfo("DAVERAGE", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    40,
                    new XlFunctionInfo("DCOUNT", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xc7,
                    new XlFunctionInfo("DCOUNTA", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xeb,
                    new XlFunctionInfo("DGET", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x2c,
                    new XlFunctionInfo("DMAX", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x2b,
                    new XlFunctionInfo("DMIN", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xbd,
                    new XlFunctionInfo("DPRODUCT", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x2d,
                    new XlFunctionInfo("DSTDEV", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xc3,
                    new XlFunctionInfo("DSTDEVP", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x29,
                    new XlFunctionInfo("DSUM", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x2f,
                    new XlFunctionInfo("DVAR", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xc4,
                    new XlFunctionInfo("DVARP", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x41,
                    new XlFunctionInfo("DATE", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    140,
                    new XlFunctionInfo("DATEVALUE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x43,
                    new XlFunctionInfo("DAY", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4042,
                    new XlFunctionInfo("DAYS", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    220,
                    new XlFunctionInfo("DAYS360", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x1c1,
                    new XlFunctionInfo("EDATE", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    450,
                    new XlFunctionInfo("EOMONTH", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x47,
                    new XlFunctionInfo("HOUR", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4043,
                    new XlFunctionInfo("ISOWEEKNUM", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x48,
                    new XlFunctionInfo("MINUTE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x44,
                    new XlFunctionInfo("MONTH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x1d8,
                    new XlFunctionInfo("NETWORKDAYS", 2, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x400f,
                    new XlFunctionInfo("NETWORKDAYS.INTL", 3, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4a,
                    new XlFunctionInfo("NOW", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    0x49,
                    new XlFunctionInfo("SECOND", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x42,
                    new XlFunctionInfo("TIME", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x8d,
                    new XlFunctionInfo("TIMEVALUE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xdd,
                    new XlFunctionInfo("TODAY", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    70,
                    new XlFunctionInfo("WEEKDAY", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x1d1,
                    new XlFunctionInfo("WEEKNUM", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1d7,
                    new XlFunctionInfo("WORKDAY", 2, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x400e,
                    new XlFunctionInfo("WORKDAY.INTL", 3, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x45,
                    new XlFunctionInfo("YEAR", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x1c3,
                    new XlFunctionInfo("YEARFRAC", 2, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x15f,
                    new XlFunctionInfo("DATEDIF", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x1ac,
                    new XlFunctionInfo("BESSELI", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1a9,
                    new XlFunctionInfo("BESSELJ", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1aa,
                    new XlFunctionInfo("BESSELK", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1ab,
                    new XlFunctionInfo("BESSELY", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x189,
                    new XlFunctionInfo("BIN2DEC", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x18b,
                    new XlFunctionInfo("BIN2HEX", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x18a,
                    new XlFunctionInfo("BIN2OCT", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x402a,
                    new XlFunctionInfo("BITAND", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x402b,
                    new XlFunctionInfo("BITLSHIFT", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x402c,
                    new XlFunctionInfo("BITOR", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x402d,
                    new XlFunctionInfo("BITRSHIFT", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x402e,
                    new XlFunctionInfo("BITXOR", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x19b,
                    new XlFunctionInfo("COMPLEX", 2, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1d4,
                    new XlFunctionInfo("CONVERT", 3, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x183,
                    new XlFunctionInfo("DEC2BIN", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x184,
                    new XlFunctionInfo("DEC2HEX", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x185,
                    new XlFunctionInfo("DEC2OCT", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1a2,
                    new XlFunctionInfo("DELTA", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1a7,
                    new XlFunctionInfo("ERF", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4011,
                    new XlFunctionInfo("ERF.PRECISE", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x1a8,
                    new XlFunctionInfo("ERFC", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4012,
                    new XlFunctionInfo("ERFC.PRECISE", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x1a3,
                    new XlFunctionInfo("GESTEP", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x180,
                    new XlFunctionInfo("HEX2BIN", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x181,
                    new XlFunctionInfo("HEX2DEC", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x182,
                    new XlFunctionInfo("HEX2OCT", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x18f,
                    new XlFunctionInfo("IMABS", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x199,
                    new XlFunctionInfo("IMAGINARY", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x197,
                    new XlFunctionInfo("IMARGUMENT", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x198,
                    new XlFunctionInfo("IMCONJUGATE", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x195,
                    new XlFunctionInfo("IMCOS", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x402f,
                    new XlFunctionInfo("IMCOSH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4030,
                    new XlFunctionInfo("IMCOT", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4031,
                    new XlFunctionInfo("IMCSC", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4032,
                    new XlFunctionInfo("IMCSCH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x18d,
                    new XlFunctionInfo("IMDIV", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x196,
                    new XlFunctionInfo("IMEXP", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x191,
                    new XlFunctionInfo("IMLN", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x193,
                    new XlFunctionInfo("IMLOG10", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x192,
                    new XlFunctionInfo("IMLOG2", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x18e,
                    new XlFunctionInfo("IMPOWER", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x19d,
                    new XlFunctionInfo("IMPRODUCT", 1, 0xff, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    410,
                    new XlFunctionInfo("IMREAL", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4033,
                    new XlFunctionInfo("IMSEC", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4034,
                    new XlFunctionInfo("IMSECH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x194,
                    new XlFunctionInfo("IMSIN", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4035,
                    new XlFunctionInfo("IMSINH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    400,
                    new XlFunctionInfo("IMSQRT", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x18c,
                    new XlFunctionInfo("IMSUB", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x19c,
                    new XlFunctionInfo("IMSUM", 1, 0xff, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4036,
                    new XlFunctionInfo("IMTAN", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    390,
                    new XlFunctionInfo("OCT2BIN", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x188,
                    new XlFunctionInfo("OCT2DEC", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x187,
                    new XlFunctionInfo("OCT2HEX", 1, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    470,
                    new XlFunctionInfo("ACCRINTM", 4, 5, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1c4,
                    new XlFunctionInfo("COUPDAYBS", 3, 4, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1c7,
                    new XlFunctionInfo("COUPNCD", 3, 4, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1c8,
                    new XlFunctionInfo("COUPNUM", 3, 4, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1c9,
                    new XlFunctionInfo("COUPPCD", 3, 4, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1c0,
                    new XlFunctionInfo("CUMIPMT", 6, 6, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1bf,
                    new XlFunctionInfo("CUMPRINC", 6, 6, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0xf7,
                    new XlFunctionInfo("DB", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0x90,
                    new XlFunctionInfo("DDB", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0x1b3,
                    new XlFunctionInfo("DISC", 4, 5, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1bb,
                    new XlFunctionInfo("DOLLARDE", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1bc,
                    new XlFunctionInfo("DOLLARFR", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1be,
                    new XlFunctionInfo("EFFECT", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x39,
                    new XlFunctionInfo("FV", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0x1dc,
                    new XlFunctionInfo("FVSCHEDULE", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1b1,
                    new XlFunctionInfo("INTRATE", 4, 5, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0xa7,
                    new XlFunctionInfo("IPMT", 5, 6, XlFunctionProperty.Normal)
                },
                { 
                    0x3e,
                    new XlFunctionInfo("IRR", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    350,
                    new XlFunctionInfo("ISPMT", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x3d,
                    new XlFunctionInfo("MIRR", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x1bd,
                    new XlFunctionInfo("NOMINAL", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x3a,
                    new XlFunctionInfo("NPER", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    11,
                    new XlFunctionInfo("NPV", 2, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x403a,
                    new XlFunctionInfo("PDURATION", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x3b,
                    new XlFunctionInfo("PMT", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0xa8,
                    new XlFunctionInfo("PPMT", 5, 6, XlFunctionProperty.Normal)
                },
                { 
                    0x1b4,
                    new XlFunctionInfo("PRICEDISC", 4, 5, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x38,
                    new XlFunctionInfo("PV", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    60,
                    new XlFunctionInfo("RATE", 5, 6, XlFunctionProperty.Normal)
                },
                { 
                    0x1b2,
                    new XlFunctionInfo("RECEIVED", 4, 5, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x8e,
                    new XlFunctionInfo("SLN", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x8f,
                    new XlFunctionInfo("SYD", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x1b6,
                    new XlFunctionInfo("TBILLEQ", 3, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1b7,
                    new XlFunctionInfo("TBILLPRICE", 3, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    440,
                    new XlFunctionInfo("TBILLYIELD", 3, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0xde,
                    new XlFunctionInfo("VDB", 6, 7, XlFunctionProperty.Normal)
                },
                { 
                    0x403b,
                    new XlFunctionInfo("RRI", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    430,
                    new XlFunctionInfo("XNPV", 3, 3, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x1b5,
                    new XlFunctionInfo("YIELDDISC", 4, 5, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x7d,
                    new XlFunctionInfo("CELL", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x105,
                    new XlFunctionInfo("ERROR.TYPE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xf4,
                    new XlFunctionInfo("INFO", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x81,
                    new XlFunctionInfo("ISBLANK", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x7e,
                    new XlFunctionInfo("ISERR", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    3,
                    new XlFunctionInfo("ISERROR", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    420,
                    new XlFunctionInfo("ISEVEN", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x403f,
                    new XlFunctionInfo("ISFORMULA", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0xc6,
                    new XlFunctionInfo("ISLOGICAL", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    2,
                    new XlFunctionInfo("ISNA", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    190,
                    new XlFunctionInfo("ISNONTEXT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x80,
                    new XlFunctionInfo("ISNUMBER", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x1a5,
                    new XlFunctionInfo("ISODD", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x69,
                    new XlFunctionInfo("ISREF", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x7f,
                    new XlFunctionInfo("ISTEXT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x83,
                    new XlFunctionInfo("N", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    10,
                    new XlFunctionInfo("NA", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    0x4040,
                    new XlFunctionInfo("SHEET", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4041,
                    new XlFunctionInfo("SHEETS", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x56,
                    new XlFunctionInfo("TYPE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x24,
                    new XlFunctionInfo("AND", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x23,
                    new XlFunctionInfo("FALSE", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    1,
                    new XlFunctionInfo("IF", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    480,
                    new XlFunctionInfo("IFERROR", 2, 2, XlFunctionProperty.Excel2003Future)
                },
                { 
                    0x403c,
                    new XlFunctionInfo("IFNA", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x26,
                    new XlFunctionInfo("NOT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x25,
                    new XlFunctionInfo("OR", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x22,
                    new XlFunctionInfo("TRUE", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    0x403d,
                    new XlFunctionInfo("XOR", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0xdb,
                    new XlFunctionInfo("ADDRESS", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    0x4b,
                    new XlFunctionInfo("AREAS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    100,
                    new XlFunctionInfo("CHOOSE", 2, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    9,
                    new XlFunctionInfo("COLUMN", 0, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4d,
                    new XlFunctionInfo("COLUMNS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x403e,
                    new XlFunctionInfo("FORMULATEXT", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x166,
                    new XlFunctionInfo("GETPIVOTDATA", 2, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x65,
                    new XlFunctionInfo("HLOOKUP", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x167,
                    new XlFunctionInfo("HYPERLINK", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x1d,
                    new XlFunctionInfo("INDEX", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x94,
                    new XlFunctionInfo("INDIRECT", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x1c,
                    new XlFunctionInfo("LOOKUP", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x40,
                    new XlFunctionInfo("MATCH", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x4e,
                    new XlFunctionInfo("OFFSET", 4, 5, XlFunctionProperty.Normal)
                },
                { 
                    8,
                    new XlFunctionInfo("ROW", 0, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4c,
                    new XlFunctionInfo("ROWS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x17b,
                    new XlFunctionInfo("RTD", 3, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x53,
                    new XlFunctionInfo("TRANSPOSE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x66,
                    new XlFunctionInfo("VLOOKUP", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x18,
                    new XlFunctionInfo("ABS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x63,
                    new XlFunctionInfo("ACOS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xe9,
                    new XlFunctionInfo("ACOSH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4015,
                    new XlFunctionInfo("ACOT", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4016,
                    new XlFunctionInfo("ACOTH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4017,
                    new XlFunctionInfo("ARABIC", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x62,
                    new XlFunctionInfo("ASIN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xe8,
                    new XlFunctionInfo("ASINH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x12,
                    new XlFunctionInfo("ATAN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x61,
                    new XlFunctionInfo("ATAN2", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xea,
                    new XlFunctionInfo("ATANH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4018,
                    new XlFunctionInfo("BASE", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x120,
                    new XlFunctionInfo("CEILING", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4019,
                    new XlFunctionInfo("CEILING.MATH", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4071,
                    new XlFunctionInfo("CEILING.PRECISE", 1, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x114,
                    new XlFunctionInfo("COMBIN", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x401a,
                    new XlFunctionInfo("COMBINA", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x10,
                    new XlFunctionInfo("COS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    230,
                    new XlFunctionInfo("COSH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x401b,
                    new XlFunctionInfo("COT", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x401c,
                    new XlFunctionInfo("COTH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x401d,
                    new XlFunctionInfo("CSC", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x401e,
                    new XlFunctionInfo("CSCH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x401f,
                    new XlFunctionInfo("DECIMAL", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x157,
                    new XlFunctionInfo("DEGREES", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x117,
                    new XlFunctionInfo("EVEN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x15,
                    new XlFunctionInfo("EXP", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xb8,
                    new XlFunctionInfo("FACT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x19f,
                    new XlFunctionInfo("FACTDOUBLE", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x11d,
                    new XlFunctionInfo("FLOOR", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4020,
                    new XlFunctionInfo("FLOOR.MATH", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4073,
                    new XlFunctionInfo("FLOOR.PRECISE", 1, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x1d9,
                    new XlFunctionInfo("GCD", 1, 0xff, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x19,
                    new XlFunctionInfo("INT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4021,
                    new XlFunctionInfo("ISO.CEILING", 1, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x1db,
                    new XlFunctionInfo("LCM", 1, 0xff, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x16,
                    new XlFunctionInfo("LN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x6d,
                    new XlFunctionInfo("LOG", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x17,
                    new XlFunctionInfo("LOG10", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xa3,
                    new XlFunctionInfo("MDETERM", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xa4,
                    new XlFunctionInfo("MINVERSE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xa5,
                    new XlFunctionInfo("MMULT", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x27,
                    new XlFunctionInfo("MOD", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x1a6,
                    new XlFunctionInfo("MROUND", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4022,
                    new XlFunctionInfo("MUNIT", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x1da,
                    new XlFunctionInfo("MULTINOMIAL", 1, 0xff, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x12a,
                    new XlFunctionInfo("ODD", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x13,
                    new XlFunctionInfo("PI", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    0x151,
                    new XlFunctionInfo("POWER", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xb7,
                    new XlFunctionInfo("PRODUCT", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x1a1,
                    new XlFunctionInfo("QUOTIENT", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x156,
                    new XlFunctionInfo("RADIANS", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x3f,
                    new XlFunctionInfo("RAND", 0, 0, XlFunctionProperty.Normal)
                },
                { 
                    0x1d0,
                    new XlFunctionInfo("RANDBETWEEN", 2, 2, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x162,
                    new XlFunctionInfo("ROMAN", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x1b,
                    new XlFunctionInfo("ROUND", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xd5,
                    new XlFunctionInfo("ROUNDDOWN", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xd4,
                    new XlFunctionInfo("ROUNDUP", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x19e,
                    new XlFunctionInfo("SERIESSUM", 4, 4, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x4023,
                    new XlFunctionInfo("SEC", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4024,
                    new XlFunctionInfo("SECH", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x1a,
                    new XlFunctionInfo("SIGN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    15,
                    new XlFunctionInfo("SIN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xe5,
                    new XlFunctionInfo("SINH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    20,
                    new XlFunctionInfo("SQRT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x1a0,
                    new XlFunctionInfo("SQRTPI", 1, 1, XlFunctionProperty.Excel2010Predefined)
                },
                { 
                    0x158,
                    new XlFunctionInfo("SUBTOTAL", 2, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    4,
                    new XlFunctionInfo("SUM", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x159,
                    new XlFunctionInfo("SUMIF", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x1e2,
                    new XlFunctionInfo("SUMIFS", 0x100, 0xff, XlFunctionProperty.Excel2003Future)
                },
                { 
                    0xe4,
                    new XlFunctionInfo("SUMPRODUCT", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x141,
                    new XlFunctionInfo("SUMSQ", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x130,
                    new XlFunctionInfo("SUMX2MY2", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x131,
                    new XlFunctionInfo("SUMX2PY2", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x12f,
                    new XlFunctionInfo("SUMXMY2", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x11,
                    new XlFunctionInfo("TAN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xe7,
                    new XlFunctionInfo("TANH", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xc5,
                    new XlFunctionInfo("TRUNC", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x10d,
                    new XlFunctionInfo("AVEDEV", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    5,
                    new XlFunctionInfo("AVERAGE", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x169,
                    new XlFunctionInfo("AVERAGEA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x1e3,
                    new XlFunctionInfo("AVERAGEIF", 2, 3, XlFunctionProperty.Excel2003Future)
                },
                { 
                    0x1e4,
                    new XlFunctionInfo("AVERAGEIFS", 0x100, 0xff, XlFunctionProperty.Excel2003Future)
                },
                { 
                    0x4014,
                    new XlFunctionInfo("BETA.DIST", 5, 6, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4051,
                    new XlFunctionInfo("BETA.INV", 4, 5, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x406f,
                    new XlFunctionInfo("BINOM.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4025,
                    new XlFunctionInfo("BINOM.DIST.RANGE", 3, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4070,
                    new XlFunctionInfo("BINOM.INV", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4065,
                    new XlFunctionInfo("CHISQ.DIST", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4066,
                    new XlFunctionInfo("CHISQ.DIST.RT", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4067,
                    new XlFunctionInfo("CHISQ.INV", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4068,
                    new XlFunctionInfo("CHISQ.INV.RT", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4069,
                    new XlFunctionInfo("CHISQ.TEST", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4010,
                    new XlFunctionInfo("CONFIDENCE.NORM", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4072,
                    new XlFunctionInfo("CONFIDENCE.T", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x133,
                    new XlFunctionInfo("CORREL", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0,
                    new XlFunctionInfo("COUNT", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0xa9,
                    new XlFunctionInfo("COUNTA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x15b,
                    new XlFunctionInfo("COUNTBLANK", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x15a,
                    new XlFunctionInfo("COUNTIF", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x1e1,
                    new XlFunctionInfo("COUNTIFS", 0xff, 0x100, XlFunctionProperty.Excel2003Future)
                },
                { 
                    0x404d,
                    new XlFunctionInfo("COVARIANCE.P", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x404e,
                    new XlFunctionInfo("COVARIANCE.S", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x13e,
                    new XlFunctionInfo("DEVSQ", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x4074,
                    new XlFunctionInfo("EXPON.DIST", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4054,
                    new XlFunctionInfo("F.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4057,
                    new XlFunctionInfo("F.DIST.RT", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4053,
                    new XlFunctionInfo("F.INV", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4063,
                    new XlFunctionInfo("F.INV.RT", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4056,
                    new XlFunctionInfo("F.TEST", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x11b,
                    new XlFunctionInfo("FISHER", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x11c,
                    new XlFunctionInfo("FISHERINV", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x135,
                    new XlFunctionInfo("FORECAST", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xfc,
                    new XlFunctionInfo("FREQUENCY", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4058,
                    new XlFunctionInfo("GAMMA.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4064,
                    new XlFunctionInfo("GAMMA.INV", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4026,
                    new XlFunctionInfo("GAMMA", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x10f,
                    new XlFunctionInfo("GAMMALN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4013,
                    new XlFunctionInfo("GAMMALN.PRECISE", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4076,
                    new XlFunctionInfo("GAUSS", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x13f,
                    new XlFunctionInfo("GEOMEAN", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x34,
                    new XlFunctionInfo("GROWTH", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    320,
                    new XlFunctionInfo("HARMEAN", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x406d,
                    new XlFunctionInfo("HYPGEOM.DIST", 5, 5, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x137,
                    new XlFunctionInfo("INTERCEPT", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x142,
                    new XlFunctionInfo("KURT", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x145,
                    new XlFunctionInfo("LARGE", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x31,
                    new XlFunctionInfo("LINEST", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x33,
                    new XlFunctionInfo("LOGEST", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x4009,
                    new XlFunctionInfo("LOGNORM.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x400b,
                    new XlFunctionInfo("LOGNORM.INV", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    7,
                    new XlFunctionInfo("MAX", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x16a,
                    new XlFunctionInfo("MAXA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0xe3,
                    new XlFunctionInfo("MEDIAN", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    6,
                    new XlFunctionInfo("MIN", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x16b,
                    new XlFunctionInfo("MINA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x4001,
                    new XlFunctionInfo("MODE.MULT", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4002,
                    new XlFunctionInfo("MODE.SNGL", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x406c,
                    new XlFunctionInfo("NEGBINOM.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x400d,
                    new XlFunctionInfo("NORM.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x400c,
                    new XlFunctionInfo("NORM.INV", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4008,
                    new XlFunctionInfo("NORM.S.DIST", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x400a,
                    new XlFunctionInfo("NORM.S.INV", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x138,
                    new XlFunctionInfo("PEARSON", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4027,
                    new XlFunctionInfo("PERMUTATIONA", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4028,
                    new XlFunctionInfo("PHI", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4049,
                    new XlFunctionInfo("PERCENTILE.EXC", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x404a,
                    new XlFunctionInfo("PERCENTILE.INC", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4047,
                    new XlFunctionInfo("PERCENTRANK.EXC", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4048,
                    new XlFunctionInfo("PERCENTRANK.INC", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x12b,
                    new XlFunctionInfo("PERMUT", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4075,
                    new XlFunctionInfo("POISSON.DIST", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x13d,
                    new XlFunctionInfo("PROB", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x404b,
                    new XlFunctionInfo("QUARTILE.EXC", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x404c,
                    new XlFunctionInfo("QUARTILE.INC", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x404f,
                    new XlFunctionInfo("RANK.AVG", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4050,
                    new XlFunctionInfo("RANK.EQ", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x139,
                    new XlFunctionInfo("RSQ", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x143,
                    new XlFunctionInfo("SKEW", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x4029,
                    new XlFunctionInfo("SKEW.P", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x13b,
                    new XlFunctionInfo("SLOPE", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x146,
                    new XlFunctionInfo("SMALL", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x129,
                    new XlFunctionInfo("STANDARDIZE", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x4006,
                    new XlFunctionInfo("STDEV.P", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4005,
                    new XlFunctionInfo("STDEV.S", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x16e,
                    new XlFunctionInfo("STDEVA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x16c,
                    new XlFunctionInfo("STDEVPA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x13a,
                    new XlFunctionInfo("STEYX", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4055,
                    new XlFunctionInfo("T.DIST", 3, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4061,
                    new XlFunctionInfo("T.DIST.2T", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4060,
                    new XlFunctionInfo("T.DIST.RT", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4052,
                    new XlFunctionInfo("T.INV", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4062,
                    new XlFunctionInfo("T.INV.2T", 2, 2, XlFunctionProperty.Excel2010Future)
                },
                { 
                    50,
                    new XlFunctionInfo("TREND", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    0x14b,
                    new XlFunctionInfo("TRIMMEAN", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x4059,
                    new XlFunctionInfo("T.TEST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4003,
                    new XlFunctionInfo("VAR.P", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4004,
                    new XlFunctionInfo("VAR.S", 1, 0xff, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x16f,
                    new XlFunctionInfo("VARA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x16d,
                    new XlFunctionInfo("VARPA", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x406b,
                    new XlFunctionInfo("WEIBULL.DIST", 4, 4, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x406a,
                    new XlFunctionInfo("Z.TEST", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0xd6,
                    new XlFunctionInfo("ASC", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x170,
                    new XlFunctionInfo("BAHTTEXT", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x6f,
                    new XlFunctionInfo("CHAR", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xa2,
                    new XlFunctionInfo("CLEAN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x79,
                    new XlFunctionInfo("CODE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x150,
                    new XlFunctionInfo("CONCATENATE", 1, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    13,
                    new XlFunctionInfo("DOLLAR", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x75,
                    new XlFunctionInfo("EXACT", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x7c,
                    new XlFunctionInfo("FIND", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xcd,
                    new XlFunctionInfo("FINDB", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    14,
                    new XlFunctionInfo("FIXED", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x73,
                    new XlFunctionInfo("LEFT", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xd0,
                    new XlFunctionInfo("LEFTB", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x20,
                    new XlFunctionInfo("LEN", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0xd3,
                    new XlFunctionInfo("LENB", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x70,
                    new XlFunctionInfo("LOWER", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x1f,
                    new XlFunctionInfo("MID", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    210,
                    new XlFunctionInfo("MIDB", 3, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x4037,
                    new XlFunctionInfo("NUMBERVALUE", 2, 3, XlFunctionProperty.Excel2010Future)
                },
                { 
                    360,
                    new XlFunctionInfo("PHONETIC", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x72,
                    new XlFunctionInfo("PROPER", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x77,
                    new XlFunctionInfo("REPLACE", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    0xcf,
                    new XlFunctionInfo("REPLACEB", 4, 4, XlFunctionProperty.Normal)
                },
                { 
                    30,
                    new XlFunctionInfo("REPT", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x74,
                    new XlFunctionInfo("RIGHT", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0xd1,
                    new XlFunctionInfo("RIGHTB", 1, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x52,
                    new XlFunctionInfo("SEARCH", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0xce,
                    new XlFunctionInfo("SEARCHB", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    120,
                    new XlFunctionInfo("SUBSTITUTE", 3, 4, XlFunctionProperty.Normal)
                },
                { 
                    130,
                    new XlFunctionInfo("T", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x30,
                    new XlFunctionInfo("TEXT", 2, 2, XlFunctionProperty.Normal)
                },
                { 
                    0x76,
                    new XlFunctionInfo("TRIM", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4039,
                    new XlFunctionInfo("UNICODE", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x71,
                    new XlFunctionInfo("UPPER", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x21,
                    new XlFunctionInfo("VALUE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    150,
                    new XlFunctionInfo("CALL", 2, 0xff, XlFunctionProperty.Normal)
                },
                { 
                    0x10b,
                    new XlFunctionInfo("REGISTER.ID", 2, 3, XlFunctionProperty.Normal)
                },
                { 
                    0x4044,
                    new XlFunctionInfo("ENCODEURL", 1, 1, XlFunctionProperty.Excel2010Future)
                },
                { 
                    0x4100,
                    new XlFunctionInfo("FIELD", 0, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4101,
                    new XlFunctionInfo("RANGE", 1, 1, XlFunctionProperty.Normal)
                },
                { 
                    0x4102,
                    new XlFunctionInfo("FIELDPICTURE", 5, 6, XlFunctionProperty.Normal)
                }
            };

        public static XlFunctionInfo GetFunctionInfo(int code) => 
            functionCodeToFunctionInfoConversionTable[code];
    }
}

