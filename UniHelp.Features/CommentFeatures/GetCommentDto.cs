namespace UniHelp.Features.CommentFeatures;

public class GetCommentDto
{
    public string Id { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Body { get; set; } = default!;

    public IEnumerable<GetCommentDto> ChildComments { get; set; } = default!;
}