using System;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

	public class HighscoreManager
	{
		public static string path = "";
		public static string file = "highscore.txt";
		public const string DEFAULT_TIME = "77:77.777";

		public static void SetPath(string str)
		{
			path = str;
		}

		public static string getHighscoreString()
		{
			if(!File.Exists(path+file))
			{
				setHighscore(DEFAULT_TIME);
			}
			try
			{
				using (StreamReader sr = new StreamReader(path+file))
				{
					return sr.ReadLine();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		return DEFAULT_TIME;
		}

		public static float getHighscoreFloat()
		{
			return TimerManager.ConvertStringToTime(getHighscoreString());
		}

		public static void setHighscore(string time)
		{
			System.IO.Directory.CreateDirectory(path);
			StreamWriter sw = new StreamWriter(path+file);
			sw.Write(time);
			sw.Close(); 
		}

		public static void setHighscore(float time)
		{
			setHighscore(TimerManager.ConvertTimeToString(time));
		}
	}

