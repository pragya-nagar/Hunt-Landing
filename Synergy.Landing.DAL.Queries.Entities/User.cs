using System;
using System.Collections.Generic;
using Synergy.Common.DAL.Abstract;

namespace Synergy.Landing.DAL.Queries.Entities
{
    public class User : IAuditEntity<Guid>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<UserRole> Roles { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
