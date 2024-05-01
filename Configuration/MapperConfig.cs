using AutoMapper;
using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Model;

namespace ExpensesManagementApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ExpenseInsertDTO, Expense>().ReverseMap();
            CreateMap<ExpenseUpdateDTO, Expense>().ReverseMap();
            CreateMap<ExpenseReadOnlyDTO, Expense>().ReverseMap();
        }
    }
}
