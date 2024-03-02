namespace UniHelp.Features.Constants;

public class CommentBody
{
    // TODO: Change ReplyBodyTemplate and QuoteBodyTemplate
    public const string DeletedCommentBody = "A comment/quote was deleted";
    public const string ReplyBodyTemplate = "<i><a href='/get-task/{0}#comment{1}'>[{2}]</a></i> {3}";
    public const string QuoteBodyTemplate = "<i><a href='/get-task/{0}#comment{1}'>[{2}]</a> {3}</i><br>{4}";
}