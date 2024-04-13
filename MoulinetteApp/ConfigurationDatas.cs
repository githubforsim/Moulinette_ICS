using System.Collections.Generic;
using System.IO;

namespace NMoulinetteApp
{
    internal class ConfigurationDatas
    {
        public string _lastEcrituresFilePath;
        public string _lastComptesFilePath;
        public string _lastTargetFilePath;

        public ConfigurationDatas()
        {
            var lines = File.ReadAllLines("configuration.txt");
            _lastEcrituresFilePath = lines[0].Split('|')[1];
            _lastComptesFilePath = lines[1].Split('|')[1];
            _lastTargetFilePath = lines[2].Split('|')[1];
        }

        public void SaveDatas()
        {
            var lines = new List<string>
            {
                "LAST_ECRITURES_FILE|" + _lastEcrituresFilePath,
                "LAST_COMPTES_FILE|" + _lastComptesFilePath,
                "LAST_TARGET_FILE|" + _lastTargetFilePath
            };

            File.WriteAllLines("configuration.txt", lines.ToArray());
        }
    }
}
