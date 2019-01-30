using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Massenger
{
        class MassandgerContext : DbContext
        {
            public MassandgerContext() : base("DBConnection")
            { }

            public DbSet<Users> Users           { get; set; }
            public DbSet<Recepients> Recepients { get; set; }
            public DbSet<Massages> Massages     { get; set; }
    }
}
