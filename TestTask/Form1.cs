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
                openFileDialog.Title = "Выберите XML-файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedXmlFilePath = openFileDialog.FileName;
                    textBox1.Text = selectedXmlFilePath; // отображаем путь
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedXmlFilePath))
            {
                MessageBox.Show("Сначала выберите XML-файл!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string directory = Path.GetDirectoryName(selectedXmlFilePath);
                string xsltPath = Path.Combine(directory, "transform.xsl");
                string resultPath = Path.Combine(directory, "result.xml");                

                if (!File.Exists(xsltPath))
                {
                    MessageBox.Show("Файл transform.xsl не найден в той же директории!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Применяем XSLT-преобразование
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                using (XmlWriter writer = XmlWriter.Create(resultPath))
                {
                    xslt.Transform(selectedXmlFilePath, writer);
                }

                // Загружаем полученный XML и добавляем атрибут total
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
                                // Заменяем запятую на точку для корректного парсинга
                                amountStr = amountStr.Trim().Replace(" ", "").Replace(',', '.');
                                if (decimal.TryParse(amountStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
                                {
                                    total += amount;
                                }
                            }
                        }

                        // Добавляем атрибут total с суммой
                        employee.SetAttributeValue("total", total.ToString("0.00").Replace(',', '.'));                        
                    }
                }

                // Сохраняем обновлённый XML
                doc.Save(resultPath);


                // добавим атрибут total в исходный файл Data

                XDocument doc2 = XDocument.Load(selectedXmlFilePath);

                // Получаем корневой элемент <Pay>
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
                            // Заменяем запятую на точку для корректного парсинга
                            amountStr = amountStr.Trim().Replace(" ", "").Replace(',', '.');
                            if (decimal.TryParse(amountStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
                            {
                                total += amount;
                            }
                        }
                    }

                    // Добавляем атрибут total с суммой
                    pay.SetAttributeValue("total", total.ToString("0.00").Replace(',', '.'));

                    // Сохраняем обновлённый XML
                    doc2.Save(selectedXmlFilePath);
                }


                MessageBox.Show($"Преобразование завершено. Результат сохранён в:\n{resultPath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
