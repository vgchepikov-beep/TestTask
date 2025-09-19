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
                string resultPath = Path.Combine(directory, "Employees.xml");

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


                // Отобразим полученный xml в таблице

                var dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Surname", typeof(string));
                dataTable.Columns.Add("Amount", typeof(decimal));
                dataTable.Columns.Add("Month", typeof(string));

                // Обходим всех Employee
                foreach (var employee in doc.Root?.Elements("Employee") ?? Enumerable.Empty<XElement>())
                {
                    string name = employee.Attribute("name")?.Value ?? "";
                    string surname = employee.Attribute("surname")?.Value ?? "";

                    // Обходим все salary внутри Employee
                    foreach (var salary in employee.Elements("salary"))
                    {
                        string amountStr = salary.Attribute("amount")?.Value;
                        string month = salary.Attribute("mount")?.Value ?? "";

                        if (!string.IsNullOrEmpty(amountStr))
                        {
                            // Нормализуем формат числа
                            amountStr = amountStr.Replace(',', '.');
                            if (decimal.TryParse(amountStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal amount))
                            {
                                dataTable.Rows.Add(name, surname, amount, month);
                            }
                        }
                    }
                }

                // Привязываем к DataGrid
                dataGridView1.DataSource = dataTable;

                //конец работы с таблицей




                // добавим атрибут total в исходный файл Data1

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

        private void buttonAdditem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedXmlFilePath))
            {
                MessageBox.Show("Сначала выберите XML-файл!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Валидация полей формы

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Поле 'Имя' не может быть пустым!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.Focus();
                return;
            }
                        
            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                MessageBox.Show("Поле 'Фамилия' не может быть пустым!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxSurname.Focus();
                return;
            }
                      
            
            decimal amountValue;
            if (string.IsNullOrWhiteSpace(textBoxAmount.Text))
            {
                MessageBox.Show("Поле 'Сумма' не может быть пустым!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }
            else if (!decimal.TryParse(
                textBoxAmount.Text.Replace(',', '.'),
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out amountValue))
            {
                MessageBox.Show("Поле 'Сумма' должно содержать корректное число (например: 100, 100.50, 100,50).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }
            else if (amountValue < 0)
            {
                MessageBox.Show("Сумма не может быть отрицательной!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAmount.Focus();
                return;
            }

            if (listBoxMonth.SelectedItem == null)
            {
                MessageBox.Show("Выберите месяц из списка!");
                return;
            }

            try
            {
                // Загружаем XML и добавляем элемент item
                XDocument doc2 = XDocument.Load(selectedXmlFilePath);

                // Находим корневой элемент <Pay>
                var pay = doc2.Root;
                if (pay == null || pay.Name.LocalName != "Pay")
                {
                    MessageBox.Show("Корневой элемент должен быть <Pay>!");
                    return;
                }

                // Создаем новый элемент <item> с атрибутами
                XElement newItem = new XElement("item",
                    new XAttribute("name", textBoxName.Text.Trim()),
                    new XAttribute("surname", textBoxSurname.Text.Trim()),
                    new XAttribute("amount", textBoxAmount.Text.Trim()),
                    new XAttribute("mount", listBoxMonth.SelectedItem?.ToString().Replace(',', '.'))
                );

                // Добавляем элемент в корень
                pay.Add(newItem);


                // Сохраняем файл
                doc2.Save(selectedXmlFilePath);

                MessageBox.Show("Элемент item успешно добавлен!");
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
