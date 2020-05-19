using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.IO;
using MetroFramework.Forms;

namespace ljm4
{


    public partial class Form1 : MetroForm
    {
        private static string mysql_str = "server=l.bsks.ac.kr;Database=p201606023;Uid=p201606023;Pwd=pp201606023;Charset=utf8";
        MySqlConnection conn = new MySqlConnection(mysql_str);

        MySqlCommand cmd;
        MySqlDataReader reader;

        public string level="";
        
        
        public Form1()
        {
            
            while (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요");
                return;
            }

            String sql1 = "select * from Admin where ADMIN_ID= '" + textBox1.Text + "'";
            

            cmd = new MySqlCommand(sql1, conn);

            
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String sql2 = "select * from Admin where ADMIN_PASS = '" + textBox2.Text + "' and ADMIN_ID = '" +textBox1.Text + "'";
                cmd = new MySqlCommand(sql2, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    string sql3 = "select ADMIN_LEVEL from Admin where ADMIN_ID = '" + textBox1.Text + "'";
                    cmd = new MySqlCommand(sql3, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Lv.Text = reader.GetString(0);
                    }
                    reader.Close();
                    Hide();
                    MessageBox.Show("로그인 되었습니다.", "information");
                    Form2 f2 = new Form2();
                    f2.Lv2.Text = Lv.Text;
                    f2.ShowDialog();
                    f2 = null;
                    Show();
                    
                    
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("비밀번호를 확인해주세요.");
                }
            }
            else
            {
                reader.Close();
                MessageBox.Show("아이디가 없습니다.");
            }

            
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            non.Text = "non";
        }

        

        public void ulogin_click(object sender, EventArgs e)
        {
            Hide();
            Form2 f2 = new Form2();
            f2.non2.Text = non.Text;
            f2.ShowDialog();
            f2 = null;
            Show();
            
        }
        

        private void non_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
    }
}
