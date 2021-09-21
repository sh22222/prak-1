﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace prak
{
    public partial class Form3 : Form
    {
        bool pr1 = false;
        Button bt1;
        TextBox tb1;
        Label lb1;
        DataGridView dataGridView1;
        DateTime dt;
        string dateFormat = "dd.MM.yyyy";
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;
        public Form3()
        {
            InitializeComponent();

            this.Width = 100;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;//фиксированное окно
            this.MaximizeBox = false;
            this.Text = "Поиск по дате";
            //создайем label, textbox, button
            lb1 = new Label();
            lb1.Text = "Введите дату\n(dd.MM.yyyy)";
            lb1.Location = new Point(10, 5);
            lb1.Height = 30;
            lb1.TextAlign = ContentAlignment.TopCenter;
            this.Controls.Add(lb1);

            tb1 = new TextBox();
            tb1.Location = new Point(10, 40);
            tb1.Width = 100;
            tb1.Height = 20;
            this.Controls.Add(tb1);

            bt1 = new Button();
            bt1.Location = new Point(10, 70);
            bt1.Width = 100;
            bt1.Height = 30;
            bt1.Text = "Выполнить";
            bt1.Click += new EventHandler(this.click_b);//событие нажатия кнопки
            this.Controls.Add(bt1);
        }
        
        private void click_b(object sender, EventArgs ev)
        {
            string d = tb1.Text;//считываем введенное
            dt = new DateTime();
            if (DateTime.TryParseExact(d, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))//проверка на ввод именно даты
            {
            //очищаем форму и переопределяем параметры
                this.Controls.Clear();
                this.Width = 500;
                this.Height = 300;
                this.MaximizeBox = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                //создаем колонки
                dataGridView1 = new DataGridView();
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.RowHeadersVisible = false;

                var dataGridViewColumn1 = new DataGridViewColumn();
                dataGridViewColumn1.HeaderText = "Код договора";
                dataGridViewColumn1.Name = "codeD";
                dataGridViewColumn1.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(dataGridViewColumn1);

                var dataGridViewColumn2 = new DataGridViewColumn();
                dataGridViewColumn2.HeaderText = "Дата поставки";
                dataGridViewColumn2.Name = "date";
                dataGridViewColumn2.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(dataGridViewColumn2);

                var dataGridViewColumn3 = new DataGridViewColumn();
                dataGridViewColumn3.HeaderText = "Поставщик";
                dataGridViewColumn3.Name = "supplier";
                dataGridViewColumn3.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(dataGridViewColumn3);

                var dataGridViewColumn4 = new DataGridViewColumn();
                dataGridViewColumn4.HeaderText = "Телефон";
                dataGridViewColumn4.Name = "phone";
                dataGridViewColumn4.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(dataGridViewColumn4);

                var dataGridViewColumn5 = new DataGridViewColumn();
                dataGridViewColumn5.HeaderText = "Препарат";
                dataGridViewColumn5.Name = "prep";
                dataGridViewColumn5.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(dataGridViewColumn5);

                var dataGridViewColumn6 = new DataGridViewColumn();
                dataGridViewColumn6.HeaderText = "Количество";
                dataGridViewColumn6.Name = "quant";
                dataGridViewColumn6.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(dataGridViewColumn6);
                //заполняем таблицу
                try
                {
                    StreamReader streamReader = new StreamReader(@"C:\Users\shage\Desktop\c#\prak\files\zak.txt", Encoding.UTF8);
                    string str;
                    dt = DateTime.ParseExact(d, dateFormat, cultureInfo);
                    int row = 0;

                    while ((str = streamReader.ReadLine()) != null)
                    {
                        string[] strN = str.Split(';');
                        DateTime dt2 = new DateTime();
                        dt2 = DateTime.ParseExact(strN[1], dateFormat, cultureInfo);//перевод в формат даты
                        if (dt.CompareTo(dt2) == 0)//проверка на равенство дат
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[row].Cells["codeD"].Value = strN[0];
                            dataGridView1.Rows[row].Cells["date"].Value = strN[1];
                            dataGridView1.Rows[row].Cells["supplier"].Value = strN[2];
                            dataGridView1.Rows[row].Cells["phone"].Value = strN[3];
                            dataGridView1.Rows[row].Cells["prep"].Value = strN[4];
                            dataGridView1.Rows[row].Cells["quant"].Value = strN[5];
                            ++row;
                        }
                        else if (dt.CompareTo(dt2) > 0)//если введенная дата произошла позже, чем считанная - смысла в проверке нет
                        {
                            break;
                        }
                    }
                    streamReader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }


                this.Controls.Add(dataGridView1);
            }
            else//если введена не дата - очищаем textbox
            {
                tb1.Text = "";
            }
        }
    }
}
