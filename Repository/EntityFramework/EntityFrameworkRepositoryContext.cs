using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;

namespace Repository.EntityFramework
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly ThreadLocal<AuDbContext> localCtx = new ThreadLocal<AuDbContext>(() => new AuDbContext());

        public EntityFrameworkRepositoryContext() { }

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Set<TAggregateRoot>().Remove(obj);
            Committed = false;
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Entry<TAggregateRoot>(obj).State = System.Data.Entity.EntityState.Modified;
            Committed = false;
        }

        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Set<TAggregateRoot>().Add(obj);
            Committed = false;
        }

        public override bool DistributedTransactionSupported
        {
            get { return true; }
        }

        public override void Rollback()
        {
            Committed = false;
        }

        protected override void DoCommit()
        {
            if (!Committed)
            {
                var validationErrors = localCtx.Value.GetValidationErrors();
                var count = localCtx.Value.SaveChanges();
                Committed = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Commit();
                localCtx.Value.Dispose();
                localCtx.Dispose();
                base.Dispose(disposing);
            }
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context
        {
            get { return localCtx.Value; }
        }

        #endregion
    }
}
