namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public class SingleGroupGenerator : ICommandGroupsGenerator
    {
        private readonly ICommandSubGroupsGenerator gen;

        public SingleGroupGenerator(ICommandsGenerator gen)
        {
            this.gen = new SingleSubGroupGenerator(gen);
        }

        ICommandSubGroupsGenerator ICommandGroupsGenerator.CreateGroup(string groupName) => 
            this.gen;
    }
}

