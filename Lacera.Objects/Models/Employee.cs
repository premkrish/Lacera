using System;
using System.Collections.Generic;
using System.ComponentModel;
using Lacera.Enums;
using Lacera.Helper;
using Lacera.Parser.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Lacera.Objects.Models
{
    public class Employee : IEmployee
    {
        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <value>
        /// The name of the employee.
        /// </value>
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        /// <value>
        /// The salary.
        /// </value>
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the date hired.
        /// </summary>
        /// <value>
        /// The date hired.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Date Hired")]
        public DateTime DateHired { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public RecordStatus Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee" /> class.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="rules">The rules.</param>
        public Employee (string[] values, IEnumerable<InvalidRecordRules> rules)
        {
            // Update the length if there are new columns/existing columns are removed
            if (values.Length != 4) return;

            // Map CSV values to Employee fields
            EmployeeName = values[0].Trim().RemoveQuotes();
            BirthDate = Generic.ConvertTo<DateTime>(values[1].Trim());
            Salary = Generic.ConvertTo<decimal>(values[2].Trim());
            DateHired = Generic.ConvertTo<DateTime>(values[3].Trim());

            if (rules != null)
            {
                var keyValuePairs = new List<KeyValuePair<Type, string>>
                {
                    new KeyValuePair<Type, string>(typeof (string), values[0]),
                    new KeyValuePair<Type, string>(typeof (DateTime), values[1]),
                    new KeyValuePair<Type, string>(typeof (string), values[2]),
                    new KeyValuePair<Type, string>(typeof (DateTime), values[3])
                };

                var businessRules = new BusinessRules();

                // Apply Business Rules and set the record status
                Status = businessRules.Apply(rules, this, keyValuePairs);
            }
            else
            {
                Status = RecordStatus.Valid;
            }
        }
    }
}
