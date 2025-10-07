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

        try
        {
            await _contacts.CreateAsync(Contact);
        }
        catch (ArgumentException)
        {
            ModelState.AddModelError("Contact.Email", "A contact with the same email already exists!");
            return Page();
        }

        TempData["SuccessMessage"] = "Contact created successfully.";
        return RedirectToPage("./Index");
    }
}
