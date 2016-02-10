using System.Web.Mvc;

namespace Worker.Web.Controllers
{
    /// <summary>
    /// The base controller.
    /// </summary>
    public abstract class BaseController<THandler> : Controller 
    {
        /// <summary>
        /// The handler.
        /// </summary>
        protected readonly THandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        protected BaseController(THandler handler)
        {
            this.handler = handler;
        }
    }
}