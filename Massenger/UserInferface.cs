using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massenger
{
	public static class UserInferface
	{
		static int callback;

		static int ShowMenu(string menuName, params string[] menuItems)
		{
			short curItem = 0, i;
			ConsoleKeyInfo key;

			do
			{
				Console.Clear();
				Console.WriteLine(menuName);
				for (i = 0; i < menuItems.Length; i++)
				{
					if (curItem == i)
					{
						Console.Write(">>");
						Console.WriteLine(menuItems[i]);
					}
					else
					{
						Console.WriteLine(menuItems[i]);
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
				else if (key.Key.ToString() == "Backspace")
				{
					return -1;
				}

			} while (key.KeyChar != 13);

			Console.Clear();

			return curItem;
		}

		public static void StartPage()
		{
			Console.Title = "Messenger";

			do
			{
				callback = ShowMenu("Please, log in or register", " Autorization", " Registration", " Exit");

				switch (callback)
				{
					case 0:
						BackEndFunctions.Autorization();
						break;
					case 1:
						BackEndFunctions.AddUser();
						break;
					case 2:
						return;
					case -1:
						return;
				}
			}
			while (!BackEndFunctions.isAutorize);
		}

		public static void MainMenu()
		{
			do
			{
				callback = ShowMenu("MAIN MENU", " Send message", " Add text", " Add recepient", " Remove recepient", " Show my recepients", " Other otions", " Exit");

				switch (callback)
				{
					case 0:
						BackEndFunctions.SendMessege();
						break;
					case 1:
						BackEndFunctions.AddMessegeText();
						break;
					case 2:
						BackEndFunctions.AddRecepient();
						break;
					case 3:
						BackEndFunctions.RemoveRecepient();
						break;
					case 4:
						BackEndFunctions.ShowAllRecipients();
						break;
					case 5:
						ShowOtherOptions();
						break;
					case 6:
						return;
					case -1:
						return;
				}
			}
			while (true);
		}

		public static void ShowOtherOptions()
		{
			do
			{
				callback = ShowMenu("OTHER OPTIONS", " Save recepients list to file", " Brovse recepient from file", " Show messaging story (beta)" , " Back to main menu");

				switch (callback)
				{
					case 0:
						BackEndFunctions.SaveRecepientsToFile();
						break;
					case 1:
						BackEndFunctions.GetRecepientsFromFile();
						break;
					case 2:
						BackEndFunctions.ShowUserStory();
						break;
					case 3:
						return;
					case -1:
						return;
				}

			}
			while (true);
		}
	}
}
