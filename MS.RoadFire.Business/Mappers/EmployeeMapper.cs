using MS.RoadFire.Business.Models;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.Business.Mappers
{
    public static class EmployeeMapper
    {
        public static Employee Map(this EmployeeDto dto) => new Employee()
        {
            Address = dto.Address,
            Email = dto.Email,
            FirtsName = dto.FirtsName,
            Id = dto.Id,
            IsActive = dto.IsActive,
            Mobile = dto.Mobile,
            Phone = dto.Phone,
            SecondName = dto.SecondName,
            SecondSurname = dto.SecondSurname,
            Surname = dto.Surname,
            BornDate = dto.BornDate,
        };

        public static EmployeeDto Map(this Employee model) => new EmployeeDto()
        {
            Address = model.Address,
            Email = model.Email,
            FirtsName = model.FirtsName,
            Id = model.Id,
            IsActive = model.IsActive,
            Mobile = model.Mobile,
            Phone = model.Phone,
            SecondName = model.SecondName,
            SecondSurname = model.SecondSurname,
            Surname = model.Surname,
            BornDate = model.BornDate,
        };
    }
}
