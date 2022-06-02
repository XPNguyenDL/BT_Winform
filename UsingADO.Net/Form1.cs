using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsingADO.Net
{
    public partial class frmQLSV : Form
    {
        List<Lop> dsLop = new List<Lop>();
        List<SinhVien> dsSinhVien = new List<SinhVien>();

        public frmQLSV()
        {
            InitializeComponent();
        }



        private void FormLoad()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            GetListLop(conn);
            GetSinhVien_LV(conn);
            conn.Dispose();
        }

        private void DisplaySinhVien(SqlDataReader reader)
        {
            lvSinhVien.Items.Clear();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                lvSinhVien.Items.Add(item);
                item.SubItems.Add(reader["HoTen"].ToString());
                item.SubItems.Add(reader["TenLop"].ToString());
                dsSinhVien.Add(new SinhVien(int.Parse(reader["ID"].ToString()), reader["HoTen"].ToString(), reader["TenLop"].ToString()));
            }

        }

        private void GetSinhVien_LV(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select sinhvien.ID, HoTen, lop.TenLop \r\nFrom dbo.SinhVien, dbo.Lop\r\nwhere Sinhvien.MaLop = lop.ID";
            conn.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            this.DisplaySinhVien(sqlDataReader);
            conn.Close();
        }

        private void GetListLop(SqlConnection conn)
        {

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * \r\nFrom dbo.Lop";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Lop");
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                dsLop.Add(new Lop(row));
            }
            conn.Close();
            cboLop.DisplayMember = "TenLop"; // Hiển thị lên ComboBox
            cboLop.ValueMember = "Id"; // Khi chọn thì sẽ lưu ID
            cboLop.DataSource = dsLop;
        }

        private void SinhVien_LV()
        {
            ListViewItem item = lvSinhVien.SelectedItems[0];
            txtID.Text = item.Text;
            txtHoTen.Text = item.SubItems[1].Text;
            cboLop.Text = item.SubItems[2].Text;

        }



        private void frmQLSV_Load(object sender, EventArgs e)
        {
            FormLoad();

        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtHoTen.Text = "";
            txtID.Text = "";
            cboLop.Text = "";

        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvSinhVien_Click(object sender, EventArgs e)
        {
            SinhVien_LV();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                //var resultList = dsSinhVien.Where(sv =>
                //    (sv.Name.IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) > 1)).ToList();
                var resultList = dsSinhVien.Where(sv => sv.Name.IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) > 1).ToList();
                LoadListView(resultList);
            }
            else
            {
                LoadListView(dsSinhVien);
            }
        }

        private void LoadListView(List<SinhVien> listSinhVien)
        {
            lvSinhVien.Items.Clear();
            foreach (SinhVien sinhVien in listSinhVien)
            {
                AddSinhVienToListView(sinhVien);
            }
        }

        private void AddSinhVienToListView(SinhVien sinhVien)
        {
            string[] row = { sinhVien.ID.ToString(), sinhVien.Name, sinhVien.Lop.ToString() };
            var item = new ListViewItem(row);
            lvSinhVien.Items.Add(item);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(cboLop.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                }
                else
                {
                    UpdateSV(conn);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(cboLop.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                }
                else
                {
                    AddSV(conn);
                    
                }
            }
        }

        private void AddSV(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            var temp = dsLop.Where(x => x.TenLop == cboLop.Text);
            string idLop = "";
            foreach (var lop in temp)
            {
                idLop = lop.Id.ToString();
                break;
                
            }

            if (string.IsNullOrEmpty(idLop))
            {
                MessageBox.Show("Vui lòng nhập đúng thông tin lớp!", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "Insert Into SinhVien(HoTen, MaLop)" +
                              "Values (N'" + txtHoTen.Text + "', " + idLop + ")";

            conn.Open();

            int numOfRowsEffected = cmd.ExecuteNonQuery();

            conn.Close();

            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm sinh viên thành công!!", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Thêm sinh viên thất bại!!", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            FormLoad();
        }

        private void UpdateSV(SqlConnection conn)
        {
            var temp = dsLop.Where(x => x.TenLop == cboLop.Text);
            string idLop = "";
            foreach (var lop in temp)
            {
                idLop = lop.Id.ToString();
                break;

            }

            if (string.IsNullOrEmpty(idLop))
            {
                MessageBox.Show("Vui lòng nhập đúng thông tin lớp!", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = conn.CreateCommand();
            

            cmd.CommandText = "Update SinhVien set HoTen = N'" + txtHoTen.Text + "'," +
                              "MaLop = " + idLop +
                              "Where ID = " + txtID.Text;

            conn.Open();

            int numOfRowsEffected = cmd.ExecuteNonQuery();

            conn.Close();

            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Cập nhật thành công!!", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!!", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            FormLoad();
        }

    }

}
