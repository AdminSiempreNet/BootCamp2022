using System;
using System.Collections.Generic;

#nullable disable

namespace TIENDA.Data.DataBaseFirst.Entities
{
    public partial class User
    {
        public User()
        {
            Billings = new HashSet<Billing>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RecoverPasswordCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }
    }
}
