namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;

    public static class DXListExtensions
    {
        public static List<TOutput> ConvertAll<TInput, TOutput>(List<TInput> instance, Converter<TInput, TOutput> converter) => 
            instance.ConvertAll<TOutput>(converter);
    }
}

