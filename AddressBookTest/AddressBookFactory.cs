using KsiazkaAdresowa;
using KsiazkaAdresowa.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookTest
{
    public class AddressBookFactory: WebApplicationFactory<Startup>
    {
        public IConfiguration Configuration { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config => 
            { 
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("integrationsettings.json")
                    .Build(); 
                config.AddConfiguration(Configuration); }
            );
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContext<DataContext>(options=> options.UseInMemoryDatabase("AddressBookdb"));
            });
        }
    }
}
