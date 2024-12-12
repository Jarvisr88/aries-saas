namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum SequenceQualifier
    {
        [Display(Name="Items", Description="ItemsDescription", ResourceType=typeof(SequenceQualifierResourcesType))]
        Items = 0,
        [Display(Name="Percents", Description="PercentsDescription", ResourceType=typeof(SequenceQualifierResourcesType))]
        Percents = 1
    }
}

