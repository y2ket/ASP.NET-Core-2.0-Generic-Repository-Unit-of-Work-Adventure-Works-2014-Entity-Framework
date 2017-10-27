using System;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EMS2.Repository
{
    public class BussinessTransaction : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public BussinessTransaction(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public void Execute(Action action)
        {
            Execute(IsolationLevel.ReadCommitted, action);
        }
        public void Execute(IsolationLevel isolationLevel, Action action)
        {
            Execute(null, isolationLevel, action);
        }

        public void Execute(int? timeout, Action action)
        {
            Execute(timeout, IsolationLevel.ReadCommitted, action);
        }

        public void Execute(int? timeout, IsolationLevel isolationLevel, Action action)
        {
            RetryPolicy policy = new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(3, TimeSpan.FromMilliseconds(750));

            try
            {

                policy.ExecuteAction(() =>
                {
                    var context = _unitOfWork.GetContext();
                    
                    if (timeout.HasValue)
                        context.Database.SetCommandTimeout(timeout.Value);

                    using (IDbContextTransaction transaction = context.Database.BeginTransaction(isolationLevel))
                    {

                        try
                        {
                            action();
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            //throw;
                        }
                    }

                });
            }
            catch { }
        }
    }
}
