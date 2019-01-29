using NUS.TheAmagingRace.BusinessModels;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.BAL
{
    public interface IMemberBAL
    {
        IEnumerable<TARUserViewModel> GetAllTARAdministrators();
        IEnumerable<TARUserViewModel> GetAllStaff();
        IEnumerable<TARUserViewModel> GetAllMember();
        void MoveMembertoAdmin(TARUserViewModel user);
        void MoveMembertoStaff(TARUserViewModel user);
        void RemoveMemberasAdmin(TARUserViewModel user);
        void RemoveMemberasStaff(TARUserViewModel user);

        void MoveMembertoAdmin(string userid);
        void MoveMembertoStaff(string userid);
        void RemoveMemberasAdmin(string userid);
        void RemoveMemberasStaff(string userid);

        void AddStaff(TARUserViewModel user);
        void AddMember(TARUserViewModel user);
        void AddAdministrator(TARUserViewModel user);
        void UpdateUser(TARUserViewModel user);
        void DeleteUser(TARUserViewModel user);
    }
}
