﻿using IOCConsoleApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCConsoleApplication.Services
{
    public class TestBTransient : ITestTransient
    {
        public void Write()
        {
            Console.WriteLine("----------------B----------------");
        }
    }
}
