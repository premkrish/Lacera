using System;
using System.Collections.Generic;
using System.Linq;
using Lacera.Enums;
using Lacera.Helper;
using Lacera.Objects.Interfaces;

namespace Lacera.Objects.Models
{
    public class BusinessRules : IBusinessRules
    {
        /// <summary>
        /// Applies the specified rules.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules">The rules.</param>
        /// <param name="obj">The object.</param>
        /// <param name="keyValuePairs">The key value pairs.</param>
        /// <returns></returns>
        /// <exception cref="Exception">keyvalue pairs not set for MissigField rule</exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public RecordStatus Apply<T>(IEnumerable<InvalidRecordRules> rules, T obj, List<KeyValuePair<Type, string>> keyValuePairs = null)
        {
            var recordStatus = RecordStatus.Valid;
            foreach (var rule in rules)
            {
                switch (rule)
                {
                    case InvalidRecordRules.InvalidDate:
                        recordStatus = CheckIfDataIsValid(obj, typeof (DateTime), rule) ? RecordStatus.Valid : RecordStatus.Invalid;
                        break;

                    case InvalidRecordRules.InvalidAmount:
                        recordStatus = CheckIfDataIsValid(obj, typeof(decimal), rule) ? RecordStatus.Valid : RecordStatus.Invalid;
                        break;

                    case InvalidRecordRules.MissingField:
                        if(keyValuePairs == null)
                            throw  new Exception("keyvalue pairs not set for MissigField rule");

                        recordStatus =  keyValuePairs.All(c=> !string.IsNullOrWhiteSpace(c.Value)) ? RecordStatus.Valid : RecordStatus.Invalid;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (recordStatus == RecordStatus.Invalid)
                    break;
            }

            return recordStatus;
        }

        /// <summary>
        /// Checks if data is valid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <param name="rule">The rule.</param>
        /// <returns></returns>
        private bool CheckIfDataIsValid<T>(T obj, Type type, InvalidRecordRules rule)
        {
            var validRecord = true;

            foreach (var prop in typeof (T).GetProperties().Where(c => c.PropertyType == type))
            {
                if (rule == InvalidRecordRules.InvalidDate)
                {
                    if (string.Equals(Generic.ConvertTo<DateTime>(prop.GetValue(obj)).ToShortDateString(), "1/1/0001"))
                    {
                        validRecord = false;
                        break;
                    }
                }
                else if (rule == InvalidRecordRules.InvalidAmount)
                {
                    if (Generic.ConvertTo<decimal>(prop.GetValue(obj)) == 0)
                    {
                        validRecord = false;
                        break;
                    }
                }
            }

            return validRecord;
        }
    }
}
