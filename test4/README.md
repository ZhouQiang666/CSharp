# 实验四 


## 知识点
- LCS
 - 泛型
 - 运算符重载
 - 二维数组
  -递归调用
 - 使用VS2017中的NuGet安装项目的外部引用。
 - 熟悉Word文件的XML格式。
 - 熟悉XML格式及OpenXmlElement处理，熟悉XML中对象的遍历。
 - 文件读写
 - 目录遍历
## NuGet安装项目的外部引用
点击项目右键》联网查询》搜索openXml 找到对应安装包，点击安装
## OpenXmlElement 类
表示一个基类派生自的 Office Open XML 文档中的所有元素。Elements<T>()枚举仅将具有指定的类型的当前元素的子级
## 实验目的 
- 本实验在上一个实验（实验3）的基础上进行。要求对每个考生的答卷（docx文档）进行自动阅卷并评分，最后把评分写入一个二进制文件中。
- 样例文件在老师的test4目录中。本实验的输入是：原题.docx，标准答案.docx，以及考生答案目录(students_answer)，在考生答案目录中存放的是考生的作答文件，每个考生文件的命名方式是考号_姓名.docx。程序必需能够自动对考生答案目录中的每个文件进行评分。
- 本实验的输出是一个二进制成绩结果文件。比如result.dat，这个文件的中存放了每个考生的个人信息和成绩。本实验必须能够写result.dat文件，并且把文件内容再正确的读出来。

##  实验过程

```
·创建窗体应用程序
·利用LCS算法进行对比
·文件的读写

result.dat文件：把成绩的结果保存为二进制文件的目的是可以防止篡改成绩。


```

## LCS算法的实现
```python
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public enum ITEM_MODE { XY, X, Y }
    public class Item<T>
    {
        public ITEM_MODE Mode;
        public T Value;
        public Item(ITEM_MODE rMODE, T item)
        {
            Mode = rMODE;
            Value = item;
        }
        public override string ToString()
        {
            string mode;
            if (Mode == ITEM_MODE.XY)
                mode = "  ";
            else if (Mode == ITEM_MODE.X)
                mode = "- ";
            else
                mode = "+ ";
            return String.Format("{0}{1}", mode, Value);
        }
    }

    /// <summary>
    /// LCS类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LCS<T>
    {
        private T[] x;
        private T[] y;
        private Item<T>[] items;
        private T[] itemscommon;

        /// <summary>
        /// 第1个数组
        /// </summary>
        public T[] X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                GenerateLCSItems();
            }
        }
        /// <summary>
        /// 第2个数组
        /// </summary>
        public T[] Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                GenerateLCSItems();
            }
        }

        /// <summary>
        /// 比较后的结果数组，是两个集合的比较结果的全集
        /// </summary>
        public Item<T>[] Items { get { return items; } }

        /// <summary>
        /// 比较后的结果数组，是两个集合的最长公共子序列（LCS）
        /// </summary>
        public T[] ItemsCommon { get { return itemscommon; } }
        public LCS(T[] x, T[] y)
        {
            this.x = x;
            this.y = y;
            GenerateLCSItems();
        }

       
        private void GenerateLCSItems()
        {
            //初始化二维数组，数组中的值全为0
            int[,] c = new int[X.Length + 1, Y.Length + 1];

            //循环第i行，从1开始
            for (int i = 1; i < X.Length + 1; i++)
            {
                //循环第j列，从1开始
                for (int j = 1; j < Y.Length + 1; j++)
                {
                    if (X[i - 1].Equals(Y[j - 1]))
                        c[i, j] = c[i - 1, j - 1] + 1;
                    //先上边，后左边，取上边和左边两个数字的最大值，这个顺序必须和下面的GetLCS()函数一致！
                    else if (c[i - 1, j] >= c[i, j - 1])
                        c[i, j] = c[i - 1, j];
                    else
                        c[i, j] = c[i, j - 1];
                }
            }

            int LCSLength = c[X.Length, Y.Length];

            itemscommon = new T[LCSLength];

            items = new Item<T>[X.Length + Y.Length - LCSLength];

            GetLCS(Items, itemscommon, c, X, Y, X.Length, Y.Length);

        }

        /// <summary>
        /// 递归获取LCS字符串
        /// </summary>
        /// <param name="rArray">输出参数</param>
        /// <param name="outLCS"></param>
        /// <param name="c">输入：c是二维表</param>
        /// <param name="x">输入：是原始字符串x</param>
        /// <param name="y">输入：是原始字符串y</param>
        /// <param name="i">输入：左下角的行坐标</param>
        /// <param name="j">输入：左下角的列坐标</param>
        private void GetLCS(Item<T>[] rArray, T[] outLCS, int[,] c, T[] x, T[] y, int i, int j)
        {
            if (i == 0 && j > 0)
            {//只剩下y[]
                while (j > 0)
                {
                    Item<T> r = new Item<T>(ITEM_MODE.Y, y[j - 1]);
                    InsertBefore(rArray, r);
                    j--;
                }
                return;
            }
            else
            if (i > 0 && j == 0)
            {//只剩下x[]
                while (i > 0)
                {
                    Item<T> r = new Item<T>(ITEM_MODE.X, x[i - 1]);
                    InsertBefore(rArray, r);
                    i--;
                }
                return;
            }
            else if (i == 0 && j == 0)
            {
                return;
            }
            if (x[i - 1].Equals(y[j - 1]))
            {
                Item<T> r = new Item<T>(ITEM_MODE.XY, x[i - 1]);
                InsertBefore(rArray, r);
                outLCS[c[i, j] - 1] = x[i - 1];

                GetLCS(rArray, outLCS, c, x, y, i - 1, j - 1);
            }
            //先上边，后左边回溯，必须与GetLCSResult()一致
            else if (c[i - 1, j] >= c[i, j - 1])
            {
                Item<T> r = new Item<T>(ITEM_MODE.X, x[i - 1]);
                InsertBefore(rArray, r);
                GetLCS(rArray, outLCS, c, x, y, i - 1, j);
            }
            else
            {
                Item<T> r = new Item<T>(ITEM_MODE.Y, y[j - 1]);
                InsertBefore(rArray, r);
                GetLCS(rArray, outLCS, c, x, y, i, j - 1);
            }
        }

        /// <summary>
        /// 从后往前插入，将r添加到rArray最后一个不为null的位置中。
        /// </summary>
        /// <param name="rArray"></param>
        /// <param name="r"></param>
        private void InsertBefore(Item<T>[] rArray, Item<T> r)
        {
            int i = 0;
            for (i = 0; i < rArray.Length; i++)
            {
                if (rArray[i] != null)
                    break;
            }
            rArray[i - 1] = r;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Item<T> item in Items)
            {
                stringBuilder.Append(item).AppendLine();
            }
            return stringBuilder.ToString();
        }

        public void Demo()
        {
            Console.WriteLine($"类型{typeof(T)}演示：\n=========================================================");

            Console.WriteLine("list1:");
            foreach (T i in x)
            {
                Console.Write(string.Format("{0}  ", i));
            }
            Console.WriteLine();

            Console.WriteLine("list2:");
            foreach (T i in y)
            {
                Console.Write($"{i}  ");
            }
            Console.WriteLine();

            //输出LCS结果：
            Console.WriteLine("\nLCS结果:");

            //调用this.ToString()
            Console.WriteLine(this);
        }
    }
}


```


