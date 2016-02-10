namespace Worker.DTO.Queries
{
    /// <summary>
    /// The employee query. Can set parameters for paging, filtering, sorting.
    /// </summary>
    public class EmployeeQuery
    {
        /// <summary>
        /// The amount elements for skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The amount elements for take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// The status employee for filtering.
        /// </summary>
        public int? Status { get; set; }
    }
}
