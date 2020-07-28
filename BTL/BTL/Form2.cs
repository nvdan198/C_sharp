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

namespace BTL
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BtnDN_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //string sqltk = "select count(*) where MaVT ='" + txtMaVT.Text + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "KT_DN";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@TenDN", txtTk.Text);
            cmd.Parameters.AddWithValue("@MK", txtMk.Text);
            
            try
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    this.Hide();
                    Form1 frmOne = new Form1();
                    frmOne.FormClosed += new FormClosedEventHandler(form1_FormClosed);
                    frmOne.LayDL(txtTk.Text);
                    frmOne.Show();
                }
                else MessageBox.Show("Đăng nhập thất bại!", "Cảnh báo");
            }
            catch
            {
                MessageBox.Show("Máy chủ hiện đang bận!", "Thông báo");
            }

            conn.Close();
            txtTk.Clear();
            txtMk.Clear();
        }

        //Hàm đóng Form 1(QLPhong) để mở lại form đăng nhập
        void form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true;
        }
        /*Hàm lấy tên TK
        internal string LayTenTK()
        {
            return tenTK;
        }*/
    }
}
