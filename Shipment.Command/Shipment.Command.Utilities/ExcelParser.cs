using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using Shipment.Command.Abstraction.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npoi.Mapper;
using Shipment.Command.Entities;
using System.Linq;

namespace Shipment.Command.Utilities
{
    public class ExcelParser : IExcelParser
    {
        /// <summary>
        /// This method is used to parse Shipment data.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sheetName"></param>
        /// <returns>List of Shipment Data</returns>
        public List<ShipmentData> ParseShipmentFile(IFormFile file, string sheetName)
        {
            try
            {
                IWorkbook workbook = WorkbookFactory.Create(file.OpenReadStream());
                int sheetNumebr = 0;
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    if (workbook.GetSheetName(i) == sheetName)
                    {
                        sheetNumebr = i;
                    }
                }
                Mapper excelMapper = new Mapper(workbook);
                return excelMapper.Take<ShipmentData>(sheetNumebr).Select(_ => _.Value).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
