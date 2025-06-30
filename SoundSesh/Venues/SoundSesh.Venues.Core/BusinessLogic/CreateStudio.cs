using AutoMapper;
using GenericBizRunner;
using Microsoft.EntityFrameworkCore;
using SoundSesh.Venues.Entities.Models;
using System;
using Create = SoundSesh.Venues.Entities.DTOs.Create;

namespace SoundSesh.Venues.Core.BusinessLogic
{
    public interface ICreateStudio : IGenericActionWriteDb<Create.StudioDTO, Studio> { }

    public class CreateStudio : BizActionStatus, ICreateStudio
    {
        private readonly StudioContext _context;
        private readonly IMapper _mapper;
        
        public CreateStudio(StudioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }


        public Studio BizAction(Create.StudioDTO model)
        {
            Studio studio = null;

            if (model.State.ToUpper() != "TN")
                AddError("Business logic says only TN studios for now.", nameof(model.State));

            if (!HasErrors)
            {
                studio = _mapper.Map<Studio>(model);
                var result = _context.Add(studio);
            }

            return studio;
        }
    }
}
