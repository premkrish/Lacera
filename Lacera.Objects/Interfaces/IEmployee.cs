using System;
using Lacera.Enums;

namespace Lacera.Parser.Interfaces
{
    public interface IEmployee
    {
        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <value>
        /// The name of the employee.
        /// </value>
        string EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        /// <value>
        /// The salary.
        /// </value>
        decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the date hired.
        /// </summary>
        /// <value>
        /// The date hired.
        /// </value>
        DateTime DateHired { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        RecordStatus Status { get; set; }
    }
}
