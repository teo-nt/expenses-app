using ExpensesManagementApp.Model;
using ExpensesManagementApp.Services.DBHelper;
using System.Data;
using System.Data.SqlClient;

namespace ExpensesManagementApp.DAO
{
    public class ExpenseDAOImpl : IExpenseDAO
    {
        public void Delete(int id)
        {
            string sql = "DELETE FROM EXPENSES WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

        }

        public Expense? Get(int id)
        {
            string sql = "SELECT * FROM EXPENSES WHERE ID = @id;";
            Expense? expense = null;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn != null) conn.Open();

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                expense = new()
                {
                    Id = reader.GetInt32("ID"),
                    Name = reader.GetString("NAME"),
                    Amount = reader.GetDecimal("AMOUNT"),
                    Category = reader.GetString("CATEGORY"),
                    Date = reader.GetDateTime("DATE")
                };
            }
            return expense;
        }

        public IList<Expense> GetAll()
        {
            string sql = "SELECT * FROM EXPENSES";
            IList<Expense> expenses = [];

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn != null) conn.Open();

            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Expense expense = new()
                {
                    Id = reader.GetInt32("ID"),
                    Name = reader.GetString("NAME"),
                    Amount = reader.GetDecimal("AMOUNT"),
                    Category = reader.GetString("CATEGORY"),
                    Date = reader.GetDateTime("DATE")
                };
                expenses.Add(expense);
            }
            return expenses;
        }

        public IList<Expense> GetByName(string name)
        {
            string sql = "SELECT * FROM EXPENSES WHERE NAME = @name;";
            IList<Expense> expenses = [];

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn != null) conn.Open();

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Expense expense = new()
                {
                    Id = reader.GetInt32("ID"),
                    Name = reader.GetString("NAME"),
                    Amount = reader.GetDecimal("AMOUNT"),
                    Category = reader.GetString("CATEGORY"),
                    Date = reader.GetDateTime("DATE")
                };
                expenses.Add(expense);
            }
            return expenses;
        }

        public Expense? Insert(Expense expense)
        {
            string sql = "INSERT INTO EXPENSES (NAME, AMOUNT, CATEGORY, DATE) VALUES (@name, @amount, @category, @date);" +
                    "SELECT SCOPE_IDENTITY();";

            using SqlConnection? conn = DBHelper.GetConnection();
            if ( conn != null ) conn.Open();

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", expense.Name);
            cmd.Parameters.AddWithValue("@amount", expense.Amount);
            cmd.Parameters.AddWithValue("@category", expense.Category);
            cmd.Parameters.AddWithValue("@date", expense.Date);

            object insertedObj = cmd.ExecuteScalar();
            if (insertedObj == null)
            {
                throw new Exception("Error in insert id");
            }
            if (!int.TryParse(insertedObj.ToString(), out int insertedId))
            {
                throw new Exception("Error in insert id");
            }

            expense.Id = insertedId;
            return expense;
        }

        public Expense? Update(Expense expense)
        {
            string sql = "UPDATE EXPENSES SET NAME = @name, AMOUNT = @amount, CATEGORY = @category, DATE = @date WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if ( conn != null ) conn.Open();

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", expense.Name);
            cmd.Parameters.AddWithValue("@amount", expense.Amount);
            cmd.Parameters.AddWithValue("@category", expense.Category);
            cmd.Parameters.AddWithValue("@date", expense.Date);
            cmd.Parameters.AddWithValue("@id", expense.Id);
            cmd.ExecuteNonQuery();

            return expense;
        }
    }
}
