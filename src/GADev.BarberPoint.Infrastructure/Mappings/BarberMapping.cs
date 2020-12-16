using Dapper.FluentMap.Dommel.Mapping;

namespace GADev.BarberPoint.Infrastructure.Mappings
{
    public class BarberMapping : DommelEntityMap<Domain.Entities.Barber>
    {
        public BarberMapping()
        {
            ToTable("tb_barber");
            Map(p => p.Id).ToColumn("id").IsKey().IsIdentity();
            Map(p => p.Name).ToColumn("name");
            Map(p => p.TimeStartWork).ToColumn("time_start_work");
            Map(p => p.TimeFinishWork).ToColumn("time_finish_work");
            Map(p => p.BarberShopId).ToColumn("id_barbershop");
            Map(p => p.UserId).ToColumn("id_user");
            Map(p => p.CreateAt).ToColumn("create_at");
            Map(p => p.UpdateAt).ToColumn("update_at");

            Map(p => p.User).Ignore();
            Map(p => p.BarberShop).Ignore();
            Map(p => p.Services).Ignore();
        }
    }
}