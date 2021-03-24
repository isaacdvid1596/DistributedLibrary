using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedLibrary.Books.Service.Services
{
    public interface IDataService<TEntity>
    {
        IEnumerable<TEntity> GetEntities();

        TEntity GetEntityById(int entityId);
    }
}
