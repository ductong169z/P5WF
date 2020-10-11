using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Entity;

namespace P5WF
{
    public partial class NDTForm : DevExpress.XtraEditors.XtraForm
    {
        public NDTForm()
        {
            InitializeComponent();
        }
        EntityMeow db;
        private void NDTForm_Load(object sender, EventArgs e)
        {
            db = new EntityMeow();
            db.nganhdts.Load();
            nganhdtBindingSource.DataSource = db.nganhdts.Local;
        }

        private void BtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nganhdtBindingSource.AddNew();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                nganhdtBindingSource.RemoveCurrent();
            }
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SaveChanges();
            XtraMessageBox.Show("Your data has been successfull save", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}