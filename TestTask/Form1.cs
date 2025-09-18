using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;


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

                // ��������� ���������� XML � ��������� ������� total
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


                // ��������� ���������� xml � �������

                var dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Surname", typeof(string));
                dataTable.Columns.Add("Amount", typeof(decimal));
                dataTable.Columns.Add("Month", typeof(string));

                // ������� ���� Employee
                foreach (var employee in doc.Root?.Elements("Employee") ?? Enumerable.Empty<XElement>())
                {
                    string name = employee.Attribute("name")?.Value ?? "";
                    string surname = employee.Attribute("surname")?.Value ?? "";

                    // ������� ��� salary ������ Employee
                    foreach (var salary in employee.Elements("salary"))
                    {
                        string amountStr = salary.Attribute("amount")?.Value;
                        string month = salary.Attribute("mount")?.Value ?? "";

                        if (!string.IsNullOrEmpty(amountStr))
                        {
                            // ����������� ������ �����
                            amountStr = amountStr.Replace(',', '.');
                            if (decimal.TryParse(amountStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
                            {
                                dataTable.Rows.Add(name, surname, amount, month);
                            }
                        }
                    }
                }

                // ����������� � DataGrid
                dataGridView1.DataSource = dataTable;

                //����� ������ � ��������




                // ������� ������� total � �������� ���� Data

                XDocument doc2 = XDocument.Load(selectedXmlFilePath);

                // �������� �������� ������� <Pay>
                var pay = doc2.Root;

                if (pay != null && pay.Name.LocalName == "Pay")
                {
                    decimal total = 0;

                    var items = pay.Elements("item");
                    foreach (var item in items)
                    {
                        string amountStr = item.Attribute("amount")?.Value;
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
                    pay.SetAttributeValue("total", total.ToString("0.00").Replace(',', '.'));

                    // ��������� ���������� XML
                    doc2.Save(selectedXmlFilePath);
                }


                MessageBox.Show($"�������������� ���������. ��������� ������� �:\n{resultPath}", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������:\n{ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
