using IOC.Framework;
using IOCConsoleApplication.Extensions;
using IOCConsoleApplication.Interfaces;
using IOCConsoleApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServiceProvider = IOC.Framework.IServiceProvider;

namespace IOCConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            InitA();
            InitB();
            Console.Read();
        }
        public static void InitA()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient<ITestTransient, TestATransient>();
            collection.AddTransient<ConstructorIOCTest, ConstructorIOCTest>();
            IServiceProvider provider =  collection.BuildServiceProvider();
            provider.GetRequiredService<ConstructorIOCTest>().WriteTestTransient();
        }
        public static void InitB()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient<ITestTransient, TestBTransient>();
            collection.AddTransient<ConstructorIOCTest, ConstructorIOCTest>();
            IServiceProvider provider = collection.BuildServiceProvider();
            provider.GetRequiredService<ConstructorIOCTest>().WriteTestTransient();
        }
    }
    public class ConstructorIOCTest
    {
        private readonly ITestTransient m_test;
        public ConstructorIOCTest(ITestTransient test)
        {
            m_test = test;
        }
        public void WriteTestTransient()
        {
            m_test.Write();
            Console.WriteLine("--------------ConstructorIOCTest-----------");
        }
    }
}
