using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab4.Helpers
{
    public class ADOHelper
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbTest"].ConnectionString;
        private DataTable dt = null;

        public DataTable LoadTable() {
            if (dt != null) return dt;

            dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandText = "Select Id, ISBN, Name as FullName, Authors, Publisher, Year From Books;";
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error when connection to db!!!");
                }
                {
                    return dt;
                }
            }
        }

        public void CreateBook(int id, string isbn, string name, string authors, string publisher, int year)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Books (Id, ISBN, Name, Authors, Publisher, Year)
                    VALUES (@Id, @ISBN, @Name, @Authors, @Publisher, @Year)";

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@ISBN", isbn);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Authors", authors);
                command.Parameters.AddWithValue("@Publisher", publisher);
                command.Parameters.AddWithValue("@Year", year);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            // refresh DataTable
            dt = null;
        }

        public void UpdateBook(int id, string isbn, string name, string authors, string publisher, int year)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Books
                    SET ISBN = @ISBN, Name = @Name, Authors = @Authors, Publisher = @Publisher, Year = @Year
                    WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@ISBN", isbn);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Authors", authors);
                command.Parameters.AddWithValue("@Publisher", publisher);
                command.Parameters.AddWithValue("@Year", year);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            dt = null;
        }

        public void DeleteBook(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Books WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            dt = null;
        }
    }
}
