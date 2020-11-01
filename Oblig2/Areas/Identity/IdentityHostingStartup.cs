using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Oblig2.Areas.Identity.IdentityHostingStartup))]
namespace Oblig2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}