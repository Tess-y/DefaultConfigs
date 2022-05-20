using BepInEx;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DefaultConfigs
{

    [BepInPlugin(ModId, ModName, Version)]
    public class DefaultConfigs : BaseUnityPlugin
    {
        private const string ModId = ".....root.configs.default";// extra '.'s to force it to load first (load order is alphabetical)
        private const string ModName = "Default Configs";
        public const string Version = "0.0.0";
        void Awake()
        {
            string pluginLocation = Regex.Replace(Regex.Replace(typeof(DefaultConfigs).Assembly.Location, @"\\(?:.(?!\\))+$","",RegexOptions.None), @"\\(?:.(?!\\))+$", "", RegexOptions.None);
            string configLocation = Regex.Replace(Regex.Replace(Regex.Replace(typeof(DefaultConfigs).Assembly.Location, @"\\(?:.(?!\\))+$","",RegexOptions.None), @"\\(?:.(?!\\))+$", "", RegexOptions.None), @"\\(?:.(?!\\))+$", "", RegexOptions.None) + "\\config";
            string[] configs = Directory.GetDirectories(pluginLocation, "Configs", SearchOption.AllDirectories);
            foreach (string config in configs)
                Copy(config, configLocation);
        }
        void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                try{File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));}catch { }

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }
    }

}
