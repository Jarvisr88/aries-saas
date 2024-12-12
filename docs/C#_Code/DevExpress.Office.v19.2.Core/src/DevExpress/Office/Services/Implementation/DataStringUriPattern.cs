namespace DevExpress.Office.Services.Implementation
{
    using System;
    using System.Text.RegularExpressions;

    public static class DataStringUriPattern
    {
        private const string imageTypePattern = @"(?<imageType>\w*)";
        private const string capacityPattern = @"(?<capacity>\w+)";
        private const string imagePattern = "(?<image>.*)";
        private const string spacePattern = @"\s*";
        private const string slashPattern = @"\/";
        private const string mediaTypePattern = @"(?<mediaType>[-\+\w]+)";
        private const string contentPattern = "(?<content>.*)";
        private static string imageDataStringPattern = string.Format("{0}?data:{0}?image{0}?{4}?{0}?{1}?{0}?;{0}?{2}{0}?,{0}?{3}{0}?", new object[] { @"\s*", @"(?<imageType>\w*)", @"(?<capacity>\w+)", "(?<image>.*)", @"\/" });
        private static string applicationDataStringPattern = string.Format("{0}?data:{0}?application{0}?{4}?{0}?{1}?{0}?;{0}?{2}{0}?,{0}?{3}{0}?", new object[] { @"\s*", @"(?<mediaType>[-\+\w]+)", @"(?<capacity>\w+)", "(?<content>.*)", @"\/" });
        public static Regex regex = new Regex(imageDataStringPattern, RegexOptions.CultureInvariant | RegexOptions.Singleline);
        public static Regex applicationDataStringRegex = new Regex(applicationDataStringPattern, RegexOptions.CultureInvariant | RegexOptions.Singleline);
    }
}

