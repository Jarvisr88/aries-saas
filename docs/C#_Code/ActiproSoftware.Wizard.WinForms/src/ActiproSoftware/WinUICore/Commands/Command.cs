namespace ActiproSoftware.WinUICore.Commands
{
    using System;

    [Serializable]
    public class Command
    {
        private string name;

        public Command()
        {
            this.name = base.GetType().Name;
        }

        public Command(string name)
        {
            this.name = name;
        }

        public string Name =>
            this.name;
    }
}

