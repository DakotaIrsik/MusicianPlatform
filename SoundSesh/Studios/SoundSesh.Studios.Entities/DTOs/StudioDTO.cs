using SoundSesh.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SoundSesh.Studios.Entities.DTOs
{

    public class StudioDTO : BaseDTO
    {
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        public string ZipCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string About { get; set; }

        [StringLength(1000)]
        public string Rules { get; set; }

        [StringLength(1000)]
        public string CancellationPolicy { get; set; }

        public IEnumerable<string> Amenities { get; set; }

        public string SkillLevel { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string WebSiteLink { get; set; }

        public string AudioClipLink { get; set; }

        public string ProfileImage { get; set; }

        public string BannerImage { get; set; }

        public List<ApplicationFileDTO> ApplicationFiles { get; set; } = new List<ApplicationFileDTO>();

        public List<HoursOfOperationDTO> HoursOfOperation { get; set; } = new List<HoursOfOperationDTO>();
        
        public List<SocialMediaDTO> SocialMedias { get; set; } = new List<SocialMediaDTO>();

        public List<RoomDTO> Rooms { get; set; } = new List<RoomDTO>();

        public List<RoomDTO> ActiveRooms => Rooms.Where(af => af.IsActive).ToList();

        public List<ApplicationFileDTO> ActiveApplicationFiles => ApplicationFiles.Where(af => af.IsActive).ToList();

        public string StockImage { get; set; }

        public string DefaultProfileImage => ApplicationFiles?.FirstOrDefault(af => af.FileType.ToLower() == ImageTypes.Types.Image.ToLower() &&
                                                                                   af.SubType.ToLower() == ImageTypes.SubTypes.Profile.ToLower() &&
                                                                                   af.IsActive &&
                                                                                   af.IsDefault)?.Url.Replace("original", "card/watermark") ?? StockImage;

        public string DefaultBannerImage => ApplicationFiles?.FirstOrDefault(af => af.FileType.ToLower() == ImageTypes.Types.Image.ToLower() &&
                                                                                  af.SubType.ToLower() == ImageTypes.SubTypes.Banner.ToLower() &&
                                                                                  af.IsActive &&
                                                                                  af.IsDefault)?.Url.Replace("original", "card/watermark") ?? StockImage;
    }
}