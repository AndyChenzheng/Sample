using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Czar.Cms.Core.Helper
{
    public class JsonHelper
    {
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss"});
        }
    }
}