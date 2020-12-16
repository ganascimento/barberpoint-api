namespace GADev.BarberPoint.Application.Services.Interfaces
{
    public interface IImageService
    {
         bool SaveImage(string base64, string nameFile);
         bool RemoveImage(string nameFile);
         string GetImage(string nameFile);
    }
}