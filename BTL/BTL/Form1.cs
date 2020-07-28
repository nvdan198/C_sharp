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
//using System.Drawing;

namespace BTL
{
    public partial class Form1 : Form
    {
        private string LayTenTK;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblHienTenDN.Text = "Xin chào " + LayTenTK;
            loadData();
            lblXoaMaVT.Text = "";
        }
        private void loadData()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = "select * from PHONG";
            SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            DataTable dtp = new DataTable();
            DataRow dr;
            dr = dt.NewRow();

            dap.Fill(dt);
            dap.Fill(dtp);

            dr[0] = "Chọn";
            dt.Rows.InsertAt(dr, 0);
            cboTKPhong.DisplayMember = "TenPhong";
            cboTKPhong.ValueMember = "TenPhong";
            cboTKPhong.DataSource = dt;


            //Đổ dữ liệu vào combobox Phong
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "TenPhong";
            cboPhong.DataSource = dtp;


            /*string sqlhien = "select MaVT as 'Mã Vật Tư' , TenVT as 'Tên Vật Tư', SoLuong as 'Số Lượng', TinhTrang as 'Tình Trạng', DonVi as 'Đơn Vị', GhiChu as 'Ghi Chú' from VATTU where TenPhong = '" + cboTKPhong.Text + "'";
            dap.SelectCommand = new SqlCommand(sqlhien, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            //Hiển thị dữ liệu
            dgvKetQua.DataSource = ds.Tables[0];
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.Refresh();*/
            conn.Close();
        }

        private void CboTKPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienAn_NutXoa(cboTKPhong.Text);
            cboTKPhong_upload(cboTKPhong.Text);
            /* SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sqlhien = "select MaVT as 'Mã Vật Tư' , TenVT as 'Tên Vật Tư', SoLuong as 'Số Lượng', TinhTrang as 'Tình Trạng', DonVi as 'Đơn Vị', GhiChu as 'Ghi Chú' from VATTU where TenPhong = '" + cboTKPhong.Text + "'";
            SqlDataAdapter dap = new SqlDataAdapter(sqlhien, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            //Hiển thị dữ liệu
            dgvKetQua.DataSource = ds.Tables[0];
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.Refresh();
            conn.Close();*/
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Khai báo biến để bắt lỗi
            bool validateMaVT = Txt_Validating(txtMaVT);
            bool validateTenVT = Txt_Validating(txtTenVT);
            bool validateSL = TxtktdangSL_Validating();
            bool validateTT = Txt_Validating(txtTT);
            bool validateDonVi = Txt_Validating(txtDonVi);
            bool kiemTraTrungKhoa = KiemTraTrungKhoa(txtMaVT);

            if (validateMaVT || validateTenVT || validateSL || validateTT || validateDonVi || kiemTraTrungKhoa)
            {

                //MessageBox.Show("Bạn không được để trống mã vật tư!");
                //txtMaVT.BackColor = Color.Red;
                return;
            }
            /*else if(KiemTraTrungKhoa(txtMaVT))
            {
                return;
            }*/
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InsertVatTu";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //Kiểm tra ghi chú
                if (txtGhiChu.Text.Trim() == "") txtGhiChu.Text = "Không có";

                //Gắn dữ liệu ở phần giao diện vào phần mềm
                cmd.Parameters.AddWithValue("@MaVT", txtMaVT.Text);
                cmd.Parameters.AddWithValue("@TenVT", txtTenVT.Text);
                cmd.Parameters.AddWithValue("@SoLuong", txtSL.Text);
                cmd.Parameters.AddWithValue("@TinhTrang", txtTT.Text);
                cmd.Parameters.AddWithValue("@DonVi", txtDonVi.Text);
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                cmd.Parameters.AddWithValue("@TenPhong", cboPhong.Text);
                //Thực thi câu lệnh sau khi đã thêm các dữ liệu
                cmd.ExecuteNonQuery();
            }
            //Đóng kết nối
            conn.Close();

            //Thiết lập lại các thuộc tính về ban đầu
            xoa_DuLieu();
            txtMaVT.Clear();

            //Tải lại trang
            loadData();
        }

        private void CboPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboUpdateMaVT.Visible)
            //{
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = "select MaVT from VATTU where TenPhong ='" + cboPhong.Text + "'";
                SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                dap.Fill(dt);
                DataRow dr = dt.NewRow();
                dr[0] = "<>";

                dt.Rows.InsertAt(dr, 0);
                //Đổ dữ liệu vào combobox mã vật tư
                cboUpdateMaVT.DisplayMember = "MaVT";
                cboUpdateMaVT.ValueMember = "MaVT";
                cboUpdateMaVT.DataSource = dt;

                //Đóng kết nối
                conn.Close();
            //}
            //else return;
        }
        //private bool BtnSua_WasClick = false;

        private void BtnSua_Click(object sender, EventArgs e)
        {
            /*SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = "select * from PHONG";
            SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            DataTable dtp = new DataTable();
            DataRow dr;
            dr = dt.NewRow();

            dap.Fill(dt);

            dr[0] = "Chọn";
            dt.Rows.InsertAt(dr, 0);
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "TenPhong";
            cboPhong.DataSource = dt;*/

            //Hiện các nút 
            cboUpdateMaVT.Visible = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            //Kiểm tra bắt lỗi
            if (txtMaVT.BackColor == Color.Red)
            {
                txtMaVT.BackColor = Color.White;
                errP.SetError(txtMaVT, "");
            }
            if (txtTenVT.BackColor == Color.Red)
            {
                txtTenVT.BackColor = Color.White;
                errP.SetError(txtTenVT, "");
            }

            if (txtSL.BackColor == Color.Red || TxtktdangSL_Validating())
            {
                txtSL.BackColor = Color.White;
                errP.SetError(txtSL, "");
            }

            if (txtTT.BackColor == Color.Red)
            {
                txtTT.BackColor = Color.White;
                errP.SetError(txtTT, "");
            }

            if (txtDonVi.BackColor == Color.Red)
            {
                txtDonVi.BackColor = Color.White;
                errP.SetError(txtDonVi, "");
            }

            //conn.Close();
            //Xóa dữ liệu
            xoa_DuLieu();
            txtMaVT.Clear();
        }

        private void CboUpdateMaVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = "select * from VATTU where MaVT = '" + cboUpdateMaVT.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader sqldata = cmd.ExecuteReader();

            if (sqldata.Read())
            {
                txtTenVT.Text = sqldata[1].ToString();
                txtSL.Text = sqldata[2].ToString();
                txtTT.Text = sqldata[3].ToString();
                txtDonVi.Text = sqldata[4].ToString();
                txtGhiChu.Text = sqldata[5].ToString();
            }
            else if(cboUpdateMaVT.Visible)
            {
                xoa_DuLieu();
            }
            conn.Close();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            bool validateTenVT = Txt_Validating(txtTenVT);
            bool validateSL = TxtktdangSL_Validating();
            bool validateTT = Txt_Validating(txtTT);
            bool validateDonVi = Txt_Validating(txtDonVi);
            bool kiemTraTrungKhoa = KiemTraTrungKhoa(txtMaVT);

            // Kiểm tra xem người dùng có chọn khóa không
            if (cboUpdateMaVT.Text.Trim() == "<>")
            {
                errP.SetError(cboUpdateMaVT, "Chọn mã vật tư muốn sửa");
                cboUpdateMaVT.BackColor = Color.Red;
                return;
            }
            else if (validateTenVT || validateSL || validateTT || validateDonVi || kiemTraTrungKhoa)
            {
                return;
            }
            else  errP.SetError(cboUpdateMaVT, "");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateVatTu";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            //Gắn dữ liệu ở phần giao diện vào phần mềm
            cmd.Parameters.AddWithValue("@MaVT", cboUpdateMaVT.Text);
            cmd.Parameters.AddWithValue("@TenVT", txtTenVT.Text);
            cmd.Parameters.AddWithValue("@SoLuong", txtSL.Text);
            cmd.Parameters.AddWithValue("@TinhTrang", txtTT.Text);
            cmd.Parameters.AddWithValue("@DonVi", txtDonVi.Text);
            cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
            cmd.Parameters.AddWithValue("@TenPhong", cboPhong.Text);
            //Thực thi câu lệnh sau khi đã thêm các dữ liệu
            cmd.ExecuteNonQuery();

            //Hiển thị lại phần vừa mới sửa lên lưới kết quả
            //string sqlhien = "select MaVT as 'Mã Vật Tư' , TenVT as 'Tên Vật Tư', SoLuong as 'Số Lượng', TinhTrang as 'Tình Trạng', DonVi as 'Đơn Vị', GhiChu as 'Ghi Chú' from VATTU where TenPhong = '" + cboTKPhong.Text + "'";
            //SqlDataAdapter dap = new SqlDataAdapter(sqlhien, conn);
            //DataSet ds = new DataSet();
            //dap.Fill(ds,"Sua");
            //Hiển thị dữ liệu
            //dgvKetQua.DataSource = ds.Tables["Sua"];
            //dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvKetQua.Refresh();

            //Đóng kết nối
            conn.Close();
            //Tải lại trang
            cboTKPhong_upload(cboPhong.Text);
            
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            //Đưa các ô về trắng
            xoa_DuLieu();
            cboUpdateMaVT.Text = cboUpdateMaVT.GetItemText(cboUpdateMaVT.Items[0]);
            cboUpdateMaVT.Visible = false;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            //btnXoa.Enabled = true;

            if (txtTenVT.BackColor == Color.Red)
            {
                txtTenVT.BackColor = Color.White;
                errP.SetError(txtTenVT, "");
            }

            if (txtSL.BackColor == Color.Red || TxtktdangSL_Validating())
            {
                txtSL.BackColor = Color.White;
                errP.SetError(txtSL, "");
            }

            if (txtTT.BackColor == Color.Red)
            {
                txtTT.BackColor = Color.White;
                errP.SetError(txtTT, "");
            }

            if (txtDonVi.BackColor == Color.Red)
            {
                txtDonVi.BackColor = Color.White;
                errP.SetError(txtDonVi, "");
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            if ((txtTKMaVT.Text.Trim() != "") && (txtTKTenVT.Text.Trim() != ""))
            {
                cmd.CommandText = "TimKiemVatTu_And";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //Truyền dữ liệu để tìm kiếm
                cmd.Parameters.AddWithValue("@MaVT", txtTKMaVT.Text);
                cmd.Parameters.AddWithValue("@TenVT", txtTKTenVT.Text);
            }
            else if (txtTKMaVT.Text.Trim() != "")
            {
                cmd.CommandText = "TimKiemVatTu_MaVT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //Truyền dữ liệu để tìm kiếm
                cmd.Parameters.AddWithValue("@MaVT", txtTKMaVT.Text);
            }
            else if (txtTKTenVT.Text.Trim() != "")
            {
                cmd.CommandText = "TimKiemVatTu_TenVT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //Truyền dữ liệu để tìm kiếm
                cmd.Parameters.AddWithValue("@TenVT", txtTKTenVT.Text);
            }
            else
            {
                cmd.CommandText = "TimKiemVatTu_All";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
            }
            //Chạy thủ tục

            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dap.Fill(ds, "TimKiem");
            //Hiển thị dữ liệu
            dgvKetQua.DataSource = ds.Tables["TimKiem"];
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.Refresh();

            conn.Close();
        }

        private void xoa_DuLieu()
        {
            txtTenVT.Clear();
            txtSL.Clear();
            txtTT.Clear();
            txtDonVi.Clear();
            txtGhiChu.Clear();
        }

        private void cboTKPhong_upload(string cbo)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            /*if (cbo == "Chọn")
            {
               sqlhien = "select MaVT as 'Mã Vật Tư' , TenVT as 'Tên Vật Tư', SoLuong as 'Số Lượng', TinhTrang as 'Tình Trạng', DonVi as 'Đơn Vị', GhiChu as 'Ghi Chú' from VATTU where TenPhong = '" + cboTKPhong.Text + "'";

            }*/

            string sqlhien = "select MaVT as 'Mã Vật Tư' , TenVT as 'Tên Vật Tư', SoLuong as 'Số Lượng', TinhTrang as 'Tình Trạng', DonVi as 'Đơn Vị', GhiChu as 'Ghi Chú' from VATTU where TenPhong = '" + cbo + "'";

            SqlDataAdapter dap = new SqlDataAdapter(sqlhien, conn);
            DataSet ds = new DataSet();
            cboTKPhong.Text = cbo;
            dap.Fill(ds);
            //Hiển thị dữ liệu
            dgvKetQua.DataSource = ds.Tables[0];
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.Refresh();
            conn.Close();
        }

        //Thiết lập lại nền cho các ô textbox khi có dữ liệu
        private void TxtMaVT_TextChanged(object sender, EventArgs e)
        {
            if (txtMaVT.BackColor == Color.Red)
            {
                txtMaVT.BackColor = Color.White;
                errP.SetError(txtMaVT, "");
            }
        }

        private void TxtTenVT_TextChanged(object sender, EventArgs e)
        {
            if (txtTenVT.BackColor == Color.Red)
            { 
                txtTenVT.BackColor = Color.White;
                errP.SetError(txtTenVT, "");
            }
        }
        private void TxtSL_TextChanged(object sender, EventArgs e)
        {
            if (txtSL.BackColor == Color.Red || TxtktdangSL_Validating()) 
            { 
                txtSL.BackColor = Color.White;
                errP.SetError(txtSL, "");
            }
        }

        private void TxtTT_TextChanged(object sender, EventArgs e)
        {
            if (txtTT.BackColor == Color.Red)
            {
                txtTT.BackColor = Color.White;
                errP.SetError(txtTT, "");
            }
        }

        private void TxtDonVi_TextChanged(object sender, EventArgs e)
        {
            if (txtDonVi.BackColor == Color.Red)
            {
                txtDonVi.BackColor = Color.White;
                errP.SetError(txtDonVi, "");
            }
        }

        private void CboUpdateMaVT_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboUpdateMaVT.BackColor == Color.Red)
            {
                cboUpdateMaVT.BackColor = Color.White;
                errP.SetError(cboUpdateMaVT, "");

                //xoa_DuLieu();
            }
        }

        //Thêm bắt lỗi không nhập dữ liệu
        /*private void TxtMaVT_Validating(object sender, CancelEventArgs e)
        {
            Txt_Validating(txtMaVT);
        }*/
        private bool Txt_Validating(TextBox textb)
        {
            bool bStatus = false;
            if (textb.Text.Trim() == "")
            {
                errP.SetError(textb, "Không được để trống");
                textb.BackColor = Color.Red;
                bStatus = true;
            }
            else errP.SetError(textb, "");
            return bStatus;
        }

        private bool TxtktdangSL_Validating()
        {
            bool bStatus = false;
            if (txtSL.Text.Trim() == "")
            {
                errP.SetError(txtSL, "Không được để trống");
                txtSL.BackColor = Color.Red;
                bStatus = true;
            }
            else
            {
                errP.SetError(txtSL, "");
                try
                {
                    int kt = int.Parse(txtSL.Text);
                    errP.SetError(txtSL, "");
                    if (kt <= 0)
                    {
                        txtSL.BackColor = Color.Red;
                        errP.SetError(txtSL, "Số lượng phải lớn hơn 0");
                        bStatus = true;
                    }
                    else errP.SetError(txtSL, "");
                }
                catch
                {
                    txtSL.BackColor = Color.Red;
                    errP.SetError(txtSL, "Dữ liệu phải là con số");
                    bStatus = true;
                }
            }

            return bStatus;       
        }

        private bool KiemTraTrungKhoa(TextBox MaVT)
        {
            bool bStatus = false;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //string sqltk = "select count(*) where MaVT ='" + txtMaVT.Text + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TimKiemVatTu_MaVT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@MaVT", MaVT.Text);

            try
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    MaVT.BackColor = Color.Red;
                    errP.SetError(MaVT, "Khóa đã tồn tại");
                    bStatus = true;
                }
                else bStatus = false;
            }
            catch
            {
                MessageBox.Show("Máy chủ hiện đang bận!", "Thông báo");
            }
            conn.Close();
            return bStatus;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            if (lblXoaMaVT.Text == "")
            {
                MessageBox.Show("Vui lòng chọn đúng dòng vật tư muốn xóa trong phần Danh sách vật tư","Cảnh Báo");
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn xóa mã vật tư: " + lblXoaMaVT.Text + "không?", "Xóa sản phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DeleteVatTu";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@MaVT", lblXoaMaVT.Text);

                cmd.ExecuteNonQuery();
                cboTKPhong_upload(cboTKPhong.Text);
                HienAn_NutXoa(cboTKPhong.Text);
                lblXoaMaVT.Text = "";
            }
            else lblXoaMaVT.Text = "";

            //Đóng kết nối
            conn.Close();
        }

        private void DgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //lblXoaMaVT.Text = dgvKetQua[e.RowIndex,e.ColumnIndex].Value.ToString();
            //try
            // {
            if (dgvKetQua.Rows.Count == 1)
                return;
            else if (e.RowIndex >= 0 && e.ColumnIndex >=0 && e.RowIndex < dgvKetQua.RowCount)
            { 
                lblXoaMaVT.Text = dgvKetQua[0, e.RowIndex].Value.ToString();
            }
            // catch
            // {
            //  MessageBox.Show("Bạn phải chọn đúng hàng để xóa!", "Cảnh báo");
            // }
        }

        private void HienAn_NutXoa(string tenPhong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.VatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TimKiemPhong";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TenPhong", tenPhong);

            //Lấy dữ liệu để so sánh
            int n = (int)cmd.ExecuteScalar();

            if (n == 0) btnXoa.Enabled = false;
            else btnXoa.Enabled = true;
        }

        //Hàm lấy dữ liệu từ form đăng nhập
        internal string LayDL( string txt)
        {
            return LayTenTK = txt;
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            frmBaoCao frmBC = new frmBaoCao();
            frmBC.Show();
        }
    }
}
// (xong)(bắt lỗi trùng khóa) xóa còn thoát