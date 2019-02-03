using System;
using static System.Console;

namespace Massenger
{
	public static class UserInferface
	{
		static int callback;

		private static int showMenu(string menuName, params string[] menuItems)
		{
			short curItem = 0, i;
			ConsoleKeyInfo key;

			do
			{
				Clear();
				WriteLine(menuName);
				for (i = 0; i < menuItems.Length; i++)
				{
					if (curItem == i)
					{
						Write(">>");
						WriteLine(menuItems[i]);
					}
					else
					{
						WriteLine(menuItems[i]);
					}
				}

				key = ReadKey(true);

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
				else if (key.Key.ToString() == "Escape")
				{
					return -1;
				}

			} while (key.KeyChar != 13);

			Clear();

			return curItem;
		}

		public static void StartPage()
		{
			Title = "Messenger";

			do
			{
				callback = showMenu("Please, log in or register", " Autorization", " Registration", " Exit");

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
				callback = showMenu("MAIN MENU", " Send message", " Add text", " Add recepient", " Remove recepient", " Show my recepients", " Other otions", " Exit");

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
				callback = showMenu("OTHER OPTIONS", " Save recepients list to file", " Brovse recepient from file", " Show messaging story" , " Change password" , " Back to main menu");

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
						BackEndFunctions.ChangePassword();
						break;
					case 4:
						return;
					case -1:
						return;
				}

			}
			while (true);
		}

		public static void RecoverPasswordOrBack()
		{
			do
			{
				callback = showMenu("Invalid phone number or password", " Recover password", " Back to start menu");

				switch (callback)
				{
					case 0:
						BackEndFunctions.RecoverPassword();
						break;
					case 1:
						return;
					case -1:
						return;
				}
			}
			while (!BackEndFunctions.isAutorize);
		}
	}
}
