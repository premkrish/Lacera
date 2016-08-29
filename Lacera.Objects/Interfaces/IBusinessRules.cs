using System;
using System.Collections.Generic;
using Lacera.Enums;

namespace Lacera.Objects.Interfaces
{
    public interface IBusinessRules
    {
        /// <summary>
        /// Applies the specified rules.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules">The rules.</param>
        /// <param name="obj">The object.</param>
        /// <param name="keyValuePairs">The key value pairs.</param>
        /// <returns></returns>
        RecordStatus Apply<T>(IEnumerable<InvalidRecordRules> rules, T obj,
            List<KeyValuePair<Type, string>> keyValuePairs = null);
    }
}
