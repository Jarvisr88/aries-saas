namespace DevExpress.Data.Svg
{
    using System;

    public class SvgKeysGenerator : IDefinitionKeysGenerator
    {
        private readonly string prefixDefinitions;
        private int keysCounter;

        public SvgKeysGenerator(string prefixDefinitions);
        string IDefinitionKeysGenerator.GenerateKey();
    }
}

