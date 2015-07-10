using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAGTask1.DAL
{
    public class BaseBankRepository
    {
        protected BankContext context;

        public BaseBankRepository(BankContext context)
        {
            this.context = context;
        }


        protected bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}