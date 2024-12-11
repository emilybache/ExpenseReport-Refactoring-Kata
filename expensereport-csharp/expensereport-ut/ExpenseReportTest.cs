using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using expensereport_csharp;
using NUnit.Framework;
using VerifyNUnit;
using VerifyTests;

namespace Tests
{
    public class ExpenseReportTest
    {
        public Expense createExpense(ExpenseType type, int amount)
        {
            var expense = new Expense();
            expense.type = type;
            expense.amount = amount;
            return expense;
        }

        [Test]
        public Task ExpenseReport()
        {
            // Redirect Console output to a StringWriter
            var sw = new StringWriter();
            Console.SetOut(sw);

            var expenses = new List<Expense>()
            {
                createExpense(ExpenseType.DINNER, 5000),
                createExpense(ExpenseType.DINNER, 5001),
                createExpense(ExpenseType.BREAKFAST, 1000),
                createExpense(ExpenseType.BREAKFAST, 1001),
                createExpense(ExpenseType.CAR_RENTAL, 200)
            };
            new ExpenseReport().PrintReport(expenses);
            
            sw.Flush();
            var allWrittenLines = sw.ToString();

            var settings = new VerifySettings();
            // TODO: improve this to only scrub the date and not the whole line
            settings.ScrubLines(line => line.Contains("Expenses"));
            return Verifier.Verify(allWrittenLines, settings);
        }
    }
}