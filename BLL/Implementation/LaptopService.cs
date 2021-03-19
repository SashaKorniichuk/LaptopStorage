using AutoMapper;
using BLL.DTO;
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

        private readonly IGenericRepository<LaptopType> _laptopTypeRepository;

        public LaptopService(IGenericRepository<Laptop> laptopRepository,
        IGenericRepository<Developer> developerRepository, IGenericRepository<LaptopType> laptopTypeRepository)
        {
            _developerRepository = developerRepository;
            _laptopRepository = laptopRepository;
            _laptopTypeRepository = laptopTypeRepository;
        }
        public async Task AddLaptopAsync(LaptopDTO laptop)
        {
            Laptop l = new Laptop()
            {
                Id = laptop.Id,
                Name = laptop.Name,
                Processor = laptop.Processor,
                RAM = laptop.RAM,
                VideoCard = laptop.VideoCard,
                Disc = laptop.Disc,
                Price = laptop.Price,
                Description = laptop.Description,
                Image = laptop.Image,
                Developer = laptop.Developer,
                LaptopType = laptop.LaptopType
           };
           await _laptopRepository.CreateAsync(l);
        }
        public IEnumerable<DeveloperDTO> GetAllDevelopers()
        {
            var mapper = new MapperConfiguration(x => x.CreateMap<Developer, DeveloperDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Developer>, List<DeveloperDTO>>(_developerRepository.GetAll());
        }

        public IEnumerable<LaptopDTO> GetAllLaptops()
        {
            var mapper = new MapperConfiguration(x => x.CreateMap<Laptop, LaptopDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Laptop>, List<LaptopDTO>>(_laptopRepository.GetAll());
        }

        public IEnumerable<LaptopTypeDTO> GetAllTypes()
        {
            var mapper = new MapperConfiguration(x => x.CreateMap<LaptopType, LaptopTypeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<LaptopType>, List<LaptopTypeDTO>>(_laptopTypeRepository.GetAll());
        }
    }
}
