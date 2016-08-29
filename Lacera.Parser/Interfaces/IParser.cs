using System.Collections.Generic;
using Lacera.Enums;

namespace Lacera.Parser.Interfaces
{
    public interface IParser
    {
        /// <summary>
        /// Parses the specified file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="rules">The business rules to be applied on the object</param>
        /// <returns></returns>
        List<T> Parse<T>(string filePath, List<InvalidRecordRules> rules);
    }
}
