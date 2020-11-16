using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class IdPasswordModel
    {

        public IdPasswordModel() { }
        public IdPasswordModel(string id, string password)
        {
            Id = id;
            Password = password;
        }
        public string Id { get; set; }
        public string Password { get; set; }
    }
}
