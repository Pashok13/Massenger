using Messenger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Massenger
{
	public static class DatabaseFunctions
	{
		public static MessandgerContext MessendgerDB;
		public static bool isAutorize = false;
		public static Users CurrentUser;

		public static void AddUser()
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

			//Recepients Recepient = new Recepients();
			//Recepient.Name = CurrentUser.Name;
			//Recepient.Adress = CurrentUser.Adress;

			MessendgerDB.Users.Add(CurrentUser);
			//MessendgerDB.Recepients.Add(Recepient);
			MessendgerDB.SaveChanges();

			Console.WriteLine($"User {CurrentUser.Name} are sucesfully added!");
			isAutorize = true;
		}

		public static void Autorization()
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
					return;
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
				return;
			}

		}

		public static void SendMessege()
		{
			Recepients currentRecepient = new Recepients();
			Recepients recepient = new Recepients();
			Messages message = new Messages();

			do
			{
				Console.Write("Recepient phone: ");
				currentRecepient.RecepientPhone = Console.ReadLine();
			}
			while (currentRecepient.RecepientPhone == null);

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
			MessendgerDB.SaveChanges();

			Messages[] messageArray = new Messages[1];
			messageArray[0] = message;
			SaveInFileJson("Test1", messageArray);

			Console.WriteLine("Messege are sended. Press any key to continue");
			Console.ReadKey();
		}

		public static void SaveInFileJson<T>(string FileName, T[] data)
		{
			DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));

			using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
			{
				jsonFormatter.WriteObject(fs, data);
			}
		}

		//public static void GetFromFileJson<T>(string FileName)
		//{
		//	DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T));

		//	using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
		//	{
		//		T data = (T)jsonFormatter.ReadObject(fs);
		//		UserId = users[0].UserId;
		//		UserPhone = users[0].UserPhone;
		//		Passeword = users[0].Password;
		//		FullName = users[0].Name;

		//		Console.WriteLine("{0} {1} {2} {3}", UserId, UserPhone, Passeword, FullName);
		//	}
		//}
	}
}
