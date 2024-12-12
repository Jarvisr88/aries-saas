namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils;
    using System;
    using System.Configuration;
    using System.Runtime.InteropServices;
    using System.Xml;

    public static class ConfigurationHelper
    {
        public const string SettingsNamespace = "DXThemeManager";
        private const string ApplicationThemeNameSettingsName = "ApplicationThemeName";
        private const string UserSettingsGroupName = "userSettings";

        private static ClientSettingsSection CreateClientSection() => 
            new ClientSettingsSection { SectionInformation = { 
                AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser,
                RequirePermission = false
            } };

        private static ClientSettingsSection CreateClientSection(System.Configuration.Configuration config)
        {
            ClientSettingsSection section = CreateClientSection();
            config.Sections.Add("DXThemeManager", section);
            return section;
        }

        private static ClientSettingsSection CreateClientSection(UserSettingsGroup userSettingsGroup)
        {
            ClientSettingsSection section = CreateClientSection();
            userSettingsGroup.Sections.Add("DXThemeManager", section);
            return section;
        }

        private static string GetApplicationThemeName(ClientSettingsSection clientSection)
        {
            SettingElement element = clientSection?.Settings.Get("ApplicationThemeName");
            return (((element == null) || ((element.Value == null) || (element.Value.ValueXml == null))) ? null : element.Value.ValueXml.InnerText);
        }

        internal static string GetApplicationThemeNameFromConfig(System.Configuration.Configuration configuration, bool useNetCoreVersionFormat)
        {
            if (useNetCoreVersionFormat)
            {
                string applicationThemeName = GetApplicationThemeName(GetClientSection(configuration));
                if (!string.IsNullOrEmpty(applicationThemeName))
                {
                    return applicationThemeName;
                }
            }
            return GetApplicationThemeName(GetClientSection(GetUserSettingsGroup(configuration)));
        }

        public static string GetApplicationThemeNameFromConfig(string appConfigPath, bool useNetCoreVersionFormat = false) => 
            GetApplicationThemeNameFromConfig(LoadConfiguration(appConfigPath), useNetCoreVersionFormat);

        private static ClientSettingsSection GetClientSection(System.Configuration.Configuration configuration) => 
            configuration.Sections["DXThemeManager"] as ClientSettingsSection;

        private static ClientSettingsSection GetClientSection(UserSettingsGroup userSettingsGroup) => 
            userSettingsGroup.Sections["DXThemeManager"] as ClientSettingsSection;

        private static UserSettingsGroup GetUserSettingsGroup(System.Configuration.Configuration configuration)
        {
            UserSettingsGroup sectionGroup = configuration.GetSectionGroup("userSettings") as UserSettingsGroup;
            if (sectionGroup == null)
            {
                sectionGroup = new UserSettingsGroup();
                configuration.SectionGroups.Add("userSettings", sectionGroup);
            }
            return sectionGroup;
        }

        private static System.Configuration.Configuration LoadConfiguration(string appConfigPath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                MachineConfigFilename = ConfigurationManager.OpenMachineConfiguration().FilePath,
                ExeConfigFilename = appConfigPath
            };
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        internal static void RemoveThemeNameFromConfig(System.Configuration.Configuration conf)
        {
            UserSettingsGroup sectionGroup = conf.GetSectionGroup("userSettings") as UserSettingsGroup;
            if (sectionGroup != null)
            {
                if (sectionGroup.Sections["DXThemeManager"] is ClientSettingsSection)
                {
                    sectionGroup.Sections.Remove("ApplicationThemeName");
                }
                conf.SectionGroups.Remove("userSettings");
                conf.Save(ConfigurationSaveMode.Modified);
            }
        }

        public static void RemoveThemeNameFromConfig(string appConfigPath)
        {
            RemoveThemeNameFromConfig(LoadConfiguration(appConfigPath));
        }

        private static void SaveApplicationThemeName(string themeName, ClientSettingsSection clientSection)
        {
            SettingElement element = new SettingElement("ApplicationThemeName", SettingsSerializeAs.String);
            XmlElement element2 = SafeXml.CreateDocument(null).CreateElement("value");
            element2.InnerText = themeName;
            element.Value.ValueXml = element2;
            clientSection.Settings.Add(element);
        }

        internal static void SaveApplicationThemeNameToConfig(System.Configuration.Configuration configuration, string themeName, bool useNetCoreVersionFormat)
        {
            ClientSettingsSection section;
            if (useNetCoreVersionFormat)
            {
                section = GetClientSection(configuration) ?? CreateClientSection(configuration);
            }
            else
            {
                UserSettingsGroup userSettingsGroup = GetUserSettingsGroup(configuration);
                section = GetClientSection(userSettingsGroup) ?? CreateClientSection(userSettingsGroup);
            }
            SaveApplicationThemeName(themeName, section);
            configuration.Save(ConfigurationSaveMode.Modified);
        }

        public static void SaveApplicationThemeNameToConfig(string appConfigPath, string themeName, bool useNetCoreVersionFormat = false)
        {
            SaveApplicationThemeNameToConfig(LoadConfiguration(appConfigPath), themeName, useNetCoreVersionFormat);
        }
    }
}

