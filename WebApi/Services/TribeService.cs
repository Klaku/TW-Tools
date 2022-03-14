using System.Collections.Generic;
using WebApi.Models;
using CoreApi.Models.DB;
using System.Linq;
namespace WebApi.Services
{
    public class TribeService
    {
        readonly CustomContext _db;
        public TribeService()
        {
            _db = new CustomContextFactory().CreateDbContext(null);
        }
        public List<dtoTribeModel> GetTribes(int id)
        {
            return _db.TribeCurrent.Where(x => x.WorldId == id).Select(tribe => new dtoTribeModel()
            {
                Id = tribe.Id,
                Name = tribe.Name,
                Tag = tribe.Tag,
            }).ToList();
        }
    }
}
