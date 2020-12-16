using MediatR;

namespace GADev.BarberPoint.Application.Commands.BarberShop
{
    public class ChangeBarberShopCommand : IRequest<Application.Responses.ResponseService>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoBase64 { get; set; }
        public bool IsActive { get; set; }
        public int AdminUserId { get; set; }
        public string PublicPlace { get; set; }
        public int? Number { get; set; }
        public string Neighborhood { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsRemovedLogo { get; set; }
    }
}