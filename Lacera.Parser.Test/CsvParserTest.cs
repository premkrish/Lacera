using System;
using System.Collections.Generic;
using System.IO;
using Lacera.Enums;
using Lacera.Objects.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lacera.Parser.Test
{
    [TestClass]
    public class CsvParserTest
    {
        /// <summary>
        /// Shoulds the return parsed record count as two.
        /// </summary>
        [TestMethod]
        public void ShouldReturnParsedRecordCountAsThree()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "testfile.csv");
            var parser = new CsvParser();
            var employee = parser.Parse<Employee>(path);

            //Asserts
            Assert.AreEqual(3,employee.Count);
        }

        /// <summary>
        /// Shoulds the return second employee name as clark kent with no quotes.
        /// </summary>
        [TestMethod]
        public void ShouldReturnSecondEmployeeNameAsClarkKentWithNoQuotes()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "testfile.csv");
            var parser = new CsvParser();
            var employee = parser.Parse<Employee>(path);

            //Asserts
            Assert.AreEqual("Clark Kent", employee[1].EmployeeName);
        }

        /// <summary>
        /// Parses the and return first employee record with no business rules.
        /// </summary>
        [TestMethod]
        public void ParseAndReturnFirstEmployeeRecordWithNoBusinessRules()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "testfile.csv");
            var parser = new CsvParser();
            var employee = parser.Parse<Employee>(path);

            //Asserts
            Assert.AreEqual("Bruce Wayne", employee[0].EmployeeName);
            Assert.AreEqual("10/10/1970", employee[0].BirthDate.ToShortDateString());
            Assert.AreEqual(92000, employee[0].Salary);
            Assert.AreEqual("1/1/2013", employee[0].DateHired.ToShortDateString());
        }

        /// <summary>
        /// Returns the record status as invalid when passed an invalid date.
        /// </summary>
        [TestMethod]
        public void ReturnRecordStatusAsInvalidWhenPassedAnInvalidDate()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "invalidrecords.csv");
            var parser = new CsvParser();
            var businessRule = new List<InvalidRecordRules> { InvalidRecordRules.InvalidDate};
            var employee = parser.Parse<Employee>(path, businessRule);

            //Asserts
            Assert.AreEqual("1/1/0001", employee[0].BirthDate.ToShortDateString());
            Assert.AreEqual("7/7/2014", employee[0].DateHired.ToShortDateString());
            Assert.AreEqual(RecordStatus.Invalid, employee[0].Status);
        }

        /// <summary>
        /// Returns the record status as invalid when passed an invalid amount.
        /// </summary>
        [TestMethod]
        public void ReturnRecordStatusAsInvalidWhenPassedAnInvalidAmount()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "invalidrecords.csv");
            var parser = new CsvParser();
            var businessRule = new List<InvalidRecordRules> { InvalidRecordRules.InvalidAmount };
            var employee = parser.Parse<Employee>(path, businessRule);

            //Asserts
            Assert.AreEqual(0, employee[1].Salary);
            Assert.AreEqual(RecordStatus.Invalid, employee[1].Status);
        }

        /// <summary>
        /// Returns the record status as invalid when passed a missing field.
        /// </summary>
        [TestMethod]
        public void ReturnRecordStatusAsInvalidWhenPassedAMissingField()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "invalidrecords.csv");
            var parser = new CsvParser();
            var businessRule = new List<InvalidRecordRules> { InvalidRecordRules.MissingField };
            var employee = parser.Parse<Employee>(path, businessRule);

            //Asserts
            Assert.AreEqual("1/1/0001", employee[2].DateHired.ToShortDateString());
            Assert.AreEqual(RecordStatus.Invalid, employee[2].Status);
        }

        /// <summary>
        /// Parses and returns records with all rules applied.
        /// </summary>
        [TestMethod]
        public void ParseAndReturnRecordsWithAllRulesApplied()
        {
            // Setup Test file
            string path = Path.Combine(Environment.CurrentDirectory, "testfile.csv");
            var parser = new CsvParser();
            var businessRule = new List<InvalidRecordRules> { InvalidRecordRules.InvalidDate, InvalidRecordRules.InvalidAmount, InvalidRecordRules.MissingField };
            var employee = parser.Parse<Employee>(path, businessRule);

            //Asserts
            Assert.AreEqual("Bruce Wayne", employee[0].EmployeeName);
            Assert.AreEqual("10/10/1970", employee[0].BirthDate.ToShortDateString());
            Assert.AreEqual(92000, employee[0].Salary);
            Assert.AreEqual("1/1/2013", employee[0].DateHired.ToShortDateString());
            Assert.AreEqual(RecordStatus.Valid, employee[0].Status);

            Assert.AreEqual("Clark Kent", employee[1].EmployeeName);
            Assert.AreEqual("5/5/1970", employee[1].BirthDate.ToShortDateString());
            Assert.AreEqual(90000, employee[1].Salary);
            Assert.AreEqual("4/4/2015", employee[1].DateHired.ToShortDateString());
            Assert.AreEqual(RecordStatus.Valid, employee[1].Status);

            Assert.AreEqual("John Doe", employee[2].EmployeeName);
            Assert.AreEqual("1/1/0001", employee[2].BirthDate.ToShortDateString());
            Assert.AreEqual(0, employee[2].Salary);
            Assert.AreEqual("4/4/2015", employee[2].DateHired.ToShortDateString());
            Assert.AreEqual(RecordStatus.Valid, employee[0].Status);
            Assert.AreEqual(RecordStatus.Invalid, employee[2].Status);
        }
    }
}
