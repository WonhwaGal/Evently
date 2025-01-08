using AutoMapper;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Domain.Events.Events;
using Evently.Modules.Events.IntegrationEvents;

namespace Evently.Modules.Events.Application.Events.CreateEvent;
public sealed class EventCreationDomainMappingProfile : Profile
{
    public EventCreationDomainMappingProfile()
    {
        CreateMap<(EventCreatedDomainEvent, EventResponse), EventCreatedIntegrationEvent>()
        .ForMember(dest => dest.IntegrationEventId, opt => opt.MapFrom(src => src.Item1.DomainEventId))
        .ForMember(dest => dest.OccurredOnUtc, opt => opt.MapFrom(src => src.Item1.OccurredOnUtc))
        .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Item2.Id))
        .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Item2.CategoryId))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item2.Title))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item2.Description))
        .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Item2.Location))
        .ForMember(dest => dest.StartAtUtc, opt => opt.MapFrom(src => src.Item2.StartsAtUtc))
        .ForMember(dest => dest.EndAtUtc, opt => opt.MapFrom(src => src.Item2.EndsAtUtc));
    }
}
