using HepsiBuradaApi.Application.Interfaces.Repositories;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Persistance.Context;
using HepsiBuradaApi.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Persistance.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_dbContext);
        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_dbContext);
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
        public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();
        public int Save() => _dbContext.SaveChanges();

    }
}
