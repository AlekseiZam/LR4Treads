using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Management;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR4Treads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int maxVal = 3000000;
        long t1, t2;

        public ProcessThreadCollection Threads{ get; }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                listBox1.Items.Add("Количество процессоров = " + item["NumberOfProcessors"]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
               }
         listBox1.Items.Add("Количество ядер = " + coreCount.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                listBox1.Items.Add("Количество логических процессоров = " + item["NumberOfLogicalProcessors"]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int number = Process.GetCurrentProcess().Threads.Count;
            listBox1.Items.Add("Количество потоков = " + number.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(0, maxVal).ToArray();
            long total = 0;
           
            t1 = DateTime.Now.Ticks;
           
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j] + (long)Math.Sqrt(j) + (long)Math.Sin(j);
                return subtotal;
            },
            (x) => Interlocked.Add(ref total, x));

            t2 = DateTime.Now.Ticks;
            listBox1.Items.Add("Сумма = " + total.ToString("N0") + "     тиков - " + (t2 - t1).ToString("# ### ###"));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(0, maxVal).ToArray(); // Массив из последовательных значений
            long total = 0;
            //ДлЯ суммы используем long, не int
            t1 = DateTime.Now.Ticks;
            for (int j = 0; j < nums.Length; j++)
                total += nums[j] + (long)Math.Sqrt(j) + (long)Math.Sin(j);
            t2 = DateTime.Now.Ticks;
            listBox1.Items.Add("Сумма = " + total.ToString("N0") + "     тиков - " + (t2 - t1).ToString("# ### ###"));
        }
    }
}
