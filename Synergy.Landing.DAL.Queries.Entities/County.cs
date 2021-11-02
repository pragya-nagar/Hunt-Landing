namespace Synergy.Landing.DAL.Queries.Entities
{
    using System;
    using System.Collections.Generic;
    using Synergy.Common.DAL.Abstract;

    public class County : IAuditEntity<int>
    {
        public string Name { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
