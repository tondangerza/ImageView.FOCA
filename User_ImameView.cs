using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class User_ImameView : UserControl
    {
        public string te { get; set; }
        public string pathFolder { get; set; }
        public string pathxml { get; set; }

        public User_ImameView()
        {
            InitializeComponent();
        }

        public void ReadAndWriteXMLFile()
        {
            try
            {
                if (File.Exists(pathxml))
                {
                    var readxml = File.ReadAllText(pathxml);
                    var criteria = ClassSaving.XMLDeserialize<ArrayOfCriteria>(readxml);

                    var rcpfldr = Directory.GetDirectories(pathFolder);
                    var rcpnames = rcpfldr.Select(x => Path.GetFileName(x)).ToArray();

                    for (int i = 0; i < rcpnames.Length; i++)
                    {
                        int id = int.Parse(rcpnames[i]);

                        string[] row = new string[] { criteria.Criteria[id].BinNo.ToString(), criteria.Criteria[id].Description.ToString() };
                        dataGridView1.Rows.Add(row);
                        var bmp = new Bitmap(15, 15);
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(criteria.Criteria[id].LabelColor.A, criteria.Criteria[id].LabelColor.R, criteria.Criteria[id].LabelColor.G, criteria.Criteria[id].LabelColor.B)), 0, 0, bmp.Width, bmp.Height);
                        }

                        DataGridViewImageCell imageColor = new DataGridViewImageCell();
                        imageColor.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dataGridView1[dtg_image.Name, dataGridView1.Rows.Count - 1] = imageColor;
                        imageColor.Value = bmp;
                    }

                    dataGridView1.ClearSelection();
                }
                else MessageBox.Show("Not found XML path");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void User_ImameView_Load(object sender, EventArgs e)
        {
            //ReadAndWriteXMLFile();
        }

        private void PopulateListView(DirectoryInfo path)
        {
            imageListView1.Items.Clear();
            imageListView1.SuspendLayout();
            Random rnd = new Random();
            FileInfo[] files = new FileInfo[0];
            try
            {
                files = path.GetFiles("*.*");
            }
            catch
            {
                files = new FileInfo[0];
            }
            foreach (FileInfo p in files)
            {
                if (p.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".ico", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".cur", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".emf", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".wmf", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                {
                    ImageListViewItem item = new ImageListViewItem(p.FullName);
                    item.SubItems.Add("random", rnd.Next(0, 999).ToString("000"));
                    imageListView1.Items.Add(item);
                }
            }
            imageListView1.ResumeLayout();
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop);

                Point pt = new Point(e.X, e.Y);
                pt = dataGridView1.PointToClient(pt);
                DataGridView.HitTestInfo info = dataGridView1.HitTest(pt.X, pt.Y);

                string selectFolder;
                string filename;
                string folderName;
                string rpFile;

                selectFolder = (string)dataGridView1.Rows[info.RowIndex].Cells[dtg_binCode.Name].Value;

                folderName = Path.GetDirectoryName(fileList[0]);

                for (int i = 0; i < fileList.Length; i++)
                {
                    filename = Path.GetFileName(fileList[i]);
                    string sourceFilePath = fileList[i];
                    string destinationFilePath = pathFolder + "\\" + selectFolder + "\\" + filename;
                    try
                    {
                        File.Move(sourceFilePath, destinationFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                PopulateListView(directoryInfo);

            }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Form_Criterias form_Criterias = new Form_Criterias();
            if (form_Criterias.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                ReadAndWriteXMLFile();
            }
        }

        private void btn_rmov_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this folder", "Delete", MessageBoxButtons.YesNo) == DialogResult.No) return;
            string selectFolder;
            selectFolder = (string)dataGridView1.CurrentRow.Cells[dtg_binCode.Name].Value;
            Directory.Delete((pathFolder + "\\" + selectFolder), true);
            dataGridView1.Rows.Clear();
            ReadAndWriteXMLFile();

            txt_fdname.Text = "-";

            imageListView1.Items.Clear();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string selectFolder;
            selectFolder = (string)dataGridView1.Rows[e.RowIndex].Cells[dtg_binCode.Name].Value;
            txt_fdname.Text = (string)dataGridView1.Rows[e.RowIndex].Cells[dtg_name.Name].Value;

            if (selectFolder == null) return;
            DirectoryInfo directoryInfo = new DirectoryInfo(pathFolder + "\\" + selectFolder);
            PopulateListView(directoryInfo);


        }
        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            dataGridView1.ClearSelection();
            Point pt = new Point(e.X, e.Y);
            pt = dataGridView1.PointToClient(pt);
            DataGridView.HitTestInfo info = dataGridView1.HitTest(pt.X, pt.Y);
            if (info.RowIndex < 0 || info.ColumnIndex < 0) return;
            dataGridView1.Rows[info.RowIndex].Cells[dtg_name.Name].Selected = true;

        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("do you want to delete this image", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectItitem = imageListView1.SelectedItems;

                for (int i = 0; i < selectItitem.Count; i++)
                {
                    File.Delete(selectItitem[i].FileName);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(selectItitem[0].FilePath);
                PopulateListView(directoryInfo);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
