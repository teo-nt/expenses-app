using AutoMapper;
using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Model;
using ExpensesManagementApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SqlServer.Server;

namespace ExpensesManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<ExpenseReadOnlyDTO> Expenses { get; set; } = [];
        public Error? ErrorObj { get; set; }
        private readonly IMapper _mapper;
        private readonly IExpenseService _expenseService;

        public IndexModel(IMapper mapper, IExpenseService expenseService)
        {
            _mapper = mapper;
            _expenseService = expenseService;
        }

        public IActionResult OnGet()
        {
            ErrorObj = null;
            try
            {
                IList<Expense> expenses = _expenseService.GetAllExpenses();

                foreach (var expense in expenses)
                {
                    Expenses.Add(_mapper.Map<ExpenseReadOnlyDTO>(expense));
                }
            }
            catch (Exception e)
            {
                ErrorObj = new Error("", e.Message, "");
            }
            return Page();
        }
    }
}
