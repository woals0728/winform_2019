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
using Tamir.SharpSsh;
using MetroFramework.Forms;

namespace ljm4
{
    public partial class Form2 : MetroForm
    {
        private static string mysql_str = "server=l.bsks.ac.kr;Database=p201606023;Uid=p201606023;Pwd=pp201606023;Charset=utf8";
        MySqlConnection conn = new MySqlConnection(mysql_str);
        MySqlConnection conn2 = new MySqlConnection(mysql_str);

        MySqlCommand cmd,cmd2;
        MySqlDataReader reader, reader2;
        String Check;
        
        

        public Form2(Form4 _form4)
        {
            while (conn.State == ConnectionState.Closed)
            {
                
                conn.Open();
            }
            InitializeComponent();
        }

        public Form2()
        {
            while (conn.State == ConnectionState.Closed)
            {
                
                conn.Open();
            }
            InitializeComponent();
        }
        
       
        private void Form2_Load(object sender, EventArgs e)
        {
            conn2.Open();
            if (Lv2.Text == "B")
            {
                tabControl1.TabPages[3].Enabled = false;
            }
            else if(Lv2.Text == "C")
            {
                tabControl1.TabPages[2].Enabled = false;
                tabControl1.TabPages[3].Enabled = false;
            }
        
            if (non2.Text == "non")
            {
                tabControl1.TabPages[0].Enabled = false;
                tabControl1.TabPages[2].Enabled = false;
                tabControl1.TabPages[3].Enabled = false;
                tabControl1.TabPages[4].Enabled = false;
                dataGridView1.Visible = false;
                dataGridView3.Visible = false;
                dataGridView4.Visible = false;
                dataGridView5.Visible = false;
                dataGridView6.Visible = false;

                //return;
            }
            

            tabControl1.TabPages[0].BackColor = Color.White;
            tabControl1.TabPages[1].BackColor = Color.White;
            tabControl1.TabPages[2].BackColor = Color.White;
            tabControl1.TabPages[3].BackColor = Color.White;
            tabControl1.TabPages[4].BackColor = Color.White;
            tabControl2.TabPages[0].BackColor = Color.White;
            tabControl2.TabPages[1].BackColor = Color.White;
            tabControl3.TabPages[0].BackColor = Color.White;
            tabControl3.TabPages[1].BackColor = Color.White;
            button4.Enabled = false;
            this.search1.PerformClick();
            this.button16.PerformClick();
            this.button13.PerformClick();
            this.Perform3();
            this.Perform();
            textBox1.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            comboBox4.SelectedItem = "전체";
            comboBox1.SelectedItem = "학번";
            comboBox5.SelectedItem = "전체";
            comboBox6.SelectedItem = "학번";
            if (textBox5.Text  == "" && textBox6.Text == "")
            {
                button11.Enabled = false;
                //return;
            }
            

            string sql1 = "select USER_STUDENT_NO, DILL_DATE, DILL_FTIME, DILL_TTIME from Users inner join PRO on USER_PRO = concat(P_YEAR,'-',P_SEASON, ':', P_NAME) left outer join Diligence on DILL_STUDENT_NO = USER_STUDENT_NO where DILL_GUBUN = '미출근'";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                string sql2 = "update Diligence set DILL_FTIME = '', DILL_TTIME = '', DILL_GUBUN = '결근' " +
                    "where DILL_GUBUN = '미출근' and DILL_DATE != curdate()";
                cmd = new MySqlCommand(sql2, conn);
                reader = cmd.ExecuteReader();
            }
            reader.Close();
            
            string sql3 = "select * from Users u inner join PRO p on u.USER_PRO = concat(p.P_YEAR,'-',p.P_SEASON, ':', p.P_NAME) left outer join Diligence on DILL_STUDENT_NO = USER_STUDENT_NO " +
                "where str_to_date(concat(p.P_YEAR, p.P_DAY_S), '%Y년%m월 %d일' ) <= curdate() " +
                "and str_to_date(concat(p.P_YEAR, p.P_DAY_F), '%Y년%m월 %d일' ) >= curdate()";

            cmd = new MySqlCommand(sql3, conn);
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                string sql4 = "select * from Diligence where DILL_DATE = '" +textBox1.Text + "' and DILL_STUDENT_NO = '" + (string)reader["USER_STUDENT_NO"] + "' ";


                cmd2 = new MySqlCommand(sql4, conn2);
                reader2 = cmd2.ExecuteReader();
                int check = 0;
                while (reader2.Read())
                {
                    check = 1;
                }
                reader2.Close();

                if (check == 1)
                {
                    continue;
                }
                sql4 = "insert into Diligence(DILL_DATE, DILL_STUDENT_NO, DILL_GUBUN) values('" + textBox1.Text + "', '" + (string)reader["USER_STUDENT_NO"] + "', '미출근') ";


