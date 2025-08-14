using MediatR;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllMostViewedPost;

public sealed class GetAllMostViewedPostQueryHandler : BaseHandler, IRequestHandler<GetAllMostViewedPostQueryRequest, IList<GetAllMostViewedPostQueryResponse>>
{
    public GetAllMostViewedPostQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<IList<GetAllMostViewedPostQueryResponse>> Handle(GetAllMostViewedPostQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Post> posts = await unitOfWork.GetReadRepository<Post>()
            .GetAllAsync(p => p.ParentId == null, sort: c => c.OrderByDescending(f => f.ViewCount));

        List<GetAllMostViewedPostQueryResponse> response = new();

        for(int i=0;i<request.Size; i++)
            response.Add(new(posts[i].Id, posts[i].Name, posts[i].CreationDate));

        return response;
    }
}
