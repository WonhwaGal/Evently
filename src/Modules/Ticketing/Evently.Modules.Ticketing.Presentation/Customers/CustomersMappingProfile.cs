using AutoMapper;
using Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
using Evently.Modules.Users.IntegrationEvents;

namespace Evently.Modules.Ticketing.Presentation.Customers;

public class CustomersMappingProfile : Profile
{

    public CustomersMappingProfile()
    {
        CreateMap<UserRegisteredIntegrationEvent, CreateCustomerCommand>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
    }

}
