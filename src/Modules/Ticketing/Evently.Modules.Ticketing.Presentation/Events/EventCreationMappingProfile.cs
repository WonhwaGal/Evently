using AutoMapper;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.Application.Events.CreateEvent;

namespace Evently.Modules.Ticketing.Presentation.Events;
public sealed class EventCreationMappingProfile : Profile
{
    public EventCreationMappingProfile()
    {
        CreateMap<EventCreatedIntegrationEvent, CreateEventCommand>()
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.StartsAtUtc, opt => opt.MapFrom(src => src.StartAtUtc))
            .ForMember(dest => dest.EndsAtUtc, opt => opt.MapFrom(src => src.EndAtUtc));
    }
}
