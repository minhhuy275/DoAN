using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lí_điểm_sinh_viên
{
    public partial class Trangchu : Form
    {
        public Trangchu()
        {
            InitializeComponent();
        }
        private string tenDangNhap;

        public Trangchu(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        private void QuảnLíSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

            QLDiem formDiem = new QLDiem(tenDangNhap);
            formDiem.Show();
        }

        private void ĐăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ĐiểmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void Trangchu_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Trangchu
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Trangchu";
            this.Load += new System.EventHandler(this.Trangchu_Load);
            this.ResumeLayout(false);

        }

        private void Trangchu_Load_1(object sender, EventArgs e)
        {

        }
    }
}
