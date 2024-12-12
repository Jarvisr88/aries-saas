namespace DevExpress.Utils.Commands
{
    using System;

    public class DefaultValueBasedCommandUIState<T> : DefaultCommandUIState, IValueBasedCommandUIState<T>, ICommandUIState
    {
        private T editValue;

        public virtual T Value { get; set; }

        public override object EditValue { get; set; }
    }
}

