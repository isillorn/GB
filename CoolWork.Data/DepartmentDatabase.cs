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
    public class DepartmentDatabase

    {
        private static DepartmentDatabase _departmentDatabase;

        public static ObservableCollection<Department> Get()
        {
            if (_departmentDatabase == null)
                _departmentDatabase = new DepartmentDatabase();
            return _departmentDatabase.Departments;
        }

        public static Department GetDeptById(int id)
        {
            Department res = null;
            foreach (var dept in _departmentDatabase.Departments)
            {
                if (dept.Id == id)
                {
                    res = dept;
                    break;
                }
            }
            return res;
        }

        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        
        public DepartmentDatabase()
        {
            LoadFromDatabase();
            _departmentDatabase = this;
        }

        public int Add(Department department)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.ConnectionString))
            {
                sqlConnection.Open();

                string sqlExpression = $@"INSERT INTO Departments (DeptName) VALUES ('{department.DeptName}') SELECT CAST(SCOPE_IDENTITY() AS INT)";

                SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);

                var res = (int)cmd.ExecuteScalar();

                if (res > 0)
                {
                    department.Id = res;
                    Departments.Add(department);
                }

                return res;
            }
        }


        private void LoadFromDatabase()
        {
            string sqlExpression = @"SELECT * FROM Departments";

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
                            var dept = new Department()
                            {
                                Id = (int)reader["DeptId"],
                                DeptName = reader["Deptname"].ToString()
                            };
                            Departments.Add(dept);
                        }
                    }
                }

            }

        }

    }
}
