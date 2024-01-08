namespace BusinessLayer.Models;

public class PaginationSettings
{
    public int pageSize { get; }
    public int pageNumber { get; }
    
    public PaginationSettings(int pageSize, int pageNumber)
    {
        this.pageSize = pageSize;
        this.pageNumber = pageNumber;
    }
    
    
}