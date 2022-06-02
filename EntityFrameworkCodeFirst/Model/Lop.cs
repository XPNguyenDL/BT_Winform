using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCodeFirst.Model
{
    public class Lop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<SinhVien> SinhVien { get; set; }
    }
}
