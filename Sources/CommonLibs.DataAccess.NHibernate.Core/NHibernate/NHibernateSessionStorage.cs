using System.Threading;
using CommonLibs.DataAccess.NHibernate.Common.Interfaces;
using NHibernate;

namespace CommonLibs.DataAccess.NHibernate.Common.NHibernate
{
    public class NHibernateSessionStorage : ISessionStorage
    {
        protected const string SessionKey = "47270DC4-0480-4C4D-8780-FA54B8875847";

        /// <summary>
        /// 	Gets/Sets current NHibernate session.
        /// </summary>
        public virtual ISession CurrentSession
        {
            get
            {
                //if (HttpContext.Current != null && HttpContext.Current.Items != null)
                //{
                //    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                //}
                return (ISession)Thread.GetData(Thread.GetNamedDataSlot(SessionKey));
            }
            set
            {
                //if (HttpContext.Current != null && HttpContext.Current.Items != null)
                //{
                //    HttpContext.Current.Items[SESSION_KEY] = value;
                //}
                //else
                //{
                    Thread.SetData(Thread.GetNamedDataSlot(SessionKey), value);
                //}
            }
        }
    }
}