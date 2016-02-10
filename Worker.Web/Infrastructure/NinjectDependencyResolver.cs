using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using Ninject;
using Worker.DAL;
using Worker.DAL.Api;
using Worker.DAL.Repositories;
using Worker.DTO.Handlers;

namespace Worker.Web.Infrastructure
{
    /// <summary>
    /// The ninject dependency resolver.
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyResolver"/> class.
        /// </summary>
        public NinjectDependencyResolver()
        {
            this.kernel = new StandardKernel();

            AddBindings();
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        /// <summary>
        /// The get services.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        /// <summary>
        /// The add bindings.
        /// </summary>
        private void AddBindings()
        {
            this.kernel.Bind<DbContext>().To<WorkerContext>();
            this.kernel.Bind<IRepository>().To<Repository>();
            this.kernel.Bind<EmployeeHandler>().ToSelf();
        }
    }
}