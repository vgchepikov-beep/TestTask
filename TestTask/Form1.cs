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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите XML-файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedXmlFilePath = openFileDialog.FileName;
                    textBox1.Text = selectedXmlFilePath; // отображаем путь
                }
            }

        }
    }
}
