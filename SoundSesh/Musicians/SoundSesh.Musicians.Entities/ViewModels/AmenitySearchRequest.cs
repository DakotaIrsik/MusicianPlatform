using SoundSesh.Common.Models;

namespace SoundSesh.Musicians.Entities.ViewModels
{
    public class AmenitySearchRequest : PagingRequest
    {
        public AmenitySearchRequest()
        {

        }

        public AmenitySearchRequest(PagingRequest pagingRequest)
        {
            From = pagingRequest.From;
            Sort = pagingRequest.Sort;
            Fields = pagingRequest.Fields;
            Size = pagingRequest.Size;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string IsActive => "True";
    }
}
