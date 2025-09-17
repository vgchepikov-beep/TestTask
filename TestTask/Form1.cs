using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;


namespace TestTask
{    
    public partial class Form1 : Form
    {
        private string selectedXmlFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.Title = "�������� XML-����";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedXmlFilePath = openFileDialog.FileName;
                    textBox1.Text = selectedXmlFilePath; // ���������� ����
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedXmlFilePath))
            {
                MessageBox.Show("������� �������� XML-����!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string directory = Path.GetDirectoryName(selectedXmlFilePath);
                string xsltPath = Path.Combine(directory, "transform.xsl");
                string resultPath = Path.Combine(directory, "result.xml");

                if (!File.Exists(xsltPath))
                {
                    MessageBox.Show("���� transform.xsl �� ������ � ��� �� ����������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
