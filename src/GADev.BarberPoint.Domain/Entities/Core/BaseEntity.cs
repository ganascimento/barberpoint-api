using System;

namespace GADev.BarberPoint.Domain.Entities.Core
{
    public class BaseEntity
    {
        public int? Id { get; set; }

        private DateTime? _createAt;
        public DateTime? CreateAt
        {
            get { return _createAt == null ? DateTime.UtcNow : _createAt; }
            set { _createAt = value; }
        }        
        
        public DateTime? UpdateAt { get; set; }

        public void SetUpdateDate() {
            this.UpdateAt = DateTime.UtcNow;
        }
    }
}