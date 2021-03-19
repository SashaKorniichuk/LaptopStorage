using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface ILaptopService
    {
        IEnumerable<LaptopDTO> GetAllLaptops();
        IEnumerable<LaptopTypeDTO> GetAllTypes();
        IEnumerable<DeveloperDTO> GetAllDevelopers();
        Task AddLaptopAsync(LaptopDTO laptop);
    }
}
