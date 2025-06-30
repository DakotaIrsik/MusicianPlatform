using System;
using System.ComponentModel.DataAnnotations;

namespace CelestialMediaGroup.Permissions
{
    public class ModulesForUser
    {
        public ModulesForUser(string userId, PaidForModules allowedPaidForModules)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            AllowedPaidForModules = allowedPaidForModules;
        }

        [Key]
        [MaxLength(450)] //Matches identity size
        public string UserId { get; set; }
        public PaidForModules AllowedPaidForModules { get; set; }
    }
}
