namespace Models;

public class Contact
{
    [Key]
    public int Id { get; set; }

    [MaxLength(32)]
    public string? First { get; set; }

    [MaxLength(32)]
    public string? Last { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
