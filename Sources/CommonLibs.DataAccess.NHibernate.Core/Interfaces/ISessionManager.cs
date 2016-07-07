using NHibernate;

namespace CommonLibs.DataAccess.NHibernate.Common.Interfaces
{
    public interface ISessionManager
    {
        /// <summary>
        /// 	Opens current SQL session.
        /// </summary>
        void OpenSession();

        /// <summary>
        /// 	Close current SQL session.
        /// </summary>
        void CloseSession();

        /// <summary>
        /// 	Begins SQL transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 	Commits SQL transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 	Rollbacks SQL transaction and close session.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Current session
        /// </summary>
        ISession CurrentSession { get; }
    }
}