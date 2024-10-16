using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ExcelImporter.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

public class FileUploadController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new[] { ".xls", ".xlsx" };
            if (!allowedExtensions.Contains(fileExtension))
            {
                ViewBag.ErrorMessage = "Invalid file type. Please upload an Excel file.";
                return View("Index");
            }

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();

                    var columnNames = new List<string> { "Select a column" };
                    for (int col = 3; col <= worksheet.Dimension.End.Column; col++)
                    {
                        columnNames.Add(worksheet.Cells[1, col].Text.Replace("\r", "").Replace("\n", "").Trim());
                    }
                    ViewBag.ColumnNames = columnNames;
                    ViewBag.FileName = file.FileName;
                    ViewBag.RowNumbers = GetRowNumbersWithData(worksheet);
                    HttpContext.Session.Set("UploadedFile", stream.ToArray());
                }
            }
        } else
        {
            ViewBag.ErrorMessage = "Please upload a file.";
        }
        return View("Index");
    }

    [HttpPost]
    public IActionResult ValidateFile(FileUploadViewModel model)
    {
        try
        {
            var fileBytes = HttpContext.Session.Get("UploadedFile") as byte[];
            if (fileBytes != null && fileBytes.Length > 0)
            {
                using (var stream = new MemoryStream(fileBytes))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var data = new List<ExcelData>();
                        var rowNumbersWithDataCount = GetRowNumbersWithData(worksheet).Count + 1;
                        var columnNames = new List<string> { "Select a column" };

                        for (int col = 3; col <= worksheet.Dimension.End.Column; col++)
                        {
                            columnNames.Add(worksheet.Cells[1, col].Text.Replace("\r", "").Replace("\n", "").Trim());
                        }

                        ViewBag.ColumnNames = columnNames;
                        ViewBag.RowNumbers = GetRowNumbersWithData(worksheet);

                        for (int row = model.RowNumber; row <= rowNumbersWithDataCount; row++)
                        {
                            if (row == 1)
                            {
                                ViewBag.ErrorMessage = "Please do not select header row.";
                                return View("Index");
                            }

                            var parsedColumnName = CompareDisplayName(model);
                            if (parsedColumnName)
                            {
                                data.Add(new ExcelData
                                {
                                    PickupStoreNumber = int.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupStoreNumber)].Text, out var num) ? num : 0,
                                    PickupStoreName = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupStoreName)].Text,
                                    PickupLat = double.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupLat)].Text, out var lat) ? lat : 0,
                                    PickupLong = double.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupLong)].Text, out var lon) ? lon : 0,
                                    PickupFormattedAddress = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupFormattedAddress)].Text,
                                    PickupContactNameFirstName = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupContactNameFirstName)].Text,
                                    PickupContactNameLastName = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupContactNameLastName)].Text,
                                    PickupContactEmail = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupContactEmail)].Text,
                                    PickupContactMobileNumber = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupContactMobileNumber)].Text,
                                    PickupEnableSMSNotification = int.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupEnableSMSNotification)].Text, out var enableSMS) ? enableSMS : 0,
                                    PickupTime = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupTime)].Text,
                                    PickupTolerance = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupTolerance)].Text,
                                    PickupServiceTime = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.PickupServiceTime)].Text,
                                    DeliveryStoreNumber = int.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryStoreNumber)].Text, out var delNum) ? delNum : 0,
                                    DeliveryStoreName = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryStoreName)].Text,
                                    DeliveryLat = double.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryLat)].Text, out var delLat) ? delLat : 0,
                                    DeliveryLong = double.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryLong)].Text, out var delLon) ? delLon : 0,
                                    DeliveryFormattedAddress = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryFormattedAddress)].Text,
                                    DeliveryContactFirstName = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryContactFirstName)].Text,
                                    DeliveryContactLastName = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryContactLastName)].Text,
                                    DeliveryContactEmail = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryContactEmail)].Text,
                                    DeliveryContactMobileNumber = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryContactMobileNumber)].Text,
                                    DeliveryEnableSMSNotification = int.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryEnableSMSNotification)].Text, out var delNotif) ? delNotif : 0,
                                    DeliveryTime = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryTime)].Text,
                                    DeliveryTolerance = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryTolerance)].Text,
                                    DeliveryServiceTime = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.DeliveryServiceTime)].Text,
                                    OrderDetails = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.OrderDetails)].Text,
                                    AssignedDriver = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.AssignedDriver)].Text,
                                    CustomerReference = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.CustomerReference)].Text,
                                    Payer = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.Payer)].Text,
                                    Vehicle = worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.Vehicle)].Text,
                                    Weight = int.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.Weight)].Text, out var weight) ? weight : 0,
                                    Price = int.TryParse(worksheet.Cells[row, GetColumnIndexByHeader(worksheet, model.Price)].Text, out var price) ? price : 0,
                                });
                            }
                        }
                        ViewBag.ValidationResult = "File Validation Successful!";
                        ViewBag.ExcelData = data;
                        ViewBag.TotalRows = data.Count;
                    }
                }
            }
            return View("Index");
        }
        catch (FormatException formex)
        {
            ViewBag.ErrorMessage = formex.Message;
            return View("Index");
        }
        catch (ArgumentException argex)
        {
            ViewBag.ErrorMessage = argex.Message;
            return View("Index");
        }
        
    }

    [HttpPost]
    public IActionResult ImportData(string excelData)
    {
        var formattedJson = Newtonsoft.Json.Linq.JToken.Parse(excelData).ToString(Newtonsoft.Json.Formatting.Indented);
        return Content(formattedJson, "application/json");
    }

    private static int GetColumnIndexByHeader(ExcelWorksheet worksheet, string headerText)
    {
        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        {
            if (worksheet.Cells[1, col].Text.Replace("\r", "").Replace("\n", "").Replace(" ", "").Trim()
                .Equals(headerText.Replace("\r", "").Replace("\n", "").Replace(" ", "").Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return col;
            }
        }
        throw new ArgumentException($"Header '{headerText}' not found.");
    }

    private static bool CompareDisplayName(FileUploadViewModel model)
    {
        var excelDataProps = typeof(ExcelData).GetProperties();
        var modelProps = model.GetType().GetProperties();

        foreach (var excelProp in excelDataProps)
        {
            var displayAttr = excelProp.GetCustomAttribute<DisplayAttribute>();

            if (displayAttr == null)
                continue;

            var modelProp = modelProps.FirstOrDefault(mp => mp.Name == excelProp.Name);

            if (modelProp == null)
            {
                throw new FormatException ($"Invalid column: '{displayAttr.Name}'.");
            }

            var selectedValue = modelProp.GetValue(model)?.ToString();
            if (displayAttr.Name!.Replace(" ", "") != selectedValue!.Replace(" ", ""))
            {
                throw new FormatException($"The value for column '{displayAttr.Name}' does not match with the excel file.");
            }
        }
        return true;
    }

    private static List<int> GetRowNumbersWithData(ExcelWorksheet worksheet)
    {
        var totalRows = worksheet.Dimension.End.Row;
        var rowNumbersWithData = new List<int>();

        for (int row = 2; row <= totalRows; row++)
        {
            bool hasData = false;
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, col].Text))
                {
                    hasData = true;
                    break;
                }
            }
            if (hasData)
            {
                rowNumbersWithData.Add(row);
            }
        }
        return rowNumbersWithData;
    }
}
