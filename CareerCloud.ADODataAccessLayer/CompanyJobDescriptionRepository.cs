using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                           ([Id]
                                           ,[Job]
                                           ,[Job_Name]
                                           ,[Job_Descriptions])
                                        VALUES
                                           (@Id
                                           ,@Job
                                           ,@Job_Name
                                           ,@Job_Descriptions)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT * FROM [dbo].[Company_Jobs_Descriptions]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1050];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Job = reader.GetGuid(1);
                        poco.JobName = reader.IsDBNull(2) ? null : reader.GetString(2);
                        poco.JobDescriptions = reader.IsDBNull(3) ? null : reader.GetString(3);
                        poco.TimeStamp = (byte[])reader[4];

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

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
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

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                        SET 
                                           [Job] = @Job
                                          ,[Job_Name] = @Job_Name
                                          ,[Job_Descriptions] = @Job_Descriptions
                                        WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

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
