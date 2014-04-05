using Newtonsoft.Json;

namespace Wimicrogrid
{
    public static class JsonExtensionMethods
    {
        public static string AsJson<T>(this T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}