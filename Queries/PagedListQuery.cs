namespace SpinitTest.Queries
{
    public class PagedListQuery
    {
        /// <summary>
        /// **Order by** _Example_: 
        /// * population desc, slugstate desc, id
        /// * id desc
        /// * year, id
        /// 
        /// ___desc = descending___
        /// 
        /// There are alot of other properties you can order by
        /// </summary>
        public string? OrderBy { get; set; }
        /// <summary>
        /// **Page Number** _Example_: If your total count in your response header is 20 and 
        /// 
        /// your page size is 10, then the other 10 is in page number 2.
        /// 
        /// Default is 1
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// **Page Size** _Example_: Select the amount of items you want to be displayed in each page
        /// 
        /// In order to get all data without pagination, set PageSize = 0
        /// 
        /// Defalut is 10
        /// </summary>
        public int PageSize { get; set; } = 10;
        
    }
}
