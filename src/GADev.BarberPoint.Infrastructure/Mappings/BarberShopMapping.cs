using Dapper.FluentMap.Dommel.Mapping;

namespace GADev.BarberPoint.Infrastructure.Mappings
{
    public class BarberShopMapping : DommelEntityMap<Domain.Entities.BarberShop>
    {
        public BarberShopMapping()
        {
            ToTable("tb_barbershop");
            Map(p => p.Id).ToColumn("id").IsKey().IsIdentity();
            Map(p => p.Name).ToColumn("name");
            Map(p => p.PathLogo).ToColumn("path_logo");
            Map(p => p.IsActive).ToColumn("is_active");
            Map(p => p.UserAdminId).ToColumn("id_admin_user");
            Map(p => p.AddressId).ToColumn("id_address");
            Map(p => p.BarberShopStatusId).ToColumn("id_barbershop_status");
            Map(p => p.CreateAt).ToColumn("create_at");
            Map(p => p.UpdateAt).ToColumn("update_at");
            
            Map(p => p.UserAdmin).Ignore();
            Map(p => p.BarberShopStatus).Ignore();
            Map(p => p.Address).Ignore();
        }
    }
}