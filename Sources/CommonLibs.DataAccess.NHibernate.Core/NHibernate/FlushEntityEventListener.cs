using NHibernate.Event;
using NHibernate.Event.Default;

namespace CommonLibs.DataAccess.NHibernate.Common.NHibernate
{
    /// <summary>
    /// Event listener for flush events. It has been using when HHibernate session per request has been using for external services.
    /// At this case an entity is not stored in any intermediate storage when it is transfered to a service and back
    /// </summary>
    public class FlushEntityEventListener : DefaultFlushEntityEventListener
    {
        public override void OnFlushEntity(FlushEntityEvent @event)
        {
            var entry = @event.EntityEntry;

            // It makes sense to check concurrency access via custom flush entity event listener if entity already exists in the DB
            if (entry.ExistsInDatabase)
            {
                var session = @event.Session;
                var entity = @event.Entity;
                var persister = entry.Persister;

                if (persister.IsVersioned)
                {
                    var version = persister.GetVersion(entity, session.EntityMode);
                    {
                        if (!persister.VersionType.IsEqual(version, entry.Version))
                        {
                            throw new ConcurrencyException(persister.EntityName, entry.Id);
                        }
                    }
                }
            }
            base.OnFlushEntity(@event);
        }
    }
}