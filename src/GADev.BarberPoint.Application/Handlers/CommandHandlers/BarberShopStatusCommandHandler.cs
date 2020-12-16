using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GADev.BarberPoint.Application.Commands.BarberShopStatus;
using GADev.BarberPoint.Application.Repositories.Core;
using GADev.BarberPoint.Application.Responses;
using GADev.BarberPoint.Application.Services.Interfaces;
using GADev.BarberPoint.Domain.Entities;
using MediatR;

namespace GADev.BarberPoint.Application.Handlers.CommandHandlers
{
    public class BarberShopStatusCommandHandler : IRequestHandler<ChangeBarberShopStatusCommand, ResponseService>
    {
        private readonly IGenericRepository<BarberShopStatus> _repositoryBarberShopStatus;
        private readonly IGenericRepository<BarberShop> _repositoryBarberShop;
        private readonly IGenericRepository<Plan> _repositoryPlan;
        private readonly IGenericRepository<PlanType> _repositoryPlanType;

        private readonly IIdentityService _identity;

        public BarberShopStatusCommandHandler(
            IGenericRepository<BarberShopStatus> repositoryBarberShopStatus, 
            IGenericRepository<BarberShop> repositoryBarberShop,
            IGenericRepository<Plan> repositoryPlan,
            IGenericRepository<PlanType> repositoryPlanType,
            IIdentityService identity
        )
        {
            _repositoryBarberShopStatus = repositoryBarberShopStatus;
            _repositoryBarberShop = repositoryBarberShop;
            _repositoryPlan = repositoryPlan;
            _repositoryPlanType = repositoryPlanType;
            _identity = identity;
        }

        public async Task<ResponseService> Handle(ChangeBarberShopStatusCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var plan = await _repositoryPlan.GetAsync(command.PlanId);

                if (plan == null)
                {
                    return new ResponseService(messageError: "This plan does not exists");
                }

                int userId = _identity.GetUserIdentity();
                var barberShop = await _repositoryBarberShop.GetAsync(command.BarberShopId);

                if (barberShop.UserAdminId == userId)
                {
                    var planType = await _repositoryPlanType.GetAsync(plan.PlanTypeId);
                    var barberShopStatus = await _repositoryBarberShopStatus.GetAsync(barberShop.BarberShopStatusId);
                    var expiration = barberShopStatus.ExpirationDate.AddDays(planType.Days);

                    barberShopStatus = new Domain.Entities.BarberShopStatus
                    {
                        PlanId = plan.Id.GetValueOrDefault(),
                        ExpirationDate = expiration,
                        UpdateAt = DateTime.UtcNow
                    };

                    using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        bool result = await _repositoryBarberShopStatus.UpdateAsync(barberShopStatus);

                        if (result)
                        {
                            return new ResponseService(result);
                        }
                        else
                        {
                            return new ResponseService(messageError: $"Error updating BarberShopStatus: {barberShopStatus.Id}");
                        }
                    }
                }
                else
                {
                    return new ResponseService(messageError: "User isn't admin");    
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }
    }
}