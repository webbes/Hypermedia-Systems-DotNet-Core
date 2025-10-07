namespace Web.Pages.Contacts;

public class CreateModel(IContactService contacts) : PageModel
{
    private readonly IContactService _contacts = contacts ?? throw new ArgumentNullException(nameof(contacts));

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Contact Contact { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _contacts.CreateAsync(Contact);

        return RedirectToPage("./Index");
    }
}
