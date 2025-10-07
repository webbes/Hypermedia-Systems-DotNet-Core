namespace Domain;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();
    ValueTask<Contact?> GetByIdAsync(int id);
    Task<Contact?> GetByEmailAsync(string email);
    Task<List<Contact>> SearchAsync(string? term = null);
    Task<Contact> CreateAsync(Contact contact);
    Task<bool> UpdateAsync(Contact contact);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(string email);
}