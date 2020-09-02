namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex {get; set;} = 1;
        private int _pageSize = 6;
        public int PageSize 
        {
            get => _pageSize;
            // If we override the pagesize (which is 6 by default) we want to ensure that this value is less than 
            // the maxpageSize. (Max number of items per page). Otherwise we just return the maxpagesize
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search 
        {
            get => _search;
            // convert any search values to lowercase
            set => _search = value.ToLower();
        }

    }
}