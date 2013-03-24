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
using DB_Scheme_Extract.Model;
using DB_Scheme_Extract.Business.exportData;

namespace DB_Scheme_Extract
{
    public partial class ExtractForm : Form
    {
        /***********************************************
         * Common
         ***********************************************/
        public ExtractForm()
        {
            InitializeComponent();
            // default to main tab
            tabGenerateTableData.SelectedTab = tabPage2;

            this.txtOwnerName.Text = this.txtUserId.Text;

            // Panel Configuration load the configuration
            SerializeUtil serUtil = new SerializeUtil();
            ConfigurationObject conf = new ConfigurationObject();
            conf = (ConfigurationObject)serUtil.DeSerialize(conf);
            setConf(conf);

            // Panel Preload Table
            for (int i = 0; i < this.preloadTablsList.Items.Count; i++)
            {
                this.genPreloadTableList.Items.Add(preloadTablsList.Items[i]);
            }

        }

        private string getRootPath()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            if (!String.IsNullOrEmpty(this.txtExportPath.Text))
            {
                appPath = this.txtExportPath.Text;
            }
            return appPath;
        }

        private void checkDBConf()
        {
            ConnectionProperty connProp = new ConnectionProperty();
            connProp.dataSource = this.txtDataSource.Text;
            connProp.userID = this.txtUserId.Text;
            connProp.password = this.txtPassword.Text;
            if (String.IsNullOrEmpty(connProp.dataSource) || String.IsNullOrEmpty(connProp.userID)
                || String.IsNullOrEmpty(connProp.password))
            {
                this.message.Text = "Please complete all text box above";
                tabGenerateTableData.SelectedTab = tabPage1;
            }
            else
            {
                ConnectionManager.initialDBConn(connProp);
            }
        }
        /***********************************************
         * Below are the Configuration Panel Actions
         ***********************************************/
        private void setConf(ConfigurationObject conf)
        {
            this.txtDataSource.Text = conf.DataSource;
            this.txtUserId.Text = conf.UserID;
            this.txtPassword.Text = conf.Password;
            this.txtExportPath.Text = conf.ExportFilePath;

            List<string> preloadTableList = conf.PreloadTableNameList;

            if (preloadTableList != null && preloadTableList.Count > 0)
            {
                foreach (var item in conf.PreloadTableNameList)
                {
                    this.preloadTablsList.Items.Add(item.ToString());
                }
            }
        }
        // save configuration to file
        private void btnSaveConf_Click(object sender, EventArgs e)
        {
            ConfigurationObject saveConfObj = new ConfigurationObject();
            saveConfObj.DataSource = this.txtDataSource.Text;
            saveConfObj.UserID = this.txtUserId.Text;
            saveConfObj.Password = this.txtPassword.Text;
            saveConfObj.ExportFilePath = this.txtExportPath.Text;

            saveConfObj.PreloadTableNameList = new List<string>();
            foreach (var item in this.preloadTablsList.Items)
            {
                saveConfObj.PreloadTableNameList.Add(item.ToString());
            }

            SerializeUtil serUtil = new SerializeUtil();
            serUtil.Serialize(saveConfObj);
        }
        // add or remove preload Table
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.preloadTablsList.Items.Add(this.txtPreloadTable.Text);
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.preloadTablsList.CheckedItems.Count > 0)
            {
                for (int i = 0; i < preloadTablsList.CheckedItems.Count; )
                {
                    this.preloadTablsList.Items.Remove(preloadTablsList.CheckedItems[i]);
                }
            }

        }

        /***********************************************
         * Below are the Main Panel Actions
         ***********************************************/
        private void generate_Click(object sender, EventArgs e)
        {
            checkDBConf();

            // conf ok will go to gen preload process
            string appPath = getRootPath();

            List<string> typeList = new List<string>();
            foreach (var item in typeListBox.CheckedItems)
            {
                typeList.Add(item.ToString().ToUpper());
            }

            GenerateClient client = new GenerateClient();
            fileBar.Value = 0;
            List<string> outputPathList = client.generateScripts(typeList, this.txtUserId.Text, appPath, fileBar);

            result.Visible = true;
            resultTxt.Visible = true;
            resultTxt.Text = outputPathList.First();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkAll.Checked)
            {
                for (int i = 0; i < typeListBox.Items.Count; i++)
                {
                    typeListBox.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < typeListBox.Items.Count; i++)
                {
                    typeListBox.SetItemChecked(i, false);
                }
            }
        }

        /***********************************************
         * Below are the Preload Table Script Generate
         ***********************************************/

        private void btnMove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.genPreloadTableList.CheckedItems.Count; i++)
            {
                this.selectedList.Items.Add(this.genPreloadTableList.CheckedItems[i]);
            }
        }

        private void btnGenPreloadTables_Click(object sender, EventArgs e)
        {
            checkDBConf();

            // conf ok will go to gen preload process
            GeneratePreloadTable genTable = new GeneratePreloadTable();
            string appPath = getRootPath() + Constants.PRELOAD_PATH;

            List<string> tableList = new List<string>();
            foreach (var item in this.genPreloadTableList.CheckedItems)
            {
                tableList.Add(item.ToString());
            }
            pbPreloadBar.Value = 0;
            List<string> outputFileList = genTable.generateTableScript(tableList, appPath, this.txtUserId.Text, pbPreloadBar);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < outputFileList.Count; i++)
            {
                result.Append(outputFileList[i] + "\r\n");
            }
            this.txtMulFile.Text = result.ToString();
        }

        private void preloadCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.preloadCheckAll.Checked)
            {
                for (int i = 0; i < genPreloadTableList.Items.Count; i++)
                {
                    genPreloadTableList.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < genPreloadTableList.Items.Count; i++)
                {
                    genPreloadTableList.SetItemChecked(i, false);
                }
            }
        }
    }
}
