using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCodeFirst.Model
{
    public class SinhVien
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaLop { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
