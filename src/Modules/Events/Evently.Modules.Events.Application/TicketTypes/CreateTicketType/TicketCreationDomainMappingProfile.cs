using AutoMapper;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
using Evently.Modules.Events.IntegrationEvents;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
public sealed class TicketCreationDomainMappingProfile : Profile
{
    public TicketCreationDomainMappingProfile()
    {
        CreateMap<(TicketTypeCreatedDomainEvent,TicketTypeResponse), TicketTypeCreatedIntegrationEvent>()
            .ForMember(dest => dest.IntegrationEventId, opt => opt.MapFrom(src => src.Item1.DomainEventId))
            .ForMember(dest => dest.OccurredOnUtc, opt => opt.MapFrom(src => src.Item1.OccurredOnUtc))
            .ForMember(dest => dest.TicketTypeId, opt => opt.MapFrom(src => src.Item2.Id))
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Item2.EventId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item2.Price))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Item2.Currency))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Item2.Quantity));
    }
}
