using System;
using System.Collections.Generic;
using DIP.Core.Domain;

namespace DIP.Core.Repository
{
    public interface IRepositoryBase<TAggregateRoot, TOptionsSearch>
        where TAggregateRoot : class, IAggregateRoot
        where TOptionsSearch : class, IOptionsSearch
    {
        void Remove(TAggregateRoot aggregateRoot);
        void Remove(Guid id);
        void Insert(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);

        TAggregateRoot Get(Guid id);
        IEnumerable<TAggregateRoot> Search(TOptionsSearch optionsSearch);

    }
}
