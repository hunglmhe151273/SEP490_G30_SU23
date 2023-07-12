﻿using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;
using VBookHaven.ViewModels;

namespace VBookHaven.DataAccess.Respository
{

    public interface IShippingInfoRespository
    {
        Task<List<ShippingInfo?>> GetAllShipInfoByUIDAsync(int userID);
        Task<ShippingInfo> GetShipInfoByIdAsync(int Id);
    }

    public class ShippingInfoRespository : IShippingInfoRespository
    {
        private readonly VBookHavenDBContext _dbContext;

        public ShippingInfoRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ShippingInfo?>> GetAllShipInfoByUIDAsync(int userID)
        {
            return await _dbContext.ShippingInfos.Where(s => s.CustomerId== userID).ToListAsync();
        }

        public async Task<ShippingInfo> GetShipInfoByIdAsync(int Id)
        {
            return await _dbContext.ShippingInfos.SingleOrDefaultAsync(x => x.ShipInfoId == Id);
        }
    }
}
