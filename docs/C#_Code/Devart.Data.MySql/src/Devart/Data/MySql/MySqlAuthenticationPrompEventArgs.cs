namespace Devart.Data.MySql
{
    using System;

    public class MySqlAuthenticationPrompEventArgs : EventArgs
    {
        private readonly string a;
        private readonly string b;
        private readonly string[] c;
        private readonly string[] d;

        internal MySqlAuthenticationPrompEventArgs(string A_0, string A_1, string[] A_2, string[] A_3);

        public string Name { get; }

        public string Instruction { get; }

        public string[] Prompts { get; }

        public string[] Responses { get; }
    }
}

