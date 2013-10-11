/*
 * 查重类
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Word
{
	/// <summary>
	/// Description of Statistics.
	/// </summary>
	public static class Statistics
	{
		private static string _exceptKeys="的在了和是一年为有不上月日这与他她它也等个就地那都着来已并以其该则用还把而能后时向又使下出于但由之更被";
		/// <summary>
		/// 对已分词文本进行词频统计，获取词频字典
		/// </summary>
		/// <param name="text">已分词的文本</param>
		/// <returns></returns>
		public static Dictionary<string,int> GetWordFrequency(string text)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Regex regex = new Regex(@"[\u4e00-\u9fa5]+");
            MatchCollection results = regex.Matches(text);
            int temp;
            foreach(System.Text.RegularExpressions.Match word in results)
            {
            	if(word.Length==1&&_exceptKeys.IndexOf(word.Value)>=0)continue;
                if (dictionary.TryGetValue(word.Value, out temp))
                {
                    temp++;
                    dictionary.Remove(word.Value);
                    dictionary.Add(word.Value, temp);
                }
                else
                {
                    dictionary.Add(word.Value, 1);
                }
            }
            return dictionary;
		}
		
		/// <summary>
		/// 对未分词文本进行分词并进行词频统计，获取词频字典
		/// </summary>
		/// <param name="text">待分词,统计的文本</param>
		/// <param name="dict">分词依据词典</param>
		/// <returns></returns>
		public static Dictionary<string,int> GetWordFrequency(string text,ChineseDictionary dict)
		{
			string sprtext = SeparateWord.Separate(dict,text);
			return GetWordFrequency(sprtext);
		}
		
		/// <summary>
        /// 两文档相似度计算
        /// </summary>
        /// <param name="text1">第一篇文档的词频词典</param>
        /// <param name="text2">第二篇文档的词频词典</param>
        /// <returns>两篇文档的相似度</returns>
        public static double Similarity(Dictionary<string, int> text1, Dictionary<string, int> text2)
        {
            double similarity = 0.0, numerator = 0.0, denominator1 = 0.0, denominator2 = 0.0;
            int temp1, temp2;
            Dictionary<string, int> dictionary1 = new Dictionary<string,int>( text1);
            Dictionary<string, int> dictionary2 = new Dictionary<string,int>( text2);
            if ((dictionary1.Count < 1) || (dictionary2.Count < 1))//如果任一篇文章中不含有汉字
            {
                return 0.0;
            }
            Dictionary<string, int>.KeyCollection keys1 = dictionary1.Keys;
            foreach (string key in keys1)
            {
            	
                dictionary1.TryGetValue(key, out temp1);
                if (!dictionary2.TryGetValue(key, out temp2))
                {
                    temp2 = 0;
                }
                dictionary2.Remove(key);
                numerator += temp1 * temp2;
                denominator1 += temp1 * temp1;
                denominator2 += temp2 * temp2;
            }
            Dictionary<string, int>.KeyCollection keys2 = dictionary2.Keys;
            foreach (string key in keys2)
            {
                dictionary2.TryGetValue(key, out temp2);
                denominator2 += temp2 * temp2;
            }
            similarity = numerator / (Math.Sqrt(denominator1 * denominator2));
            return similarity;
        }
	}
}
