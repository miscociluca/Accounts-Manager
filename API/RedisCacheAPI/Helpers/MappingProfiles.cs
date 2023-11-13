using AutoMapper;
using AccountManagementRedis.dto;
using AccountManagementRedis.Models;
using System.Net;

namespace AccountManagementRedis.Helpers
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
