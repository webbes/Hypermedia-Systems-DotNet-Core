namespace Web.Pages.Contacts;

public class DeleteModel(IContactService contacts) : PageModel
{
    private readonly IContactService _contacts = contacts ?? throw new ArgumentNullException(nameof(contacts));

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contact = await _contacts.GetByIdAsync(id.Value);
        if (contact != null)
        {
            await _contacts.DeleteAsync(id.Value);
        }

        return RedirectToPage("./Index");
    }
}
