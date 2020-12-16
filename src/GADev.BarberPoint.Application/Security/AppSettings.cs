using System;
using System.Text;

namespace GADev.BarberPoint.Application.Security
{
    public class AppSettings
    {
        public string Secret { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int Expiration { get; set; }

        public AppSettings()
        {
            string key = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            byte[] bytes = Encoding.ASCII.GetBytes(key);
            this.Secret = Convert.ToBase64String(bytes);
        }
    }
}