namespace Shared.Mock;

public static class UsersFaker
{
    public static UserModel User(int id)
    {
        return new()
        {
            Id = id,
            Name = $"Test{id}",
            Age = id * 2,
            Email = $"test{id}@test.com",
            Address = $"St Saint Louis, 1234-{id}",
            City = "Lousiana",
            Country = "US",
            PostalCode = $"9{id}{id}-9{id}"
        };
    }

    public static List<UserModel> UsersList()
    {
        return [User(10), User(20), User(30), User(40)];
    }
}
