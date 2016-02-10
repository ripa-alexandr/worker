using Worker.DTO.Api;

namespace Worker.DTO.Entities
{
    /// <summary>
    /// The EmployeeDto.
    /// </summary>
    public class EmployeeDto : IBaseDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of emploee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public StatusDto Status { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        public decimal Salary { get; set; }
    }
}
