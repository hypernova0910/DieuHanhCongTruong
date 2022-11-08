using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
namespace VNRaPaBomMin
{
    public partial class CompressedFile : Form
    {
        public string json = "";
        public string Phieudieutra = "", Baocao = "", Bando = "";
        public List<string> lst = new List<string>();
        public CompressedFile(string json, List<string> lst)
        {
            this.json = json;
            this.lst = lst;
            InitializeComponent();
        }
        FolderBrowserDialog fd;

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fd = new FolderBrowserDialog();
            fd.Description = "Hãy chọn thư mục làm việc";
            fd.ShowNewFolderButton = false;
            fd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                lbPath.Text = fd.SelectedPath.ToString();
            }
        }
        private bool CombineFile(string nameFolder, string sourceFile, string targetPath)
        {
            string A = nameFolder;
            //string sourceFile = "C:\\Users\\Laptop\\Desktop\\Data\\test.txt";
            //string targetPath = "C:\\Users\\LAPTOP\\Desktop\\Data";

            targetPath = System.IO.Path.Combine(targetPath, A);

            string fileName = sourceFile.Split('\\')[sourceFile.Split('\\').Length - 1];
            // Use Path class to manipulate file and directory paths.
            string sourcePath = sourceFile.Replace(fileName, "");

            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
            System.IO.Directory.CreateDirectory(targetPath);

            // To copy a file to another location and
            // overwrite the destination file if it already exists.

            // To copy all the files in one directory to another directory.
            // Get the files in the source folder. (To recursively iterate through
            // all subfolders under the current directory, see
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously
            //       in this code example.
            if (System.IO.Directory.Exists(sourcePath))
            {
                try
                {
                    System.IO.File.Copy(sourceFile, destFile, true);
                }
                catch
                {
                    MessageBox.Show("File không tồn tại", "Thất bại");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Đường dẫn file không tồn tại", "Thất bại");
                return false;
                //Console.WriteLine("Source path does not exist!");
            }
            return true;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox_Name.Text == "")
            {
                label4.Visible = true;
            }
            else
            {
                label4.Visible = false;
                if (lbPath.Text == "...")
                {
                    label5.Visible = true;
                }
                else
                {
                    label5.Visible = false;
                    try
                    {
                        var link = lbPath.Text + "\\" + textBox_Name.Text;
                        bool success = true;
                        foreach(string a in lst)
                        {
                            if(!CombineFile(textBox_Name.Text, a, lbPath.Text))
                            {
                                success = false;
                            }
                        }
                        //string json = JsonConvert.SerializeObject(LstDaily, Formatting.Indented);
                        System.IO.File.WriteAllText(link + "\\" + textBox_Name.Text + ".json", json);
                        System.IO.Compression.ZipFile.CreateFromDirectory(link, link + ".zip");
                        if (success)
                        {
                            MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                        }
                        else
                        {
                            MessageBox.Show("Xuất file dữ liệu không thành công... ", "Lỗi");
                        }
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Có vấn đề trong việc export file, Vui lòng kiểm tra lại các file báo cáo", "Thất bại");
                    }

                }
            }
        }
    }
}
