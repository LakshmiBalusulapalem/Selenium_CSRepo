using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    class ExcelLibrary
    {
        public static List<Datacollection> dataCol;

        private static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            string localPath = new Uri(fileName).LocalPath;
            FileStream stream = File.Open(localPath, FileMode.Open, FileAccess.Read);

            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
                                                                                           //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;
            //Return as DataSet
            DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];

            //return
            return resultTable;
        }
        public static void PopulateInCollection()
        {
            dataCol = new List<Datacollection>();
            string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            DataTable table = ExcelToDataTable(filepath + "\\DataPools\\HA_HotelSearch.xlsx");
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col <= table.Columns.Count - 1; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()

                    };
                    //Add all the details for each row
                    dataCol.Add(dtTable);


                }
            }
        }
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {

                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();


                return data.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }

    }
}
