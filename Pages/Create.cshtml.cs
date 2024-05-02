using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Model;
using ExpensesManagementApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpensesManagementApp.Pages
{
    public class CreateModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = [];
        public ExpenseInsertDTO ExpenseInsertDTO { get; set; } = new();

        private readonly IExpenseService _expenseService;
        private readonly IValidator<ExpenseInsertDTO> _validator;

        public CreateModel(IExpenseService expenseService, IValidator<ExpenseInsertDTO> validator)
        {
            _expenseService = expenseService;
            _validator = validator;
        }

        public void OnGet()
        {
            
        }

        public void OnPost(ExpenseInsertDTO dto) 
        {
            ExpenseInsertDTO = dto;
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Console.WriteLine(dto);
                _expenseService.InsertExpense(dto);
                Response.Redirect("/Index");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
