using System;
using Synergy.Common.DAL.Abstract;

namespace Synergy.Landing.DAL.Queries.Entities
{
    public class Task : IAuditEntity<Guid>
    {
        public DateTime DueDate { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid EventId { get; set; }

        public Event Event { get; set; }

        public int TypeId { get; set; }

        public TaskType Type { get; set; }

        public int StatusId { get; set; }

        public TaskStatus Status { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
