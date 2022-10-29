namespace SpinitTest.Queries
{
    public class StateQuery : PagedListQuery
    {
        /// <summary>
        /// **Id of the state** _Example_:  04000US01
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// **State name** _Example_: Alabama
        /// </summary>
        public string? State { get; set; }
        /// <summary>
        /// **Id of the year for that state** _Example_:  2020
        /// </summary>
        public int? IdYear { get; set; }
        /// <summary>
        /// **Year/Years you want a set of data from** _Example_:  2020 or 2018, 2019, 2020 (must be comma separated)
        /// </summary>
        public string? Year { get; set; }
        /// <summary>
        /// **State population larger than inserted number** _Example_:  20000
        /// </summary>
        public int? Population { get; set; }
        /// <summary>
        /// **SlugState** _Example_:  alabama
        /// </summary>
        public string? SlugState { get; set; }
    }
}