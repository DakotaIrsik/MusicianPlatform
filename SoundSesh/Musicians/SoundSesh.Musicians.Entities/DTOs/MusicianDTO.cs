using SoundSesh.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using constants = SoundSesh.Common.Constants;

namespace SoundSesh.Musicians.Entities.DTOs
{

    public class MusicianDTO : BaseDTO
    {

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
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string About { get; set; }

        [StringLength(750)]
        public string Discography { get; set; }

        public IEnumerable<string> Crafts { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string ProfileImage { get; set; }

        public string BannerImage { get; set; }

        public List<ApplicationFileDTO> ApplicationFiles { get; set; } = new List<ApplicationFileDTO>();
        
        public List<SocialMediaDTO> SocialMedias { get; set; } = new List<SocialMediaDTO>();

        public List<ApplicationFileDTO> ActiveApplicationFiles => ApplicationFiles.Where(af => af.IsActive).ToList();

        public string DefaultProfileImage => ApplicationFiles?.FirstOrDefault(af => af.FileType.ToLower() == ImageTypes.Types.Image.ToLower() &&
                                                                                   af.SubType.ToLower() == ImageTypes.SubTypes.Profile.ToLower() &&
                                                                                   af.IsActive &&
                                                                                   af.IsDefault)?.Url.Replace("original", "card/watermark");

        public string DefaultBannerImage => ApplicationFiles?.FirstOrDefault(af => af.FileType.ToLower() == ImageTypes.Types.Image.ToLower() &&
                                                                                  af.SubType.ToLower() == ImageTypes.SubTypes.Banner.ToLower() &&
                                                                                  af.IsActive &&
                                                                                  af.IsDefault)?.Url.Replace("original", "card/watermark");
    }
}