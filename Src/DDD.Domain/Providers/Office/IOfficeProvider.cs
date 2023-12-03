using System.Collections.Generic;

namespace DDD.Domain.Providers.Office;

public interface IOfficeProvider
{
    string ExportAndUploadExcel<T>(IList<T> data, IList<ExcelFormat> formats, string fileName);
}
