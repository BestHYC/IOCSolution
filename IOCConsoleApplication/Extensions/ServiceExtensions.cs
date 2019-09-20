using IOC.Framework;
using IOCConsoleApplication.Interfaces;
using IOCConsoleApplication.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCConsoleApplication.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddExtension(this IServiceCollection service)
        {
            service.AddTransient<ITestTransient, TestATransient>();
        }
    }
}
