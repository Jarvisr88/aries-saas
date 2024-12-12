namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ModificationResult
    {
        public ParameterValue[] Identities;

        public ModificationResult() : this(new ParameterValue[0])
        {
        }

        public ModificationResult(List<ParameterValue> identities) : this(identities.ToArray())
        {
        }

        public ModificationResult(ParameterValue[] identities)
        {
            this.Identities = identities;
        }
    }
}

