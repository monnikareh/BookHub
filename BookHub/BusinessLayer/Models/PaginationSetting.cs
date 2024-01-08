namespace BusinessLayer.Models;

public class PaginationSetting
{
    public int pageSize { get; }
    public int pageNumber { get; }
    
    public PaginationSetting(int pageSize, int pageNumber)
    {
        this.pageSize = pageSize;
        this.pageNumber = pageNumber;
    }
    
    
}