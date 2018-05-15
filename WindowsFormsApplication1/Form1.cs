using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Work();
        }


        /// <summary>
        /// 正态分布随机数
        /// </summary>
        //const int N = 100;
        //const int MAX = 50;
        //const double MIN = 0.1;
        //const int MIU = 40;
        //const int SIGMA = 1;
        static Random aa = new Random(unchecked((int)(DateTime.Now.Ticks)));
        public double AverageRandom(double min, double max)//产生(min,max)之间均匀分布的随机数
        {
            int MINnteger = (int)(min * 10000);
            int MAXnteger = (int)(max * 10000);
            int resultInteger = aa.Next(MINnteger, MAXnteger);
            return resultInteger / 10000.0;
        }
        public double Normal(double x, double miu, double sigma) //正态分布概率密度函数
        {
            //return 1.0 / (x * Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (Math.Log(x) - miu) * (Math.Log(x) - miu) / (2 * sigma * sigma));
            return 1.0 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (x - miu) * (x - miu) / (2 * sigma * sigma));
        }
        public double Random_Normal(double miu, double sigma, double min, double max)//产生正态分布随机数
        {
            double x;
            double dScope;
            double y;
            do
            {
                x = AverageRandom(min, max);//最大最小间的均匀随机数
                y = Normal(x, miu, sigma);//x的密度
                dScope = AverageRandom(0, Normal(miu, miu, sigma));//0到平均值密度的均匀随机数
            } while (dScope > y);
            return x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Work();
        }

        private void Work()
        {
            DateTime startTime = DateTime.Now;
            int count = 1000000;//模拟值个数
            double u = 0;//模拟平均值
            double s = 3.2;//模拟标准差
            double min = -50;//模拟最小值
            double max = 51;//模拟最大值
            //设置坐标轴名称
            //chart1.ChartAreas["ChartArea1"].AxisX.Title = "数值";
            //chart1.ChartAreas["ChartArea1"].AxisY.Title = "个数";

            int[] countArr = new int[(int)(max) - (int)(min) + 1];
            //添加随机数
            Random rd = new Random();
            for (int i = 1; i < count; i++)
            {
                double ran = Random_Normal(u, s, min, max);
                int ranInt = (int)(ran - min);//模拟值
                //GaussianRNG grng = new GaussianRNG();
                //int ranInt = (int)(grng.Next());//模拟值
                countArr[ranInt]++;
            }
            double sum = 0.0;
            for (int i = 1; i < countArr.Length; i++)
            {
                chart1.Series["数值"].Points.AddXY(i + min, countArr[i]);
                if ((i + min) >= 5)
                {
                    sum += (i + min) * 200 * countArr[i];
                }
            }
            //int countS = 0;
            //for (int i=90;i<110; i++)
            //{
            //    countS += countArr[i];
            //}
            //MessageBox.Show(countArr[0].ToString());
            //MessageBox.Show(sum.ToString());

            TimeSpan costTime = DateTime.Now - startTime;
            this.Text = "耗时 " + costTime.TotalSeconds.ToString() + " 秒 sum=" + sum.ToString();
        }
    }
}
