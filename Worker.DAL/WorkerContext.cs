using System.Data.Entity;
using Worker.DAL.Entities;

namespace Worker.DAL
{
    /// <summary>
    /// The worker context.
    /// </summary>
    public class WorkerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerContext"/> class.
        /// </summary>
        public WorkerContext()
            : base("Worker")
        {
        }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
    }
}
