using AutoMapper;
using Binbin.Linq;
using BLL.DTO;
using BLL.Filters;
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using DAL.Repository.Interface;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Implementation
{
    public class LaptopService : ILaptopService
    {
        private readonly IGenericRepository<Laptop> _laptopRepository;
        private readonly IGenericRepository<Developer> _developerRepository;
        private readonly IGenericRepository<LaptopType> _laptopTypeRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartItem> _cartItemRepository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IGenericRepository<IdentityUserRole> _roleUserRepository;
        private readonly IGenericRepository<IdentityRole> _roleRepository;


        public LaptopService(IGenericRepository<Laptop> laptopRepository,
        IGenericRepository<Developer> developerRepository, IGenericRepository<LaptopType> laptopTypeRepository,IGenericRepository<Cart> cart,
        IGenericRepository<CartItem> cartItemRepository,IGenericRepository<ApplicationUser> userRepository,IGenericRepository <IdentityUserRole> roleUser,
        IGenericRepository<IdentityRole>role)
        {
            _developerRepository = developerRepository;
            _laptopRepository = laptopRepository;
            _laptopTypeRepository = laptopTypeRepository;
            _cartRepository = cart;
            _cartItemRepository = cartItemRepository;
            _userRepository = userRepository;
            _roleUserRepository = roleUser;
            _roleRepository = role;
        }

        public async Task AddCart(CartDTO cart)
        {
            Cart c = new Cart()
            {
                UserId = cart.UserId,
                
                
            };
          await  _cartRepository.CreateAsync(c);
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
                DeveloperId = laptop.DeveloperId,
                LaptopTypeId = laptop.LaptopTypeId,
                
            };
            await _laptopRepository.CreateAsync(l);
        }

        public async Task AddOrder(CartItemDTO order)
        {
            CartItem b = new CartItem()
            {           
                LaptopId = order.LaptopId,
                CartId=order.CartId               
            };     
            await _cartItemRepository.CreateAsync(b);        
        }


        public async Task DeleteAsync(int id)
        {
            await _laptopRepository.DeleteAsync(id);

        }

        public async Task DeleteOrder(int id)
        {
            await _cartItemRepository.DeleteAsync(id);
        }

        public async Task EditAsync(LaptopDTO laptop)
        {
            Laptop l=new Laptop()
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
                DeveloperId = laptop.DeveloperId,
                LaptopTypeId = laptop.LaptopTypeId
            } ;
            await _laptopRepository.UpdateAsync(l);
        }

        public IEnumerable<DeveloperDTO> GetAllDevelopers()
        {
            var _mapper = new MapperConfiguration(x => x.CreateMap<Developer, DeveloperDTO>()).CreateMapper();
            return _mapper.Map<IEnumerable<Developer>, IEnumerable<DeveloperDTO>>(_developerRepository.GetAll());
        }

        public IEnumerable<LaptopDTO> GetAllLaptops(List<LaptopsFilter> filters)
        {
            var _mapper = new MapperConfiguration(x => x.CreateMap<Laptop, LaptopDTO>()
               .ForMember(y => y.Developer, s => s.MapFrom(z => z.Developer.Name))
               .ForMember(y=>y.LaptopType,s=>s.MapFrom(z=>z.LaptopType.Name))
             ).CreateMapper();
            if (filters == null || filters.Count == 0)
            {
                return _mapper.Map<IEnumerable<Laptop>, IEnumerable<LaptopDTO>>(_laptopRepository.GetAll());
            }

            var groupFilter = filters.GroupBy(x => x.Type);
            var predicates = new List<Expression<Func<Laptop, bool>>>();

            foreach (var item in groupFilter)
            {
                var builder = PredicateBuilder.Create(item.ToArray().FirstOrDefault().Predicate);
                for (int i = 1; i < item.Count(); i++)
                {
                    builder = builder.Or(item.ToArray()[i].Predicate);
                }
                predicates.Add(builder);
            }
            var builder2 = PredicateBuilder.Create(predicates.FirstOrDefault());

            for (int i = 1; i < predicates.Count; i++)
            {
                builder2 = builder2.And(predicates[i]);
            }
            return _mapper.Map<IEnumerable<Laptop>, IEnumerable<LaptopDTO>>(_laptopRepository.GetAll().Where(builder2.Compile()));
        }

        public IEnumerable<CartItemDTO> GetAllOrders()
        {
            var _mapper = new MapperConfiguration(x => x.CreateMap<CartItem, CartItemDTO>()).CreateMapper();
            return _mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemDTO>>(_cartItemRepository.GetAll());
        }

        public IEnumerable<LaptopTypeDTO> GetAllTypes()
        {
            var _mapper = new MapperConfiguration(x => x.CreateMap<LaptopType, LaptopTypeDTO>()).CreateMapper();
            return _mapper.Map<IEnumerable<LaptopType>, IEnumerable<LaptopTypeDTO>>(_laptopTypeRepository.GetAll());
        }

        public IEnumerable<string> GetDevelopers()
        {
            return _developerRepository.GetAll().Select(x => x.Name);
        }

     
        public IEnumerable<string> GetTypes()
        {
            return _laptopTypeRepository.GetAll().Select(x => x.Name);
        }

        public IEnumerable<ApplicationUser> GetAdmins()
        {
            var roleIdAdmin = _roleRepository.GetAll().Where(z => z.Name == "Admin").FirstOrDefault().Id;
            var userRole = _roleUserRepository.GetAll().Where(y => y.RoleId == roleIdAdmin).FirstOrDefault();
            var admins=_userRepository.GetAll().Where(x => x.Roles.Any(r=>r.RoleId==userRole.RoleId)).ToList();
            return admins;
        }

        public IEnumerable<CartDTO> GetOrderCart()
        {
            var _mapper = new MapperConfiguration(y=>y.CreateMap<Cart, CartDTO>()
              .ForMember(x => x.UserId, s => s.MapFrom(z => z.UserId))
              .ForMember(x => x.MakeOrder, s => s.MapFrom(z => z.MakeOrder))
              .ForMember(x => x.State, s => s.MapFrom(z => z.State))
              .ForMember(x => x.Id, s => s.MapFrom(z => z.Id))).CreateMapper();


            return _mapper.Map<IEnumerable<Cart>, IEnumerable<CartDTO>>( _cartRepository.GetAll().Where(x=>x.MakeOrder==true)); 
        }
        public Cart GetCart(int id)
        {
            return _cartRepository.Get(id);
        }

        public IEnumerable<CartItem> GetCartItems(int id)
        {
            return _cartItemRepository.GetAll().Where(x => x.CartId == id);
        }

        public async Task EditCart(CartDTO cartDTO)
        {
            Cart c = new Cart()
            {
                Id = cartDTO.Id,
                UserId = cartDTO.UserId,
                State = cartDTO.State,
                MakeOrder = cartDTO.MakeOrder
            };
            await _cartRepository.UpdateAsync(c);
        }

        public async Task UpdateStateCart(Cart cart)
        {
            await _cartRepository.UpdateAsync(cart);
        }

        
       
    }
}
