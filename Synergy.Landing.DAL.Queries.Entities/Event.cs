using System;
using Synergy.Common.DAL.Abstract;

namespace Synergy.Landing.DAL.Queries.Entities
{
    public class Event : IAuditEntity<Guid>
    {
        public string Number { get; set; }

        public int CountyId { get; set; }

        public County County { get; set; }

        public string CurrentTask { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public int TypeId { get; set; }

        public EventType Type { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
