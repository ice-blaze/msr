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
		if(!str.EndsWith("/"))
		{
			str+="/";
		}
		str+="LevelsHighscore/";
		path = str;
	}

	public static string getHighscoreString(string levelID)
	{
		if(!File.Exists(path+levelID+file))
		{
			setHighscore(DEFAULT_TIME, levelID);
		}
		try
		{
			using (StreamReader sr = new StreamReader(path+levelID+file))
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

	public static float getHighscoreFloat(string levelID)
	{
		return TimerManager.ConvertStringToTime(getHighscoreString(levelID));
	}

	public static void setHighscore(string time,string levelID)
	{
		System.IO.Directory.CreateDirectory(path);
		StreamWriter sw = new StreamWriter(path+levelID+file);
		sw.Write(time);
		sw.Close(); 
	}

	public static void setHighscore(float time, string levelID)
	{
		setHighscore(TimerManager.ConvertTimeToString(time),levelID);
	}
}

