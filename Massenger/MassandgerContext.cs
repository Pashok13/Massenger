using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Messenger
{
        public class MessandgerContext : DbContext
        {
            public MessandgerContext() : base("DBConnection")
            { }

            public DbSet<User> Users           { get; set; }
            public DbSet<Recepient> Recepients { get; set; }
            public DbSet<Message> Massages     { get; set; }
    }
}
