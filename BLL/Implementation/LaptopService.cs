using BLL.Interfaces;
using DAL.Entities;
using DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class LaptopService : ILaptopService
    {
        private readonly IGenericRepository<Laptop> _laptopRepository;
        public LaptopService(IGenericRepository<Laptop> laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }
        public async Task AddLaptop(Laptop laptop)
        {
            await _laptopRepository.Create(laptop);
        }


        public IEnumerable<Developer> GetAllDevelopers()
        {
           
        }

        public IEnumerable<Laptop> GetAllLaptops()
        {
            
        }

        public IEnumerable<LaptopType> GetAllTypes()
        {
            
        }
    }
}
