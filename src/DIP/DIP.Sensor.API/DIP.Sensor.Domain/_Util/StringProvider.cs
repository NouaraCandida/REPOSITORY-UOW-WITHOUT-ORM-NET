using System;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace DIP.Sensor.Domain._Util
{
    public class StringProvider : IStringLocalizer
    {
        private readonly ILogger _logger;

        private readonly Dictionary<string, string> values = new Dictionary<string, string>();

        public StringProvider(IConfiguration configuration, ILogger<StringProvider> logger)
        {
            this._logger = logger;

            string arquivo = configuration.GetSection("Language:File").Value;
            if (arquivo == null)
                logger.LogError("Configuration does not have Language section:File configurada.");
            else
                values = LoadValues(arquivo);
        }

        #region IStringLocalizer

        public LocalizedString this[string name]
        {
            get
            {
                if (values.ContainsKey(name))
                    return new LocalizedString(name, values[name]);
                else
                    return new LocalizedString(name, $"[{name}]", true);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return from string key in values.Keys
                   select new LocalizedString(key, values[key]);
        }

        #endregion

        private Dictionary<string, string> LoadValues(string pathFile)
        {

            if (!File.Exists(pathFile))
                _logger.LogError($"Language file '{pathFile}' not found.");
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(pathFile));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"File '{pathFile}' has formatting problems.");
                return new Dictionary<string, string>();
            }
        }


    }
}
