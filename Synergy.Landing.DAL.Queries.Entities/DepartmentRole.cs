using System;
using Synergy.Common.DAL.Abstract;

namespace Synergy.Landing.DAL.Queries.Entities
{
    public class DepartmentRole : IAuditEntity<Guid>
    {
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public int DepartmentId { get; set; }

        public bool IsManager { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
