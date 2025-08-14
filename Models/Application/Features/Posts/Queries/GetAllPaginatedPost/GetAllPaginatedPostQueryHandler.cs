using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllPaginatedPost;



public sealed class GetAllPaginatedPostQueryHandler : BaseHandler, IRequestHandler<GetAllPaginatedPostQueryRequest, GetAllPaginatedPostQueryResponse>
{
    public GetAllPaginatedPostQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<GetAllPaginatedPostQueryResponse> Handle(GetAllPaginatedPostQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Post> Posts = await unitOfWork.GetReadRepository<Post>()
            .GetAllByPagingAsync(
                p=>p.ParentId == null,
                include: c => c.Include(p => p.User),
                currentPage: request.PageNumber,
                pageSize: request.PageSize,
                sort: c => c.OrderByDescending(p => p.CreationDate)
            );

        int count = await unitOfWork.GetReadRepository<Post>().CountAsync(p => p.ParentId == null);


        List<PostDto> responsePostDto = new();

        int totalPage = (int)Math.Ceiling(count / (double)request.PageSize);
        bool isHasNext = request.PageNumber < totalPage ? true : false;
        bool isHasPrevious = request.PageNumber > 1 ? true : false;

        foreach (var post in Posts)
            responsePostDto.Add(new(post.Id, post.User.FullName, post.ImageUrl, 
                post.Name, post.Description, post.Tags, post.ViewCount, post.CreationDate));


        return new(request.PageNumber,isHasPrevious,isHasNext,responsePostDto);
    }
}