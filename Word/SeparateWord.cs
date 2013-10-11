/*
 中文分词
 */
using System;
using System.Text;

namespace Word
{
	/// <summary>
	/// Description of SeparateWord.
	/// </summary>
	public class SeparateWord
	{
		private static string Delimiter;
		public static string Separate(ChineseDictionary dict,string text,string delimiter)
		{
			Delimiter=delimiter;
			return Separate(dict,text);
		}
		private static TimeSpan timespan;
		public static string SpanTime
		{
			get
			{
				return timespan.TotalMilliseconds.ToString();
			}
		}
		public static string Separate(ChineseDictionary dict,string text)
		{
			Delimiter="/";
			bool isSeparated=false;
			int separateLength=0;
			StringBuilder sprText=new StringBuilder(text.Length+20);
			DateTime start =DateTime.Now;
			for(int i=0;i<text.Length;)
			{
				isSeparated=false;
				if(text.Length-i<dict.WordMaxLength)
				{
					separateLength=text.Length-i;
				}
				else
				{
					separateLength=dict.WordMaxLength;
				}
				string spr=text.Substring(i,separateLength);
				//如果是数字或者英文
				if(Match.isNumberic(spr[0])||Match.isEnglish(spr[0]))
				{
					int index=1;
					bool isNumber=false;
					while(index<spr.Length&&Match.isNumberic(spr[index]))
					{
						index++;
						isNumber=true;
					}
					while(!isNumber&&index<spr.Length&&Match.isEnglish(spr[index]))
					{
						index++;
					}
					i+=index;
					sprText.Append(spr.Substring(0,index)+Delimiter);
					continue;
				}
				else
				{
					while(spr.Length>1)
					{
						if(dict.Find(spr))
						{
							sprText.Append(spr+Delimiter);
							isSeparated=true;
							i+=spr.Length;
							break;
						}
						else
						{
							spr=spr.Substring(0,spr.Length-1);
						}
					}
					if(!isSeparated)
					{
						sprText.Append(spr[0]+Delimiter);
						i++;
					}
				}
			}
			timespan=DateTime.Now-start;
			return sprText.ToString();
		}
	}
}
