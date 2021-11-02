using System;
using Synergy.Common.Domain.Models.Abstracts;

namespace Synergy.Landing.Models
{
    public class UserModel : IResultModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
