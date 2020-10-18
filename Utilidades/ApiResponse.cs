using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilidades
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public int Status { get; set; } = 200;
        public string Message { get; set; } = null;
    }
}
