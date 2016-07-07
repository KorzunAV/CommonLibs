//using System.Web;
//using MakeHappy.DataAccess.IoC;
//using Ninject;

//namespace MakeHappy.DataAccess.NHibernate
//{
//    public class HttpSessionModule : IHttpModule
//    {
//        /// <summary>
//        /// 	Initializes a module and prepares it to handle requests.
//        /// </summary>
//        /// <param name = "context">An <see cref = "T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application 
//        /// </param>
//        public void Init(HttpApplication context)
//        {
//            //TODO: Think how to change.
//            IKernel kernel = new StandardKernel(new DataAccessModule());
//            var sessionManager = kernel.Get<ISessionManager>();

//            context.BeginRequest += delegate
//            {
//                sessionManager.OpenSession();
//                sessionManager.BeginTransaction();
//            };

//            context.EndRequest += delegate
//            {
//                sessionManager.CommitTransaction();
//                sessionManager.CloseSession();
//            };
//            context.Error += delegate
//            {
//                sessionManager.RollbackTransaction();
//            };
//        }

//        /// <summary>
//        /// 	Disposes of the resources (other than memory) used by the module that implements <see cref = "T:System.Web.IHttpModule" />.
//        /// </summary>
//        public void Dispose()
//        {
//        }
//    }
//}