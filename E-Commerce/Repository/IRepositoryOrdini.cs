﻿using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IRepositoryOrdini
    {
        public Task SaveOrdine(Ordini ord);
    }
}
