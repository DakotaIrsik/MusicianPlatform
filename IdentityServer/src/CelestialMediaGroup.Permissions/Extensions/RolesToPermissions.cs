using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CelestialMediaGroup.Permissions.Extensions
{
    public class RoleToPermissions
    {
        private string _permissionsInRole;

        /// <summary>
        /// ShortName of the role
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(450)] //Matches identity size
        public string RoleName { get; private set; }

        /// <summary>
        /// A human-friendly description of what the Role does
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; private set; }

        /// <summary>
        /// This returns the list of permissions in this role
        /// </summary>
        public IEnumerable<ApplicationPermissions> PermissionsInRole => _permissionsInRole.UnpackPermissionsFromString();

        private RoleToPermissions() { }

        /// <summary>
        /// This creates the Role with its permissions
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="description"></param>
        /// <param name="permissions"></param>
        public RoleToPermissions(string roleName, string description, ICollection<ApplicationPermissions> permissions)
        {
            RoleName = roleName;
            Update(description, permissions);
        }

        public void Update(string description, ICollection<ApplicationPermissions> permissions)
        {
            Description = description;
            if (permissions == null || !permissions.Any())
                throw new InvalidOperationException("There should be at least one permission associated with a role.");

            _permissionsInRole = permissions.PackPermissionsIntoString();
        }
    }
}
