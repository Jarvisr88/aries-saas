namespace DevExpress.Mvvm
{
    using System;

    public class LocatorException : Exception
    {
        public LocatorException(string locatorName, string type, Exception innerException) : base($"{locatorName} cannot resolve the {type} type. See the inner exception for details.", innerException)
        {
        }
    }
}

