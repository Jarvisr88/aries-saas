namespace Devart.Data.MySql
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MySqlDependency : IDisposable
    {
        private static IList a;
        private bool b;
        private int c;
        private Timer d;
        private IList e;
        private OnCheckEventHandler f;
        private ArrayList g;
        private ArrayList h;
        private const string j = "checksum";
        private const string k = "timestamp fields checksum";

        public event OnChangeEventHandler OnChange;

        public event OnCheckEventHandler OnCheck;

        static MySqlDependency();
        public MySqlDependency();
        public MySqlDependency(MySqlCommand command, int timeout);
        public MySqlDependency(MySqlCommand command, int timeout, MySqlDependencyCheckType checkType);
        private void a(MySqlConnection A_0);
        private void a(object A_0);
        private static int a(string A_0);
        private void a(ArrayList A_0, MySqlConnection A_1);
        private bool a(object A_0, OnCheckEventArgs A_1);
        private static bool a(string A_0, string A_1, string A_2, string A_3);
        public void AddCommandDependency(MySqlCommand command);
        public void AddCommandDependency(MySqlCommand command, MySqlDependencyCheckType checkType);
        private void b(string A_0);
        public void Dispose();
        public static void Start(MySqlConnection connection);
        public static void Start(string connectionString);
        public static void Stop(string connectionString);

        public int CheckTimeout { get; set; }

        public bool HasChanges { get; }

        private class a
        {
            public readonly MySqlCommand a;
            public readonly MySqlConnection b;
            public readonly DataTable c;
            public readonly string d;
            public string e;
            public MySqlCommand f;
            public string g;
            public bool h;
            public MySqlDependencyCheckType i;

            public a(MySqlCommand A_0, MySqlConnection A_1, DataTable A_2, string A_3, MySqlDependencyCheckType A_4);
        }

        private class b
        {
            [CompilerGenerated]
            private MySqlCommand a;
            [CompilerGenerated]
            private MySqlDependencyCheckType b;

            public b(MySqlCommand A_0, MySqlDependencyCheckType A_1);
            [CompilerGenerated]
            public MySqlCommand a();
            [CompilerGenerated]
            private void a(MySqlCommand A_0);
            [CompilerGenerated]
            private void a(MySqlDependencyCheckType A_0);
            [CompilerGenerated]
            public MySqlDependencyCheckType b();
        }
    }
}

