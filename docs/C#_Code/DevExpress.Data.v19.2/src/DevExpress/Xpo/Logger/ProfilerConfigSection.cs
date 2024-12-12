namespace DevExpress.Xpo.Logger
{
    using System;
    using System.Configuration;

    public class ProfilerConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("serverType")]
        public string ServerType
        {
            get => 
                (string) base["serverType"];
            set => 
                base["serverType"] = value;
        }

        [ConfigurationProperty("serverAssembly")]
        public string ServerAssembly
        {
            get => 
                (string) base["serverAssembly"];
            set => 
                base["serverAssembly"] = value;
        }

        [ConfigurationProperty("categories")]
        public string Categories
        {
            get => 
                (string) base["categories"];
            set => 
                base["categories"] = value;
        }

        [ConfigurationProperty("port")]
        public int Port
        {
            get => 
                (int) base["port"];
            set => 
                base["port"] = value;
        }
    }
}

