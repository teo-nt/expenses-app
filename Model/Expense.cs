namespace ExpensesManagementApp.Model
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }

        public override string? ToString() => 
            $"Id: {Id}, Name: {Name}, Amount: {Amount}, Category: {Category}, Date: {Date}";
       
    }
}
