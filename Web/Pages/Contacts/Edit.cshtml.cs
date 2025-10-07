namespace Web.Pages.Contacts;

public class EditModel(IContactService contacts) : PageModel
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

        var contact =  await _contacts.GetByIdAsync(id.Value);
        if (contact == null)
        {
            return NotFound();
        }
        Contact = contact;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _contacts.UpdateAsync(Contact);


        return RedirectToPage("./Index");
    }
}
