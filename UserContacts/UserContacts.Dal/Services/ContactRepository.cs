using Microsoft.EntityFrameworkCore;
using UserContacts.Core.Entities;
using UserContacts.Core.Errors;
using UserContacts.Dal;

namespace UserContacts.Repository.Services;

public class ContactRepository(MainContext _mainContext) : IContactRepository
{
    public async Task<long> AddContactAsync(Contact contact)
    {
        await _mainContext.Contacts.AddAsync(contact);
        await _mainContext.SaveChangesAsync();
        return contact.Id;
    }

    public async Task DeleteContactAsync(long contactId, long userId)
    {
        var contact = await GetContactByIdAsync(contactId, userId);
        _mainContext.Contacts.Remove(contact);
        await _mainContext.SaveChangesAsync();
    }

    public async Task<List<Contact>> GetAllContactsAsync(long userId)
    {
        return await _mainContext.Contacts.Where(_ => _.UserId == userId).ToListAsync();
    }

    public async Task<Contact> GetContactByIdAsync(long contactId, long userId)
    {
        var contact = await _mainContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
        if (contact.UserId != userId)
        {
            throw new ForbiddenException("User id not allowed");
        }
        return contact;
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        _mainContext.Contacts.Update(contact);
        await _mainContext.SaveChangesAsync();
    }
}
