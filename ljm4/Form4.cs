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
    public partial class Form4 : MetroForm
    {
        private static string mysql_str = "server=l.bsks.ac.kr;Database=p201606023;Uid=p201606023;Pwd=pp201606023;Charset=utf8";
        MySqlConnection conn = new MySqlConnection(mysql_str);

        MySqlCommand cmd;
        MySqlDataReader reader;

        public Form4()
        {
            while (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();

            textBox1.Text = "미승인";
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
        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Male_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Female_CheckedChanged(object sender, EventArgs e)
        {
            
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

        private void button7_Click(object sender, EventArgs e)
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (num.Text == "")
            {
                MessageBox.Show("학번을 입력하세요");
                return;
            }
            if (major.Text == "")
            {
                MessageBox.Show("학번을 입력하세요");
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
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("이미지를 넣어주세요");
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("실습을 선택해주세요.");
                return;
            }
            string src = num.Text + "." + imgName.Text.Substring(imgName.Text.LastIndexOf('.') + 1);
            string sql1 = "select * from Users where USER_STUDENT_NO = '" + num.Text + "'";
            string sql2 = "insert into Users(USER_STUDENT_NO, USER_MAJOR, USER_NAME, " +
                "USER_BIRTH, USER_SEX, USER_PHONE, USER_MAIL, USER_DORM, USER_ADDNUM," +
                "USER_ADDR, USER_IMAGE, USER_PRO, USER_DATA)";
            sql2 = sql2 + "values('" + num.Text + "', '"+major.Text + "', '" + name.Text + "', " +
                "'" + dateTimePicker2.Text + "', '" + sex.Text + "', '" + phone.Text + "', " +
                "'" + email.Text + "', '" + dormtxt.Text + "','" + addnum.Text + "'," +
                "'" + address.Text + "', '" + src + "','"+comboBox1.Text + "', '" + textBox1.Text + "')";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("학번이 이미 존재합니다.");
                reader.Close();
            }

            else
            {
                reader.Close();
                cmd = new MySqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();

                Uploadimg(imgName.Text, imgName.Text.Substring(imgName.Text.LastIndexOf('.') + 1), num.Text);

                MessageBox.Show("신청을 완료하였습니다. \n관리자의 승인을 기다리세요.", "신청 완료");

                num.Text = "";
                major.Text = "";
                name.Text = "";
                dateTimePicker2.Text = "";
                phone.Text = "";
                email.Text = "";
                dormtxt.Text = "";
                addnum.Text = "";
                address.Text = "";
                sex.Text = "";
                comboBox1.Text = "";
                Male.Checked = false;
                Female.Checked = false;
                dorm1.Checked = false;
                dorm2.Checked = false;
                pictureBox1.Image = null;
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sex_TextChanged(object sender, EventArgs e)
        {

        }

        private void imgName_TextChanged(object sender, EventArgs e)
        {

        }

        public static string fname;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string sql1 = "select concat(P_YEAR,'-',P_SEASON, ':', P_NAME) from PRO where P_DAY_S > date_format(now(), '%m월 %d일')";
            cmd = new MySqlCommand(sql1, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void dorm1_CheckedChanged(object sender, EventArgs e)
        {
            dormtxt.Text = "신청";
        }

        private void dorm2_CheckedChanged(object sender, EventArgs e)
        {
            dormtxt.Text = "미신청";
        }

        private void Male_CheckedChanged_1(object sender, EventArgs e)
        {
            sex.Text = "남";
        }

        private void Female_CheckedChanged_1(object sender, EventArgs e)
        {
            sex.Text = "여";
        }
    }
}
