using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL.Reporsitory
{
    public class TeamRepository : GenericRepository<Team>
    {
        public TeamRepository(TARDBContext context) : base(context)
        {
            
        }

        public override IEnumerable<Team> Get()
        {
            return base.Get();
        }

        public override IEnumerable<Team> GetAll()
        {
            return base.GetAll();
        }

        public override Team GetByID(object id)
        {
            return base.GetByID(id);
        }

        public override IEnumerable<Team> GetMany(Func<Team, bool> where)
        {
            return base.GetMany(where);
        }

        public override IQueryable<Team> GetManyQueryable(Func<Team, bool> where)
        {
            return base.GetManyQueryable(where);
        }
    }
}
