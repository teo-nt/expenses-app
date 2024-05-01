﻿namespace ExpensesManagementApp.DTO
{
    public class ExpenseInsertDTO
    {
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
    }
}
