using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using DevExpress.XtraEditors;

namespace P5WF
{
    public partial class BDTForm : DevExpress.XtraEditors.XtraForm
    {
        public BDTForm()
        {
            InitializeComponent();
        }
        EntityMeow db;
        private void Form1_Load(object sender, EventArgs e)
        {
            db = new EntityMeow();
            db.bacdts.Load();
            bacdtBindingSource.DataSource = db.bacdts.Local;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bacdtBindingSource.AddNew();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bacdtBindingSource.RemoveCurrent();
            }

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool cellEmpty = false;
            int emptyRow = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            { 
                if (String.IsNullOrWhiteSpace(gridView1.GetRowCellValue(i, "IDBDT").ToString()) || String.IsNullOrWhiteSpace(gridView1.GetRowCellValue(i, "TenBacDT").ToString()))
                {
                    emptyRow = i + 1;
                    cellEmpty = true;
                    break;
                }
            }

            if (!cellEmpty)
            {
                db.SaveChanges();
                XtraMessageBox.Show("Your data has been successfully saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(string.Format("Row {0} data fields must not be empty!", emptyRow), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var changed = db.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var obj in changed)
            {
                switch (obj.State)
                {
                    case EntityState.Modified:
                        obj.CurrentValues.SetValues(obj.OriginalValues);
                        obj.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        obj.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        obj.State = EntityState.Unchanged;
                        break;

                }
            }
            bacdtBindingSource.ResetBindings(false);
        }
    }
}
