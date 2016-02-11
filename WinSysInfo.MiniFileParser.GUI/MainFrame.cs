using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using WinSysInfo.MiniFileParser.Model;
using WinSysInfo.MiniFileParser.Process;
using System.Reflection;
using System.Collections;

namespace WinSysInfo.MiniFileParser.GUI
{
    public partial class MainFrame : Form
    {
        FileLoader loader;

        public MainFrame()
        {
            InitializeComponent();
        }

        private void OnClickFileOpenMenuItem(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Title = "Choose Windows File";
            openFileDlg.Multiselect = false;
            openFileDlg.Filter = "Win EXE (*.exe)|*.exe";
            DialogResult dlgResult = openFileDlg.ShowDialog();

            if(dlgResult == DialogResult.OK | dlgResult == DialogResult.Yes)
            {
                loader = new FileLoader(openFileDlg.FileName);
                loader.Read();

                LoadTreeView();
            }
        }

        private void LoadTreeView()
        {
            treeViewFile.Nodes.Clear();

            foreach (KeyValuePair<EnumPEStructureId, object> kv in
                (Dictionary<EnumPEStructureId, object>)loader.DataStore.FileData)
            {
                TreeNode parent = new TreeNode(kv.Key.ToString());
                treeViewFile.Nodes.Add(parent);
            }
            
            treeViewFile.SelectedNode = treeViewFile.Nodes[0];
        }

        private void OnSelectedNode(object sender, TreeViewEventArgs e)
        {
            listViewDetails.Items.Clear();

            EnumPEStructureId enumVal = (EnumPEStructureId)Enum.Parse(typeof(EnumPEStructureId), e.Node.Text);
            object objData = loader.DataStore.FileData[enumVal];

            if (objData == null) return;

            Type type = objData.GetType();

            if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)) ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
                return;

            PropertyInfo DataPi = type.GetProperty("Data");
            object rawObjData = DataPi.GetValue(objData);
            foreach (var f in DataPi.PropertyType.GetFields().Where(f => f.IsPublic))
            {
                if(f.FieldType.IsArray == true)
                {
                    Array array = f.GetValue(rawObjData) as Array;
                    string data = string.Empty;
                    for (int indx = 0; indx < array.Length; ++indx) data += array.GetValue(indx);
                    listViewDetails.Items.Add(
                        new ListViewItem(new string[] { f.Name, data }));
                }
                else
                {
                    listViewDetails.Items.Add(
                        new ListViewItem(new string[] { f.Name, f.GetValue(rawObjData).ToString() }));
                }
            }
        }
    }
}
