using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonLibs.DataAccess.NHibernate.Common.Interfaces;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;

namespace CommonLibs.DataAccess.NHibernate.Common.NHibernate
{
    public class SessionManager : ISessionManager
    {
        #region [ Fields ]
        
        private readonly ISessionFactory _factory;
        private readonly ISessionStorage _storage;
        private ValidatorEngine _validatorEngine; 

        #endregion

        #region [ Properties ]

        /// <summary>
        /// 	Gets/Sets current NHibernate session.
        /// </summary>
        public ISession CurrentSession
        {
            get { return _storage.CurrentSession; }
            private set { _storage.CurrentSession = value; }
        }

        #endregion

        public SessionManager(ISessionStorage storage)
        {
            _storage = storage;

            var configuration = new Configuration().Configure();

            InitValidator(configuration);

            configuration.EventListeners.FlushEntityEventListeners = new IFlushEntityEventListener[] { new FlushEntityEventListener() };

            var validateFieldsEventListener = new ValidateFieldsEventListener(_validatorEngine);
            configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { validateFieldsEventListener, new ValidatePreInsertEventListener() };
            configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { validateFieldsEventListener, new ValidatePreUpdateEventListener() };

            _factory = InitFluentMappings(configuration).BuildSessionFactory();
        }
        
        private static FluentConfiguration InitFluentMappings(Configuration configuration)
        {
            var fluentConfiguration = Fluently.Configure(configuration);

            var assemblies = GetDataAccessAssemblies();

            foreach (var assembly in assemblies)
            {
                var temp = assembly;
                fluentConfiguration.Mappings(v => v.FluentMappings.AddFromAssembly(temp));
            }

            return fluentConfiguration;
        }

        //определяем набор доменных сборок в которых определены мапинги для NH
        private static IEnumerable<Assembly> GetDataAccessAssemblies()
        {
            var currentDirectory = AssemblyDirectory();
            var strings = Directory.GetFiles(currentDirectory, "*DataAccess*.dll");
            return strings.Select(Assembly.LoadFile);
        }

        private void InitValidator(Configuration configuration)
        {
            _validatorEngine = new ValidatorEngine();

            _validatorEngine.Configure();

            ValidatorInitializer.Initialize(configuration, _validatorEngine);
        }


        private static string AssemblyDirectory()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        }

        public void OpenSession()
        {
            CurrentSession = _factory.OpenSession();
            CurrentSession.FlushMode = FlushMode.Never;
        }

        public void CloseSession()
        {
            if (CurrentSession != null && CurrentSession.IsOpen)
            {
                CurrentSession.Close();
                CurrentSession = null;
            }
        }

        public void BeginTransaction()
        {
            if (CurrentSession != null)
            {
                CurrentSession.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (CurrentSession != null && CurrentSession.Transaction.IsActive)
                {
                    CurrentSession.Transaction.Commit();
                }
            }
            catch (Exception)
            {
                RollbackTransaction();
                throw;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (CurrentSession != null && CurrentSession.Transaction.IsActive)
                {
                    _storage.CurrentSession.Transaction.Rollback();
                }
            }
            finally
            {
                CloseSession();
            }
        }
    }
}