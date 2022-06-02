using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDBFirst
{
    public partial class frmQLSV : Form
    {
        QLSinhVienEntities db = new QLSinhVienEntities();
        public frmQLSV()
        {
            InitializeComponent();
        }

        private void frmQLSV_Load(object sender, EventArgs e)
        {
            var temp = db.SinhViens.Where(s => s.ID > 0).ToList();


            foreach (var sinhVien in temp)
            {
                ListViewItem item = new ListViewItem(sinhVien.ID.ToString());
                lvSinhVien.Items.Add(item);
                item.SubItems.Add(sinhVien.HoTen);
                

                item.SubItems.Add(sinhVien.Lop.TenLop);
            }
        }
    }

}
