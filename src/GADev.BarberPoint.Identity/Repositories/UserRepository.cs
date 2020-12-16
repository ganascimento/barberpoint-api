using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using GADev.BarberPoint.Identity.Models;
using MySql.Data.MySqlClient;

namespace GADev.BarberPoint.Identity.Repositories
{
    public class UserRepository :
        IUserStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var query = $@"INSERT INTO tb_user 
                            (   
                                name,
                                user_name, 
                                normalized_user_name, 
                                email,
                                normalized_email, 
                                email_confirmed, 
                                password_hash,
                                create_at
                            )
                            VALUES 
                            (
                                @{nameof(ApplicationUser.Name)},
                                @{nameof(ApplicationUser.UserName)}, 
                                @{nameof(ApplicationUser.NormalizedUserName)}, 
                                @{nameof(ApplicationUser.Email)},
                                @{nameof(ApplicationUser.NormalizedEmail)}, 
                                @{nameof(ApplicationUser.EmailConfirmed)},
                                @{nameof(ApplicationUser.PasswordHash)},
                                @{nameof(ApplicationUser.CreateAt)}
                            );
                            SELECT LAST_INSERT_ID();";

                user.Id = await connection.QuerySingleAsync<int>(query, user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM tb_user WHERE id = @{nameof(ApplicationUser.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"
                    SELECT  id as Id,
                            name as Name,
                            user_name as UserName,
                            normalized_user_name as NormalizedUserName,
                            email as Email,
                            normalized_email as NormalizedEmail,
                            email_confirmed as EmailConfirmed,
                            create_at as CreateAt,
                            update_at as UpdateAt
                    FROM    tb_user
                    WHERE   id = @{nameof(userId)}", 
                    new { userId });
            }
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"
                    SELECT  id as Id,
                            name as Name,
                            user_name as UserName,
                            normalized_user_name as NormalizedUserName,
                            email as Email,
                            normalized_email as NormalizedEmail,
                            email_confirmed as EmailConfirmed,
                            create_at as CreateAt,
                            update_at as UpdateAt
                    FROM    tb_user
                    WHERE   normalized_user_name = @{nameof(normalizedUserName)}", 
                    new { normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"
                    UPDATE  tb_user 
                    SET     name = @{nameof(ApplicationUser.Name)},
                            user_name = @{nameof(ApplicationUser.UserName)},
                            normalized_user_name = @{nameof(ApplicationUser.NormalizedUserName)},
                            email = @{nameof(ApplicationUser.Email)},
                            normalized_email = @{nameof(ApplicationUser.NormalizedEmail)},
                            email_confirmed = @{nameof(ApplicationUser.EmailConfirmed)},
                            password_hash = @{nameof(ApplicationUser.PasswordHash)},
                            update_at = {DateTime.UtcNow}
                    WHERE   id = @{nameof(ApplicationUser.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"
                    SELECT  id as Id,
                            name as Name,
                            user_name as UserName,
                            normalized_user_name as NormalizedUserName,
                            email as Email,
                            normalized_email as NormalizedEmail,
                            email_confirmed as EmailConfirmed,
                            create_at as CreateAt,
                            update_at as UpdateAt
                    FROM    tb_user
                    WHERE   normalized_email = @{nameof(normalizedEmail)}", 
                    new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<string>($@"
                    SELECT  password_hash
                    FROM    tb_user
                    WHERE   id = @{nameof(user.Id)}", 
                    new { user.Id });
            }
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($@"
                    SELECT  Id 
                    FROM    tb_role 
                    WHERE   normalized_name = @{nameof(normalizedName)}", 
                    new { normalizedName });

                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync($@"
                        INSERT  INTO tb_role
                        (
                            name,
                            normalized_name
                        ) 
                        VALUES
                        (
                            @{nameof(roleName)}, 
                            @{nameof(normalizedName)}
                        )",
                        new { roleName, normalizedName });

                await connection.ExecuteAsync($@"                                     
                    INSERT IGNORE INTO tb_user_role
                    (
                        id_user, 
                        id_role
                    ) 
                    VALUES
                    (
                        @userId, 
                        @roleId
                    )",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>(@"
                    SELECT  id 
                    FROM    tb_role 
                    WHERE   normalized_name = @normalizedName", 
                    new { normalizedName = roleName.ToUpper() });

                if (!roleId.HasValue)
                    await connection.ExecuteAsync($@"
                    DELETE  FROM tb_user_role 
                    WHERE   id_user = @userId AND 
                            id_role = @{nameof(roleId)}", 
                    new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>(@"
                    SELECT  r.name 
                    FROM    tb_role r 
                    INNER   JOIN tb_user_role ur 
                    ON      ur.id_role = r.Id
                    WHERE  ur.id_user = @userId", 
                    new { userId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>(@"
                    SELECT  id 
                    FROM    tb_role 
                    WHERE   normalized_name = @normalizedName", 
                    new { normalizedName = roleName.ToUpper() });

                if (roleId == default(int)) return false;
                
                var matchingRoles = await connection.ExecuteScalarAsync<int>($@"
                    SELECT  COUNT(*) 
                    FROM    tb_user_role 
                    WHERE   id_user = @userId AND 
                            id_role = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<ApplicationUser>(@"
                    SELECT  u.* 
                    FROM    tb_user u
                    INNER   JOIN tb_user_role ur 
                    ON      ur.id_user = u.id 
                    INNER   JOIN tb_role r 
                    ON      r.id = ur.id_role 
                    WHERE   r.normalized_name = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            
        }
    }
}