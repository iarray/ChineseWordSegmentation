using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Microsoft.Win32;
using Word;

namespace 中文分词
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
    	private ChineseDictionary chinesedict;
        public MainWindow()
        {
            InitializeComponent();
        }

        void btnSeparate_Click(object sender, RoutedEventArgs e)
		{
			string spr=SeparateWord.Separate(chinesedict,tbText1.Text.Trim());
			tbText1.Text=spr;
			tbTime.Text=SeparateWord.SpanTime;
			
			string spr2=SeparateWord.Separate(chinesedict,tbText2.Text.Trim());
			tbText2.Text=spr2;
		}
		
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DateTime start=DateTime.Now;
			if(File.Exists(@"Default.dict"))
			{
			    using(FileStream fs = new FileStream(@"Default.dict", FileMode.Open))
			    {
			      BinaryFormatter formatter = new BinaryFormatter();
			      chinesedict = (ChineseDictionary)formatter.Deserialize(fs);//
			    }
			}
			TimeSpan ts=DateTime.Now-start;
			tbTime.Text=ts.TotalMilliseconds.ToString();
		}
		
		void Window_Closing(object sender, EventArgs e)
		{
			string filePath="Default.dict";
			if(File.Exists(filePath)==false)
			{
				FileStream fs = new FileStream(filePath, FileMode.Create);
	      		BinaryFormatter formatter = new BinaryFormatter();
	      		formatter.Serialize(fs, chinesedict);
	      		fs.Close();
			}
			
		}

        void btnSmiliarity_Click(object sender, RoutedEventArgs e)
		{
			Dictionary<string,int> dc1 = Statistics.GetWordFrequency(tbText1.Text);
			Dictionary<string,int> dc2 = Statistics.GetWordFrequency(tbText2.Text);
			tbSmiler.Text=Statistics.Similarity(dc1,dc2).ToString("0.0000");
		}

        private void btnOpenDict_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt";
            if (ofd.ShowDialog()==true)
            {
                chinesedict = ChineseDictionary.InitializeFormFile(ofd.FileName);
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"Default.dict", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, chinesedict);
                fs.Close();
            }
        }
    }
}
