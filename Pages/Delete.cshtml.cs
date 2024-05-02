using ExpensesManagementApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpensesManagementApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IExpenseService _expenseService;

        public DeleteModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                _expenseService.DeleteExpense(id);
            }
            catch (Exception)
            {

            }
            return Redirect("/Index");
        }
    }
}
