using System;
using System.Data;
using System.Data.SqlClient;


namespace BACKSERVICES
{
    
    public class DBAccess : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DBAccess(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        public void Open()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(procedureName, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);
                    return result;
                }
            }
        }

        public int ExecuteStoredProcedureNonQuery(string procedureName, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(procedureName, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteStoredProcedureScalar(string procedureName, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(procedureName, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }

}
