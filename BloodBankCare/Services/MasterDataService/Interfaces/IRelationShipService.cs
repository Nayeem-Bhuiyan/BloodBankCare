using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IRelationShipService
    {
        Task<IEnumerable<RelationShip>> GetAllRelationShip();
        Task<RelationShip> GetRelationShipById(int? id);
        Task<int> SaveRelationShip(RelationShip model);
        Task<bool> DeleteRelationShipById(int? id);
    }
}
