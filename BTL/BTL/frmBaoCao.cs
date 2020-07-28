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
using Microsoft.Reporting.WinForms;

namespace BTL
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void FrmBaoCao_Load(object sender, EventArgs e)
        {
            load_Data();
            this.rpvBaoCaoVatTu.RefreshReport();
        }

        private void BtnNewReport_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.QLVatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand();
            //if(cboThongKeTinhTrang.Text.Trim() == "Không")
            if (cboThongKePhong.Text.Trim() != "Tất cả")
            {
                if(cboThongKeTinhTrang.Text.Trim() != "Không")
                {
                    cmd.CommandText = "BCSLVTTheoPHONGVATINHTRANG";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@TenPhong", cboThongKePhong.Text);
                    cmd.Parameters.AddWithValue("@TinhTrang", cboThongKeTinhTrang.Text);
                }
                else
                {
                    cmd.CommandText = "BCSLVATTUTRONGMOTPHONG";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@TenPhong", cboThongKePhong.Text);
                }
                //Đổ dữ liệu vào dataset
                DataSet ds = new DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(ds);
                //thiết lập báo cáo
                rpvBaoCaoVatTu.ProcessingMode = ProcessingMode.Local;
                rpvBaoCaoVatTu.LocalReport.ReportPath = "rptSLVT.rdlc";
            
                ReportDataSource rds = new ReportDataSource("dsVatTuSL", ds.Tables[0]);

                //Gắn lên mẫu báo cáo
                rpvBaoCaoVatTu.LocalReport.DataSources.Clear();
                rpvBaoCaoVatTu.LocalReport.DataSources.Add(rds);
                rpvBaoCaoVatTu.RefreshReport();
            }
            else
            {
                if(cboThongKeTinhTrang.Text.Trim() == "Không")
                {
                    cmd.CommandText = "BCTONGSL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    //Đổ dữ liệu vào dataset
                    DataSet ds = new DataSet();
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
                    //thiết lập báo cáo
                    rpvBaoCaoVatTu.ProcessingMode = ProcessingMode.Local;
                    rpvBaoCaoVatTu.LocalReport.ReportPath = "rptTongSLVT.rdlc";

                    ReportDataSource rds = new ReportDataSource("tongVT", ds.Tables[0]);

                    //Gắn lên mẫu báo cáo
                    rpvBaoCaoVatTu.LocalReport.DataSources.Clear();
                    rpvBaoCaoVatTu.LocalReport.DataSources.Add(rds);
                    rpvBaoCaoVatTu.RefreshReport();
                }
                else
                {
                    cmd.CommandText = "BCSLVTTHEOTINHTRANG";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@TinhTrang", cboThongKeTinhTrang.Text);

                    //Đổ dữ liệu vào dataset
                    DataSet ds = new DataSet();
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
                    //thiết lập báo cáo
                    rpvBaoCaoVatTu.ProcessingMode = ProcessingMode.Local;
                    rpvBaoCaoVatTu.LocalReport.ReportPath = "rptSLVT.rdlc";

                    ReportDataSource rds = new ReportDataSource("dsVatTuSL", ds.Tables[0]);

                    //Gắn lên mẫu báo cáo
                    rpvBaoCaoVatTu.LocalReport.DataSources.Clear();
                    rpvBaoCaoVatTu.LocalReport.DataSources.Add(rds);
                    rpvBaoCaoVatTu.RefreshReport();
                }
            }
        }

        private void load_Data()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Properties.Settings.Default.QLVatTuConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = "Select distinct(TenPhong) from VATTU ";
            string sqlTT = "select distinct(TinhTrang) from VATTU";
            SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            DataTable dtsl = new DataTable();

            DataRow dr;
            dap.Fill(dt);
            SqlDataAdapter daptt = new SqlDataAdapter(sqlTT, conn);
            daptt.Fill(dtsl);

            dr = dt.NewRow();
            dr[0] = "Tất cả";
            dt.Rows.Add(dr);

            DataRow drt = dtsl.NewRow();
            drt[0] = "Không";
            //dr[0] = "Chọn";
            //DataRow drtt = dtsl.NewRow();
            //drtt[0] = "Tất cả";
            //dtsl.Rows.Add(drtt);
            //drtt[0] = "Tất cả";
            //drt = "Tất cả";
            dtsl.Rows.InsertAt(drt, 0);
            //Đổ vào trong 2 combobox
            cboThongKePhong.DisplayMember = "TenPhong";
            cboThongKePhong.ValueMember = "TenPhong";
            cboThongKePhong.DataSource = dt;

            cboThongKeTinhTrang.DisplayMember = "TinhTrang";
            cboThongKeTinhTrang.ValueMember = "TinhTrang";
            cboThongKeTinhTrang.DataSource = dtsl;
            //drt[dtsl.Rows.Count] = "Tất cả";
   
            conn.Close();
            
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
