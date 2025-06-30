using SoundSesh.Common.Models;

namespace SoundSesh.Musicians.Entities.ViewModels
{
    public class MusicianSearchRequest : PagingRequest
    {
        public MusicianSearchRequest() { }

        public MusicianSearchRequest(PagingRequest pagingRequest)
        {
            From = pagingRequest.From;
            Sort = pagingRequest.Sort;
            Fields = pagingRequest.Fields;
            Size = pagingRequest.Size;
        }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string About { get; set; }

        public string UserId { get; set; }

        public int Id { get; set; }

        public string IsActive => "True";
    }
}
