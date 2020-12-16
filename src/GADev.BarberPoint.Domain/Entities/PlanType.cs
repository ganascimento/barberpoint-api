namespace GADev.BarberPoint.Domain.Entities
{
    public class PlanType : Core.BaseEntity
    {
        public string Description { get; set; }
        public short Days { get; set; }

        public PlanType()
        {
            
        }

        public PlanType(int id)
        {
            Id = id;
        }
    }
}