using DieuHanhCongTruong.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong
{
    public partial class MenuLoaderManagerFrm : Form
    {
        public bool _IsAutoStart = true;

        public MenuLoaderManagerFrm()
        {
            InitializeComponent();
        }

        //private List<ObjectRibbonTabName> GetTabName()
        //{
        //    List<ObjectRibbonTabName> retVal = new List<ObjectRibbonTabName>();

        //    // Get all panel and tab
        //    Autodesk.Windows.RibbonControl rc = Autodesk.Windows.ComponentManager.Ribbon;
        //    if (rc == null)
        //        return retVal;

        //    foreach (var ribbonTab in rc.Tabs)
        //    {
        //        if (ribbonTab.IsContextualTab)
        //            continue;

        //        ObjectRibbonTabName objRibbonTab = new ObjectRibbonTabName();
        //        objRibbonTab.tabName = ribbonTab.Title;
        //        objRibbonTab.isTab = true;
        //        objRibbonTab.status = ribbonTab.IsVisible;
        //        objRibbonTab.objectData = ribbonTab;

        //        ribbonTab.IsActive = true;
        //        if (ribbonTab.Title == "RPBM")
        //            continue;

        //        foreach (var panel in ribbonTab.Panels)
        //        {
        //            ObjectRibbonTabName objectPanel = new ObjectRibbonTabName();
        //            objectPanel.tabName = panel.UID;
        //            var splitData = panel.UID.Split('_');
        //            if (splitData.Length > 1)
        //                objectPanel.tabName = splitData.LastOrDefault();

        //            objectPanel.isTab = false;
        //            objectPanel.status = panel.IsVisible;
        //            objectPanel.objectData = panel;
        //            objRibbonTab.lstObjectPanel.Add(objectPanel);
        //        }

        //        retVal.Add(objRibbonTab);
        //    }

        //    return retVal;
        //}

        //private void InitDataTreeview()
        //{
        //    tvControl.Nodes.Clear();
        //    List<ObjectRibbonTabName> lstObjectTabName = GetTabName();

        //    foreach (var ojbectTab in lstObjectTabName)
        //    {
        //        var nodeParent = tvControl.Nodes.Add(ojbectTab.tabName);
        //        nodeParent.Checked = ojbectTab.status;
        //        nodeParent.Tag = ojbectTab;
        //        foreach (var objectPanel in ojbectTab.lstObjectPanel)
        //        {
        //            var childNode = nodeParent.Nodes.Add(objectPanel.tabName);
        //            childNode.Checked = objectPanel.status;
        //            childNode.Tag = objectPanel;
        //        }
        //    }

        //    //tvControl.ExpandAll();
        //}

        private void MenuLoaderManagerFrm_Load(object sender, EventArgs e)
        {
            //InitDataTreeview();

            SetPreVal();
        }

        private void SetPreVal()
        {
            //AppUtils.LoadRecentInput(cbkAutoStart, "True");
            AppUtils.LoadRecentInput(tbHeSoMayDoBom, AppUtils.DefaultNanoTesla.ToString());
            AppUtils.LoadRecentInput(tbHeSoMayDoMin, AppUtils.DefaultNanoTeslaMin.ToString());
            AppUtils.LoadRecentInput(tbKhoangThoiGian, "10");
            AppUtils.LoadRecentInput(lbTepDuongDan, "...");
        }

        private void tvControl_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void SetChildrenChecked(TreeNode treeNode)
        {
            foreach (TreeNode item in treeNode.Nodes)
            {
                if (item.Checked != treeNode.Checked)
                {
                    item.Checked = treeNode.Checked;
                }

                if (item.Nodes.Count > 0)
                {
                    SetChildrenChecked(item);
                }
            }
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void tvControl_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                CheckAllChildNodes(e.Node, e.Node.Checked);
            }
        }

        private void CheckTv(TreeNode node, bool isCheck)
        {
            node.Checked = isCheck;
            foreach (TreeNode tn in node.Nodes)
            {
                tn.Checked = isCheck;
                if (tn.Nodes.Count != 0)
                {
                    for (int i = 0; i < tn.Nodes.Count; i++)
                    {
                        CheckTv(tn.Nodes[i], isCheck);
                    }
                }
            }
        }

        //private void btnCheckAll_Click(object sender, EventArgs e)
        //{
        //    foreach (TreeNode item in tvControl.Nodes)
        //        CheckTv(item, true);
        //}

        //private void btnUnCheck_Click(object sender, EventArgs e)
        //{
        //    foreach (TreeNode item in tvControl.Nodes)
        //        CheckTv(item, false);
        //}

        private void btchapnhan_Click(object sender, EventArgs e)
        {
            //_IsAutoStart = cbkAutoStart.Checked;

            if (AppUtils.ValidateNumber(tbHeSoMayDoBom) == false ||
                AppUtils.ValidateNumber(tbHeSoMayDoMin) == false ||
                AppUtils.ValidateNumber(tbKhoangThoiGian) == false)
            {
                MessageBox.Show("Hãy nhập số");
                return;
            }

            //AppUtils.SaveRecentInput(cbkAutoStart);
            AppUtils.SaveRecentInput(tbHeSoMayDoBom);
            AppUtils.SaveRecentInput(tbHeSoMayDoMin);
            AppUtils.SaveRecentInput(tbKhoangThoiGian);
            AppUtils.SaveRecentInput(lbTepDuongDan);

            this.DialogResult = DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btDuongDan_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            string folderPath = string.Empty;
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Chọn.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                folderPath = System.IO.Path.GetDirectoryName(folderBrowser.FileName);

                lbTepDuongDan.Text = folderPath;
            }
        }
    }
}