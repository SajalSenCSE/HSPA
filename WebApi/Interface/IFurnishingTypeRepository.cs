using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface IFurnishingTypeRepository
    {
         Task<IEnumerable<FurnishingType>> GetFurnishingTypeAsync();
    }
}