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
        private readonly IGenericRepository<Developer> _developerRepository;

        private readonly IGenericRepository<LaptopType> _laptopTyperRepository;

        public LaptopService(IGenericRepository<Laptop> laptopRepository,
        IGenericRepository<Developer> developerRepository, IGenericRepository<LaptopType> laptopTypeRepository)
        {
            _developerRepository = developerRepository;
            _laptopRepository = laptopRepository;
            _laptopTyperRepository = laptopTypeRepository;
        }
        public async Task AddLaptopAsync(Laptop laptop)
        {
            await _laptopRepository.CreateAsync(laptop);
        }
        public IEnumerable<Developer> GetAllDevelopers()
        {
            return _developerRepository.GetAll();
        }

        public IEnumerable<Laptop> GetAllLaptops()
        {
            return _laptopRepository.GetAll();
        }

        public IEnumerable<LaptopType> GetAllTypes()
        {
            return _laptopTyperRepository.GetAll();
        }
    }
}
