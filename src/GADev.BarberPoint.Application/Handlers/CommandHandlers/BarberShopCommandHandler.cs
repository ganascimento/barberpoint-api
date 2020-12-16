using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GADev.BarberPoint.Application.Commands.BarberShop;
using GADev.BarberPoint.Application.Repositories.Core;
using GADev.BarberPoint.Application.Responses;
using GADev.BarberPoint.Application.Services.Interfaces;
using MediatR;

namespace GADev.BarberPoint.Application.Handlers.CommandHandlers
{
    public class BarberShopCommandHandler : IRequestHandler<CreateBarberShopCommand, ResponseService>,
                                            IRequestHandler<ChangeBarberShopCommand, ResponseService>,
                                            IRequestHandler<RemoveBarberShopCommand, ResponseService>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<Domain.Entities.BarberShop> _repositoryBarberShop;
        private readonly IGenericRepository<Domain.Entities.Address> _repositoryAddress;
        private readonly IGenericRepository<Domain.Entities.BarberShopStatus> _repositoryBarberShopStatus;
        private readonly IIdentityService _identity;
        private readonly IImageService _imageService;

        public BarberShopCommandHandler(
            IMediator mediator,
            IGenericRepository<Domain.Entities.BarberShop> repositoryBarberShop,
            IGenericRepository<Domain.Entities.Address> repositoryAddress,
            IGenericRepository<Domain.Entities.BarberShopStatus> repositoryBarberShopStatus,
            IIdentityService identity,
            IImageService imageService
        )
        {
            _mediator = mediator;
            _repositoryBarberShop = repositoryBarberShop;
            _repositoryAddress = repositoryAddress;
            _repositoryBarberShopStatus = repositoryBarberShopStatus;
            _identity = identity;
            _imageService = imageService;
        }

        public async Task<ResponseService> Handle(CreateBarberShopCommand command, CancellationToken cancellationToken)
        {                        
            try 
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                var address = new Domain.Entities.Address
                {
                    PublicPlace = command.PublicPlace,
                    Number = command.Number,
                    Neighborhood = command.Neighborhood,
                    Locality = command.Locality,
                    State = command.State,
                    Latitude = command.Latitude,
                    Longitude = command.Longitude
                };

                var barberShopStatus = new Domain.Entities.BarberShopStatus(DateTime.UtcNow);

                var barberShop = new Domain.Entities.BarberShop
                {
                    IsActive = false,
                    Name = command.Name,
                    UserAdminId = _identity.GetUserIdentity()
                };

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (!string.IsNullOrEmpty(command.LogoBase64.Trim()))
                    {
                        string nameFile = Guid.NewGuid().ToString();
                        barberShop.PathLogo = nameFile;

                        _imageService.SaveImage(command.LogoBase64, nameFile);
                    }

                    int? addressId = await _repositoryAddress.CreateAsync(address);
                    int? barberShopStatusId = await _repositoryBarberShopStatus.CreateAsync(barberShopStatus);

                    barberShop.AddressId = addressId.GetValueOrDefault();
                    barberShop.BarberShopStatusId = barberShopStatusId.GetValueOrDefault();

                    int? barberShopId = await _repositoryBarberShop.CreateAsync(barberShop);

                    if (barberShopId > 0)
                    {
                        transaction.Complete();
                        return new ResponseService(barberShopId);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error inserting BarberShop: {barberShop.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }

            // CreateBarberShopNotification addBarberShopNotification = new CreateBarberShopNotification(barberShop);
            // await _mediator.Publish(addBarberShopNotification);
        }

        public async Task<ResponseService> Handle(ChangeBarberShopCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var barberShop = await _repositoryBarberShop.GetAsync(command.Id);
                barberShop.Name = command.Name;
                barberShop.IsActive = command.IsActive;
                barberShop.UpdateAt = DateTime.UtcNow;

                var address = new Domain.Entities.Address
                {
                    Id = barberShop.AddressId,
                    PublicPlace = command.PublicPlace,
                    Number = command.Number,
                    Neighborhood = command.Neighborhood,
                    Locality = command.Locality,
                    State = command.State,
                    Latitude = command.Latitude,
                    Longitude = command.Longitude,
                    UpdateAt = DateTime.UtcNow
                };

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    bool result;

                    if (command.IsRemovedLogo && !string.IsNullOrEmpty(barberShop.PathLogo))
                    {
                        _imageService.RemoveImage(barberShop.PathLogo);
                    }

                    if (!string.IsNullOrEmpty(command.LogoBase64.Trim()))
                    {
                        string nameFile = Guid.NewGuid().ToString();
                        barberShop.PathLogo = nameFile;

                        _imageService.SaveImage(command.LogoBase64, nameFile);
                    }

                    result = await _repositoryAddress.UpdateAsync(address);
                    
                    if (result) result = await _repositoryBarberShop.UpdateAsync(barberShop);

                    if (result) {
                        transaction.Complete();
                        return new ResponseService(result);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error updating BarberShop: {barberShop.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        public async Task<ResponseService> Handle(RemoveBarberShopCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                var barberShop = await _repositoryBarberShop.GetAsync(command.Id);
                var address = new Domain.Entities.Address(barberShop.AddressId);
                var barberShopStatus = new Domain.Entities.BarberShopStatus(barberShop.BarberShopStatusId);
                
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    bool result;

                    if (!string.IsNullOrEmpty(barberShop.PathLogo))
                    {
                        _imageService.RemoveImage(barberShop.PathLogo);
                    }

                    result = await _repositoryAddress.DeleteAsync(address);
                    if (result) result = await _repositoryBarberShopStatus.DeleteAsync(barberShopStatus);
                    if (result) result = await _repositoryBarberShop.DeleteAsync(barberShop);
                    
                    if (result) {
                        transaction.Complete();
                        return new ResponseService(result);
                    }
                    else
                    {
                        return new ResponseService(messageError: $"Error deleting BarberShop: {barberShop.Name}");
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