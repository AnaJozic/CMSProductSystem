namespace CMSProductSystem.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class SessionExtensions
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default : JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            var data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);
        }
    }
}
