using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GADev.BarberPoint.Application.Commands.Service;
using GADev.BarberPoint.Application.Repositories.Core;
using GADev.BarberPoint.Application.Responses;
using MediatR;

namespace GADev.BarberPoint.Application.Handlers.CommandHandlers
{
    public class ServiceCommandHandler : IRequestHandler<CreateServiceCommand, ResponseService>,
                                         IRequestHandler<ChangeServiceCommand, ResponseService>,
                                         IRequestHandler<RemoveServiceCommand, ResponseService>
    {
        private readonly IGenericRepository<Domain.Entities.Service> _respositoryService;

        public ServiceCommandHandler(IGenericRepository<Domain.Entities.Service> respositoryService)
        {
            _respositoryService = respositoryService;
        }

        public async Task<ResponseService> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var service = new Domain.Entities.Service
                {
                    Duration = command.Duration,
                    Name = command.Name,
                    Value = command.Value,
                    BarberShopId = command.BarberShopId
                };

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    int? result = await _respositoryService.CreateAsync(service);

                    if (result > 0)
                    {
                        return new ResponseService(result);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error inserting BarberShop: {service.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        public async Task<ResponseService> Handle(ChangeServiceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var service = await _respositoryService.GetAsync(command.Id);

                service.Duration = command.Duration;
                service.Name = command.Name;
                service.Value = command.Value;
                service.BarberShopId = command.BarberShopId;
                service.UpdateAt = DateTime.UtcNow;

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    bool result = await _respositoryService.UpdateAsync(service);

                    if (result)
                    {
                        return new ResponseService(result);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error updating BarberShop: {service.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        public async Task<ResponseService> Handle(RemoveServiceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var service = new Domain.Entities.Service(command.Id);

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    bool result = await _respositoryService.DeleteAsync(service);

                    if (result)
                    {
                        return new ResponseService(result);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error deleting BarberShop: {service.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }
    }
}