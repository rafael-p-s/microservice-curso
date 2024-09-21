namespace Infrastructure.Context.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }

    public User() { }

    public User(UserDto userDto)
    {
        Id = userDto.Id;
        Name = userDto.Name;
        Age = userDto.Age;
        Email = userDto.Email;
        Address = userDto.Address;
        City = userDto.City;
        Country = userDto.Country;
        PostalCode = userDto.PostalCode;
    }

    public User(UserBaseDto userBaseDto)
    {
        Name = userBaseDto.Name;
        Age = userBaseDto.Age;
        Email = userBaseDto.Email;
        Address = userBaseDto.Address;
        City = userBaseDto.City;
        Country = userBaseDto.Country;
        PostalCode = userBaseDto.PostalCode;
    }

    public static UserDto? MapDto(User? user)
    {
        if (user is null) return null;

        return new()
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            Email = user.Email,
            Address = user.Address,
            City = user.City,
            Country = user.Country,
            PostalCode = user.PostalCode
        };
    }

    public static List<UserDto> MapDtoList(List<User> users)
    {
        if (!users.Any())
            return [];

        return users.Select(MapDto).ToList()!;
    }
}
