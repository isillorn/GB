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
    public class PositionDatabase
    {

        private static PositionDatabase _positionDatabase;

        public static ObservableCollection<Position> Get()
        {
            if (_positionDatabase == null)
                _positionDatabase = new PositionDatabase();
            return _positionDatabase.Positions;
        }

        public static Position GetPositionById(int id)
        {
            Position res = null;
            foreach (var position in _positionDatabase.Positions) { 
                if (position.Id == id) { 
                    res = position;
                    break;
                }
            }
            return res;
        }

        public ObservableCollection<Position> Positions { get; set; } = new ObservableCollection<Position>();

        public PositionDatabase()
        {
            LoadFromDatabase();
            _positionDatabase = this;
        }

        public int Add(Position position)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.ConnectionString))
            {
                sqlConnection.Open();

                string sqlExpression = $@"INSERT INTO Positions (PositionName) VALUES ('{position.PositionName}') SELECT CAST(SCOPE_IDENTITY() AS INT)";

                SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);

                var res = (int)cmd.ExecuteScalar();

                if (res > 0)
                {
                    position.Id = res;
                    Positions.Add(position);
                }

                return res;
            }
        }

        private void LoadFromDatabase()
        {
            string sqlExpression = @"SELECT * FROM Positions";

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
                            var position = new Position()
                            {
                                Id = (int)reader["PositionId"],
                                PositionName = reader["PositionName"].ToString()
                            };
                            Positions.Add(position);
                        }
                    }
                }

            }

        }




    }
}
