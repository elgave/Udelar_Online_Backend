using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class IdPassword
    {

        public IdPassword() { }
        public IdPassword(string id, string password)
        {
            Id = id;
            Password = password;
        }

        public string Id { get; set; }
        public string Password { get; set; }
    }
}
