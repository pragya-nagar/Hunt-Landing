﻿using System;
using Synergy.Common.DAL.Abstract;

namespace Synergy.Landing.DAL.Queries.Entities
{
    public class TaskStatus : IAuditEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedById { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
