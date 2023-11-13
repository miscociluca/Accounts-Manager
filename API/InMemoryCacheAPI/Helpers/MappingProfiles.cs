using AutoMapper;
using AccountManagement.dto;
using AccountManagement.Models;
using System.Net;

namespace AccountManagement.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
