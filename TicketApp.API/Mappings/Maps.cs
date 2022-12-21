using AutoMapper;
using TicketApp_API.Data;
using TicketApp_API.DTOs;

namespace TicketApp_API.Mappings;

public class Maps : Profile
{
    public Maps()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
        CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();
        CreateMap<Ticket, TicketDTO>().ReverseMap();
        CreateMap<Ticket, TicketCreateDTO>().ReverseMap();
        CreateMap<Ticket, TicketUpdateDTO>().ReverseMap();
    }
}