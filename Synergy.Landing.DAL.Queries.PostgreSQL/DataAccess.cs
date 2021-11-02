using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Synergy.Common.DAL.Access.PostgreSQL;

namespace Synergy.Landing.DAL.Queries.PostgreSQL
{
    public class DataAccess : BaseDataAccess
    {
        private const string Schema = "main";

        public DataAccess(ILoggerFactory loggerFactory, string nameOrConnectionString)
            : base(loggerFactory, nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Schema);

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
