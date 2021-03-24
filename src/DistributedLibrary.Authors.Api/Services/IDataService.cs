using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedLibrary.Authors.Api.Services
{
    public interface IDataService<TEntity>
    {
        IEnumerable<TEntity> GetEntities();

        TEntity GetEntityById(int entityId);
    }
}
