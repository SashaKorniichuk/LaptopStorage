﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interface
{
   public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);


    }
}
