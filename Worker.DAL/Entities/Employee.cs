using Worker.DAL.Api;

namespace Worker.DAL.Entities
{
    /// <summary>
    /// The Employee.
    /// </summary>
    public class Employee : IBaseEntity
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
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        public decimal Salary { get; set; }
    }
}
