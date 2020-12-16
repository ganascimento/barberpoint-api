using Dapper.FluentMap.Dommel.Mapping;

namespace GADev.BarberPoint.Infrastructure.Mappings
{
    public class AddressMapping : DommelEntityMap<Domain.Entities.Address>
    {
        public AddressMapping()
        {
            ToTable("tb_address");
            Map(p => p.Id).ToColumn("id").IsKey().IsIdentity();
            Map(p => p.PublicPlace).ToColumn("public_place");
            Map(p => p.Number).ToColumn("number");
            Map(p => p.Neighborhood).ToColumn("neighborhood");
            Map(p => p.Locality).ToColumn("locality");
            Map(p => p.State).ToColumn("state");
            Map(p => p.Latitude).ToColumn("latitude");
            Map(p => p.Longitude).ToColumn("longitude");
            Map(p => p.CreateAt).ToColumn("create_at");
            Map(p => p.UpdateAt).ToColumn("update_at");
        }
    }
}