﻿namespace ExpensesManagementApp.DTO
{
    public class ExpenseReadOnlyDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
    }
}
