using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Domain.Providers.Office;

public interface IOfficeProvider
{
    Task<string> ExportAndUploadExcel<T>(IList<T> data, IList<ExcelFormat> formats, string fileName);
}
