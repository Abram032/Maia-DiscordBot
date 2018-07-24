using Maia.Core.Converters;
using Maia.Core.Updater;
using Maia.Persistence.Updater;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maia.Persistence.Converters
{
    class UpdateInfoConverter : JsonCreationConverter<IUpdateInfo>
    {
        protected override IUpdateInfo Create(Type objectType, JObject jObject)
        {
            IUpdateInfo updateInfo = new UpdateInfo();

            if (FieldExists("tag_name", jObject))
            {
                updateInfo.Version = jObject["tag_name"].ToString();
            }
            if (FieldExists("assets", jObject))
            {
                if (FieldExists("browser_download_url", (JObject)jObject["assets"][0]))
                {
                    updateInfo.DownloadURL = jObject["assets"][0]["browser_download_url"].ToString();
                }
            }

            return updateInfo;
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}
