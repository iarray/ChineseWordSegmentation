/*
 中文字典
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Word
{
	/// <summary>
	/// Description of Dictionary.
	/// </summary>
	[Serializable]
    public class ChineseDictionary
	{
		private Int32 _wordMaxLength;
		private List<Table> _list;
		public Int32 Count
		{
			get
			{
				return _list.Count;
			}
		}
		public Int32 WordMaxLength
		{
			get
			{
				return _wordMaxLength;
			}
		}
		public ChineseDictionary()
		{
			_list = new List<Table>();
			_list.Add(new Table(2));
			_wordMaxLength=2;
		}
		public void Add(string word)
		{
			if(word.Length<2)
			{
				return;
			}
			while(word.Length>_wordMaxLength)
			{
				_list.Add(new Table(++_wordMaxLength));
			}
			_list[word.Length-2].AddWord(word[0],word.Substring(1));
		}
		public bool Find(string word)
		{
			if(word.Length<2)
			{
				return false;
			}
			else
			{
				return _list[word.Length-2].FindWord(word[0],word.Substring(1));
			}
		}
		public static ChineseDictionary InitializeFormFile(string filePath)
		{
			if(File.Exists(filePath)==false)
			{
				throw new Exception("File does not exist");
			}
			ChineseDictionary dict=new ChineseDictionary();
			StreamReader stream=new StreamReader(filePath,System.Text.Encoding.UTF8);
			while(!stream.EndOfStream)
			{
				dict.Add(stream.ReadLine());
			}
			stream.Close();
			return dict;
		}
	}
}
