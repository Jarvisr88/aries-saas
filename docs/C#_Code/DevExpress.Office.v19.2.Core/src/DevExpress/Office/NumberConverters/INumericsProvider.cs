namespace DevExpress.Office.NumberConverters
{
    using System;

    public interface INumericsProvider
    {
        string[] Separator { get; }

        string[] SinglesNumeral { get; }

        string[] Singles { get; }

        string[] Teens { get; }

        string[] Tenths { get; }

        string[] Hundreds { get; }

        string[] Thousands { get; }

        string[] Million { get; }

        string[] Billion { get; }

        string[] Trillion { get; }

        string[] Quadrillion { get; }

        string[] Quintillion { get; }
    }
}

