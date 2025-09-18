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

                // ��������� XSLT-��������������
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                using (XmlWriter writer = XmlWriter.Create(resultPath))
                {
                    xslt.Transform(selectedXmlFilePath, writer);
                }

                // ��������� ���������� XML � ��������� <total>
                XDocument doc = XDocument.Load(resultPath);

                var employees = doc.Root?.Elements("Employee");
                if (employees != null)
                {
                    foreach (var employee in employees)
                    {
                        decimal total = 0;

                        var salaries = employee.Elements("salary");
                        foreach (var salary in salaries)
                        {
                            string amountStr = salary.Attribute("amount")?.Value;
                            if (!string.IsNullOrEmpty(amountStr))
                            {
                                // �������� ������� �� ����� ��� ����������� ��������
                                amountStr = amountStr.Trim().Replace(" ", "").Replace(',', '.');
                                if (decimal.TryParse(amountStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
                                {
                                    total += amount;
                                }
                            }
                        }

                        // ��������� ������� total � ������
                        employee.SetAttributeValue("total", total.ToString("0.00").Replace(',', '.'));                        
                    }
                }

                // ��������� ���������� XML
                doc.Save(resultPath);

                MessageBox.Show($"�������������� ���������. ��������� ������� �:\n{resultPath}", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
