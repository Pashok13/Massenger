using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massenger
{
    class Program
    {
        static void Main(string[] args)
        {
            MassandgerContext MassandgerDB = new MassandgerContext();

            Users User = new Users();
            User.PhoneId = 38;

            MassandgerDB.Users.Add(User);
            MassandgerDB.SaveChanges();

            Console.ReadKey();


        }
    }
}
