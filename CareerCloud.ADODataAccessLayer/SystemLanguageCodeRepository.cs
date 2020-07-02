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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                               ([LanguageID]
                                               ,[Name]
                                               ,[Native_Name])
                                        VALUES
                                               (@LanguageID
                                               ,@Name
                                               ,@Native_Name)";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT * FROM [dbo].[System_Language_Codes]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[10];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                        poco.LanguageID = reader.IsDBNull(0) ? null : reader.GetString(0);
                        poco.Name = reader.IsDBNull(1) ? null : reader.GetString(1);
                        poco.NativeName = reader.IsDBNull(2) ? null : reader.GetString(2);

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

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                        WHERE LanguageID = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
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

        public void Update(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                        SET 
                                               [Name] = @Name
                                              ,[Native_Name] = @Native_Name
                                        WHERE LanguageID = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

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
