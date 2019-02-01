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
		public static Recepients CurrentRecepient;
		public static Messages Message;
		public static List<Recepients> RecepientsCollection = new List<Recepients>();

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

			//MessendgerDB.Recepients.Add(Recepient);
			MessendgerDB.Users.Add(CurrentUser);
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
			if(Message == null)
			{
				Console.WriteLine("Please, add a text of messege. Press any key to continue");
				Console.ReadKey();
				return;
			}
			else if(RecepientsCollection.Count == 0)
			{
				Console.WriteLine("Please, add a recepients. Press any key to continue");
				Console.ReadKey();
				return;
			}

			foreach (Recepients rep in RecepientsCollection)
			{
				Message.RecepientId = rep.RecepientId;
				Message.UserId = CurrentUser.UserId;

				MessendgerDB.Massages.Add(Message);
				MessendgerDB.SaveChanges();

				Messages[] messageArray = new Messages[1];
				messageArray[0] = Message;
				SaveInFileJson($"Message № {Message.Id}", messageArray);
			}

			Console.WriteLine("Messeges are sended. Press any key to continue");
			Console.ReadKey();
		}

		public static void AddMessegeText()
		{
			Message = new Messages();
			Console.WriteLine("Text messege:");
			Console.Write(Message.TextMessage);
			Message.TextMessage = Console.ReadLine();
		}

		public static void AddRecepient()
		{
			CurrentRecepient = new Recepients();
			Recepients recepient = new Recepients();

			do
			{
				Console.Write("Recepient phone: ");
				CurrentRecepient.RecepientPhone = Console.ReadLine();
			}
			while (CurrentRecepient.RecepientPhone == null);

			recepient = MessendgerDB.Recepients.FirstOrDefault(p => p.RecepientPhone == CurrentRecepient.RecepientPhone);

			if (recepient == null)
			{
				Console.Write("Recepient name (optional) : ");
				CurrentRecepient.Name = Console.ReadLine();
				MessendgerDB.Recepients.Add(CurrentRecepient);
				MessendgerDB.SaveChanges();
			}
			else
			{
				CurrentRecepient = recepient;
			}

			RecepientsCollection.Add(CurrentRecepient);

			Console.WriteLine($"Recepient {CurrentRecepient.Name} are added. Press any key to continue");
			Console.ReadKey();
		}

		public static void RemoveRecepient()
		{
			CurrentRecepient = new Recepients();

			do
			{
				Console.Write("Recepient phone: ");
				CurrentRecepient.RecepientPhone = Console.ReadLine();
			}
			while (CurrentRecepient.RecepientPhone == null);

			CurrentRecepient = RecepientsCollection.Find(x => x.RecepientPhone.Contains(CurrentRecepient.RecepientPhone));
			
			if (CurrentRecepient != null)
			{
				RecepientsCollection.Remove(CurrentRecepient);
				Console.WriteLine($"Recepient {CurrentRecepient.Name} are removed. Press any key to continue");
			}
			else
			{
				Console.WriteLine($"Recepient not found in your collection. Press any key to continue");
			}

			Console.ReadKey();
		}

		public static void ShowAllRecipients()
		{
			if(RecepientsCollection.Count == 0 )
			{
				Console.WriteLine("No such recepients");
			}

			foreach (Recepients rec in RecepientsCollection)
				Console.WriteLine($"{rec.RecepientPhone} - {rec.Name}");

			Console.WriteLine("Press any key to back in main menu");
			Console.ReadKey();
		}

		public static void SaveRecepientsToFile()
		{
			if (RecepientsCollection.Count == 0)
			{
				Console.WriteLine("No such recepients");
			}
			
			Recepients[] recepientsArray = new Recepients[RecepientsCollection.Count];
			string fileName;
			int i = 0;

			do
			{
				Console.Write("Enter file name: ");
				fileName = Console.ReadLine();
			}
			while (fileName == null);

			foreach (Recepients rec in RecepientsCollection)
			{			
				recepientsArray[i] = rec;
				i++;
			}
			SaveInFileJson(fileName, recepientsArray);

			Console.WriteLine($"Recepients list are saved in file: {fileName}");
			Console.WriteLine("Press any key to back in main menu");
			Console.ReadKey();
		}

		public static void GetRecepientsFromFile()
		{
			Recepients[] recepientsArray;

			Console.Write("Enter file name : ");
			string fileName = Console.ReadLine();

			try
			{
				recepientsArray = GetFromFileJson(fileName);
			}
			catch(Exception IvalidFilePath)
			{
				Console.WriteLine("Invalid file name. Press any key to continue");
				Console.ReadKey();
				return;
			}

			if (recepientsArray != null)
			{
				RecepientsCollection = recepientsArray.ToList();
				Console.WriteLine("Recepients are successfully download. Press any key to continue");
			}

			Console.ReadKey();
		}

		static void SaveInFileJson<T>(string FileName, T[] data)
		{
			DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));

			using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
			{
				jsonFormatter.WriteObject(fs, data);
			}
		}

		static Recepients[] GetFromFileJson(string FileName)
		{
			Recepients[] rec;

			DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Recepients[]));
			using (FileStream fs = new FileStream(FileName, FileMode.Open))
			{
				try
				{
					rec = (Recepients[])jsonFormatter.ReadObject(fs);
				}
				catch(Exception InvalidFileData)
				{
					Console.WriteLine("Invalid file data. Press any key to continue");
					return null;
				}
			}

			return rec;
		}
	}
}
