using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BargainBarterV2.Models
{
    public interface IBarterAdRepository : IDisposable
    {
        IEnumerable<BarterAdd> GetBarterAdds();
        BarterAdd GetBarterAddById(int barterAddId);
        void InsertBarterAdd(BarterAdd barterAdd);
        void DeleteBarter(int barterAddId);
        void UpdateBarterAdd(BarterAdd barterAdd);
        void Save();
    }
}