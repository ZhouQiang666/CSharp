using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
	class Program
	{
		static void Main(string[] args)
		{
			string word = @"E:\CSharp\科研细则.docx"; //调出文档所在地址储存到word
			using (WordprocessingDocument mydoc = WordprocessingDocument.Open(word, true))//创建一个新的WordprocessingDocument的类
			{
				Body ab = mydoc.MainDocumentPart.Document.Body; //创建文档Body的类
				foreach (var parph in ab.Elements<Paragraph>())//将对象都访问一次
				{
					Console.WriteLine(parph.InnerText);//将对象打印出来
				}
			}
			Console.ReadLine();//显示
		}
	}
}
