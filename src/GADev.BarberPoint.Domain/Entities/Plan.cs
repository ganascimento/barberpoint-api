namespace GADev.BarberPoint.Domain.Entities
{
    public class Plan : Core.BaseEntity
    {
        public string Name { get; set; }
        public int AmountProfessinal { get; set; }
        public decimal Value { get; set; }
        public int PlanTypeId { get; set; }

        public PlanType PlanType { get; set; }

        public Plan()
        {
            
        }

        public Plan(int id)
        {
            Id = id;
        }
    }
}