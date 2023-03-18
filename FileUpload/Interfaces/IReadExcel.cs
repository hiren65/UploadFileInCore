using System.Data;
using FileUpload.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileUpload.Interfaces
{
    public interface IReadExcel
    {
        
      public Task<List<ReportModelGeneral>> GetDataFromExcel(DataSet ds);
    }
}
