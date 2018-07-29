using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Settings
{
    class Configuration : IConfiguration
    {
        private Dictionary<string, string> config;
        private Dictionary<string, string> defaultConfig;
        private string configPath = Environment.CurrentDirectory + @"/Config/config.ini";
        private string directoryPath = Environment.CurrentDirectory + @"/Config";
        private static Configuration instance = null;
        private static readonly object syncRoot = new object();

        private Configuration()
        {
            config = new Dictionary<string, string>();
            defaultConfig = new Dictionary<string, string>();
            InitConfig(defaultConfig);
            if (File.Exists(configPath) == false)
                CreateConfig().Wait();
        }

        public static IConfiguration Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Configuration();
                }
                return instance;
            }
        }

        private void InitConfig(Dictionary<string, string> config)
        {
            config.Add(ConfigKeys.Token, string.Empty);
            config.Add(ConfigKeys.OwnerID, string.Empty);
            config.Add(ConfigKeys.CommandPrefix, "!t");
            config.Add(ConfigKeys.SaveLogs, "false");
            config.Add(ConfigKeys.LogSeverity, "Info");
        }

        public string GetValueOrDefault(string key)
        {
            if(config != null && config.ContainsKey(key))
                return config[key];
            return defaultConfig[key];
        }

        public string GetValue(string key)
        {
            return config[key];
        }

        public string GetDefaultValue(string key)
        {
            return defaultConfig[key];
        }

        private async Task CreateConfig()
        {
            CreateDirectory();
            await CreateFile();
        }

        public void LoadConfig()
        {
            using (StreamReader sr = new StreamReader(configPath))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith('#') == false && line.Equals(string.Empty) == false)
                    {
                        string key = ExtractKeyAndValue(line, 0);
                        string value = ExtractKeyAndValue(line, 1);
                        config.Add(key, value);
                    }
                }
            }
            if (ValidateConfig() == false)
            {
                File.Delete(configPath);
                CreateConfig().Wait();
                config.Clear();
                LoadConfig();
            }
        }

        private string ExtractKeyAndValue(string line, int index)
        {
            line = line.Trim();
            line = line.Replace(" ", string.Empty);
            string[] pairs = line.Split(':');
            if (pairs.Length > index)
                return pairs[index];
            else
                return string.Empty;
        }

        private void CreateDirectory()
        {
            if (Directory.Exists(directoryPath) == false)
                Directory.CreateDirectory(directoryPath);
        }

        private bool ValidateConfig()
        {
            List<string> keys = defaultConfig.Keys.ToList();
            List<string> fileKeys = config.Keys.ToList();
            if (keys.Count > fileKeys.Count)
                return false;
            foreach (var key in keys)
            {
                if (fileKeys.Contains(key) == false)
                    return false;
            }
            return true;
        }

        private async Task CreateFile()
        {
            using (StreamWriter sw = new StreamWriter(configPath))
            {
                await sw.WriteLineAsync("# I highly suggest editing this file using other programs than notepad, line notepad++ or sublime.");
                await sw.WriteLineAsync("# Those programs color the syntax so it's easier for you to read the file.");
                await sw.WriteLineAsync("# Put your token here so your bot can connect.");
                await sw.WriteLineAsync("# You can get your app token from here https://discordapp.com/developers/applications/me");
                await sw.WriteLineAsync("# Example: Token: 123456789xyzabc");
                await sw.WriteLineAsync(ConfigKeys.Token + ": " + GetValueOrDefault(ConfigKeys.Token));
                await sw.WriteLineAsync();
                await sw.WriteLineAsync("# Your owner ID, gives you privileges to use owner only commands from chat.");
                await sw.WriteLineAsync("# To get your ID, right click on yourself in the chat and Copy ID.");
                await sw.WriteLineAsync("# Example: 1234567890");
                await sw.WriteLineAsync(ConfigKeys.OwnerID + ": " + GetValueOrDefault(ConfigKeys.OwnerID));
                await sw.WriteLineAsync();
                await sw.WriteLineAsync("# Saves all incomming console logs to a file.");
                await sw.WriteLineAsync("# <true, false>, Default: false");
                await sw.WriteLineAsync(ConfigKeys.SaveLogs + ": " + GetValueOrDefault(ConfigKeys.SaveLogs));
                await sw.WriteLineAsync();
                await sw.WriteLineAsync("# Command prefix for all your bot commands.");
                await sw.WriteLineAsync("# Can be any you like for example: '!', '*', ';' etc.");
                await sw.WriteLineAsync("# It can also be a string of characters like: \"!t\", \"!test\" etc.");
                await sw.WriteLineAsync("# Default: !t");
                await sw.WriteLineAsync(ConfigKeys.CommandPrefix + ": " + GetValueOrDefault(ConfigKeys.CommandPrefix));
                await sw.WriteLineAsync();
                //await sw.WriteLineAsync("# Deletes messages that bot sends after a while.");
                //await sw.WriteLineAsync("# <true, false>, Default: false");
                //await sw.WriteLineAsync(ConfigKeys.DeleteMessages + ": false");
                //await sw.WriteLineAsync();
                //await sw.WriteLineAsync("# Deletes messages that are command calls after a while.");
                //await sw.WriteLineAsync("# <true, false>, Default: false");
                //await sw.WriteLineAsync(ConfigKeys.DeleteCommandMessages + ": false");
                //await sw.WriteLineAsync();
                await sw.WriteLineAsync("# Sets severity for console logs.");
                await sw.WriteLineAsync("# <Critical, Debug, Error, Info, Verbose, Warning>, Default: Info");
                await sw.WriteLineAsync(ConfigKeys.LogSeverity + ": " + GetValueOrDefault(ConfigKeys.LogSeverity));
            }
        }
    }
}