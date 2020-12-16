using MediatR;

namespace GADev.BarberPoint.Application.Commands.BarberShop
{
    public class CreateBarberShopCommand : IRequest<Application.Responses.ResponseService>
    {
        public string Name { get; set; }
        public string LogoBase64 { get; set; }
        public int AdminUserId { get; set; }
        public string PublicPlace { get; set; }
        public int? Number { get; set; }
        public string Neighborhood { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}