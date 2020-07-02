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
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };
            try
            {
                conn.Open();
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                               ([Id],
                                               [Applicant],
                                               [Job],
                                               [Application_Date])
                                        VALUES
                                               (@Id,
                                               @Applicant,
                                               @Job,
                                               @Application_Date)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex.ToString());
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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT *  FROM [dbo].[Applicant_Job_Applications]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[300];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Applicant = reader.GetGuid(1);
                        poco.Job = reader.GetGuid(2);
                        poco.ApplicationDate = reader.GetDateTime(3);
                        poco.TimeStamp = (byte[])reader[4];

                        pocos[index++] = poco;
                    }
                } 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                        WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                        SET
                                              [Applicant] = @Applicant
                                              ,[Job] = @Job
                                              ,[Application_Date] = @Application_Date
                                        WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}
