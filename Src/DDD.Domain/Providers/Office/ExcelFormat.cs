namespace DDD.Domain.Providers.Office;

public class ExcelFormat
{
    public string ColId { get; set; }
    public string ColName { get; set; }
    public int ColIndex { get; set; }
    public int Width { get; set; }
    public bool IsBold { get; set; }
    public bool IsCurrency { get; set; }
    public bool IsDate { get; set; }
    public bool IsHide { get; set; }
}
