namespace Web.Pages.Contacts;

public class IndexModel(IContactService contacts) : PageModel
{
    private readonly IContactService _contacts = contacts ?? throw new ArgumentNullException(nameof(contacts));

    public IList<Contact> Contacts { get;set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        Contacts = await _contacts.SearchAsync(SearchTerm);
    }
}
