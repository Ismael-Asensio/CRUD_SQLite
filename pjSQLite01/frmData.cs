using pjSQLite01.Logic;
using pjSQLite01.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjSistematico04
{
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
            ShowProducts();
        }


        #region Utilidades Formulario

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);


        private void Minimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Home_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtProductID.Text = row.Cells["ProductID"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtProductType.Text = row.Cells["ProductType"].Value.ToString();

            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            _Save();
        }

        #region Metodos
        private void _Save()
        {
            product objc = new product()
            {
                ProductName = txtProductName.Text,
                ProductType = txtProductType.Text
            };

            bool ask = ProductLogic.Instance.Save(objc);

            if (ask)
            {
                ShowProducts();
            }
        }

        private void ShowProducts()
        {
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = ProductLogic.Instance.lst();
        }

        private void Update()
        {
            product objc = new product()
            {
                ProductID = int.Parse(txtProductID.Text),
                ProductName = txtProductName.Text,
                ProductType = txtProductType.Text
            };

            bool ask = ProductLogic.Instance.Edit(objc);

            if (ask)
            {
                ShowProducts();
                Clear();
            }
        }

        private void Clear()
        {
            txtProductID.Text = String.Empty;
            txtProductName.Text = String.Empty;
            txtProductType.Text = String.Empty;
        }

        private void Delete()
        {
            product objc = new product()
            {
                ProductID = int.Parse(txtProductID.Text)
            };

            bool ask = ProductLogic.Instance.Delete(objc);

            if (ask)
            {
                ShowProducts();
                Clear();
            }

        }

        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
