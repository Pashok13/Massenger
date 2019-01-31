using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{

    class Program
    {

		public static MessandgerContext MessendgerDB;

		static void AddUser()
		{
			Users User = new Users();

			Console.WriteLine("Input user data:");

			Console.Write("Name: ");
			User.Name = Console.ReadLine();

			do
			{
				Console.Write("Number: ");
				User.UserPhone = Console.ReadLine();
				Users alreadyRegisteredUser = MessendgerDB.Users.FirstOrDefault(p => p.UserPhone == User.UserPhone);
				if (alreadyRegisteredUser != null)
				{
					Console.WriteLine("This phone number is already register");
					return;
				}
			}
			while (User.UserPhone == null);

			do
			{
				Console.Write("E-mail adress: ");
				User.Adress = Console.ReadLine();

			}
			while (User.Adress == null);

			Console.Write("Password: ");
			User.Password = Console.ReadLine();

			MessendgerDB.Users.Add(User);
			MessendgerDB.SaveChanges();

			Console.WriteLine($"User {User.Name} are added!");

		}

		static void LogIn()
		{
			Console.WriteLine("You are login!");
		}

		// remake for params
		static void StartPage()
		{
			short curItem = 0, c;
			ConsoleKeyInfo key;

			string[] menuItems = { " Autorization", " Registration" };

			do
			{
				Console.Clear();
				Console.WriteLine("Select an option . . .");
				for (c = 0; c < menuItems.Length; c++)
				{
					if (curItem == c)
					{
						Console.Write(">>");
						Console.WriteLine(menuItems[c]);
					}
					else
					{
						Console.WriteLine(menuItems[c]);
					}
				}

				key = Console.ReadKey(true);

				if (key.Key.ToString() == "DownArrow")
				{
					curItem++;
					if (curItem > menuItems.Length - 1) curItem = 0;
				}
				else if (key.Key.ToString() == "UpArrow")
				{
					curItem--;
					if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
				}
			} while (key.KeyChar != 13);

			Console.Clear();

			if (curItem == 0)
				LogIn();
			else if (curItem == 1)
				AddUser();
		}


		static void Main(string[] args)
        {
            MessendgerDB = new MessandgerContext();

			StartPage();

			Console.ReadKey();
        }
    }
}
