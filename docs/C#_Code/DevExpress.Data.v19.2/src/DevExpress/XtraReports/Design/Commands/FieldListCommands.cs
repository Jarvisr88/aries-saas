namespace DevExpress.XtraReports.Design.Commands
{
    using System;
    using System.ComponentModel.Design;

    public static class FieldListCommands
    {
        private const int cmdidAddCalculatedField = 1;
        private const int cmdidDeleteCalculatedField = 2;
        private const int cmdidEditCalculatedFields = 3;
        private const int cmdidEditExpressionCalculatedField = 4;
        private const int cmdidAddParameter = 5;
        private const int cmdidEditParameters = 6;
        private const int cmdidDeleteParameter = 7;
        private const int cmdidClearCalculatedFields = 8;
        private const int cmdidClearParameters = 9;
        private static readonly Guid fieldListCommandSet;
        public static readonly CommandID AddCalculatedField;
        public static readonly CommandID DeleteCalculatedField;
        public static readonly CommandID EditCalculatedFields;
        public static readonly CommandID EditExpressionCalculatedField;
        public static readonly CommandID AddParameter;
        public static readonly CommandID EditParameters;
        public static readonly CommandID DeleteParameter;
        public static readonly CommandID ClearCalculatedFields;
        public static readonly CommandID ClearParameters;

        static FieldListCommands();

        public static CommandID[] AllCommands { get; }
    }
}