### 存储和读取读取文件。

```python
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public class Student
    {
        public string Name;
        public string Number;
        public int Grade;
        public Student(string name,string number, int grade)
        {
            Name = name;
            Number = number;
            Grade = grade;
        }
    }
    class MyFile
    {
        public FileStream F;
        public MyFile(FileStream F)
        {
            this.F = F;
        }
        public void WriteInt(int i)
        {
            byte[] intBuff = BitConverter.GetBytes(i); // 将 int 转换成字节数组      
            F.Write(intBuff, 0, 4);
        }
        public void WriteString(string str)
        {
            byte[] strArray = System.Text.Encoding.Default.GetBytes(str);
            WriteInt(strArray.Length);
            F.Write(strArray, 0, strArray.Length);
        }
        public int ReadInt()
        {
            byte[] intArray = new byte[4];
            F.Read(intArray, 0, 4);
            int iRead = BitConverter.ToInt32(intArray, 0);
            return iRead;
        }
        public string ReadString()
        {
            int len = ReadInt();
            byte[] strArray = new byte[len];
            F.Read(strArray, 0, len);
            string strRead = System.Text.Encoding.Default.GetString(strArray);
            return strRead;
        }
    }
    class GradeFile
    {
        public GradeFile(List<Student> students)
        {
            FileStream F = new FileStream("C:\\Users\\ac\\Desktop\\test4\\TestWindows\\result.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            MyFile MyFile = new MyFile(F);
            string strWrite = "";
          
            foreach (Student student in students)
            {
                //stuStrs.Add(student);
                strWrite += "name:" + student.Name + ",number:" + student.Number + ",grade:" + student.Grade + ";";
            }
            MyFile.WriteString(strWrite);
            
            F.Position = 0;
            string strRead = MyFile.ReadString();
            F.Close();
        }
       
    }
    class ReadGradeFile
    {
        public string[] grades;
        public ReadGradeFile()
        {
            FileStream F = new FileStream("C:\\Users\\ac\\Desktop\\test4\\TestWindows\\result.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            MyFile MyFile = new MyFile(F);
            //考号、学生姓名、分数
            //Student student = new Student(name,number,grade);
            F.Position = 0;
            string strRead = MyFile.ReadString();
            //int intRead = MyFile.ReadInt();
            char[] separator = { ';' };
            grades = strRead.Split(separator);

            F.Close();
        }

    }
}

```

## 运行结果
```
替换题：请将文中所有的文字“国考”替换为“GK”。总分：9分
考试结果：
1001    张三    9
1002    李思思  7
1003    王五    6
```
## 参考

实验1 LCS ，实验2 用Open XML解析Word文件 ，实验3 自动出题程序
