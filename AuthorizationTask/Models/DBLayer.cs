using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AuthorizationTask.Models
{
    public class DBLayer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public int ExecuteDML(string procedure, SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(procedure, con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter param in parameters)
            {
                if (param.Value != null)
                    sqlCommand.Parameters.Add(param);
            }
            if (con.State == ConnectionState.Closed)
                con.Open();
            int result = sqlCommand.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public DataTable ExecuteSelect(string procedure)
        {
            SqlCommand sqlCommand = new SqlCommand(procedure, con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            dataAdapter.Fill(dt);
            return dt;
        }
        public DataTable ExecuteSelect(string procedure, SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(procedure, con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter param in parameters)
            {
                if (param.Value != null)
                    sqlCommand.Parameters.Add(param);
            }
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            dataAdapter.Fill(dt);
            return dt;
        }
        public object ExecuteScaler(string procedure, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter param in parameters)
            {
                if (param.Value != null)
                    cmd.Parameters.Add(param);
            }
            if (con.State == ConnectionState.Closed)
                con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            return result;
        }
    }
}