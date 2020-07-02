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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                           ([Id]
                                           ,[Registration_Date]
                                           ,[Company_Website]
                                           ,[Contact_Phone]
                                           ,[Contact_Name]
                                           ,[Company_Logo])
                                        VALUES
                                           (@Id
                                           ,@Registration_Date
                                           ,@Company_Website
                                           ,@Contact_Phone
                                           ,@Contact_Name
                                           ,@Company_Logo)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT * FROM [dbo].[Company_Profiles]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1050];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        CompanyProfilePoco poco = new CompanyProfilePoco();
                        poco.Id = reader.GetGuid(0);
                        poco.RegistrationDate = reader.GetDateTime(1);
                        poco.CompanyWebsite = reader.IsDBNull(2) ? null : reader.GetString(2);
                        poco.ContactPhone = reader.IsDBNull(3) ? null : reader.GetString(3);
                        poco.ContactName = reader.IsDBNull(4) ? null : reader.GetString(4);
                        poco.CompanyLogo = reader.IsDBNull(5) ? null : (byte[])reader.GetValue(5);
                        poco.TimeStamp = (byte[])reader[6];

                        pocos[index++] = poco;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception = {0}", ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
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

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                       SET 
                                           [Registration_Date] = @Registration_Date
                                          ,[Company_Website] = @Company_Website
                                          ,[Contact_Phone] = @Contact_Phone
                                          ,[Contact_Name] = @Contact_Name
                                          ,[Company_Logo] = @Company_Logo
                                       WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

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
