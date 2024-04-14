using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.IO;
using System.Runtime.ConstrainedExecution;
using NPOI.XSSF.UserModel;

namespace CoursePractice.Controllers
{
    public class ОтчётController : Controller
    {
        private string connectionString = @"Data Source=MKVPC\SQLEXPRESS;Initial Catalog=MonitoringSystemDB;Integrated Security=True";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] // Добавлен атрибут HttpPost для метода GenerateReport
        public ActionResult GenerateReport()
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT МКД.адрес AS 'Адрес объекта МКД'," +
                " COUNT(*) AS 'Количество обращений на объект МКД'," +
                " SUM(CASE WHEN Обращения.статус_обращения = 3 THEN 1 ELSE 0 END) AS 'Количество решенных обращений на объект МКД'" +
                " FROM Обращения" +
                " INNER JOIN Учётные_записи ON Обращения.учётная_запись = Учётные_записи.код_пользователя" +
                " INNER JOIN МКД ON Обращения.объект_МКД = МКД.код_МКД" +
                $" WHERE Учётные_записи.код_пользователя = {Session["UserId"]}" +
                " GROUP BY Учётные_записи.логин, МКД.адрес" +
                " ORDER BY Учётные_записи.логин, МКД.адрес;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                // Store the DataTable in Session
                Session["DataTable"] = dataTable;

                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Success");
            }
        }
        [HttpPost]
        public ActionResult DownloadReport()
        {
            
            // Получение DataTable из TempData
            DataTable dataTable = (DataTable)Session["DataTable"];
            if (dataTable != null)
            {
                // Создание нового рабочего книги Excel
                var workbook = new XSSFWorkbook();

                // Создание нового листа Excel
                var sheet = workbook.CreateSheet("Отчет");

                // Создание стиля для заголовка
                var headerStyle = workbook.CreateCellStyle();
                var headerFont = workbook.CreateFont();
                headerFont.FontHeightInPoints = 16; // Установка размера шрифта для заголовка
                headerStyle.SetFont(headerFont);

                // Заполнение листа данными из DataTable
                var headerRow = sheet.CreateRow(0);
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var cell = headerRow.CreateCell(i);
                    cell.SetCellValue(dataTable.Columns[i].ColumnName);
                    cell.CellStyle = headerStyle; // Применение стиля заголовка
                }

                // Создание стиля для данных
                var dataStyle = workbook.CreateCellStyle();
                var dataFont = workbook.CreateFont();
                dataFont.FontHeightInPoints = 14; // Установка размера шрифта для данных
                dataStyle.SetFont(dataFont);

                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    var dataRow = dataTable.Rows[rowIndex];
                    var sheetRow = sheet.CreateRow(rowIndex + 1);

                    for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                    {
                        var cell = sheetRow.CreateCell(columnIndex);
                        cell.SetCellValue(dataRow[columnIndex].ToString());
                        cell.CellStyle = dataStyle; // Применение стиля данных
                    }
                }

                // Автоматическое подбор ширины столбцов
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                // Генерация имени файла
                string fileName = "Отчёт об обращениях пользователя " + DateTime.Now.ToString("G") + ".xlsx";

                // Сохранение рабочей книги Excel в поток
                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);

                // Получение байтового массива из MemoryStream
                byte[] byteArray = stream.ToArray();

                // Создание нового MemoryStream на основе байтового массива
                MemoryStream newStream = new MemoryStream(byteArray);

                // Предложение файла для скачивания
                return File(newStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else
            {
                return Content("Данных для отчета нет");
            }
            
        }

        public ActionResult Success()
        {
            // Получение DataTable из TempData
            DataTable dataTable = (DataTable)Session["DataTable"];

            return View(dataTable);
        }

        public ActionResult NoData()
        {
            return View("NoData", "_ReportLayout");
        }
    }
}
