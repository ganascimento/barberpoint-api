using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GADev.BarberPoint.Application.Commands.Barber;
using GADev.BarberPoint.Application.Repositories.Core;
using GADev.BarberPoint.Application.Responses;
using MediatR;

namespace GADev.BarberPoint.Application.Handlers.CommandHandlers
{
    public class BarberCommandHandler : IRequestHandler<CreateBarberCommand, ResponseService>,
                                        IRequestHandler<ChangeBarberCommand, ResponseService>,
                                        IRequestHandler<RemoveBarberCommand, ResponseService>
    {
        private readonly IGenericRepository<Domain.Entities.Barber> _repositoryBarber;

        public BarberCommandHandler(IGenericRepository<Domain.Entities.Barber> repositoryBarber)
        {
            _repositoryBarber = repositoryBarber;
        }

        public async Task<ResponseService> Handle(CreateBarberCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var barber = new Domain.Entities.Barber
                {
                    BarberShopId = command.BarberShopId,
                    Name = command.Name,
                    TimeFinishWork = command.TimeFinishWork,
                    TimeStartWork = command.TimeStartWork,
                    UserId = command.UserId,
                    IsConfirmed = false
                };

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    int? id = await _repositoryBarber.CreateAsync(barber);

                    if (id > 0)
                    {
                        transaction.Complete();
                        return new ResponseService(true);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error inserting barber: {barber.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        public async Task<ResponseService> Handle(ChangeBarberCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var barber = await _repositoryBarber.GetAsync(command.Id);

                barber.Name = command.Name;
                barber.TimeFinishWork = command.TimeFinishWork;
                barber.TimeStartWork = command.TimeStartWork;
                barber.UpdateAt = DateTime.UtcNow;

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    bool result = await _repositoryBarber.UpdateAsync(barber);
                    
                    if (result)
                    {
                        transaction.Complete();
                        return new ResponseService(result);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error updating barber: {barber.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        public async Task<ResponseService> Handle(RemoveBarberCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var barber = new Domain.Entities.Barber(command.Id);

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    bool result = await _repositoryBarber.DeleteAsync(barber);

                    if (result)
                    {
                        transaction.Complete();
                        return new ResponseService(true);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error deleting barber: {barber.Name}");
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