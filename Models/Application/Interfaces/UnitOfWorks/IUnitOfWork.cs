using MyBlog.Models.Application.Interfaces.Repositories;
using MyBlog.Models.Domain.Commons;

namespace MyBlog.Models.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IWriteRepository<T> GetWriteRepository<T>() where T : EntityBase, new();
    IReadRepository<T> GetReadRepository<T>() where T : EntityBase, new();
    Task<int> SaveAsync();
    int Save();
}
