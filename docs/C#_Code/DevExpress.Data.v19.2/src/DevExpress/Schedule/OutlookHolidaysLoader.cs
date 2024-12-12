namespace DevExpress.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class OutlookHolidaysLoader
    {
        internal const int CAL_GREGORIAN = 1;
        internal const int CAL_GREGORIAN_ARABIC = 10;
        internal const int CAL_GREGORIAN_ME_FRENCH = 9;
        internal const int CAL_GREGORIAN_US = 2;
        internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;
        internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;
        internal const int CAL_HEBREW = 8;
        internal const int CAL_HIJRI = 6;
        internal const int CAL_JAPAN = 3;
        internal const int CAL_JULIAN = 13;
        internal const int CAL_KOREA = 5;
        internal const int CAL_TAIWAN = 4;
        internal const int CAL_THAI = 7;
        internal const int DefaultCalendarType = 1;
        private Dictionary<int, Calendar> calendarHash = new Dictionary<int, Calendar>();

        private static Calendar CreateCalendarInstance(int calendarType)
        {
            switch (calendarType)
            {
                case 1:
                    return new GregorianCalendar();

                case 2:
                    return new GregorianCalendar(GregorianCalendarTypes.USEnglish);

                case 3:
                    return new JapaneseCalendar();

                case 4:
                    return new TaiwanCalendar();

                case 5:
                    return new KoreanCalendar();

                case 6:
                    return new HijriCalendar();

                case 7:
                    return new ThaiBuddhistCalendar();

                case 8:
                    return new HebrewCalendar();

                case 9:
                    return new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench);

                case 10:
                    return new GregorianCalendar(GregorianCalendarTypes.Arabic);

                case 11:
                    return new GregorianCalendar(GregorianCalendarTypes.TransliteratedEnglish);

                case 12:
                    return new GregorianCalendar(GregorianCalendarTypes.TransliteratedFrench);

                case 13:
                    return new JulianCalendar();
            }
            return null;
        }

        protected internal virtual Holiday CreateHoliday(string holidayInfo, string location)
        {
            Holiday holiday;
            if (string.IsNullOrEmpty(holidayInfo))
            {
                return null;
            }
            location ??= string.Empty;
            char[] separator = new char[] { this.HolidayLineSeparator };
            string[] strArray = holidayInfo.Split(separator);
            int length = strArray.Length;
            if ((length < 2) || (length > 3))
            {
                return null;
            }
            try
            {
                int calendarType = (length == 3) ? Convert.ToInt32(strArray[2]) : 1;
                Calendar calendar = this.QueryCalendar(calendarType);
                if (calendar == null)
                {
                    holiday = null;
                }
                else
                {
                    char[] chArray2 = new char[] { this.HolidayDateSeparator };
                    string[] strArray2 = strArray[1].Split(chArray2);
                    holiday = (strArray2.Length == 3) ? new Holiday(calendar.ToDateTime(Convert.ToInt32(strArray2[0]), Convert.ToInt32(strArray2[1]), Convert.ToInt32(strArray2[2]), 0, 0, 0, 0), strArray[0], location) : null;
                }
            }
            catch
            {
                holiday = null;
            }
            return holiday;
        }

        protected virtual StreamReader CreateStreamReader(Stream stream, Encoding encoding) => 
            (encoding != null) ? new StreamReader(stream, encoding) : new StreamReader(stream);

        protected internal string ExtractLocationName(string line)
        {
            int index = line.IndexOf("[");
            int num2 = line.LastIndexOf("]");
            return (((index < 0) || (num2 <= 0)) ? string.Empty : line.Substring(index + 1, (num2 - index) - 1));
        }

        public string[] ExtractLocations(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return this.ReadLocations(reader).ToArray();
            }
        }

        public string[] ExtractLocations(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new string[0];
            }
            using (FileStream stream = File.OpenRead(fileName))
            {
                return this.ExtractLocations(stream);
            }
        }

        protected void FillHolidays(HolidayBaseCollection target, StreamReader sr, List<string> locations)
        {
            bool flag = locations.Count > 0;
            bool flag2 = !flag;
            string line = null;
            string currentLocation = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                if (this.IsLocation(line))
                {
                    currentLocation = this.ExtractLocationName(line);
                    if (!flag)
                    {
                        continue;
                    }
                    flag2 = this.MatchLocation(locations, currentLocation);
                    continue;
                }
                if (flag2)
                {
                    Holiday holiday = this.CreateHoliday(line, currentLocation);
                    if (holiday != null)
                    {
                        target.Add(holiday);
                    }
                }
            }
        }

        ~OutlookHolidaysLoader()
        {
            this.calendarHash.Clear();
            this.calendarHash = null;
        }

        public HolidayBaseCollection FromFile(string fileName) => 
            this.FromFile(fileName, new string[0]);

        public HolidayBaseCollection FromFile(string fileName, string[] locations) => 
            this.FromFile(fileName, null, locations);

        public HolidayBaseCollection FromFile(string fileName, Encoding encoding, string[] locations)
        {
            if (!File.Exists(fileName))
            {
                return new HolidayBaseCollection();
            }
            using (FileStream stream = File.OpenRead(fileName))
            {
                return this.FromStream(stream, encoding, locations);
            }
        }

        public HolidayBaseCollection FromStream(Stream stream) => 
            this.FromStream(stream, new string[0]);

        public HolidayBaseCollection FromStream(Stream stream, string[] locations) => 
            this.FromStream(stream, null, locations);

        public HolidayBaseCollection FromStream(Stream stream, Encoding encoding, string[] locations)
        {
            HolidayBaseCollection target = new HolidayBaseCollection();
            if ((locations != null) && ((stream != null) && (stream.Length != 0)))
            {
                using (StreamReader reader = this.CreateStreamReader(stream, encoding))
                {
                    this.FillHolidays(target, reader, new List<string>(locations));
                }
            }
            return target;
        }

        protected internal bool IsLocation(string line) => 
            !string.IsNullOrEmpty(line) ? (line.StartsWith("[") && (line.IndexOf("]") > 0)) : false;

        protected internal bool MatchLocation(List<string> locations, string currentLocation)
        {
            int count = locations.Count;
            for (int i = 0; i < count; i++)
            {
                if (string.Compare(locations[i], currentLocation, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        protected internal Calendar QueryCalendar(int calendarType)
        {
            Calendar calendar;
            if (!this.calendarHash.TryGetValue(calendarType, out calendar))
            {
                calendar = CreateCalendarInstance(calendarType);
                if (calendar != null)
                {
                    this.calendarHash.Add(calendarType, calendar);
                }
            }
            return calendar;
        }

        protected List<string> ReadLocations(StreamReader sr)
        {
            List<string> list = new List<string>();
            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (!this.IsLocation(line))
                {
                    continue;
                }
                list.Add(this.ExtractLocationName(line));
            }
            return list;
        }

        protected virtual char HolidayLineSeparator =>
            ',';

        protected virtual char HolidayDateSeparator =>
            '/';
    }
}

