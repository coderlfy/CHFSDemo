using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Testchfs.chfsapi;

namespace Testchfs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region
            CHFSServer.Start();
            #endregion
        }
        
        private void btnlogin_Click(object sender, EventArgs e)
        {
            #region
            ResponseResult r = CHFSApi.Login();

            Console.WriteLine(r.code);
            Console.WriteLine(r.data);
            foreach (var s in r.cookie)
                Console.WriteLine(s);
            #endregion
        }

        private void btngetfiles_Click(object sender, EventArgs e)
        {
            #region
            ResponseResult r = CHFSApi.GetFiles();

            Console.WriteLine(r.code);
            Console.WriteLine(r.data);
            #endregion
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            #region
            OpenFileDialog open_file = new OpenFileDialog();

            DialogResult open_result = open_file.ShowDialog(this);

            if (open_result == DialogResult.OK)
            {
                ResponseResult r = CHFSApi.Upload(open_file.FileName);
                Console.WriteLine(r.code);
                Console.WriteLine(r.data);
            }
            #endregion
        }

        private void btnExist_Click(object sender, EventArgs e)
        {
            #region
            OpenFileDialog open_file = new OpenFileDialog();

            DialogResult open_result = open_file.ShowDialog(this);

            if (open_result == DialogResult.OK)
            {
                ResponseResult r = CHFSApi.Exist(open_file.FileName);
                Console.WriteLine(r.code);
                Console.WriteLine(r.data);
            }
            #endregion
        }


    }
}
