using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DistributedLibrary.Authors.Api.Services
{
    public class DataService<TEntity> : IDataService<TEntity>
    {
        private const string FileName = @"authors.json";
        public IEnumerable<TEntity> GetEntities()
        {
            return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(File.ReadAllText(FileName));
        }

        public TEntity GetEntityById(int entityId)
        {
            return JsonConvert.DeserializeObject<TEntity>(File.ReadAllText(FileName));
        }
    }
}
