using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lacera.Enums;
using Lacera.Objects.Models;
using Lacera.Parser;

namespace Lacera.ConsoleApp
{
    static class Program
    {
        #region Constants
        const string FilePathNotExists = "The File path doesn't exist";
        const string ArgumentNotFound = "No Arguments were provided";
        #endregion

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
           try
            {
                if (args.Length > 0)
                {
                    var path = args[0];

                    //Check if the path exists
                    if (File.Exists(path))
                    {
                        var parser = new CsvParser();

                        //Apply rules
                        var businessRule = new List<InvalidRecordRules> {InvalidRecordRules.InvalidDate,InvalidRecordRules.InvalidAmount,InvalidRecordRules.MissingField};

                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("LACERA parsing begins");

                        //Parse the CSV 
                        var employee = parser.Parse<Employee>(path, businessRule);

                        Console.WriteLine("LACERA parsing completed");
                        Console.WriteLine("Printing Object Properties and values");

                       var propertyNames = string.Join("\t", typeof (Employee).GetProperties().Select(p => p.Name));

                        Console.WriteLine("---------------------------------------------------------------------");
                        //Write header
                        Console.WriteLine(propertyNames);

                        Console.WriteLine("---------------------------------------------------------------------");
                        
                        foreach (var emp in employee)
                        {
                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine(string.Join("\t", emp.EmployeeName,emp.BirthDate.ToShortDateString(),emp.Salary,emp.DateHired.ToShortDateString(),emp.Status));                           
                        }
                    }
                    else
                    {
                        Console.WriteLine(FilePathNotExists);
                    }
                }
                else
                {
                    Console.WriteLine(ArgumentNotFound);
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            
        }
    }
}