                cmd2 = new MySqlCommand(sql4, conn2);
                reader2 = cmd2.ExecuteReader();
                reader2.Close();
            }
            reader.Close();
            
        }

     

        

        #region Uploadimg()
        //WPF로 파일 업로드 (localfile : 컴퓨터 파일 경로, hwak : 확장자, std_no : 학번)
        //설치 순서 주의! - packages폴더에 dll이 있어야함.
        //사용시 https://sourceforge.net/projects/sharpssh/ 에서 다운한 파일 중 'Tamir.SharpSSH.dll'을 참조 후 누겟의 Tamir.SharpSSH 설치, 그 후 앞서 참조한 dll 참조 지우기
        private void Uploadimg(string localfile, string hwak, string std_no)
        {
            Sftp oSftp = null;
            try
            {
                SshExec se = new SshExec("l.bsks.ac.kr", "p201606023", "pp201606023");
                se.Connect(22);
                string dir = "/home/p201606023/public_html/image/";
                se.RunCommand("rm " + dir + std_no + ".jpg");
                se.RunCommand("rm " + dir + std_no + ".jpeg");
                se.RunCommand("rm " + dir + std_no + ".png");
                se.RunCommand("rm " + dir + std_no + ".gif");
                se.RunCommand("rm " + dir + std_no + ".JPG");
                se.RunCommand("rm " + dir + std_no + ".PNG");
                se.RunCommand("rm " + dir + std_no + ".GIF");
                se.Close();

                string _ftpURL = "l.bsks.ac.kr"; //Host URL or address of the SFTP server
                string _UserName = "p201606023";      //User Name of the SFTP server
                string _Password = "pp201606023";   //Password of the SFTP server
                int _Port = 22;                  //Port No of the SFTP server (if any)
                string _ftpDirectory = "/home/p201606023/public_html/image/"; //The directory in SFTP server where the files will be uploaded

                oSftp = new Sftp(_ftpURL, _UserName, _Password);
                oSftp.Connect(_Port);

                oSftp.Put(localfile, _ftpDirectory + std_no + "." + hwak);
            }
            catch (Exception ex)
            {
                MessageBox.Show("이미지 저장에 실패하였습니다.\n" + ex.Message + "::" + ex.StackTrace, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            finally
            {
                if (oSftp != null) oSftp.Close();
            }
        }
        #endregion

        void timerperform()
        {
            label59.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        
        
        
        private void Perform3()
        {
            string sql1 = "select concat(P_YEAR,'-',P_SEASON, ':', P_NAME) from PRO";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            comboBox3.Items.Clear();
            while (reader.Read())
            {
                comboBox3.Items.Add(reader.GetString(0));
            }
            reader.Close();
        }
        
        

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            button2.Enabled = false;
            button3.Enabled = false;
            button21.Enabled = false;
            button4.Enabled = true;
            button6.Enabled = true;
            num.Enabled = false;
            Check = "update";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button21.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            button6.Enabled = true;
            major.Enabled = false;
            name.Enabled = false;
            dateTimePicker1.Enabled = false;
            phone.Enabled = false;
            email.Enabled = false;
            addnum.Enabled = false;
            address.Enabled = false;
            Male.Enabled = false;
            Female.Enabled = false;
            dorm1.Enabled = false;
            dorm2.Enabled = false;
            textBox15.Enabled = false;
            textBox21.Enabled = false;
            comboBox3.Enabled = false;
            button7.Enabled = false;

            Check = "delete";
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            if (Check == "update")
            {
                if (num.Text == "")
                {
                    MessageBox.Show("학번을 입력하세요");
                    return;
                }
                if (major.Text == "")
                {
                    MessageBox.Show("학과를 입력하세요");
                    return;
                }
                if (name.Text == "")
                {
                    MessageBox.Show("이름을 입력하세요");
                    return;
                }
                if (phone.Text == "")
                {
                    MessageBox.Show("연락처를 입력하세요");
                    return;
                }
                if (addnum.Text == "")
                {
                    MessageBox.Show("우편번호를 입력하세요");
                    return;
                }
                if (address.Text == "")
                {
                    MessageBox.Show("주소를 입력하세요");
                    return;
                }
                if (sex.Text == "")
                {
                    MessageBox.Show("성별을 체크하세요");
                    return;
                }
                if (imgName.Text == "")
                {
                    MessageBox.Show("이미지를 넣어주세요");
                    return;
                }
                if (comboBox3.Text == "")
                {
                    MessageBox.Show("실습을 선택해주세요.");
                    return;
                }
                string src = num.Text + "." + imgName.Text.Substring(imgName.Text.LastIndexOf('.') + 1);
                string sql1 = "select * from Users where USER_STUDENT_NO = '" + num.Text + "'";
                string sql2 = "update Users set USER_MAJOR = '" + major.Text + "', " +
                    "USER_NAME = '" + name.Text + "', " +
                    "USER_BIRTH = '" + dateTimePicker1.Text + "'," +
                    "USER_SEX = '" + sex.Text + "'," +
                    "USER_PHONE = '" + phone.Text + "'," +
                    "USER_MAIL = '" + email.Text + "'," +
                    "USER_DORM = '" + textBox15.Text + "', " +
                    "USER_ADDNUM = '" + addnum.Text + "', " +
                    "USER_ADDR = '" + address.Text + "'," +
                    "USER_IMAGE = '" + src + "', " +
                    "USER_PRO = '" + comboBox3.Text + "', " +
                    "USER_DATA = '" + textBox21.Text + "' where USER_STUDENT_NO = '" + num.Text + "'";
                cmd = new MySqlCommand(sql1, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.ExecuteNonQuery();
                    Uploadimg(imgName.Text, imgName.Text.Substring(imgName.Text.LastIndexOf('.') + 1), num.Text);
                    MessageBox.Show("변경되었습니다.");
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("학번이 존재하지 않습니다.");
                }
            }
            else if (Check == "delete")
            {
                
                if (num.Text == "")
                {
                    MessageBox.Show("학생 정보를 선택하세요.");
                    return;
                }

                String sql1 = "Select * from Users where USER_STUDENT_NO='" + num.Text + "' ";
                String sql2 = "delete from Users where USER_STUDENT_NO='" + num.Text + "' ";

                cmd = new MySqlCommand(sql1, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (MessageBox.Show("학생 정보를 삭제하시겠습니까?", "information", MessageBoxButtons.YesNo)==DialogResult.Yes)
                    {
                        reader.Close();
                        cmd = new MySqlCommand(sql2, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("삭제되었습니다.");
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("취소하였습니다.");
                    }
                    
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("학번이 없습니다.");
                }

                
            }
            else if(Check == "rename")
            {
                if (num.Text == "")
                {
                    MessageBox.Show("학생 정보를 선택하세요.");
                    return;
                }

                string sql1 = "select * from Users where USER_STUDENT_NO = '" + num.Text + "'";
                string sql2 = "update Users set USER_DATA = '미승인' where USER_STUDENT_NO = '" + num.Text + "'";
                cmd = new MySqlCommand(sql1, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("변경되었습니다.");
                }
                reader.Close();
            }

            this.button6.PerformClick();
            this.search1.PerformClick();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            button2.Enabled = true;
            button3.Enabled = true;
            button21.Enabled = true;
            button4.Enabled = false;

            num.Text = "";
            major.Text = "";
            name.Text = "";
            dateTimePicker1.Text = "";
            phone.Text = "";
            email.Text = "";
            addnum.Text = "";
            address.Text = "";
            imgName.Text = "";
            sex.Text = "";
            textBox15.Text = "";
            textBox21.Text = "";
            comboBox3.Text = "";
            Male.Checked = false;
            Female.Checked = false;
            dorm1.Checked = false;
            dorm2.Checked = false;

            pictureBox1.Image = null;

            num.Enabled = true;
            major.Enabled = true;
            name.Enabled = true;
            dateTimePicker1.Enabled = true;
            phone.Enabled = true;
            addnum.Enabled = true;
            address.Enabled = true;
            email.Enabled = true;
            Male.Enabled = true;
            Female.Enabled = true;
            dorm1.Enabled = true;
            dorm2.Enabled = true;
            comboBox3.Enabled = true;

            return;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void imgName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (num.Text == "")
            {
                MessageBox.Show("학번을 먼저 입력해 주세요");
                return;
            }
            OpenFileDialog f1 = new OpenFileDialog();
            if (f1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null) pictureBox1.Image.Dispose();

                pictureBox1.Image = System.Drawing.Image.FromFile(f1.FileName);
                /*
                fname = Path.GetFileName(f1.FileName);
                
                imgName.Text = f1.FileName;

                fname = num.Text + fname.Substring(fname.LastIndexOf('.'));
                pictureBox1.Image.Save(fname);
                */
                imgName.Text = f1.FileName;
                


            }
            
        }

        private void num_TextChanged(object sender, EventArgs e)
        {

        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void birth_TextChanged(object sender, EventArgs e)
        {

        }

        private void Male_CheckedChanged(object sender, EventArgs e)
        {
            sex.Text = "남";
        }

        private void Female_CheckedChanged(object sender, EventArgs e)
        {
            sex.Text = "여";
        }

        private void phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void address_TextChanged(object sender, EventArgs e)
        {

        }

        private void sex_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void search1_Click(object sender, EventArgs e)
        {
            sss();
        }
        void sss()
        {
            string sql1;
            sql1 = "select * from Users where USER_STUDENT_NO like '" + textBox3.Text + "%' and USER_DATA = '승인됨'";
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("USER_STUDENT_NO", "학번");
            dataGridView1.Columns.Add("USER_MAJOR", "학과");
            dataGridView1.Columns.Add("USER_NAME", "이름");
            dataGridView1.Columns.Add("USER_BIRTH", "생년월일");
            dataGridView1.Columns.Add("USER_SEX", "성별");
            dataGridView1.Columns.Add("USER_PHONE", "전화번호");
            dataGridView1.Columns.Add("USER_MAIL", "이메일");
            dataGridView1.Columns.Add("USER_DORM", "기숙사 신청여부");
            dataGridView1.Columns.Add("USER_ADDNUM", "우편번호");
            dataGridView1.Columns.Add("USER_ADDR", "주소");
            dataGridView1.Columns.Add("USER_PRO", "신청실습");
            dataGridView1.Columns.Add("USER_DATA", "승인여부");
            dataGridView1.Columns.Add("USER_IMAGE", "이미지");
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = (string)reader["USER_STUDENT_NO"];
                dataGridView1.Rows[i].Cells[1].Value = (string)reader["USER_MAJOR"];
                dataGridView1.Rows[i].Cells[2].Value = (string)reader["USER_NAME"];
                dataGridView1.Rows[i].Cells[3].Value = (string)reader["USER_BIRTH"];
                dataGridView1.Rows[i].Cells[4].Value = (string)reader["USER_SEX"];
                dataGridView1.Rows[i].Cells[5].Value = (string)reader["USER_PHONE"];
                dataGridView1.Rows[i].Cells[6].Value = (string)reader["USER_MAIL"];
                dataGridView1.Rows[i].Cells[7].Value = (string)reader["USER_DORM"];
                dataGridView1.Rows[i].Cells[8].Value = (string)reader["USER_ADDNUM"];
                dataGridView1.Rows[i].Cells[9].Value = (string)reader["USER_ADDR"];
                dataGridView1.Rows[i].Cells[10].Value = (string)reader["USER_PRO"];
                dataGridView1.Rows[i].Cells[11].Value = (string)reader["USER_DATA"];
                dataGridView1.Rows[i].Cells[12].Value = (string)reader["USER_IMAGE"];
                i++;
            }
            reader.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void diltxt_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String txt = comboBox1.SelectedItem as String;

            if(txt == "학번")
            {
                textBox22.Text = "USER_STUDENT_NO";
                return;
            }
            else if (txt == "학과")
            {
                textBox22.Text = "USER_MAJOR";
                return;
            }
            else if (txt == "이름")
            {
                textBox22.Text = "USER_NAME";
                return;
            }
            else if (txt == "정보")
            {
                textBox22.Text = "USER_DATA";
                return;
            }

        }

        

        private void label9_Click(object sender, EventArgs e)
        {

        }

       

        private void Form_closing(object sender, FormClosingEventArgs e)
        {
            Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            Show();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

       
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            aa(e.RowIndex);
        }
        void aa(int RowIndex)
        {
            int a = int.Parse(RowIndex.ToString());
            if (a < 0) return;
            
            num1.Text = dataGridView3.Rows[RowIndex].Cells[0].FormattedValue.ToString();
            major1.Text = dataGridView3.Rows[RowIndex].Cells[1].FormattedValue.ToString();
            name1.Text = dataGridView3.Rows[RowIndex].Cells[2].FormattedValue.ToString();
            birth1.Text = dataGridView3.Rows[RowIndex].Cells[3].FormattedValue.ToString();
            sex1.Text = dataGridView3.Rows[RowIndex].Cells[4].FormattedValue.ToString();
            phone1.Text = dataGridView3.Rows[RowIndex].Cells[5].FormattedValue.ToString();
            email1.Text = dataGridView3.Rows[RowIndex].Cells[6].FormattedValue.ToString();
            dorm3.Text = dataGridView3.Rows[RowIndex].Cells[7].FormattedValue.ToString();
            addnum1.Text = dataGridView3.Rows[RowIndex].Cells[8].FormattedValue.ToString();
            address1.Text = dataGridView3.Rows[RowIndex].Cells[9].FormattedValue.ToString();
            textBox14.Text = dataGridView3.Rows[RowIndex].Cells[10].FormattedValue.ToString();
            textBox25.Text = dataGridView3.Rows[RowIndex].Cells[11].FormattedValue.ToString();
            data.Text = dataGridView3.Rows[RowIndex].Cells[12].FormattedValue.ToString();

            string src = dataGridView3.Rows[RowIndex].Cells[10].Value.ToString();
            if (string.IsNullOrWhiteSpace(src))
            {

            }
            else if (src.Contains(':'))
            {
                pictureBox2.Image = Bitmap.FromFile(src);
            }
            else
            {
                //서버의 img 불러오기
                string serversrc = "http://l.bsks.ac.kr/~p201606023/image/" + src;
                pictureBox2.ImageLocation = serversrc;
            }



            return;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from Users where USER_STUDENT_NO = '" + num1.Text + "'";
            string sql2 = "update Users set USER_DATA = '승인됨' where USER_STUDENT_NO = '" + num1.Text + "'";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                cmd = new MySqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("변경되었습니다.");
                this.Perform();
                num1.Text = "";
                major1.Text = "";
                name1.Text = "";
                birth1.Text = "";
                sex1.Text = "";
                phone1.Text = "";
                email1.Text = "";
                dorm3.Text = "";
                addnum1.Text = "";
                address1.Text = "";
                data.Text = "";
                textBox14.Text = "";
                textBox25.Text = "";
                pictureBox2.Image = null;
            }
            reader.Close();

        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            
        }

        private void Perform()
        {
            string sql1;
            sql1 = "select * from Users where USER_DATA = '미승인'";
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("USER_STUDENT_NO", "학번");
            dataGridView3.Columns.Add("USER_MAJOR", "학과");
            dataGridView3.Columns.Add("USER_NAME", "이름");
            dataGridView3.Columns.Add("USER_BIRTH", "생년월일");
            dataGridView3.Columns.Add("USER_SEX", "성별");
            dataGridView3.Columns.Add("USER_PHONE", "전화번호");
            dataGridView3.Columns.Add("USER_MAIL", "이메일");
            dataGridView3.Columns.Add("USER_DORM", "기숙사 신청여부");
            dataGridView3.Columns.Add("USER_ADDNUM", "우편번호");
            dataGridView3.Columns.Add("USER_ADDR", "주소");
            dataGridView3.Columns.Add("USER_IMAGE", "이미지");
            dataGridView3.Columns.Add("USER_PRO", "신청실습");
            dataGridView3.Columns.Add("USER_DATA", "승인여부");
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {

                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = (string)reader["USER_STUDENT_NO"];
                dataGridView3.Rows[i].Cells[1].Value = (string)reader["USER_MAJOR"];
                dataGridView3.Rows[i].Cells[2].Value = (string)reader["USER_NAME"];
                dataGridView3.Rows[i].Cells[3].Value = (string)reader["USER_BIRTH"];
                dataGridView3.Rows[i].Cells[4].Value = (string)reader["USER_SEX"];
                dataGridView3.Rows[i].Cells[5].Value = (string)reader["USER_PHONE"];
                dataGridView3.Rows[i].Cells[6].Value = (string)reader["USER_MAIL"];
                dataGridView3.Rows[i].Cells[7].Value = (string)reader["USER_DORM"];
                dataGridView3.Rows[i].Cells[8].Value = (string)reader["USER_ADDNUM"];
                dataGridView3.Rows[i].Cells[9].Value = (string)reader["USER_ADDR"];
                dataGridView3.Rows[i].Cells[10].Value = (string)reader["USER_IMAGE"];
                dataGridView3.Rows[i].Cells[11].Value = (string)reader["USER_PRO"];
                dataGridView3.Rows[i].Cells[12].Value = (string)reader["USER_DATA"];
                i++;
            }
            reader.Close();
        }
        
        private void tabevent(object sender, EventArgs e)
        {
            this.Perform();
            this.button16.PerformClick();
            this.button13.PerformClick();
            this.button9.PerformClick();
            this.button5.PerformClick();
            this.button1.PerformClick();
            this.search1.PerformClick();
            this.Perform5();
        }

        private void Perform5()
        {
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void addnum_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged_2(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void dorm1_CheckedChanged(object sender, EventArgs e)
        {
            textBox15.Text = "신청";
        }

        private void dorm2_CheckedChanged(object sender, EventArgs e)
        {
            textBox15.Text = "미신청";
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void non2_TextChanged(object sender, EventArgs e)
        {

        }
        

        

        private void button11_Click(object sender, EventArgs e)
        {
            
            if(textBox2.Text == "")
            {
                MessageBox.Show("학번을 입력해주세요.");
                button24.Enabled = true;
                button23.Enabled = true;
                return;
            }
            if(textBox5.Text != "" && textBox6.Text == "")
            {
                if (textBox1.Text != System.DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    MessageBox.Show("지난 날짜는 출근이나 퇴근을 할 수 없습니다.");
                    return;
                }
                string sql1 = "select * from Users where USER_STUDENT_NO = '" + textBox2.Text + "'";
                string sql3 = "select P_TIME_S from PRO inner join Users on USER_PRO = concat(P_YEAR,'-',P_SEASON,':',P_NAME)" +
                    "where USER_STUDENT_NO = '" + textBox2.Text + "'";
                cmd = new MySqlCommand(sql1, conn);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                   reader.Close();
                   cmd = new MySqlCommand(sql3, conn);
                   reader = cmd.ExecuteReader();
                   
                   if(reader.Read())
                   {
                        p_time_s.Text = (string)reader["P_TIME_S"];
                        reader.Close();
                        TimeSpan a = new TimeSpan(Convert.ToInt32(p_time_s.Text.Substring(0, 2)), 
                            Convert.ToInt32(p_time_s.Text.Substring(3, 2)), 0);
                        TimeSpan b = new TimeSpan(Convert.ToInt32(textBox5.Text.Substring(0, 2)), 
                            Convert.ToInt32(textBox5.Text.Substring(3, 2)), 0);

                        string sql4 = "select * from Diligence where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "' and (DILL_GUBUN = '업무중' or DILL_GUBUN = '지각')";
                        cmd = new MySqlCommand(sql4, conn);
                        reader = cmd.ExecuteReader();
                        if(reader.Read())
                        {
                            reader.Close();
                            button24.Enabled = true;
                            button23.Enabled = true;
                            MessageBox.Show("이미 출근하셨습니다.");
                            return;
                        }
                        else
                        {
                            if (a >= b)
                            {
                                reader.Close();
                                string sql2 = "update Diligence set DILL_FTIME = '" + textBox5.Text + "', DILL_GUBUN = '업무중' where DILL_STUDENT_NO = '" + textBox2.Text +"' and DILL_DATE = '" + textBox1.Text + "'";
                                cmd = new MySqlCommand(sql2, conn);
                                reader = cmd.ExecuteReader();
                                MessageBox.Show("출근 하셨습니다.");
                            }
                            else if (a < b)
                            {
                                dilgubun.Text = "지각";
                                reader.Close();
                                string sql2 = "update Diligence set DILL_FTIME = '" + textBox5.Text + "', DILL_GUBUN = '" + dilgubun.Text + "' where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "'";
                                cmd = new MySqlCommand(sql2, conn);
                                reader = cmd.ExecuteReader();
                                MessageBox.Show("지각입니다.");
                            }
                        }
                        

                        reader.Close();
                   }
                   
                    
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("존재하지 않는 학번입니다.");
                    return;
                }
            }
            else if(textBox6.Text != "" && textBox5.Text == "")
            {
                if (textBox1.Text != System.DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    MessageBox.Show("지난 날짜는 출근이나 퇴근을 할 수 없습니다.");
                    return;
                }
                string sql1 = "select * from Users where USER_STUDENT_NO = '" + textBox2.Text + "'";
                string sql3 = "select * from Diligence where (DILL_GUBUN = '정상' or DILL_GUBUN = '조퇴' or DILL_GUBUN = '지각') and DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "' and DILL_TTIME is not null";
                string sql4 = "select DILL_STUDENT_NO from Diligence where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "'";
                string sql5 = "select P_TIME_F from PRO inner join Users on USER_PRO = concat(P_YEAR,'-',P_SEASON,':',P_NAME)" +
                    "where USER_STUDENT_NO = '" + textBox2.Text + "'";
                cmd = new MySqlCommand(sql1, conn);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql4, conn);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        cmd = new MySqlCommand(sql5, conn);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            p_time_f.Text = (string)reader["P_TIME_F"];
                            reader.Close();
                            TimeSpan a = new TimeSpan(Convert.ToInt32(p_time_f.Text.Substring(0, 2)),
                                Convert.ToInt32(p_time_f.Text.Substring(3, 2)), 0);
                            TimeSpan b = new TimeSpan(Convert.ToInt32(textBox6.Text.Substring(0, 2)),
                                Convert.ToInt32(textBox6.Text.Substring(3, 2)), 0);
                            cmd = new MySqlCommand(sql3, conn);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                if ((string)reader["DILL_GUBUN"] == "미출근")
                                {
                                    MessageBox.Show("미출근 상태입니다.");
                                    reader.Close();
                                    button24.Enabled = true;
                                    button23.Enabled = true;
                                    return;
                                }
                                else if((string)reader["DILL_GUBUN"] == "정상" || (string)reader["DILL_GUBUN"] == "조퇴")
                                {
                                    MessageBox.Show("이미 퇴근하셨습니다.");
                                    reader.Close();
                                    button24.Enabled = true;
                                    button23.Enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                if (a > b)
                                {
                                    if (MessageBox.Show("퇴근시간이 아닙니다.\n조퇴 하시겠습니까?", "" ,MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        dilgubun.Text = "조퇴";
                                        reader.Close();
                                        string sql2 = "update Diligence set DILL_TTIME = '" + textBox6.Text + "' , DILL_GUBUN = '" + dilgubun.Text + "'" +
                    "where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "'";
                                        cmd = new MySqlCommand(sql2, conn);
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("조퇴하였습니다.");
                                    }
                                    else
                                    {
                                        reader.Close();
                                        button24.Enabled = true;
                                        button23.Enabled = true;
                                        MessageBox.Show("취소하였습니다.");
                                        return;
                                    }
                                }
                                else if(a<=b)
                                {
                                    dilgubun.Text = "정상";
                                    reader.Close();
                                    string sql2 = "update Diligence set DILL_TTIME = '" + textBox6.Text + "' , DILL_GUBUN = '" + dilgubun.Text + "'" +
                    "where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "'";
                                    cmd = new MySqlCommand(sql2, conn);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("퇴근하였습니다.");
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("존재하지 않는 학번입니다.");
                    return;
                }
            }
            

            else if(Check == "dilupdate")
            {
                
                if(textBox2.Text == "")
                {
                    MessageBox.Show("변경할 정보를 선택해주세요.");
                    return;
                }
                if(dilgubun.Text != "결근")
                {
                    if (textBox5.Text == "")
                    {
                        MessageBox.Show("출근시각을 입력해주세요.");
                        return;
                    }
                    if (textBox6.Text == "")
                    {
                        MessageBox.Show("퇴근시각을 입력해주세요.");
                        return;
                    }
                }
                
                string sql5 = "select * from Diligence where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "'";
                string sql3 = "update Diligence set DILL_FTIME = '" + textBox5.Text + "', DILL_TTIME = '" + textBox6.Text + "', DILL_GUBUN = '" + dilgubun.Text + "'" +
                    "where DILL_STUDENT_NO = '" + textBox2.Text + "' and DILL_DATE = '" + textBox1.Text + "'";
                cmd = new MySqlCommand(sql5, conn);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql3, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("변경하였습니다.");
                }
            }
            reader.Close();
            bt9perform();
            button11.Enabled = false;
            button10.Enabled = true;
            textBox5.Enabled = false;
            textBox5.ReadOnly = true;
            textBox6.Enabled = false;
            textBox6.ReadOnly = true;
            button23.Enabled = true;
            button24.Enabled = true;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bt9perform();
        }

        void bt9perform()
        {
            if (dateTimePicker5.Value.Date < dateTimePicker4.Value.Date)
            {
                MessageBox.Show("종료일자가 시작일자보다 빠를 수 없습니다.");
                return;
            }
            string sql1;
            sql1 = "select DILL_DATE, P_TIME_S, USER_PRO, USER_STUDENT_NO, USER_MAJOR, USER_NAME, DILL_FTIME, DILL_TTIME, DILL_GUBUN " +
                "from Users u inner join Diligence d on DILL_STUDENT_NO = USER_STUDENT_NO inner join PRO p on USER_PRO =concat(P_YEAR,'-',P_SEASON,':',P_NAME) " +
                "where DILL_DATE <= '" + dateTimePicker5.Text + "' and DILL_DATE>= '" + dateTimePicker4.Text + "' and " + textBox22.Text + " like '%" + diltxt.Text + "%' " +
                "and USER_DATA = '승인됨'";



            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("DILL_DATE", "출근 일자");
            dataGridView2.Columns.Add("USER_PRO", "실습명");
            dataGridView2.Columns.Add("USER_STUDENT_NO", "학번");
            dataGridView2.Columns.Add("USER_MAJOR", "학과");
            dataGridView2.Columns.Add("USER_NAME", "이름");
            dataGridView2.Columns.Add("DILL_FTIME", "출근 시간");
            dataGridView2.Columns.Add("DILL_TTIME", "퇴근 시간");
            dataGridView2.Columns.Add("DILL_GUBUN", "구분");
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = (string)reader["DILL_DATE"];
                dataGridView2.Rows[i].Cells[1].Value = (string)reader["USER_PRO"];
                dataGridView2.Rows[i].Cells[2].Value = (string)reader["USER_STUDENT_NO"];
                dataGridView2.Rows[i].Cells[3].Value = (string)reader["USER_MAJOR"];
                dataGridView2.Rows[i].Cells[4].Value = (string)reader["USER_NAME"];
                dataGridView2.Rows[i].Cells[5].Value = (string)reader["DILL_FTIME"].ToString();
                dataGridView2.Rows[i].Cells[6].Value = (string)reader["DILL_TTIME"].ToString();
                dataGridView2.Rows[i].Cells[7].Value = (string)reader["DILL_GUBUN"];
                i++;
            }
            reader.Close();
        }

        void ddd(int RowIndex)
        {
            int a = int.Parse(RowIndex.ToString());
            if (a < 0) return;

            textBox2.Text = dataGridView2.Rows[RowIndex].Cells[2].FormattedValue.ToString();
            textBox1.Text = dataGridView2.Rows[RowIndex].Cells[0].FormattedValue.ToString();
            textBox5.Text = dataGridView2.Rows[RowIndex].Cells[5].FormattedValue.ToString();
            textBox6.Text = dataGridView2.Rows[RowIndex].Cells[6].FormattedValue.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
                ddd(e.RowIndex);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Check = "dilupdate";
            button11.Enabled = true;
            button10.Enabled = false;
            textBox5.Enabled = true;
            textBox5.ReadOnly = false;
            textBox6.Enabled = true;
            textBox6.ReadOnly = false;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton5.Visible = true;
            radioButton6.Visible = true;
            button23.Enabled = false;
            button24.Enabled = false;

       

        }

        

        private void button10_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            String txt = comboBox4.SelectedItem as String;

            if (txt == "동계")
            {
                seasontxt2.Text = "동계";
                return;
            }
            else if (txt == "하계")
            {
                seasontxt2.Text = "하계";
                return;
            }
            else if (txt == "전체")
            {
                seasontxt2.Text = "%계";
                return;
            }

        }

        private void seasontxt2_TextChanged(object sender, EventArgs e)
        {

        }

        private void seasontxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if(dateTimePicker6.Value.Date < dateTimePicker10.Value.Date)
            {
                MessageBox.Show("종료연도가 시작연도보다 빠를 수 없습니다.");
                return;
            }
            string sql1;
            sql1 = "select * from PRO where P_YEAR >= '"+dateTimePicker10.Text+"' and P_YEAR <='"+dateTimePicker6.Text+"' and P_SEASON like '" + seasontxt2.Text + "'";
            dataGridView5.Rows.Clear();
            dataGridView5.Columns.Clear();
            dataGridView5.Columns.Add("P_YEAR", "연도");
            dataGridView5.Columns.Add("P_SEASON", "계절");
            dataGridView5.Columns.Add("P_NAME", "실습명칭");
            dataGridView5.Columns.Add("P_DAY_S", "시작날짜");
            dataGridView5.Columns.Add("P_DAY_F", "종료날짜");
            dataGridView5.Columns.Add("P_TIME_S", "출근시간");
            dataGridView5.Columns.Add("P_TIME_F", "퇴근시간");
            dataGridView5.Columns.Add("P_NUM", "a");
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                dataGridView5.Rows.Add();
                dataGridView5.Rows[i].Cells[0].Value = (string)reader["P_YEAR"];
                dataGridView5.Rows[i].Cells[1].Value = (string)reader["P_SEASON"];
                dataGridView5.Rows[i].Cells[2].Value = (string)reader["P_NAME"];
                dataGridView5.Rows[i].Cells[3].Value = (string)reader["P_DAY_S"];
                dataGridView5.Rows[i].Cells[4].Value = (string)reader["P_DAY_F"];
                dataGridView5.Rows[i].Cells[5].Value = (string)reader["P_TIME_S"];
                dataGridView5.Rows[i].Cells[6].Value = (string)reader["P_TIME_F"];
                dataGridView5.Rows[i].Cells[7].Value = reader["P_NUM"].ToString();
                i++;
            }
            reader.Close();

            dataGridView5.Columns[7].Visible = false;
        }

        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            seasontxt.Text = "하계";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            seasontxt.Text = "동계";
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("실습명칭을 입력하세요");
                return;
            }
            if (dateTimePicker3.Value.Date >= dateTimePicker7.Value.Date)
            {
                MessageBox.Show("종료날짜가 시작날짜와 같거나 빠를 수 없습니다.");
                return;
            }

            if(dateTimePicker3.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("시작날짜가 오늘과 같거나 빠를 수 없습니다.");
                return;

            }
            if (dateTimePicker7.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("종료날짜가 오늘과 같거나 빠를 수 없습니다.");
                return;
            }
            if(dateTimePicker9.Value.Hour >= dateTimePicker8.Value.Hour)
            {
                MessageBox.Show("퇴근시간이 출근시간과 같거나 빠를 수 없습니다.");
                TimeSpan a = new TimeSpan(Convert.ToInt32(dateTimePicker9.Text.Substring(3, 2)));
                TimeSpan b = new TimeSpan(Convert.ToInt32(dateTimePicker8.Text.Substring(3, 2)));
                if (a >= b)
                {
                    MessageBox.Show("퇴근시간이 출근시간과 같거나 빠를 수 없습니다.");
                    return;
                }
                return;
            }
            
            
            if(seasontxt.Text == "")
            {
                MessageBox.Show("계절을 선택하세요");
                return;
            }
            

            string sql1 = "select * from PRO where P_YEAR = '" + dateTimePicker2.Text + "' and P_SEASON = '"+seasontxt.Text + "' and P_NAME = '"+textBox7.Text + "'";
            string sql2 = "insert into PRO(P_YEAR, P_SEASON, P_NAME, P_DAY_S, P_DAY_F, P_TIME_S, P_TIME_F)";
            sql2 = sql2 + "values('" + dateTimePicker2.Text + "', '" + seasontxt.Text + "', '" + textBox7.Text + "', " +
                "'" + dateTimePicker3.Text + "', '" + dateTimePicker7.Text + "', '" + dateTimePicker9.Text + "', " +
                "'" + dateTimePicker8.Text + "')";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("존재하는 실습입니다.");
                reader.Close();
            }

            else
            {
                reader.Close();
                cmd = new MySqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("실습 입력을 완료하였습니다.", "생성 완료");

                textBox7.Text = "";
                dateTimePicker2.Text = "";
                dateTimePicker3.Text = "";
                dateTimePicker7.Text = "";
                dateTimePicker9.Text = "";
                dateTimePicker8.Text = "";
                seasontxt.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                this.button16.PerformClick();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value.Date > dateTimePicker7.Value.Date)
            {
                MessageBox.Show("종료날짜가 시작날짜보다 빠를 수 없습니다.");
                return;
            }

            if (dateTimePicker3.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("시작날짜가 오늘보다 빠를 수 없습니다.");
                return;
            }
            if (dateTimePicker7.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("종료날짜가 오늘보다 빠를 수 없습니다.");
                return;

            }
            if (dateTimePicker9.Value.Hour > dateTimePicker8.Value.Hour)
            {
                MessageBox.Show("퇴근시간이 출근시간보다 빠를 수 없습니다.");
                return;
            }
            
                string sql1 = "select * from PRO where P_NUM = '" + pnum.Text + "'";
                string sql2 = "update PRO set P_NAME = '" + textBox7.Text + "'," +
                    "P_DAY_S = '" + dateTimePicker3.Text + "'," +
                    "P_DAY_F = '" + dateTimePicker7.Text + "'," +
                    "P_TIME_S = '" + dateTimePicker9.Text + "'," +
                    "P_TIME_F = '" + dateTimePicker8.Text + "' " +
                    "where P_NUM = '" + pnum.Text + "'";
                string sql3 = "update Users set USER_PRO = concat('" + dateTimePicker2.Text + "','-','" + seasontxt.Text + "', ':', '" + textBox7.Text + "')" +
                    "where USER_PRO in(select concat(P_YEAR,'-',P_SEASON, ':', P_NAME) from PRO)";
            


                cmd = new MySqlCommand(sql1, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql3, conn);     
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand(sql2, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("변경되었습니다.");
                    Perform();
                    Perform3();
                    asdf();
                    sss();
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Selected == true)
                        {
                            aa(i);
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected == true)
                        {
                            bb(i);
                        }
                    }
                    for (int i = 0; i < dataGridView6.Rows.Count; i++)
                    {
                        if (dataGridView6.Rows[i].Selected == true)
                        {
                            cc(i);
                        }
                    }
                    textBox7.Text = "";
                    dateTimePicker2.Text = "";
                    dateTimePicker3.Text = "";
                    dateTimePicker7.Text = "";
                    dateTimePicker9.Text = "";
                    dateTimePicker8.Text = "";
                    seasontxt.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    this.button16.PerformClick();
                    
            }
                else
                {
                    reader.Close();
                    MessageBox.Show("존재하지 않는 실습입니다.");
                    return;
                }
                

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a = int.Parse(e.RowIndex.ToString());
            if (a < 0) return;
            
            dateTimePicker2.Text = dataGridView5.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            seasontxt.Text = dataGridView5.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            textBox7.Text = dataGridView5.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            dateTimePicker3.Text = dataGridView5.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            dateTimePicker7.Text = dataGridView5.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            dateTimePicker9.Text = dataGridView5.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            dateTimePicker8.Text = dataGridView5.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            pnum.Text = dataGridView5.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();


            if (seasontxt.Text == "하계")
            {
                radioButton1.Checked = true;
            }
            else if (seasontxt.Text == "동계")
            {
                radioButton2.Checked = true;
            }

            return;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Text == "")
            {
                MessageBox.Show("연도를 입력하세요");
                return;
            }
            if (seasontxt.Text == "")
            {
                MessageBox.Show("계절을 선택하세요");
                return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("실습명칭을 입력하세요");
                return;
            }

            String sql1 = "select * from PRO where P_YEAR = '" + dateTimePicker2.Text + "' and P_SEASON = '" + seasontxt.Text + "' and P_NAME = '" + textBox7.Text + "'";
            String sql2 = "delete from PRO where P_YEAR = '" + dateTimePicker2.Text + "' and P_SEASON = '" + seasontxt.Text + "' and P_NAME = '" + textBox7.Text + "'";

            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (MessageBox.Show("삭제하시겠습니까?", "information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("삭제되었습니다.", "information");
                    this.button16.PerformClick();
                    textBox7.Text = "";
                    dateTimePicker2.Text = "";
                    dateTimePicker3.Text = "";
                    dateTimePicker7.Text = "";
                    dateTimePicker9.Text = "";
                    dateTimePicker8.Text = "";
                    seasontxt.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("취소하였습니다.");
                }

            }
            else
            {
                reader.Close();
                MessageBox.Show("실습이 존재하지 않습니다.");
            }
        }

        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        

        
        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            button6.Enabled = true;
            major.Enabled = false;
            name.Enabled = false;
            dateTimePicker1.Enabled = false;
            phone.Enabled = false;
            email.Enabled = false;
            addnum.Enabled = false;
            address.Enabled = false;
            Male.Enabled = false;
            Female.Enabled = false;
            dorm1.Enabled = false;
            dorm2.Enabled = false;
            textBox15.Enabled = false;
            textBox21.Enabled = false;
            comboBox3.Enabled = false;
            button7.Enabled = false;

            Check = "rename";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from Admin where ADMIN_ID = '" + textBox17.Text + "'";
            string sql2 = "insert into Admin(ADMIN_ID, ADMIN_PASS, ADMIN_NAME, ADMIN_LEVEL)";
            sql2 = sql2 + "values('" + textBox17.Text + "', '" + textBox18.Text + "', '" + textBox19.Text + "', " +
                "'" + comboBox2.Text + "')";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("아이디가 이미 존재합니다.");
                reader.Close();
            }

            else
            {
                reader.Close();
                cmd = new MySqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("생성이 완료되었습니다.");

                textBox17.Text = "";
                textBox18.Text = "";
                textBox19.Text = "";
                comboBox2.Text = "";
                this.button13.PerformClick();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from Admin where ADMIN_ID = '" + textBox17.Text + "'";
            string sql2 = "update Admin set ADMIN_PASS = '" + textBox18.Text + "', " +
                "ADMIN_NAME = '" + textBox19.Text + "', " +
                "ADMIN_LEVEL = '" + comboBox2.Text + "' where ADMIN_ID = '" + textBox17.Text + "'";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                reader.Close();
                cmd = new MySqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("변경되었습니다.");

                textBox17.Text = "";
                textBox18.Text = "";
                textBox19.Text = "";
                comboBox2.Text = "";
                this.button13.PerformClick();
            }
            else
            {
                reader.Close();
                MessageBox.Show("존재하지 않는 실습입니다.");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            String sql1 = "select * from Admin where ADMIN_ID = '" + textBox17.Text + "'";
            String sql2 = "delete from Admin where ADMIN_ID = '" + textBox17.Text + "'";

            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (MessageBox.Show("삭제하시겠습니까?", "information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    reader.Close();
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("삭제되었습니다.", "information");
                    textBox17.Text = "";
                    textBox18.Text = "";
                    textBox19.Text = "";
                    comboBox2.Text = "";
                    this.button13.PerformClick();
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("취소하였습니다.");
                }

            }
            else
            {
                reader.Close();
                MessageBox.Show("아이디가 존재하지 않습니다.");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string sql1;
            sql1 = "select * from Admin where ADMIN_ID like '" + textBox20.Text + "%'";
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView4.Columns.Add("ADMIN_ID", "아이디");
            dataGridView4.Columns.Add("ADMIN_PASS", "비밀번호");
            dataGridView4.Columns.Add("ADMIN_NAME", "이름");
            dataGridView4.Columns.Add("ADMIN_LEVEL", "권한");
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {

                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = (string)reader["ADMIN_ID"];
                dataGridView4.Rows[i].Cells[1].Value = (string)reader["ADMIN_PASS"];
                dataGridView4.Rows[i].Cells[2].Value = (string)reader["ADMIN_NAME"];
                dataGridView4.Rows[i].Cells[3].Value = (string)reader["ADMIN_LEVEL"];
                i++;
            }
            reader.Close();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a = int.Parse(e.RowIndex.ToString());
            if (a < 0) return;


            textBox17.Text = dataGridView4.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            textBox18.Text = dataGridView4.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            textBox19.Text = dataGridView4.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            comboBox2.Text = dataGridView4.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

            return;
        }

        private void num_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void major_TextChanged(object sender, EventArgs e)
        {

        }

        private void name_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void phone_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void email_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addnum_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void address_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Lv2_TextChanged(object sender, EventArgs e)
        {

        }

        private void sex_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            bb(e.RowIndex);
        }

        void bb(int RowIndex)
        {
            int a = int.Parse(RowIndex.ToString());
            if (a < 0) return;
            num.Text = dataGridView1.Rows[RowIndex].Cells[0].FormattedValue.ToString();
            major.Text = dataGridView1.Rows[RowIndex].Cells[1].FormattedValue.ToString();
            name.Text = dataGridView1.Rows[RowIndex].Cells[2].FormattedValue.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[RowIndex].Cells[3].FormattedValue.ToString();
            sex.Text = dataGridView1.Rows[RowIndex].Cells[4].FormattedValue.ToString();
            phone.Text = dataGridView1.Rows[RowIndex].Cells[5].FormattedValue.ToString();
            email.Text = dataGridView1.Rows[RowIndex].Cells[6].FormattedValue.ToString();
            textBox15.Text = dataGridView1.Rows[RowIndex].Cells[7].FormattedValue.ToString();
            addnum.Text = dataGridView1.Rows[RowIndex].Cells[8].FormattedValue.ToString();
            address.Text = dataGridView1.Rows[RowIndex].Cells[9].FormattedValue.ToString();
            comboBox3.Text = dataGridView1.Rows[RowIndex].Cells[10].FormattedValue.ToString();
            textBox21.Text = dataGridView1.Rows[RowIndex].Cells[11].FormattedValue.ToString();
            imgName.Text = dataGridView1.Rows[RowIndex].Cells[12].FormattedValue.ToString();

            string src = dataGridView1.Rows[RowIndex].Cells[12].Value.ToString();
            if (string.IsNullOrWhiteSpace(src))
            {

            }
            else if (src.Contains(':'))
            {
                pictureBox1.Image = Bitmap.FromFile(src);
            }
            else
            {
                //서버의 img 불러오기
                string serversrc = "http://l.bsks.ac.kr/~p201606023/image/" + src;
                pictureBox1.ImageLocation = serversrc;
            }

            if (sex.Text == "남")
            {
                Male.Checked = true;
            }
            else if (sex.Text == "여")
            {
                Female.Checked = true;
            }

            if (textBox15.Text == "신청")
            {
                dorm1.Checked = true;
            }
            else if (textBox15.Text == "미신청")
            {
                dorm2.Checked = true;
            }

            return;
        }

        private void tabControl3_Click(object sender, EventArgs e)
        {
            this.search1.PerformClick();
        }

        

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker10_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            asdf();
        }

        void asdf()
        {
            string sql1;
            sql1 = "select USER_PRO, DILL_STUDENT_NO, USER_MAJOR, USER_NAME, USER_PHONE," +
                "count(case when DILL_GUBUN = '지각' then 1 end)," +
                "count(case when DILL_GUBUN = '결근' then 1 end)," +
                "count(case when DILL_GUBUN = '조퇴' then 1 end)," +
                "count(case when DILL_GUBUN = '정상' then 1 end)," +
                "USER_IMAGE from Diligence inner join Users on DILL_STUDENT_NO = USER_STUDENT_NO where USER_PRO like '%" + textBox28.Text + "%' and USER_DATA = '승인됨' group by DILL_STUDENT_NO";
            dataGridView6.Rows.Clear();
            dataGridView6.Columns.Clear();
            dataGridView6.Columns.Add("USER_PRO", "실습명");
            dataGridView6.Columns.Add("DILL_STUDENT_NO", "학번");
            dataGridView6.Columns.Add("USER_MAJOR", "학과");
            dataGridView6.Columns.Add("USER_NAME", "이름");
            dataGridView6.Columns.Add("USER_PHONE", "전화번호");
            dataGridView6.Columns.Add("count(case when DILL_GUBUN = '지각' then 1 end)", "지각횟수");
            dataGridView6.Columns.Add("count(case when DILL_GUBUN = '결근' then 1 end)", "결근횟수");
            dataGridView6.Columns.Add("count(case when DILL_GUBUN = '조퇴' then 1 end)", "조퇴횟수");
            dataGridView6.Columns.Add("count(case when DILL_GUBUN = '정상' then 1 end)", "정상출근");
            dataGridView6.Columns.Add("USER_IMAGE", "이미지");
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                dataGridView6.Rows.Add();
                dataGridView6.Rows[i].Cells[0].Value = (string)reader["USER_PRO"];
                dataGridView6.Rows[i].Cells[1].Value = (string)reader["DILL_STUDENT_NO"];
                dataGridView6.Rows[i].Cells[2].Value = (string)reader["USER_MAJOR"];
                dataGridView6.Rows[i].Cells[3].Value = (string)reader["USER_NAME"];
                dataGridView6.Rows[i].Cells[4].Value = (string)reader["USER_PHONE"];
                dataGridView6.Rows[i].Cells[5].Value = (Int64)reader["count(case when DILL_GUBUN = '지각' then 1 end)"];
                dataGridView6.Rows[i].Cells[6].Value = (Int64)reader["count(case when DILL_GUBUN = '결근' then 1 end)"];
                dataGridView6.Rows[i].Cells[7].Value = (Int64)reader["count(case when DILL_GUBUN = '조퇴' then 1 end)"];
                dataGridView6.Rows[i].Cells[8].Value = (Int64)reader["count(case when DILL_GUBUN = '정상' then 1 end)"];
                dataGridView6.Rows[i].Cells[9].Value = (string)reader["USER_IMAGE"];
                i++;
            }
            reader.Close();
        }

        

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cc(e.RowIndex);
        }

        void cc(int RowIndex)
        {
            int a = int.Parse(RowIndex.ToString());
            if (a < 0) return;

            
            textBox16.Text = dataGridView6.Rows[RowIndex].Cells[0].FormattedValue.ToString();
            textBox10.Text = dataGridView6.Rows[RowIndex].Cells[1].FormattedValue.ToString();
            textBox4.Text = dataGridView6.Rows[RowIndex].Cells[2].FormattedValue.ToString();
            textBox12.Text = dataGridView6.Rows[RowIndex].Cells[3].FormattedValue.ToString();
            textBox8.Text = dataGridView6.Rows[RowIndex].Cells[4].FormattedValue.ToString();
            textBox9.Text = dataGridView6.Rows[RowIndex].Cells[5].FormattedValue.ToString();
            textBox11.Text = dataGridView6.Rows[RowIndex].Cells[6].FormattedValue.ToString();
            textBox13.Text = dataGridView6.Rows[RowIndex].Cells[7].FormattedValue.ToString();
            textBox27.Text = dataGridView6.Rows[RowIndex].Cells[8].FormattedValue.ToString();
            textBox26.Text = dataGridView6.Rows[RowIndex].Cells[9].FormattedValue.ToString();

            string src = dataGridView6.Rows[RowIndex].Cells[9].Value.ToString();
            if (string.IsNullOrWhiteSpace(src))
            {

            }
            else if (src.Contains(':'))
            {
                pictureBox3.Image = Bitmap.FromFile(src);
            }
            else
            {
                //서버의 img 불러오기
                string serversrc = "http://l.bsks.ac.kr/~p201606023/image/" + src;
                pictureBox3.ImageLocation = serversrc;
            }

            return;
        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            this.button1.PerformClick();
        }

        

        private void dataGridView7_CellContentClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

        }
        

        private void button5_Click_1(object sender, EventArgs e) //통계 버튼
        {
            if (dateTimePicker12.Value.Date > dateTimePicker11.Value.Date)
            {
                MessageBox.Show("종료연도가 시작연도보다 빠를 수 없습니다.");
                return;
            }
            string sql1;
            sql1 = "select P_YEAR, P_SEASON, P_NAME, USER_STUDENT_NO, USER_MAJOR, USER_NAME," +
                "count(case when DILL_GUBUN = '지각' then 1 end)," +
                "count(case when DILL_GUBUN = '결근' then 1 end)," +
                "count(case when DILL_GUBUN = '조퇴' then 1 end)," +
                "count(case when DILL_GUBUN = '정상' then 1 end)," +
                "TO_DAYS(date_format(now(), '%Y-%m-%d'))-TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_S), '%Y년%m월 %d일' ))," +
                "TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_F), '%Y년%m월 %d일' )) - TO_DAYS(date_format(now(), '%Y-%m-%d'))," +
                "TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_F), '%Y년%m월 %d일' ))-TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_S), '%Y년%m월 %d일' )) " +
                "from Diligence inner join Users on DILL_STUDENT_NO = USER_STUDENT_NO inner join PRO " +
                "on concat(P_YEAR,'-',P_SEASON, ':', P_NAME) = USER_PRO " +
                "where P_YEAR >= '" + dateTimePicker12.Text + "' and P_YEAR <='" + dateTimePicker11.Text + "' and P_SEASON like '" + seasontxt4.Text + "' " +
                "and "+textBox24.Text+" like '%" + textBox23.Text + "%' and USER_DATA = '승인됨' group by USER_STUDENT_NO";
            dataGridView7.Rows.Clear();
            dataGridView7.Columns.Clear();
            dataGridView7.Columns.Add("P_YEAR", "연도");
            dataGridView7.Columns.Add("P_SEASON", "계절");
            dataGridView7.Columns.Add("P_NAME", "실습명");
            dataGridView7.Columns.Add("USER_STUDENT_NO", "학번");
            dataGridView7.Columns.Add("USER_MAJOR", "학과");
            dataGridView7.Columns.Add("USER_NAME", "이름");
            dataGridView7.Columns.Add("count(case when DILL_GUBUN = '지각' then 1 end)", "지각횟수");
            dataGridView7.Columns.Add("count(case when DILL_GUBUN = '결근' then 1 end)", "결근횟수");
            dataGridView7.Columns.Add("count(case when DILL_GUBUN = '조퇴' then 1 end)", "조퇴횟수");
            dataGridView7.Columns.Add("count(case when DILL_GUBUN = '정상' then 1 end)", "정상출근");
            dataGridView7.Columns.Add("TO_DAYS(date_format(now(), '%Y-%m-%d'))-TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_S), '%Y년%m월 %d일' ))", "실습한기간");
            dataGridView7.Columns.Add("TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_F), '%Y년%m월 %d일' )) - TO_DAYS(date_format(now(), '%Y-%m-%d'))", "남은기간");
            dataGridView7.Columns.Add("TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_F), '%Y년%m월 %d일' ))-TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_S), '%Y년%m월 %d일' ))", "실습총기간");
            
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {

                dataGridView7.Rows.Add();
                dataGridView7.Rows[i].Cells[0].Value = (string)reader["P_YEAR"];
                dataGridView7.Rows[i].Cells[1].Value = (string)reader["P_SEASON"];
                dataGridView7.Rows[i].Cells[2].Value = (string)reader["P_NAME"];
                dataGridView7.Rows[i].Cells[3].Value = (string)reader["USER_STUDENT_NO"];
                dataGridView7.Rows[i].Cells[4].Value = (string)reader["USER_MAJOR"];
                dataGridView7.Rows[i].Cells[5].Value = (string)reader["USER_NAME"];
                dataGridView7.Rows[i].Cells[6].Value = (Int64)reader["count(case when DILL_GUBUN = '지각' then 1 end)"];
                dataGridView7.Rows[i].Cells[7].Value = (Int64)reader["count(case when DILL_GUBUN = '결근' then 1 end)"];
                dataGridView7.Rows[i].Cells[8].Value = (Int64)reader["count(case when DILL_GUBUN = '조퇴' then 1 end)"];
                dataGridView7.Rows[i].Cells[9].Value = (Int64)reader["count(case when DILL_GUBUN = '정상' then 1 end)"];
                dataGridView7.Rows[i].Cells[10].Value = (string)reader["TO_DAYS(date_format(now(), '%Y-%m-%d'))-TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_S), '%Y년%m월 %d일' ))"].ToString();
                dataGridView7.Rows[i].Cells[11].Value = (string)reader["TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_F), '%Y년%m월 %d일' )) - TO_DAYS(date_format(now(), '%Y-%m-%d'))"].ToString();
                dataGridView7.Rows[i].Cells[12].Value = (string)reader["TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_F), '%Y년%m월 %d일' ))-TO_DAYS(str_to_date(concat(P_YEAR, P_DAY_S), '%Y년%m월 %d일' ))"].ToString();

                i++;
            }
            reader.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox5.Text = System.DateTime.Now.ToString("HH:mm");
            textBox6.Text = "";
            button23.Enabled = true;
            button24.Enabled = false;
            button11.Enabled = true;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox6.Text = System.DateTime.Now.ToString("HH:mm");
            textBox5.Text = "";
            button24.Enabled = true;
            button23.Enabled = false;
            button11.Enabled = true;
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            button23.Enabled = true;
            button24.Enabled = true;
            button11.Enabled = false;
            button10.Enabled = true;
            textBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dilgubun.Text = "정상";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            dilgubun.Text = "조퇴";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            dilgubun.Text = "결근";
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            String txt = comboBox5.SelectedItem as String;

            if (txt == "동계")
            {
                seasontxt4.Text = "동계";
                return;
            }
            else if (txt == "하계")
            {
                seasontxt4.Text = "하계";
                return;
            }
            else if (txt == "전체")
            {
                seasontxt4.Text = "%계";
                return;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            String txt = comboBox6.SelectedItem as String;

            if (txt == "학번")
            {
                textBox24.Text = "USER_STUDENT_NO";
                return;
            }
            else if (txt == "학과")
            {
                textBox24.Text = "USER_MAJOR";
                return;
            }
            else if (txt == "이름")
            {
                textBox24.Text = "USER_NAME";
                return;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            dilgubun.Text = "지각";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label59.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
    
