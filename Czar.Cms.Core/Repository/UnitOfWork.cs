using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Czar.Cms.Core.DbHelper;
using Czar.Cms.Core.Options;
using Dapper;
using Microsoft.Extensions.Options;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Czar.Cms.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection _dbConnection;
        protected DbOption _dbOption;
        private Dictionary<object, Action> addEntities;
        private Dictionary<object, Action> updateEntities;
        private Dictionary<object, Action> deleteEntities;

        public UnitOfWork(IOptionsSnapshot<DbOption> options)
        {
            _dbOption = options.Get("CzarCms");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            addEntities=new Dictionary<object, Action>();
            updateEntities=new Dictionary<object, Action>();
            deleteEntities=new Dictionary<object, Action>();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
           this.addEntities.Add(entity, () => { _dbConnection.Insert<TEntity>(entity); });
        }

        public int Commit()
        {
            int count = 0;
            using (TransactionScope scope=CreateTransactionScope())
            {
                try
                {
                    using (_dbConnection=ConnectionFactory.CreateConnection(_dbOption.DbType,_dbOption.ConnectionString))
                    {
                        foreach (var entity in deleteEntities.Keys)
                        {
                            this.deleteEntities[entity]();
                        }

                        foreach (var entity in updateEntities.Keys)
                        {
                            this.updateEntities[entity]();
                        }

                        foreach (var entity in addEntities.Keys)
                        {
                            this.addEntities[entity]();
                        }
                    }
                    scope.Complete();
                    count = deleteEntities.Count + updateEntities.Count + addEntities.Count;
                    deleteEntities.Clear();
                    updateEntities.Clear();
                    addEntities.Clear();
                    if (_dbConnection != null)
                    {
                        if (_dbConnection.State == ConnectionState.Open)
                        {
                            _dbConnection.Close();
                        }
                    }

                }
                catch (Exception e)
                {
                    count = 0;
                    Console.WriteLine(e);
                    throw e;
                }
            }


            return count;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.deleteEntities.Add(entity, () => { _dbConnection.Delete(entity); });
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            this.updateEntities.Add(entity, () => { _dbConnection.Update(entity); });
        }

        public static TransactionScope CreateTransactionScope()
        {
            var transactionOptions=new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TransactionManager.MaximumTimeout;
            return new TransactionScope(TransactionScopeOption.Required,transactionOptions);
        }
    }
}