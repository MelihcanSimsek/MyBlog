using MyBlog.Models.Application.Interfaces.Repositories;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Persistence.Context;
using MyBlog.Models.Persistence.Repositories;

namespace MyBlog.Models.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;
    public UnitOfWork(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
    public int Save() => dbContext.SaveChanges();
    public async Task<int> SaveAsync() => await dbContext.SaveChangesAsync();
    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(dbContext);
    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);
}
