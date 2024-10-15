namespace Infrastructure.Context.Entities;

public class UserRole
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;

    public static UserRole? Map(UserRoleDto? userRole)
    {
        if (userRole is null) return null;

        return new()
        {
            Id = userRole.Id,
            UserId = userRole.UserId,
            Name = userRole.Name
        };
    }

    public static List<UserRole> MapList(List<UserRoleDto>? userRoles)
    {
        if (!userRoles?.Any() ?? true)
            return [];

        return userRoles!.Select(Map).ToList()!;
    }


    public static UserRoleDto? MapDto(UserRole? userRole)
    {
        if (userRole is null) return null;

        return new()
        {
            Id = userRole.Id,
            UserId = userRole.UserId,
            Name = userRole.Name
        };
    }

    public static List<UserRoleDto> MapDtoList(List<UserRole>? userRoles)
    {
        if (!userRoles?.Any() ?? true)
            return [];

        return userRoles!.Select(MapDto).ToList()!;
    }
}
