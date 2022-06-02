using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingADO.Net
{
    public class Lop
    {
        public int Id { get; set; }
        public string TenLop { get; set; }

        public Lop(DataRow row)
        {
            Id = Convert.ToInt32(row["ID"]);
            TenLop = row["TenLop"].ToString();
        }

        public Lop(int id, string tenLop)
        {
            this.Id = id;
            this.TenLop = tenLop;
        }

        public Lop()
        {
            
        }
    }
}
