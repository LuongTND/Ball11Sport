using System.Text.Json;

namespace System
{
    public static class ExtensionHelper
    {
        //public static string ToVnd(this decimal Sum)
        //{
        //    return $"{Sum:#,##0.00} vnđ";
        //}
        public static string ToVnd(decimal amount)
        {
            return $"{amount:N0} VND"; // Sử dụng chuỗi format "N0" để định dạng số với phân cách hàng nghìn và không có số lẻ
        }
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
