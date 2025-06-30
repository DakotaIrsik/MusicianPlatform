using Refit;
using SoundSesh.Common.Models;
using SoundSesh.Common.LookUps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Interfaces
{
    public interface ISeedSoundSeshGeneralAPI
    {
        #region City
        [Post("/api/v1/City")]
        Task<List<City>> GetCities([Body] GeoDbPagingRequest request);
        #endregion

        #region Genre
        [Get("/api/v1/Genre")]
        Task<List<Genre>> GetGenres();

        [Get("/api/v1/Genre/{id}")]
        Task<Genre> GetGenre(string id);
        #endregion

        #region SkillLevel
        [Get("/api/v1/SkillLevel")]
        Task<List<SkillLevel>> GetSkillLevels();

        [Get("/api/v1/SkillLevel/{id}")]
        Task<SkillLevel> GetSkillLevel(string id);
        #endregion

        #region Craft
        [Get("/api/v1/Craft")]
        Task<List<Craft>> GetCrafts();

        [Get("/api/v1/Craft/{id}")]
        Task<Craft> GetCraft(string id);
        #endregion

        #region State
        [Get("/api/v1/State")]
        Task<List<State>> GetStates();

        [Get("/api/v1/State/{id}")]
        Task<Dictionary<string, string>> GetState(string id);
        #endregion

        #region Country
        [Get("/api/v1/Country")]
        Task<List<State>> GetCountries();

        [Get("/api/v1/Country/{id}")]
        Task<Dictionary<string, string>> GetCountry(string id);
        #endregion

        #region Images
        [Post("/api/v1/Image?applicationName={applicationName}&fileType={fileType}&subType={subType}")]
        [Multipart]
        Task<ApplicationFile> UploadImage(string applicationName, string fileType, string subType, [AliasAs("imageContent")] StreamPart imageStream);
        #endregion

    }
}
