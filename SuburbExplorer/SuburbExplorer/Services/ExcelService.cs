using OfficeOpenXml;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuburbExplorer.Services
{
    public class ExcelService
    {
        //Embeded Excel that contains suburb codes
        const string FileName = "SuburbCodes.xlsx";
        public ExcelService() 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        //Read the excel file and extract suburb code and state code
        public async Task <(int StateCode, int SuburbCode)> LookUpStateAndSuburbCodeAsync(string SuburbName, string StateName)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(FileName);
            using var package = new ExcelPackage(stream);

            //Open the first worksheet in the Excel
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int maxRows = worksheet.Dimension.Rows;

            for (int row = 2; row <= maxRows; row++)
            {
                string excelSuburbName = worksheet.Cells[row, 2].Text.Trim();
                string excelStateName = worksheet.Cells[row, 4].Text.Trim();
                if (excelSuburbName.ToLower() == SuburbName.ToLower()
                    && excelStateName == StateName) 
                {
                    int StateCode = int.Parse(worksheet.Cells[row, 3].Text);
                    int SuburbCode = int.Parse(worksheet.Cells[row, 1].Text);
                    return (StateCode, SuburbCode);
                }
            }

            throw new Exception("Suburb not found");

        }



    }
}
