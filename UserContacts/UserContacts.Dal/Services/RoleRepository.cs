using Microsoft.EntityFrameworkCore;
using UserContacts.Core.Entities;
using UserContacts.Core.Errors;
using UserContacts.Dal;

namespace UserContacts.Repository.Services;

public class RoleRepository(MainContext _context) : IRoleRepository
{
    public async Task<List<UserRole>> GetAllRolesAsync()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task<ICollection<User>> GetAllUsersByRoleAsync(string role)
    {
        var foundRole = await _context.UserRoles.Include(u => u.Users).FirstOrDefaultAsync(_ => _.Name == role);
        if (foundRole is null)
        {
            throw new EntityNotFoundException(role);
        }
        return foundRole.Users;
    }

    public async Task<long> GetRoleIdAsync(string role)
    {
        var foundRole = await _context.UserRoles.FirstOrDefaultAsync(_ => _.Name == role);
        if (foundRole is null)
        {
            throw new EntityNotFoundException(role + " - not found");
        }
        return foundRole.Id;
    }
}
