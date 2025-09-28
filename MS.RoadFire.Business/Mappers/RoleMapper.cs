using MS.RoadFire.Business.Models;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Business.Mappers
{
    public static class RoleMapper
    {
        public static Role Map(this RoleDto role) => new Role()
        {
            Description = role.Description,
            Id = role.Id,
            Name = role.Name,
        };

        public static RoleDto Map(this Role entity) => new RoleDto()
        {
            Description = entity.Description,
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
