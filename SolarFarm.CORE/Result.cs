using System;
using System.Collections.Generic;
using System.Text;

namespace SolarFarm.CORE
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
