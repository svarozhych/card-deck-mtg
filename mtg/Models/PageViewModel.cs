using Microsoft.AspNetCore.Mvc;
public class PageViewModel
{
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }
    public string? Search { get; private set; }

    public PageViewModel(int count, int pageNumber, int pageSize, string? search)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Search = search;
    }

    public int CurrentPage
    {
        get
        {
            return PageNumber;
        }
    }

    public bool HasPreviousPage
    {
        get
        {
            return (PageNumber > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return (PageNumber < TotalPages);
        }
    }
}
