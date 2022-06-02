using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingADO.Net
{
    public class SinhVien
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lop { get; set; }

        public SinhVien()
        {

        }
        public SinhVien(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Name = row["Ten"].ToString();
            this.Lop = row["MaLop"].ToString();
        }

        public SinhVien(int id, string hoTen, string maLop)
        {
            this.ID = id;
            this.Name = hoTen;
            this.Lop = maLop;
        }
    }
}
