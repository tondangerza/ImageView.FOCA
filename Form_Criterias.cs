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
    public partial class Form_Criterias : Form
    {
        private User_ImameView user = new User_ImameView();


        public string binCodeTF;

        public Form_Criterias()
        {
            InitializeComponent();
        }

        private void Form_Criterias_Load(object sender, EventArgs e)
        {
           ReadAndWriteXMLFile();
        }
        void ReadAndWriteXMLFile()
        {

            if (File.Exists(user.pathxml))
            {
                try
                {
                    var readxml = File.ReadAllText(user.pathxml);
                    var criteria = ClassSaving.XMLDeserialize<ArrayOfCriteria>(readxml);


                    for (int i = 0; i < criteria.Criteria.Length; i++)
                    {
                        string[] row = new string[] { criteria.Criteria[i].BinNo.ToString(), criteria.Criteria[i].Description.ToString() };
                        dataGridView1.Rows.Add(row);

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else
                MessageBox.Show("Not found XML path");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(user.pathFolder))
                Directory.CreateDirectory(user.pathFolder);

            string path = user.pathFolder + "\\" + dataGridView1.CurrentRow.Cells[dtg_binCode.Name].Value;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                MessageBox.Show("This folder has created");
                return;
            }
            
            binCodeTF = (string)dataGridView1.CurrentRow.Cells[dtg_binCode.Name].Value;
            DialogResult = DialogResult.OK;
        }


    }
}
