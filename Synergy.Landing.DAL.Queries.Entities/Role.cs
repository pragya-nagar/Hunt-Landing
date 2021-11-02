using System;
using System.Collections.Generic;
using Synergy.Common.DAL.Abstract;

namespace Synergy.Landing.DAL.Queries.Entities
{
    public class Role : IAuditEntity<Guid>
    {
        public string Name { get; set; }

        public IEnumerable<DepartmentRole> Departments { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
