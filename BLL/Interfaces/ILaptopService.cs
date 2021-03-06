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
        IEnumerable<Laptop> GetAllLaptops();
        IEnumerable<LaptopType> GetAllTypes();
        IEnumerable<Developer> GetAllDevelopers();
        Task AddLaptopAsync(Laptop laptop);
    }
}
