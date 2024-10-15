namespace Shared.ViewModels;

public class UserBaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public bool IsSysAdmin { get; set; }
}
