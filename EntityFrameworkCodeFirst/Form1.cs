using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityFrameworkCodeFirst.DAL;

namespace EntityFrameworkCodeFirst
{
    public partial class frmQLSV : Form
    {
        QLSinhVienDBContext db = new QLSinhVienDBContext();
        public frmQLSV()
        {
            InitializeComponent();
        }

        private void frmQLSV_Load(object sender, EventArgs e)
        {
            var temp = db.SinhVien.Where(x => x.Id > 0).ToList();


            foreach (var sinhVien in temp)
            {
                ListViewItem item = new ListViewItem(sinhVien.Id.ToString());
                lvSinhVien.Items.Add(item);
                item.SubItems.Add(sinhVien.Name);


                item.SubItems.Add(sinhVien.Lop.Name);
            }
        }
    }
    
}
