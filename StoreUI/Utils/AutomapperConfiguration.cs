using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using StoreUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreUI.Utils
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<LaptopViewModel, LaptopDTO>()
                .ForMember(x => x.DeveloperId, s => s.MapFrom(z => z.DeveloperId))
                   .ForMember(x => x.LaptopTypeId, s => s.MapFrom(z => z.LaptopTypeId));

            CreateMap<CartItem,OrderDetailsViewModel>()
                  .ForMember(x => x.LaptopName, s => s.MapFrom(z => z.Laptop.Name))
                     .ForMember(x => x.LaptopImage, s => s.MapFrom(z => z.Laptop.Image))
                        .ForMember(x => x.LaptopPrice, s => s.MapFrom(z => z.Laptop.Price))
                           .ForMember(x => x.LaptopId, s => s.MapFrom(z => z.Laptop.Id));

            CreateMap<CartDTO, HistoryViewModel>()
                .ForMember(x => x.Id, s => s.MapFrom(z => z.Id))
                .ForMember(x => x.State, s => s.MapFrom(z => z.State));
                    


        }
    }
}