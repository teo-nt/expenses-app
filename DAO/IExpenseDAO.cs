using ExpensesManagementApp.Model;

namespace ExpensesManagementApp.DAO
{
    public interface IExpenseDAO
    {
        Expense? Insert(Expense expense);
        Expense? Update(Expense expense);
        void Delete(int id);
        Expense? Get(int id);
        IList<Expense> GetByName(string name);
        IList<Expense> GetAll();
    }
}
