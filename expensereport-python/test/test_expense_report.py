import io
import sys

from approvaltests import approvals, Options
from approvaltests.scrubbers.date_scrubber import DateScrubber

import expense_report

def createExpense(type, amount):
    expense = expense_report.Expense()
    expense.type = type
    expense.amount = amount
    return expense

def test_expense_report():
    orig_sysout = sys.stdout
    try:
        fake_stdout = io.StringIO()
        sys.stdout = fake_stdout

        expenses = [
            createExpense(expense_report.ExpenseType.DINNER, 5000),
            createExpense(expense_report.ExpenseType.DINNER, 5001),
            createExpense(expense_report.ExpenseType.BREAKFAST, 1000),
            createExpense(expense_report.ExpenseType.BREAKFAST, 1001),
            createExpense(expense_report.ExpenseType.CAR_RENTAL, 200)
        ]
        expense_report.ExpenseReport().print_report(expenses)

        answer = fake_stdout.getvalue()
        approvals.verify(answer, options=Options().add_scrubber(
            DateScrubber.get_scrubber_for("Wed Dec 11 14:59:44 2024")
        ))
    finally:
        sys.stdout = orig_sysout

