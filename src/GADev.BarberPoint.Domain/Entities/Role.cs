namespace GADev.BarberPoint.Domain.Entities
{
    public class Role : Core.BaseEntity
    {
        public string Name { get; set; }

        public Role()
        {
            
        }

        public Role(int id)
        {
            Id = id;
        }
    }
}