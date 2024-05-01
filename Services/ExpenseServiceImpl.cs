using AutoMapper;
using AutoMapper.Configuration.Annotations;
using ExpensesManagementApp.DAO;
using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Model;
using System.Transactions;

namespace ExpensesManagementApp.Services
{
    public class ExpenseServiceImpl : IExpenseService
    {
        private readonly IExpenseDAO _expenseDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<ExpenseServiceImpl> _logger;

        public ExpenseServiceImpl(IExpenseDAO expenseDAO, IMapper mapper, ILogger<ExpenseServiceImpl> logger)
        {
            _expenseDAO = expenseDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public Expense? DeleteExpense(int id)
        {
            Expense? expenseToReturn = null;

            try
            {
                using TransactionScope scope = new();
                expenseToReturn = _expenseDAO.Get(id);
                if (expenseToReturn is null) throw new ApplicationException("The expense with id " + id + " was not found");
                _expenseDAO.Delete(id);
                scope.Complete();
                _logger.LogInformation("Expense with id {id} was deleted", id);
            }
            catch (Exception e) 
            {
                _logger.LogError(e.Message);
                throw;
            }
            return expenseToReturn;
        }

        public IList<Expense> GetAllExpenses()
        {
            IList<Expense> expenses = [];

            try
            {
                expenses = _expenseDAO.GetAll();
                if (expenses.Count == 0) throw new ApplicationException("There are no expenses right now");
                _logger.LogInformation("All expenses were retrieved");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                throw;
            }
            return expenses;
        }

        public Expense? GetExpense(int id)
        {
            try
            {
                Expense? expense = _expenseDAO.Get(id);
                if (expense is null) throw new ApplicationException("Expense with id " + id + " was not found");
                _logger.LogInformation("Expense with id {id} was retrieved", id);
                return expense;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public IList<Expense> GetExpensesByName(string name)
        {
            try
            {
                IList<Expense> expenses = _expenseDAO.GetByName(name);
                if (expenses.Count == 0) throw new ApplicationException("No expenses found with name: " + name);
                _logger.LogInformation("All expenses with name: {name} were retrieved", name);
                return expenses;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public Expense? InsertExpense(ExpenseInsertDTO dto)
        {
            try
            {
                var expense = _mapper.Map<Expense>(dto);
                using TransactionScope scope = new();
                expense = _expenseDAO.Insert(expense);
                scope.Complete();
                _logger.LogInformation($"Inserted expense: {expense}");
                return expense;
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured while inserting expense: " + e.Message);
                throw;
            }
        }

        public Expense? UpdateExpense(ExpenseUpdateDTO dto)
        {
            try
            {
                var expense = _mapper.Map<Expense>(dto);
                using TransactionScope scope = new();
                Expense? existingExpense = _expenseDAO.Get(expense.Id);
                if (existingExpense == null) throw new ApplicationException("Update Error: Expense with id " + dto.Id + " was not found");
                expense = _expenseDAO.Update(expense);
                scope.Complete();
                _logger.LogInformation($"Updated expense: {expense}");
                return expense;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
