using BLL.DTO;
using BLL.Filters;
using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface ILaptopService
    {
     
        IEnumerable<LaptopDTO> GetAllLaptops(List<LaptopsFilter> filters);
        IEnumerable<LaptopTypeDTO> GetAllTypes();
        IEnumerable<DeveloperDTO> GetAllDevelopers();
        IEnumerable<CartItemDTO> GetAllOrders();
        Task AddLaptopAsync(LaptopDTO laptop);
        Task DeleteAsync(int id);
        Task EditAsync(LaptopDTO laptop);
        Task AddOrder(CartItemDTO order);
        Task AddCart(CartDTO cart);

        Task DeleteOrder(int id);
        IEnumerable<string> GetTypes();
        IEnumerable<string> GetDevelopers();
        IEnumerable<ApplicationUser> GetAdmins();
        Cart GetCart(int id);
        IEnumerable<CartDTO> GetOrderCart();
        IEnumerable<CartItem> GetCartItems(int id);
        Task EditCart(CartDTO cart);
        Task UpdateStateCart(Cart cart);

    }
}
