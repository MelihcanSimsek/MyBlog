using MyBlog.Models.Application.Interfaces.UnitOfWorks;

namespace MyBlog.Models.Application.Bases;

public class BaseHandler
{
    public readonly IUnitOfWork unitOfWork;

    public BaseHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}
