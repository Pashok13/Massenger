using Messenger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace Massenger
{
	public static class BackEndFunctions
	{
		public static MessandgerContext MessendgerDB;
		public static bool isAutorize = false;
		static User CurrentUser;
		static Recepient CurrentRecepient;
		static List<Recepient> RecepientsCollection = new List<Recepient>();
		static string MessageText;

		public static void AddUser()
		{
			CurrentUser = new User();

			Console.WriteLine("Input user data:");
			Console.Write("Name: ");
			CurrentUser.Name = Console.ReadLine();

			do
			{
				Console.Write("Number: ");
				CurrentUser.UserPhone = Console.ReadLine();

				User alreadyRegisteredUser = MessendgerDB.Users.FirstOrDefault(p => p.UserPhone == CurrentUser.UserPhone);
				
				if (alreadyRegisteredUser != null)
				{
					Console.Clear();
					Console.WriteLine("This phone number is already register. Press any button to continue");
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

				do
				{
					Console.Write("Number: ");
					phoneNumber = Console.ReadLine();

					if (!IsValidPhone(phoneNumber))
						Console.WriteLine("Incorrect format. Input number in format +xxxxxxxxxxx");
					else
						break;
				}
				while (true);

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
				UserInferface.RecoverPasswordOrBack();
				return;
			}

		}

		public static void SendMessege()
		{
			int i = 0;
			Message Message = new Message();

			if (MessageText == "")
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

			Message[] messageArray = new Message[RecepientsCollection.Count];

			foreach (Recepient rep in RecepientsCollection)
			{
				Message = new Message();
				Message.RecepientId = rep.RecepientId;
				Message.UserId = CurrentUser.UserId;
				Message.TextMessage = MessageText;
				MessendgerDB.Massages.Add(Message);		
				messageArray[i] = Message;
				i++;
			}
			MessendgerDB.SaveChanges();

			if (RecepientsCollection.Count == 1)
				SaveInFileJson($"Message № {Message.Id}", messageArray);
			else
				SaveInFileJson($"Message № {Message.Id - RecepientsCollection.Count + 1} - {Message.Id}", messageArray);

			Console.WriteLine("Messeges are sended. Press any key to continue");
			Console.ReadKey();
		}

		public static void AddMessegeText()
		{
			Console.WriteLine("Text messege:");
			MessageText = Console.ReadLine();
		}

		public static void AddRecepient()
		{
			CurrentRecepient = new Recepient();
			Recepient recepient = new Recepient();

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
			CurrentRecepient = new Recepient();

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

			foreach (Recepient rec in RecepientsCollection)
				Console.WriteLine($"{rec.RecepientPhone} - {rec.Name}");

			Console.WriteLine("Press any key to back in main menu");
			Console.ReadKey();
		}

		public static void SaveRecepientsToFile()
		{
			if (RecepientsCollection.Count == 0)
			{
				Console.WriteLine("No such recepients. Press any key to continue");
				Console.ReadKey();
				return;
			}
			
			Recepient[] recepientsArray = new Recepient[RecepientsCollection.Count];
			string fileName;
			int i = 0;

			do
			{
				Console.Write("Enter file name: ");
				fileName = Console.ReadLine();
			}
			while (fileName == null);

			foreach (Recepient rec in RecepientsCollection)
			{			
				recepientsArray[i] = rec;
				i++;
			}
			SaveInFileJson(fileName, recepientsArray);

			Console.WriteLine($"Recepients list are saved in file: {fileName}");
			Console.WriteLine("Press any key to back in menu");
			Console.ReadKey();
		}

		public static void GetRecepientsFromFile()
		{
			Recepient[] recepientsArray;

			Console.Write("Enter file name : ");
			string fileName = Console.ReadLine();

			try
			{
				recepientsArray = GetFromFileJson(fileName);
			}
			catch(FileNotFoundException)
			{
				Console.WriteLine("Invalid file name. Press any key to continue");
				Console.ReadKey();
				return;
			}

			if (recepientsArray != null)
			{
				RecepientsCollection = recepientsArray.ToList();

				if (AddUnknownRecepientsToDataBase(RecepientsCollection))
				{
					Console.WriteLine("Recepients are successfully download. Press any key to continue");
				}
			}

			Console.ReadKey();
		}

		static bool AddUnknownRecepientsToDataBase(List<Recepient> recepientsList)
		{
			foreach (Recepient res in RecepientsCollection)
			{
				Recepient notedRecepient;

				try
				{
					notedRecepient = MessendgerDB.Recepients.FirstOrDefault(p => p.RecepientPhone == res.RecepientPhone);

					if (notedRecepient == null)
					{
						MessendgerDB.Recepients.Add(res);
					}
				}
				catch (Exception)
				{
					Console.WriteLine("Incomplete important data in JSON file. Press any key to continue");
					Console.ReadKey();
					return false;
				}
			}

			MessendgerDB.SaveChanges();
			return true;
		}

		static void SaveInFileJson<T>(string FileName, T[] data)
		{
			DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));

			using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
			{
				jsonFormatter.WriteObject(fs, data);
			}
		}

		static Recepient[] GetFromFileJson(string FileName)
		{
			Recepient[] recArray;

			DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Recepient[]));
			using (FileStream fs = new FileStream(FileName, FileMode.Open))
			{
				try
				{
					recArray = (Recepient[])jsonFormatter.ReadObject(fs);
				}
				catch(Exception)
				{
					Console.WriteLine("Invalid file data. Press any key to continue");
					return null;
				}
			}

			return recArray;
		}

		public static void ShowUserStory()
		{
			//var messages = MessendgerDB.Massages.Join
			//(MessendgerDB.Recepients, m => m.RecepientId, r => r.RecepientId, (m, c) => new
			//{
			//	c.Name,
			//	m.TextMessage,
			//	m.DateOfSend,
			//	m.TimeOfSend,
			//});

			var messages =	from mes in MessendgerDB.Massages
							join rec in MessendgerDB.Recepients on mes.RecepientId equals rec.RecepientId
							where mes.User.UserId == CurrentUser.UserId
							select new { rec.Name, mes.TextMessage, mes.DateOfSend, mes.TimeOfSend };

			foreach (var message in messages)
			{
				Console.WriteLine($"To:   {message.Name}");
				Console.WriteLine($"Date: {message.DateOfSend.ToShortDateString()}");
				Console.WriteLine($"Time: {message.TimeOfSend.Hours}:{message.TimeOfSend.Minutes}:{message.TimeOfSend.Seconds}");
				Console.WriteLine("Text:");
				Console.WriteLine("");
				Console.WriteLine(message.TextMessage);
				Console.WriteLine("");
				Console.WriteLine(new string('*', 25));
				Console.WriteLine("");
			}

			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
		}

		public static void RecoverPassword()
		{
			string phone;

			do
			{
				Console.Write("Phone number: ");
				phone = Console.ReadLine();

				if (IsValidPhone(phone))
					break;
				else
					Console.WriteLine("Incorrect e-mail adress");
			}
			while (true);

			CurrentUser = MessendgerDB.Users.FirstOrDefault(p => p.UserPhone == phone);

			if (CurrentUser != null)
			{
				ChangePassword();
			}
			else
			{
				Console.WriteLine("User with this number not found");
			}
		}

		public static void ChangePassword()
		{
			string email;

			do
			{
				Console.Write("Your e-mail: ");
				email = Console.ReadLine();

				if (IsValidEmail(email))
					break;
				else
					Console.WriteLine("Incorrect e-mail adress");
			}
			while (true);

			if(email == CurrentUser.Adress)
			{
				Console.Write("New password: ");
				CurrentUser.Password = Console.ReadLine();
				MessendgerDB.SaveChanges();
				Console.WriteLine("Password are sucesfully changed.Press any key to continue");
				isAutorize = true;
			}
			else
			{
				Console.WriteLine("This address is not attached to this phone number");
			}

			Console.ReadKey();

		}

		public static bool IsValidEmail(string email)
		{
			Regex mailTemplate = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

			if (email != null && mailTemplate.IsMatch(email))
			{
				return true;
			}

			return false;
		}

		public static bool IsValidPhone(string number)
		{
			Regex numberTemplate = new Regex(@"^\+\d{12}");

			if (number != null && numberTemplate.IsMatch(number))
			{
				return true;
			}

			return false;
		}
	}
}
