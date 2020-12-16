using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GADev.BarberPoint.Application.Repositories;
using GADev.BarberPoint.Application.Repositories.Core;
using GADev.BarberPoint.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace GADev.BarberPoint.Infrastructure.Repositories
{
    public class BarberShopRepository : IBarberShopRepository
    {
        private readonly string _connectionString;

        public BarberShopRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<BarberShop> GetBarberShopById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string command = $@"
                SELECT 	barbershop.id as Id,
                        barbershop.name as Name,
                        barbershop.path_logo as PathLogo,
                        barbershop.is_active as IsActive,
                        barbershop.id_admin_user as UserAdminId,
                        address.id as IdAddress,
                        address.public_place as  PublicPlace,
                        address.number as Number,
                        address.neighborhood as Neighborhood,
                        address.locality as Locality,
                        address.state as State,
                        address.latitude as Latitude,
                        address.longitude as Longitude,
                        barbershop_status.id as IdBarberShopStatus,
                        barbershop_status.expiration_at as ExpirationDate,
                        plan.Id as IdPlan,
                        plan.name as Name,
                        plan.amount_professional as AmountProfessinal,
                        plan.value as Value,
                        plan_type.id as IdPlanType,
                        plan_type.description as Description,
                        plan_type.days as Days
                FROM	tb_barbershop barbershop
                INNER	JOIN
                        tb_address address
                ON		barbershop.id_address = address.id
                INNER	JOIN
                        tb_barbershop_status barbershop_status
                ON		barbershop.id_barbershop_status = barbershop_status.id
                INNER	JOIN
                        tb_plan plan
                ON		barbershop_status.id_plan = plan.id
                INNER	JOIN
                        tb_plan_type plan_type
                ON		plan.id_plan_type = plan_type.id
                WHERE	barbershop.id = @{nameof(id)}";

                var mapAddress = new CustomPropertyTypeMap(typeof(Address), (type, columnName) => 
                {
                    if (columnName == "IdAddress") return type.GetProperty("Id");                    
                    throw new InvalidOperationException($"No matching mapping for {columnName}");
                });

                var mapBarberShopStatus = new CustomPropertyTypeMap(typeof(BarberShopStatus), (type, columnName) => 
                {
                    if (columnName == "IdBarberShopStatus") return type.GetProperty("Id");                    
                    throw new InvalidOperationException($"No matching mapping for {columnName}");
                });

                var mapPlan = new CustomPropertyTypeMap(typeof(Plan), (type, columnName) => 
                {
                    if (columnName == "IdPlan") return type.GetProperty("Id");                    
                    throw new InvalidOperationException($"No matching mapping for {columnName}");
                });

                var mapPlanType = new CustomPropertyTypeMap(typeof(PlanType), (type, columnName) => 
                {
                    if (columnName == "IdPlanType") return type.GetProperty("Id");                    
                    throw new InvalidOperationException($"No matching mapping for {columnName}");
                });

                SqlMapper.SetTypeMap(typeof(Address), mapAddress);
                SqlMapper.SetTypeMap(typeof(BarberShopStatus), mapBarberShopStatus);
                SqlMapper.SetTypeMap(typeof(Plan), mapPlan);
                SqlMapper.SetTypeMap(typeof(PlanType), mapPlanType);

                var result = (await connection.QueryAsync<BarberShop, Address, BarberShopStatus, Plan, PlanType, BarberShop>(
                    command, 
                    (barberShop, address, barberShopStatus, plan, planType) => {
                        plan.PlanType = planType;
                        barberShopStatus.Plan = plan;
                        barberShop.Address = address;
                        barberShop.BarberShopStatus = barberShopStatus;

                        return barberShop;
                    },
                    splitOn: "IdAddress,IdBarberShopStatus,IdPlan,IdPlanType")).Distinct().ToList();

                return result.First();
            }
        }
    }
}