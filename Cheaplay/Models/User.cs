using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cheaplay.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Index(IsUnique = true)]
        public string Login { get; set; }

        public string Password { get; set; }

        public System.DateTime Birthday { get; set; }

        [Index(IsUnique = true)]
        public string Email { get; set; }

        public int Id { get; set; }

        public List<Subscription> Subscriptions { get; set; }
    }
}
