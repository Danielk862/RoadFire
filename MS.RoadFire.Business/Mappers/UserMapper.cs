using MS.RoadFire.Business.Models;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Business.Mappers
{
    public static class UserMapper
    {
        public static User Map(this UserDto dto) => new User()
        {
            EmployeeId = dto.EmployeeId,
            Id = dto.Id,
            Password = dto.Password,
            RoleId = dto.RoleId,
            State = dto.State,
            Username = dto.Username
        };

        public static UserDto Map(this User model) => new UserDto()
        {
            EmployeeId = model.EmployeeId,
            Id = model.Id,
            Password = model.Password,
            RoleId = model.RoleId,
            State = model.State,
            Username = model.Username
        };
    }
}
