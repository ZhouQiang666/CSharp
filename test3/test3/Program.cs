using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadWord rw = new ReadWord();
            //测试readWord：
            //rw.readList2();
            //测试LCS：
            //LCS<string> strLCS = new LCS<string>(strList1, strList2);
            //strLCS.Demo();
            //string[] str = rw.readList1().ToArray();
            //string[] str1 = rw.readList2().ToArray();
            
            bool a = true;
            while (a)
            {
                Console.WriteLine("                                              ");
                Console.WriteLine("=================选择1-2-3-4==================");
                Console.WriteLine("**********************************************");
                Console.WriteLine("* 1: 输入国考_原题.docx；国考_标准答案1.docx *");
                Console.WriteLine("* 2: 输入国考_原题.docx；国考_标准答案2.docx *");
                Console.WriteLine("* 3: 输入国考_原题.docx；国考_标准答案3.docx *");
                Console.WriteLine("* 4: 输入国考_原题.docx；国考_原题.docx      *");
                Console.WriteLine("**********************************************");
                Console.WriteLine("input:");
                string s1 = Console.ReadLine();
                if (s1.Equals("1"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList1());
                    strLCS.Demo();
                }
                else if (s1.Equals("2"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList2());
                    strLCS.Demo();
                }
                else if (s1.Equals("3"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList3());
                    strLCS.Demo();
                }
                else if (s1.Equals("4"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList4());
                    strLCS.Demo();
                }
                else
                {
                    Console.WriteLine("Wrong!!!{0}", s1);
                    Console.WriteLine("Input Any Key Out");
                    a = false;

                }

                Console.ReadKey();
            }
            
        }
    }
}
