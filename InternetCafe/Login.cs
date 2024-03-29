﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetCafe
{
    public partial class frmLogin : Form
    {
        InternetCafeDataContext DB = new InternetCafeDataContext();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        public frmLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var checkUser = DB.getAllUser().FirstOrDefault(x => x.account == loginValidate.myTextBox && x.password == passValidate.myTextBox);
                if (checkUser != null)
                {
                    if (checkUser.role == "admin" || checkUser.role == "inventory")
                    {
                        MessageBox.Show("Welcome Admin : " + checkUser.firstName + " " + checkUser.lastName);
                        this.Hide();
                        UI.Manager.frmHome adminHome = new UI.Manager.frmHome(checkUser.firstName + " " + checkUser.lastName, checkUser.role_id);
                        adminHome.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Welcome Vendor : " + checkUser.firstName + " " + checkUser.lastName);
                        this.Hide();
                        UI.Employee.frmHome empHome = new UI.Employee.frmHome(checkUser.firstName + " " + checkUser.lastName);
                        empHome.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Account or password is not exist");
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var checkUser = DB.getAllUser().FirstOrDefault(x => x.account == loginValidate.myTextBox && x.password == passValidate.myTextBox);
            if (checkUser != null)
            {
                if (checkUser.role == "admin" || checkUser.role == "inventory")
                {
                    MessageBox.Show("Welcome Admin : " + checkUser.firstName + " " + checkUser.lastName);
                    this.Hide();
                    UI.Manager.frmHome adminHome = new UI.Manager.frmHome(checkUser.firstName + " " + checkUser.lastName, checkUser.role_id);
                    adminHome.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Welcome Vendor : " + checkUser.firstName + " " + checkUser.lastName);
                    this.Hide();
                    UI.Employee.frmHome empHome = new UI.Employee.frmHome(checkUser.firstName + " " + checkUser.lastName);
                    empHome.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Account or password is not exist");
            }
        }

        private void linkFindPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmForgotPassword fp = new frmForgotPassword();
            fp.ShowDialog();
            fp.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
