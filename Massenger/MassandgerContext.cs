﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Messenger
{
        class MessandgerContext : DbContext
        {
            public MessandgerContext() : base("DBConnection")
            { }

            public DbSet<Users> Users           { get; set; }
            public DbSet<Recepients> Recepients { get; set; }
            public DbSet<Messages> Massages     { get; set; }
    }
}
