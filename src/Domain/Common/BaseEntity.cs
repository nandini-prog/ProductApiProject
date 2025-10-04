// Domain/Common/BaseEntity.cs
using System;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }                      // PK
        public string? CreatedBy { get; set; }           // user who created it (nullable - set by app)
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        public string? ModifiedBy { get; set; }          // last modifier
        public DateTimeOffset? ModifiedOn { get; set; }  // last modified time
        public byte[]? RowVersion { get; set; }          // optional concurrency token (handled in infra)
    }
}
