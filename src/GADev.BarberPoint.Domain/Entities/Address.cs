namespace GADev.BarberPoint.Domain.Entities
{
    public class Address : Core.BaseEntity
    {
        public string PublicPlace { get; set; }
        public int? Number { get; set; }
        public string Neighborhood { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Address()
        {
            
        }

        public Address(int id)
        {
            Id = id;
        }
    }
}