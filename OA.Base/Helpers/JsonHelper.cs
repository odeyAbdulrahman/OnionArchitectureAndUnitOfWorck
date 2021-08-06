using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Base.Helpers
{
    class JsonHelper
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static TObject Deserialize<TObject>(string json)
        {
            return JsonConvert.DeserializeObject<TObject>(json, settings);
        }

    }
}
