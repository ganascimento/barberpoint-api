using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace GADev.BarberPoint.Infrastructure
{
    public static class RegisterMappings
    {
        public static void Register()
        {
            if (FluentMapper.EntityMaps.IsEmpty) {
                FluentMapper.Initialize(config =>
                {
                    config.AddMap(new Mappings.BarberShopMapping());
                    config.AddMap(new Mappings.AddressMapping());
                    config.AddMap(new Mappings.BarberMapping());
                    config.ForDommel();
                });
            }
        }
    }
}