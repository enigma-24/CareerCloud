﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                               ([Id]
                                               ,[Login]
                                               ,[Current_Salary]
                                               ,[Current_Rate]
                                               ,[Currency]
                                               ,[Country_Code]
                                               ,[State_Province_Code]
                                               ,[Street_Address]
                                               ,[City_Town]
                                               ,[Zip_Postal_Code])
                                        VALUES
                                               (@Id
                                               ,@Login
                                               ,@Current_Salary
                                               ,@Current_Rate
                                               ,@Currency
                                               ,@Country_Code
                                               ,@State_Province_Code
                                               ,@Street_Address
                                               ,@City_Town
                                               ,@Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            string sqlQuery = @"SELECT * FROM [dbo].[Applicant_Profiles]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[100];
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        ApplicantProfilePoco poco = new ApplicantProfilePoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Login = reader.GetGuid(1);
                        poco.Currency = reader.IsDBNull(4) ? null : reader.GetString(4);
                        poco.Country = reader.IsDBNull(5) ? null : reader.GetString(5);
                        poco.Province = reader.IsDBNull(6) ? null : reader.GetString(6);
                        poco.Street = reader.IsDBNull(7) ? null : reader.GetString(7);
                        poco.City = reader.IsDBNull(8) ? null : reader.GetString(8);
                        poco.PostalCode = reader.IsDBNull(9) ? null : reader.GetString(9);
                        poco.TimeStamp = (byte[])reader[10];
                        poco.CurrentSalary = reader.IsDBNull(2) ? null : reader.GetDecimal(2) as decimal?;
                        poco.CurrentRate = reader.IsDBNull(3) ? null : (decimal?)reader.GetDecimal(3);

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

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
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

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                        SET 
                                                [Login] = @Login
                                              ,[Current_Salary] = @Current_Salary
                                              ,[Current_Rate] = @Current_Rate
                                              ,[Currency] = @Currency
                                              ,[Country_Code] = @Country_Code
                                              ,[State_Province_Code] = @State_Province_Code
                                              ,[Street_Address] = @Street_Address
                                              ,[City_Town] = @City_Town
                                              ,[Zip_Postal_Code] = @Zip_Postal_Code
                                        WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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
