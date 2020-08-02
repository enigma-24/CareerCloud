using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<TPoco> : IDataRepository<TPoco> where TPoco : class
    {
        public void Add(params TPoco[] items)
        {
            try
            {
                using (var context = new CareerCloudContext())
                {
                    foreach (var item in items)
                    {
                        context.Entry<TPoco>(item).State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex);
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<TPoco> GetAll(params Expression<Func<TPoco, object>>[] navigationProperties)
        {
            IQueryable<TPoco> dbQuery = default;
            try
            {
                using (var context = new CareerCloudContext())
                {
                    dbQuery = context.Set<TPoco>();
                    foreach (var navProp in navigationProperties)
                    {
                        dbQuery = dbQuery.Include<TPoco, object>(navProp);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex);
            }
            return dbQuery.ToList();
        }

        public IList<TPoco> GetList(Expression<Func<TPoco, bool>> where, params Expression<Func<TPoco, object>>[] navigationProperties)
        {
            IList<TPoco> result = default;
            try
            {
                using (var context = new CareerCloudContext())
                {
                    IQueryable<TPoco>  dbQuery = context.Set<TPoco>();
                    foreach (var navProp in navigationProperties)
                    {
                        dbQuery = dbQuery.Include<TPoco, object>(navProp);
                    }
                    result = dbQuery.Where(where).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex);
            }
            return result;
        }

        public TPoco GetSingle(Expression<Func<TPoco, bool>> where, params Expression<Func<TPoco, object>>[] navigationProperties)
        {
            TPoco result = default;
            try
            {
                using (var context = new CareerCloudContext())
                {
                    IQueryable<TPoco> dbQuery = context.Set<TPoco>();
                    foreach (Expression<Func<TPoco, object>> navProp in navigationProperties)
                    {
                        dbQuery = dbQuery.Include<TPoco, object>(navProp);
                    }
                    result = dbQuery.Where(where).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex);
            }
            return result;
        }

        public void Remove(params TPoco[] items)
        {
            try
            {
                using (var context = new CareerCloudContext())
                {
                    foreach (var item in items)
                    {
                        context.Entry<TPoco>(item).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception - {ex}");
            }
        }

        public void Update(params TPoco[] items)
        {
            try
            {
                using (var context = new CareerCloudContext())
                {
                    foreach (var item in items)
                    {
                        context.Entry<TPoco>(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception - {0}", ex);
            }
        }
    }
}
