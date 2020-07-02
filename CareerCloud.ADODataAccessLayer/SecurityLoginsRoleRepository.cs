using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                                               ([Id]
                                               ,[Login]
                                               ,[Role])
                                        VALUES
                                               (@Id
                                               ,@Login
                                               ,@Role)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception - {ex}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT * FROM [dbo].[Security_Logins_Roles]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[100];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Login = reader.GetGuid(1);
                        poco.Role = reader.GetGuid(2);
                        poco.TimeStamp = (byte[])reader[3];

                        pocos[index++] = poco;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception - {ex}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles]
                                        WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception - {ex}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                                       SET 
                                           [Login] = @Login
                                          ,[Role] = @Role
                                       WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception - {ex}");
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}
