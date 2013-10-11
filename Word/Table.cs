/*
 词典目录
 */
using System;
using System.Collections.Generic;

namespace Word
{
	/// <summary>
	/// Dictionary of Table.
	/// </summary>
	[Serializable]
	public class Table
	{
		private List<char> _key;
		private List<List<string>> _value;
		private Int32 _lengthOfWord;
		public Int32 Count
		{
			get
			{
				return _key.Count;
			}
		}
		public Table(Int32 wordlength)
		{
			_lengthOfWord=wordlength;
			_value=new List<List<string>>();
			_key=new List<char>();
		}
		public void AddWord(char key,string value)
		{
			if(value.Length+1>_lengthOfWord)return;
			int find=_key.IndexOf(key);
			if(find==-1)
			{
				this._key.Add(key);
				List<string> str=new List<string>();
				str.Add(value);
				this._value.Add(str);
			}
			else
			{
				this._value[find].Add(value);
			}
		}
		public bool FindWord(char key,string value)
		{
			int index=_key.IndexOf(key);
			if(index==-1)
			{
				return false;
			}
			else
			{
				return _value[index].Contains(value);
			}
		}
	}
}
