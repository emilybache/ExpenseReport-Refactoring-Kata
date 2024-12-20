<?php

namespace Expenses\Test;

use Expenses\Expense;
use Expenses\ExpenseReport;
use Expenses\ExpenseType;
use PHPUnit\Framework\TestCase;
use ApprovalTests\Approvals;

class ExpenseReportTest extends TestCase {

    function createExpense($type, $amount): Expense
    { return new Expense($type, $amount); }

    public function testReport()
    {
        ob_start();

        $expenses = [
            $this->createExpense(ExpenseType::DINNER, 5000),
            $this->createExpense(ExpenseType::DINNER, 5001),
            $this->createExpense(ExpenseType::BREAKFAST, 1000),
            $this->createExpense(ExpenseType::BREAKFAST, 1001),
            $this->createExpense(ExpenseType::CAR_RENTAL, 200)
        ];
        $report = new ExpenseReport();
        $report->print_report($expenses);

        $output = ob_get_clean();
        // drop the first line because it varies. Crude but effective.
        $output = preg_replace('/^.+\n/', '', $output);
        Approvals::verifyString($output);
    }
}


