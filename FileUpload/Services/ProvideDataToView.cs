using ExcelDataReader;
using FileUpload.Interfaces;
using FileUpload.Models;
using System.Data;
using System.IO;
using System.Reflection.PortableExecutable;

namespace FileUpload.Services
{
    public class ProvideDataToView:IReadExcel
    {
        // private readonly IFormFile _file;
     

        public ProvideDataToView()
        {
           
           
        }

   Task<List<ReportModelGeneral>> IReadExcel.GetDataFromExcel(DataSet ds)
        {
            List<ReportModelGeneral> dataL = new List<ReportModelGeneral>();
            DataTable serviceDetails = ds.Tables[0];
            for (int i = 1; i < serviceDetails.Rows.Count; i++)
            {
                ReportModelGeneral rmg = new ReportModelGeneral();
                rmg.Id = i;
                rmg.OrderNumber = Convert.ToInt32( serviceDetails.Rows[i][1]);
                rmg.CustName = Convert.ToString(serviceDetails.Rows[i][2]);

                dataL.Add(rmg);
            }

            return Task.FromResult(dataL);
        }
    }
}
