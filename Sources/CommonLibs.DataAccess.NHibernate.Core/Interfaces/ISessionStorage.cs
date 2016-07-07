using NHibernate;

namespace CommonLibs.DataAccess.NHibernate.Common.Interfaces
{
    /// <summary>
    /// 	Represents NHibernate session storage interface.
    /// </summary>
    public interface ISessionStorage
    {
        /// <summary>
        /// 	Gets/Sets current NHibernate session.
        /// </summary>
        ISession CurrentSession { get; set; }
    }
}