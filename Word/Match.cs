/*
 匹配类
 */
using System;
using System.Text.RegularExpressions;

namespace Word
{
	/// <summary>
	/// Description of Number.
	/// </summary>
	public static class Match
	{
		public static bool isNumberic(string message)
		{
//	        System.Text.RegularExpressions.Regex rex=
//	        new System.Text.RegularExpressions.Regex(@"^\d+$");
//	        if (rex.IsMatch(message))
//	        {
//	            return true;
//	        }
//	        else
//	            return false;
			int test=0;
			if(int.TryParse(message,out test))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static bool isEnglish(string test)
		{
			Regex regEnglish = new Regex(@"^[a-zA-Z]+$");
			if (regEnglish.IsMatch(test))
            {
                return true;
            }
			else
			{
				return false;
			}
		}
		
		public static bool isNumberic(char test)
		{
			if(test>=48&&test<=57)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public static bool isEnglish(char test)
		{
			if(test>=97&&test<=122||test>=65&&test<=90)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
