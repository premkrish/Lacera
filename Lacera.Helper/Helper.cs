using System;

namespace Lacera.Helper
{
    public static class Generic
    {
        /// <summary>
        /// Returns the object o as a type T, if the conversion fails default value of that type is returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">object to convert</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>
        /// object of type T
        /// </returns>
        public static T ConvertTo<T>(object o, T defaultValue = default(T)) where T : IConvertible
        {
            var returnVal = defaultValue;
            try
            {
                if (o != null && !o.Equals(DBNull.Value))
                    returnVal = (T)Convert.ChangeType(o, typeof(T));
                else
                {
                    if (typeof(T) == typeof(string))
                    {
                        returnVal = (T)Convert.ChangeType(string.Empty, typeof(T));
                    }
                }
            }
            catch (Exception)
            {
                // ignore the exception
            }

            return returnVal;
        }

    }
}
