using System;
using System.Collections.Generic;
using System.IO;
using Lacera.Enums;
using Lacera.Helper;
using Lacera.Parser.Interfaces;

namespace Lacera.Parser
{
    public class CsvParser: IParser
    {
        /// <summary>
        /// Parses the specified file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="rules">The business rules to be applied on the object</param>
        /// <returns></returns>
        public List<T> Parse<T>(string filePath, List<InvalidRecordRules> rules= null)
        {
            // Create a new instance for List<T> to add the objects
            var parsedObj = Activator.CreateInstance<List<T>>();

            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    //skip header
                    streamReader.ReadLine();
                    string line;

                    // Read all lines until EOF
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // Remove the double quotes in the line
                        var splitRows = line.RemoveQuotes().Split(',');

                        // Create a new instance for the class and pass in the string array and business rules to map the rows to the respective object
                        var obj = (T)Activator.CreateInstance(typeof(T), splitRows, rules);

                        // Add the new instance to the collection
                        parsedObj.Add(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

           return parsedObj;
        }
        
    }
}
