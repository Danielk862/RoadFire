using Microsoft.EntityFrameworkCore;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Context
{
    public class SeedDb
    {
        private readonly DbRoadFireContext _dataContext;

        public SeedDb(DbRoadFireContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckEmployeesAsync();
            await CheckRolesAsync();
            await CheckUsersAsync();
        }

        private async Task CheckRolesAsync()
        {
            if (!_dataContext.Roles.Any())
            {
                _dataContext.Roles.Add(new Role
                {
                    Name = "Administrador",
                    Description = "Rol encargado de administrar el sistema"
                });
                _dataContext.Roles.Add(new Role
                {
                    Name = "Ventas",
                    Description = "Rol encargado de las ventas"
                });
            }
            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckEmployeesAsync()
        {
            if (!_dataContext.Employees.Any())
            {
                _dataContext.Employees.Add(new Employee
                {
                    Address = "Calle 23",
                    BornDate = DateTime.Now,
                    Email = "example@example.com",
                    FirtsName = "Daniel",
                    IsActive = true,
                    Mobile = "3245748578",
                    Phone = string.Empty,
                    SecondName = string.Empty,
                    SecondSurname = string.Empty,
                    Surname = "Gómez"
                });
                _dataContext.Employees.Add(new Employee
                {
                    Address = "Calle 13",
                    BornDate = DateTime.Now,
                    Email = "example1@example.com",
                    FirtsName = "Andrés",
                    IsActive = true,
                    Mobile = "3245745485",
                    Phone = string.Empty,
                    SecondName = string.Empty,
                    SecondSurname = string.Empty,
                    Surname = "Alvarez"
                });
            }
            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckUsersAsync()
        {
            if (!_dataContext.Users.Any())
            {
                var adminRole = await _dataContext.Roles.FirstOrDefaultAsync(r => r.Name == "Administrador");
                var salesRole = await _dataContext.Roles.FirstOrDefaultAsync(r => r.Name == "Ventas");
                var employee1 = await _dataContext.Employees.FirstOrDefaultAsync(e => e.FirtsName == "Daniel");
                var employee2 = await _dataContext.Employees.FirstOrDefaultAsync(e => e.FirtsName == "Andrés");

                if (adminRole == null || salesRole == null || employee1 == null || employee2 == null)
                    throw new Exception("Roles o empleados no encontrados para el seed de usuarios.");

                _dataContext.Users.Add(new User
                {
                    CreatedAt = DateTime.Now,
                    EmployeeId = employee1.Id,
                    Password = "123456",
                    RoleId = adminRole.Id,
                    State = true,
                    Username = "dgomez"
                });
                _dataContext.Users.Add(new User
                {
                    CreatedAt = DateTime.Now,
                    EmployeeId = salesRole.Id,
                    Password = "123456",
                    RoleId = employee2.Id,
                    State = true,
                    Username = "calvarez"
                });
            }
            await _dataContext.SaveChangesAsync();
        }
    }
}
