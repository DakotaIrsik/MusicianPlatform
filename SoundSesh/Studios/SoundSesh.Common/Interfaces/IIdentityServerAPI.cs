using Refit;
using SoundSesh.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoundSesh.Common
{
    public interface IIdentityServerAPI
    {

        [Post("/api/Account")]
        Task<RegistrationResponse> Register();
    }
}
