using System.Net;

namespace Web.Controllers;

public class ContactsController(IContactService contacts, LinkGenerator linkGenerator) : Controller
{
    private readonly IContactService _contacts = contacts ?? throw new ArgumentNullException(nameof(contacts));
    private readonly LinkGenerator _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));

    [HttpDelete("Contacts/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var contact = await _contacts.GetByIdAsync(id);

        if (contact == null) {
            return NotFound();
        }
   
        await _contacts.DeleteAsync(id);

        TempData["SuccessMessage"] = "Contact deleted successfully.";

        var contactsUrl = _linkGenerator.GetPathByPage("/Contacts/Index");
        Response.Headers.Location = contactsUrl;
        
        return StatusCode((int)HttpStatusCode.SeeOther);
    }
}
