namespace Shared.ViewModels;

public class UserBaseModel
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public bool IsSysAdmin { get; set; }
    public List<UserRoleDto>? UserRoles { get; set; }
}
