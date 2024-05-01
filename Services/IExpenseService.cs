using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Model;

namespace ExpensesManagementApp.Services
{
    public interface IExpenseService
    {
        Expense? InsertExpense(ExpenseInsertDTO dto);
        Expense? UpdateExpense(ExpenseUpdateDTO dto);
        Expense? DeleteExpense(int id);
        Expense? GetExpense(int id);
        IList<Expense> GetAllExpenses();
        IList<Expense> GetExpensesByName(string name);
    }
}
