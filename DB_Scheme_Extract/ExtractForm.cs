using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DB_Scheme_Extract.Persistence;
using DB_Scheme_Extract.Business;
using DB_Scheme_Extract.Utility;

namespace DB_Scheme_Extract
{
    public partial class ExtractForm : Form
    {
        public ExtractForm()
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabPage2;
        }

        private void generate_Click(object sender, EventArgs e)
        {
            ConnectionProperty connProp = new ConnectionProperty();
            connProp.dataSource = this.dataSource.Text;
            connProp.userID = this.userId.Text;
            connProp.password = this.password.Text;
            if (String.IsNullOrEmpty(connProp.dataSource) || String.IsNullOrEmpty(connProp.userID)
                || String.IsNullOrEmpty(connProp.password))
            {
                this.message.Text = "Please complete all text box above";
                tabControl1.SelectedTab = tabPage1;
            }
            else
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                List<string> typeList = new List<string>();
                foreach (var item in typeListBox.CheckedItems)
                {
                    typeList.Add(item.ToString().ToUpper());
                }

                GenerateClient client = new GenerateClient();
                fileBar.Value = 0;
                List<string> outputPathList = client.generateScripts(typeList, connProp, appPath, fileBar);

                result.Visible = true;
                resultTxt.Visible = true;
                resultTxt.Text = outputPathList.First();
            }
        }
    }
}
