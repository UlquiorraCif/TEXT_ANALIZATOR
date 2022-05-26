using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEXT_ANALIZATOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private Dictionary<string, int> frequency = new Dictionary<string, int>();
        private int count = 0;

        public Dictionary<string, int> Method(string[] masSt)
        {
            textBox2.Text = string.Empty;
            Dictionary<string, int> FreqDic = new Dictionary<string, int>();
            foreach (string s in masSt)
                if (FreqDic.ContainsKey(s.ToUpper()))
                    FreqDic[s.ToUpper()]++;
                else
                    FreqDic.Add(s.ToUpper(), 1);
            var stringBuilder = new StringBuilder();
            foreach (var k_v in FreqDic.OrderByDescending(x => x.Value))
            {
                stringBuilder.Append(k_v.Key);
                stringBuilder.Append(" ");
                stringBuilder.Append(k_v.Value);
                stringBuilder.Append(Environment.NewLine);
            }
            textBox2.Text = stringBuilder.ToString();
            return FreqDic;

        }

            private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inpf;
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Текстовые файлы|*.txt";
                od.Title = "Выберите файл с текстом для анализа";
                if (od.ShowDialog() == DialogResult.OK)
                {
                    inpf = od.FileName;
                    SaveFileDialog sv = new SaveFileDialog();
                    sv.Filter = "Текстовые файлы|*.txt";
                    sv.Title = "Выберите файл для сохранения результата";
                    if (sv.ShowDialog() == DialogResult.OK)
                    {
                        od.Title = "Выберите файл программы mystem";
                        od.Filter = "Приложения|*.exe";
                        if (od.ShowDialog() == DialogResult.OK)
                        {
                            System.Diagnostics.Process command = new System.Diagnostics.Process();
                            command.StartInfo.FileName = od.FileName;
                            command.StartInfo.Arguments = inpf + " " + sv.FileName;
                            command.Start();
                            MessageBox.Show("Успешно!!");
                            textBox1.Text = File.ReadAllText(sv.FileName);
                            textBox2.Text = string.Empty;
                            string[] Mas = textBox1.Text.Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);
                            var ItogS = new StringBuilder();
                            var textBox2Text = new StringBuilder();

                            foreach (string s in Mas)
                            {
                                if (s.Contains('|'))
                                {
                                    textBox2Text.Append(s.Remove(s.IndexOf('|')));
                                    textBox2Text.Append(Environment.NewLine);
                                }
                                else
                                {
                                    textBox2Text.Append(s);
                                    textBox2Text.Append(Environment.NewLine);
                                }
                                string[] st = s.Split('{');
                                if (st[1].Contains('|'))
                                {
                                    ItogS.Append(st[1].Remove(st[1].IndexOf('|')));//
                                    ItogS.Append(" ");
                                }
                                else
                                {
                                    ItogS.Append(st[1]);
                                    ItogS.Append(" ");
                                }
                            }
                            textBox1.Text = ItogS.ToString();
                            textBox2.Text = textBox2Text.ToString();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] masST = textBox1.Text.ToString().Split(new char[] { ' ', ',', '.', '!', '?', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> FreqDic = Method(masST);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}

