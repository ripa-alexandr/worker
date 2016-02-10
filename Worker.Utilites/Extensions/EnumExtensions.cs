using System.ComponentModel;

namespace Worker.Utilites.Extensions
{
    /// <summary>
    /// The enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get enum description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription<T>(this T value) where T : struct
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            
            return value.ToString();
        }
    }
}
