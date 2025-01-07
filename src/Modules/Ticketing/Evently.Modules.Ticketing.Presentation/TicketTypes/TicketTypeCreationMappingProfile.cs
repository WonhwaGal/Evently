using AutoMapper;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.Application.TicketTypes.CreateTicketType;

namespace Evently.Modules.Ticketing.Presentation.TicketTypes;
public sealed class TicketTypeCreationMappingProfile : Profile
{
    public TicketTypeCreationMappingProfile()
    {
        CreateMap<TicketTypeCreatedIntegrationEvent, CreateTicketTypeCommand>()
            .ForMember(dest => dest.TicketTypeId, opt => opt.MapFrom(src => src.TicketTypeId))
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}
