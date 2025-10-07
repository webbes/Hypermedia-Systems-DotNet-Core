namespace Domain;

internal class ContactService(HypermediaDbContext context) : IContactService
{
    public Task<List<Contact>> GetAllAsync()
        => context.Contacts.ToListAsync();

    public ValueTask<Contact?> GetByIdAsync(int id)
        => context.Contacts.FindAsync(id);

    public Task<Contact?> GetByEmailAsync(string email)
        => context.Contacts.FirstOrDefaultAsync(c => c.Email == email);

    public Task<List<Contact>> SearchAsync(string? term = null)
        => context.Contacts
            .Where(c => string.IsNullOrEmpty(term)
                    || (c.First!.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        c.Last!.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        c.Email.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        c.Phone!.Contains(term, StringComparison.OrdinalIgnoreCase)))
            .ToListAsync();

    public async Task<Contact> CreateAsync(Contact contact)
    {
        if (await ExistsAsync(contact.Email))
        {
            throw new ArgumentException("A contact with the same email already exists.", nameof(contact));
        }

        var entity = new Contact
        {
            First = contact.First,
            Last = contact.Last,
            Phone = contact.Phone,
            Email = contact.Email
        };

        context.Contacts.Add(entity);
        
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> UpdateAsync(Contact contact)
    {
        var entity = await context.Contacts.FindAsync(contact.Id);
        if (entity is null)
        {
            return false;
        }

        entity.First = contact.First;
        entity.Last = contact.Last;
        entity.Phone = contact.Phone;

        if (entity.Email != contact.Email && await ExistsAsync(contact.Email))
        {
            throw new ArgumentException("A contact with the same email already exists.", nameof(contact));
        }

        entity.Email = contact.Email;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Contacts.FindAsync(id);
        if (entity is null)
        {
            return false;
        }

        context.Contacts.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public Task<bool> ExistsAsync(int id) => context.Contacts.AnyAsync(e => e.Id == id);
    public Task<bool> ExistsAsync(string email) => context.Contacts.AnyAsync(e => e.Email == email);
}
