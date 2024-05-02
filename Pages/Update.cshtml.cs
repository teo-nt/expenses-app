using AutoMapper;
using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Model;
using ExpensesManagementApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpensesManagementApp.Pages
{
    public class UpdateModel : PageModel
    {
        public ExpenseUpdateDTO ExpenseUpdateDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = [];

        private readonly IExpenseService _expenseService;
        private readonly IValidator<ExpenseUpdateDTO> _validator;
        private readonly IMapper _mapper;

        public UpdateModel(IExpenseService expenseService, IValidator<ExpenseUpdateDTO> validator, IMapper mapper)
        {
            _expenseService = expenseService;
            _validator = validator;
            _mapper = mapper;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Expense expense = _expenseService.GetExpense(id);
                ExpenseUpdateDTO = _mapper.Map<ExpenseUpdateDTO>(expense);
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
            return Page();
        }

        public void OnPost(ExpenseUpdateDTO dto)
        {
            ExpenseUpdateDTO = dto;
            var validationResult = _validator.Validate(dto);

            if(!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                _expenseService.UpdateExpense(dto);
                Response.Redirect("/Index");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
