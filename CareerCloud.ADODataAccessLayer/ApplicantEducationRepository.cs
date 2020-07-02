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
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                           ([Id]
                                           ,[Applicant]
                                           ,[Major]
                                           ,[Certificate_Diploma]
                                           ,[Start_Date]
                                           ,[Completion_Date]
                                           ,[Completion_Percent])
                                   VALUES
                                           (@Id
                                           ,@Applicant
                                           ,@Major
                                           ,@Certificate_Diploma
                                           ,@Start_Date
                                           ,@Completion_Date
                                           ,@Completion_Percent)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT * FROM [dbo].[Applicant_Educations]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[100];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    int index = 0;
                    while (reader.Read())
                    {
                        ApplicantEducationPoco poco = new ApplicantEducationPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Applicant = reader.GetGuid(1);
                        poco.Major = reader.IsDBNull(2) ? null : reader.GetString(2);
                        poco.CertificateDiploma = reader.IsDBNull(3) ? null : reader.GetString(3);
                        poco.StartDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4);
                        poco.CompletionDate = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5);
                        poco.CompletionPercent = reader.IsDBNull(6) ? null : (byte?)reader.GetByte(6);
                        poco.TimeStamp = (byte[])reader[7];

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

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations]
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

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                   SET 
                                       [Applicant] = @Applicant
                                      ,[Major] = @Major
                                      ,[Certificate_Diploma] = @Certificate_Diploma
                                      ,[Start_Date] = @Start_Date
                                      ,[Completion_Date] = @Completion_Date
                                      ,[Completion_Percent] = @Completion_Percent
                                   WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

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
