using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messenger
{

    class Program
    {

		public static MessandgerContext MessendgerDB;
		public static bool isAutorize = false;
		public static Users CurrentUser;

		static void AddUser()
		{
			CurrentUser = new Users();

			Console.WriteLine("Input user data:");

			Console.Write("Name: ");
			CurrentUser.Name = Console.ReadLine();

			do
			{
				Console.Write("Number: ");
				CurrentUser.UserPhone = Console.ReadLine();
				Users alreadyRegisteredUser = MessendgerDB.Users.FirstOrDefault(p => p.UserPhone == CurrentUser.UserPhone);
				if (alreadyRegisteredUser != null)
				{
					Console.Clear();
					Console.WriteLine(" >> Error: This phone number is already register. Press any button to continue <<");
					Console.ReadKey();
					return;
				}
			}
			while (CurrentUser.UserPhone == null);

			do
			{
				Console.Write("E-mail adress: ");
				CurrentUser.Adress = Console.ReadLine();
			}
			while (CurrentUser.Adress == null);

			Console.Write("Password: ");
			CurrentUser.Password = Console.ReadLine();

			Recepients Recepient = new Recepients();
			Recepient.Name = CurrentUser.Name;
			Recepient.Adress = CurrentUser.Adress;

			MessendgerDB.Users.Add(CurrentUser);
			MessendgerDB.Recepients.Add(Recepient);
			MessendgerDB.SaveChanges();

			Console.WriteLine($"User {CurrentUser.Name} are sucesfully added!");
			isAutorize = true;
		}

		static void Autorization()
		{
			string phoneNumber;
			string password;

			do
			{
				Console.Clear();
				Console.Write("Number: ");
				phoneNumber = Console.ReadLine();

				CurrentUser = MessendgerDB.Users.FirstOrDefault(p => p.UserPhone == phoneNumber);

				if (CurrentUser == null)
				{
					Console.WriteLine("Undefined phone number. Press any key to continue");
					Console.ReadKey();
				}
			}
			while (CurrentUser == null);

			Console.Write("Password: ");
			password = Console.ReadLine();

			if (password == CurrentUser.Password)
			{
				isAutorize = true;
			}
			else
			{
				Console.WriteLine(" >> Error: Invalid data. Please, check in or try again. Press any key to continue<<");
				Console.ReadKey();
			}

		}

		static void NewMessege()
		{
			Recepients currentRecepient = new Recepients();
			Recepients recepient;
			Messages message = new Messages();

			do
			{
				Console.Write("Recepient phone: ");
				currentRecepient.RecepientPhone = Console.ReadLine();
			}
			while (currentRecepient == null);

			recepient = MessendgerDB.Recepients.FirstOrDefault(p => p.RecepientPhone == currentRecepient.RecepientPhone);

			if (recepient == null)
			{
				Console.Write("Recepient name (optional) : ");
				currentRecepient.Name = Console.ReadLine();
				MessendgerDB.Recepients.Add(currentRecepient);
				MessendgerDB.SaveChanges();
				recepient = MessendgerDB.Recepients.FirstOrDefault(p => p.RecepientPhone == currentRecepient.RecepientPhone);
				//recepient = MessendgerDB.Recepients.Find(currentRecepient.RecepientPhone);
			}

			Console.WriteLine("Text messege:");
			message.TextMessage = Console.ReadLine();
			message.RecepientId = recepient.RecepientId;
			message.UserId = CurrentUser.UserId;

			MessendgerDB.Massages.Add(message);
			
			// can`t save data in Messages table
			MessendgerDB.SaveChanges(); 
			
			Console.WriteLine("Messege are sended. Press any key to continue");
			Console.ReadKey();
		}

		static int UserInterface(params string[] menuItems)
		{
			short curItem = 0, c;
			ConsoleKeyInfo key;

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

			return curItem;
		}


		static void Main(string[] args)
        {
            MessendgerDB = new MessandgerContext();
			int callback;

			do
			{
				callback = UserInterface(" Autorization", " Registration", "Exit");

				switch (callback)
				{
					case 0 :
						Autorization();
						break;
					case 1 :
						AddUser();
						break;
					case 2:
						return;
				}
			}
			while (!isAutorize);

			do
			{
				callback = UserInterface(" New message", " Exit");

				switch (callback)
				{
					case 0:
						NewMessege();
						break;
					case 1:
						return;
				}
			}
			while (true);
        }
    }
}
