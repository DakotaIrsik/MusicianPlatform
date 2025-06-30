using SoundSesh.Common.Models;

namespace SoundSesh.Studios.Entities.ViewModels
{
    public class ChatSearchRequest : PagingRequest
    {
        public ChatSearchRequest() { }
        public ChatSearchRequest(string userId)
        {
            UserId = userId;
        }

        public ChatSearchRequest(PagingRequest pagingRequest)
        {
            From = pagingRequest.From;
            Sort = pagingRequest.Sort;
            Fields = pagingRequest.Fields;
            Size = pagingRequest.Size;
        }
        public string UserId { get; set; }
    }
}
