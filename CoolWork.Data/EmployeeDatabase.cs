using CoolWork.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork
{
    public class EmployeeDatabase
    {
        //private const string ConnectionString = "Data Source = localhost\\SQLEXPRESS; Initial Catalog=CoolWork; User ID=rootcw; Password=rootcw";
        private static string[] FNames = { "Иван", "Петр", "Илья", "Максим", "Николай", "Алексей", "Виталий", "Сергей", "Константин", "Василий" };
        private static string[] SNames = { "Иванович", "Петрович", "Ильич", "Максимович", "Николаевич", "Алексеевич", "Витальевич", "Сергеевич", "Константинович", "Васильевич" };
        private static string[] LNames = { "Иванов", "Петров", "Ильин", "Максимов", "Николаев", "Алексеев", "Витальев", "Сергеев", "Константинов", "Васильев" };

        private ObservableCollection<Department> _departments;
        private ObservableCollection<Position> _positions;

        private Random rnd = new Random();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public EmployeeDatabase(ObservableCollection<Department> departments, ObservableCollection<Position> positions)
        {
            //Employees = new ObservableCollection<Employee>();
            _departments = departments;
            _positions = positions;

            LoadFromDatabase();
            //SyncToDatabase();
            //GenerateEmployees(50);
        }

        public int Add(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.ConnectionString))
            {
                sqlConnection.Open();

                var fired = employee.Fired ? 1 : 0;
                string sqlExpression = $@"INSERT INTO Employees (FirstName,SecondName,LastName,Phone,Fired,Comment,DeptId,PositionId) 
                                        VALUES ('{employee.FirstName}','{employee.SecondName}','{employee.LastName}','{employee.Phone}',{fired},'{employee.Comment}',{employee.Dept.Id},{employee.Position.Id}) 
                                        SELECT CAST(SCOPE_IDENTITY() AS INT)";

                SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);

                var res = (int)cmd.ExecuteScalar();

                if (res > 0)
                {
                    Employees.Add(employee);
                }

                return res;
            }
        }

        private void LoadFromDatabase()
        {
            string sqlExpression = @"SELECT Employees.EmployeeId, Employees.FirstName, Employees.SecondName, Employees.LastName, Employees.Phone, Employees.Fired, Employees.Comment, Positions.PositionId, Departments.DeptId 
                                    FROM Employees 
                                    INNER JOIN Departments ON Employees.DeptId = Departments.DeptId 
                                    INNER JOIN Positions ON Employees.PositionId = Positions.PositionId";

            using (SqlConnection sqlConnection = new SqlConnection(Constants.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee()
                            {
                                Id = (int)reader["EmployeeId"],
                                FirstName = reader["FirstName"].ToString().Trim(),
                                SecondName = reader["SecondName"].ToString().Trim(),
                                LastName = reader["LastName"].ToString().Trim(),
                                Phone = reader["Phone"].ToString().Trim(),
                                Comment = reader["Comment"].ToString().Trim(),
                                Fired = (bool)reader["Fired"],
                                Dept = DepartmentDatabase.GetDeptById((int)reader["DeptId"]),
                                Position = PositionDatabase.GetPositionById((int)reader["PositionId"])
                            };
                            Employees.Add(employee);
                        }
                    }
                }

            }

        }

        public int Update(Employee employee)
        {
            var fired = employee.Fired ? 1 : 0;
            string sqlExpression = $@"UPDATE Employees 
                                   SET FirstName = '{employee.FirstName}', SecondName = '{employee.SecondName}', LastName = '{employee.LastName}', Comment = '{employee.Comment}', Phone = '{employee.Phone}', Fired = '{fired}', DeptId = '{employee.Dept.Id}', PositionId = '{employee.Position.Id}' 
                                   WHERE EmployeeId = '{employee.Id}'";

            using (SqlConnection sqlConnection = new SqlConnection(Constants.ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
                return cmd.ExecuteNonQuery();

            }
        }

        public int Remove(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.ConnectionString))
            {
                sqlConnection.Open();

                string sqlExpression = $@"DELETE FROM Employees WHERE EmployeeId = '{employee.Id}'";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);

                var res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Employees.Remove(employee);
                }
                return res;

            }
        }
        //private void GenerateEmployees(int amount)
        //{
        //    for (var i = 0; i < amount; i++)
        //    {
        //        //Employees.Add(new Employee(FNames[rnd.Next(9)], SNames[rnd.Next(9)], LNames[rnd.Next(9)], $" + 7 499 {rnd.Next(100,999)}-{rnd.Next(1000,9999)}", _departments[rnd.Next(5)], _positions[rnd.Next(5)], ""));
        //        var emp = new Employee(FNames[rnd.Next(9)], SNames[rnd.Next(9)], LNames[rnd.Next(9)], $"+7 499 {rnd.Next(100, 999)}-{rnd.Next(1000, 9999)}", _departments[rnd.Next(5)], _positions[rnd.Next(5)], "");
        //        Add(emp);
        //    }
        //}




    }

}
