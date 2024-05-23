using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Controls;

namespace Warehouse.Service
{
    internal class OutputDocumentsService
    {
        public void ExportToWord(DataGrid dataGrid)
        {
            try
            {
                // Создаем новый документ Word
                using (var document = WordprocessingDocument.Create(@"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\test.docx", WordprocessingDocumentType.Document))
                {
                    // Создаем основной текстовый элемент документа
                    var mainDocumentPart = document.AddMainDocumentPart();
                    var body = new Body();

                    // Создаем заголовок
                    var heading = new Paragraph(new Run(new Text("Данные из DataGrid")));
                    heading.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId { Val = "Heading1" });
                    body.Append(heading);

                    // Добавляем таблицу в тело документа
                    var table = new Table();
                    var tableProperties = new TableProperties(
                        new TableBorders(
                            new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 }
                        )
                    );
                    table.AppendChild(tableProperties);

                    // Добавляем заголовки столбцов
                    var headerRow = new TableRow();
                    foreach (var column in dataGrid.Columns)
                    {
                        var cell = new TableCell(new Paragraph(new Run(new Text(column.Header.ToString()))));
                        headerRow.AppendChild(cell);
                    }
                    table.AppendChild(headerRow);

                    // Добавляем строки данных
                    if (dataGrid.ItemsSource != null)
                    {
                        foreach (var item in dataGrid.ItemsSource)
                        {
                            var dataRow = new TableRow();
                            foreach (var column in dataGrid.Columns)
                            {
                                var cell = new TableCell(new Paragraph(new Run(new Text(column.GetCellContent(item).ToString()))));
                                dataRow.AppendChild(cell);
                            }
                            table.AppendChild(dataRow);
                        }
                    }

                    body.AppendChild(table);
                    mainDocumentPart.Document = new Document(body);
                }

                MessageBox.Show("Файл 'test.docx' успешно создан.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Word: {ex.Message}");
            }
        }
    }
}
