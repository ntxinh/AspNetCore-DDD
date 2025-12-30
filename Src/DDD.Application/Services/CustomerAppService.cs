using System;
using System.Collections.Generic;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using DDD.Application.EventSourcedNormalizers;
using DDD.Application.Interfaces;
using DDD.Application.ViewModels;
using DDD.Domain.Commands;
using DDD.Domain.Core.Bus;
using DDD.Domain.Interfaces;
using DDD.Domain.Specifications;
using DDD.Infra.Data.Repository.EventSourcing;

namespace DDD.Application.Services;

public class CustomerAppService(
    IMapper mapper,
    ICustomerRepository customerRepository,
    IMediatorHandler bus,
    IEventStoreRepository eventStoreRepository) : ICustomerAppService
{
    private readonly IMapper _mapper = mapper;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;
    private readonly IMediatorHandler _bus = bus;

    public IEnumerable<CustomerViewModel> GetAll() =>
        _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);

    public IEnumerable<CustomerViewModel> GetAll(int skip, int take) =>
        _customerRepository.GetAll(new CustomerFilterPaginatedSpecification(skip, take))
                           .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);

    public CustomerViewModel GetById(Guid id) => _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));

    public void Register(CustomerViewModel customerViewModel)
    {
        var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
        _bus.SendCommand(registerCommand);
    }

    public void Update(CustomerViewModel customerViewModel)
    {
        var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
        _bus.SendCommand(updateCommand);
    }

    public void Remove(Guid id)
    {
        var removeCommand = new RemoveCustomerCommand(id);
        _bus.SendCommand(removeCommand);
    }

    public IList<CustomerHistoryData> GetAllHistory(Guid id) =>
        CustomerHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));

    public void Dispose() => GC.SuppressFinalize(this);
}
