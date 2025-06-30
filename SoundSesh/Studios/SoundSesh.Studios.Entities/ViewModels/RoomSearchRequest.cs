using SoundSesh.Common.Models;

namespace SoundSesh.Studios.Entities.ViewModels
{
        public class RoomSearchRequest : PagingRequest
        {
            public RoomSearchRequest() { }

            public RoomSearchRequest(PagingRequest pagingRequest)
            {
                From = pagingRequest.From;
                Sort = pagingRequest.Sort;
                Fields = pagingRequest.Fields;
                Size = pagingRequest.Size;
            }

            public string UserId { get; set; }

            public int Id { get; set; }

            public string IsActive => "True";
        }
}
