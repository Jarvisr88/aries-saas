namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public class SingleSubGroupGenerator : ICommandSubGroupsGenerator
    {
        private readonly ICommandsGenerator gen;

        public SingleSubGroupGenerator(ICommandsGenerator gen)
        {
            this.gen = gen;
        }

        ICommandsGenerator ICommandSubGroupsGenerator.CreateSubGroup(string groupName) => 
            this.gen;
    }
}

