namespace App.Contracts.Queries
{
    public class PaginationQueries
    {
        public PaginationQueries()
        {
            PageNumber = 1;
            PageSize = 500;
        }

        public PaginationQueries(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}