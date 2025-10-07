namespace Web.Pages.Contacts;

public class DetailsModel(IContactService contacts) : PageModel
{
    private readonly IContactService _contacts = contacts ?? throw new ArgumentNullException(nameof(contacts));

    public Contact Contact { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contact = await _contacts.GetByIdAsync(id.Value);

        if (contact is not null)
        {
            Contact = contact;

            return Page();
        }

        return NotFound();
    }
}
