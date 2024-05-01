using ExpensesManagementApp.Configuration;
using ExpensesManagementApp.DAO;
using ExpensesManagementApp.DTO;
using ExpensesManagementApp.Services;
using ExpensesManagementApp.Validators;
using FluentValidation;
using Serilog;

namespace ExpensesManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IExpenseDAO, ExpenseDAOImpl>();
            builder.Services.AddScoped<IExpenseService, ExpenseServiceImpl>();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IValidator<ExpenseInsertDTO>, ExpenseInsertValidator>();
            builder.Services.AddScoped<IValidator<ExpenseUpdateDTO>, ExpenseUpdateValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
