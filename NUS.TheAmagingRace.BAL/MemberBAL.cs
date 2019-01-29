using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using NUS.TheAmagingRace.BusinessModels;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmagingRace.DAL.UnitOfWork;

namespace NUS.TheAmagingRace.BAL
{
    public class MemberBAL : IMemberBAL
    {
        private TARDBContext db = new TARDBContext();
        
        private readonly UOW _unitOfWork;
        public MemberBAL()
        {
            _unitOfWork = new UOW();

            // need to reset mapper. otherwise, mapper already initialised error            

           


        }

        public void AddAdministrator(TARUserViewModel user)
        {
            // TARUser aRUser= SaveUser(user);
            TARUser aRUser = Mapper.Map<TARUser>(user);
            AddUserToRole(aRUser, "Admin");
        }

        private TARUser SaveUser(TARUserViewModel user)
        {
            using (var scope = new TransactionScope())
            {
                TARUser aUser = Mapper.Map<TARUser>(user);
                aUser.CreatedAt = DateTime.Now;
                _unitOfWork.UserRepository.Insert(aUser);
                _unitOfWork.Save();
                return aUser;
               
            }

        }

        private void AddUserToRole(TARUser tARUser,string RoleName)
        {
            using (var scope = new TransactionScope())
            {
                IdentityUserRole userRole = new IdentityUserRole();
                string roleid= _unitOfWork.RoleRepository.GetFirst(m => m.Name == RoleName)?.Id;
                userRole.RoleId = roleid;
                userRole.UserId = tARUser.Id;
                if (!tARUser.Roles.Any(m => m.RoleId == roleid))
                {
                    tARUser.Roles.Add(userRole);
                }
                _unitOfWork.Save();
                scope.Complete();
            }
        }
        private void RemoveUserToRole(TARUser tARUser, string RoleName)
        {
            using (var scope = new TransactionScope())
            {
                IdentityUserRole userRole = new IdentityUserRole();
                IdentityRole role = _unitOfWork.RoleRepository.GetWithInclude(m => m.Name == RoleName, "Users").First();
                userRole= tARUser.Roles.FirstOrDefault(m=>m.RoleId==role.Id);
                if (userRole != null)
                {
                    role.Users.Remove(userRole);
                }
                               _unitOfWork.Save();
                scope.Complete();
            }
        }
        public void AddMember(TARUserViewModel user)
        {
            TARUser aRUser = Mapper.Map<TARUser>(user);
            AddUserToRole(aRUser, "Member");
        }

        public void AddStaff(TARUserViewModel user)
        {
            TARUser aRUser = Mapper.Map<TARUser>(user);
            AddUserToRole(aRUser, "Staff");
        }

        public void DeleteUser(TARUserViewModel user)
        {
            using (var scope = new TransactionScope())
            {
                TARUser aRUser = Mapper.Map<TARUser>(user);
                _unitOfWork.UserRepository.Delete(aRUser);
                _unitOfWork.Save();
                scope.Complete();
            }
        }

        public IEnumerable<TARUserViewModel> GetAllMember()
        {
            IdentityRole Staffrole = _unitOfWork.RoleRepository.GetFirst(m => m.Name == "Staff");
            IdentityRole Adminrole = _unitOfWork.RoleRepository.GetFirst(m => m.Name == "Admin");
            var users = _unitOfWork.UserRepository.GetWithInclude(m => !(m.Roles.Any(n => n.RoleId == Staffrole.Id)) && !(m.Roles.Any(n => n.RoleId == Adminrole.Id)), "Roles");
            var UserViewModels = Mapper.Map<List<TARUser>, List<TARUserViewModel>>(users.ToList());
            return UserViewModels;

        }

        public IEnumerable<TARUserViewModel> GetAllStaff()
        {
            IdentityRole Staffrole = _unitOfWork.RoleRepository.GetFirst(m => m.Name == "Staff");
           
            var users = _unitOfWork.UserRepository.GetWithInclude(m => (m.Roles.Any(n => n.RoleId == Staffrole.Id)), "Roles");
            var UserViewModels = Mapper.Map<List<TARUser>, List<TARUserViewModel>>(users.ToList());
            return UserViewModels;
        }

        public IEnumerable<TARUserViewModel> GetAllTARAdministrators()
        {
            IdentityRole Adminrole = _unitOfWork.RoleRepository.GetFirst(m => m.Name == "Admin");
            var users = _unitOfWork.UserRepository.GetWithInclude(m => (m.Roles.Any(n => n.RoleId == Adminrole.Id)),"Roles");
            var UserViewModels = Mapper.Map<List<TARUser>, List<TARUserViewModel>>(users.ToList());
            return UserViewModels;
        }

        public void MoveMembertoAdmin(TARUserViewModel user)
        {
           TARUser tARUser= Mapper.Map<TARUserViewModel,TARUser>(user);
            AddUserToRole(tARUser, "Admin");
         }
        public void MoveMembertoAdmin(string userid)
        {
            
            TARUser tARUser = _unitOfWork.UserRepository.GetByID(userid);
            AddUserToRole(tARUser, "Admin");
        }
        public void MoveMembertoStaff(TARUserViewModel user)
        {
            TARUser tARUser = Mapper.Map<TARUserViewModel, TARUser>(user);
            AddUserToRole(tARUser, "Staff");
        }

        public void RemoveMemberasAdmin(TARUserViewModel user)
        {
            TARUser tARUser = Mapper.Map<TARUserViewModel, TARUser>(user);
            RemoveUserToRole(tARUser, "Admin");
        }

        public void RemoveMemberasStaff(TARUserViewModel user)
        {
            TARUser tARUser = Mapper.Map<TARUserViewModel, TARUser>(user);
            RemoveUserToRole(tARUser, "Staff");
        }

        public void UpdateUser(TARUserViewModel user)
        {
            TARUser tARUser = Mapper.Map<TARUserViewModel, TARUser>(user);
            using (var scope=new TransactionScope())
            {
                _unitOfWork.UserRepository.Update(tARUser);
                _unitOfWork.Save();
                scope.Complete();
            }
        }

        public void MoveMembertoStaff(string userid)
        {
            TARUser tARUser = _unitOfWork.UserRepository.GetByID(userid);
            AddUserToRole(tARUser, "Staff");
        }

        public void RemoveMemberasAdmin(string userid)
        {
            TARUser tARUser = _unitOfWork.UserRepository.GetByID(userid);
            RemoveUserToRole(tARUser, "Admin");
        }

        public void RemoveMemberasStaff(string userid)
        {
            TARUser tARUser = _unitOfWork.UserRepository.GetByID(userid);
            RemoveUserToRole(tARUser, "Staff");
        }

        public List<Member> AddMembers(String email,int teamId,int eventId)
        {
             Member member = new Member();
            Event events = db.Events.SingleOrDefault(x => x.EventID == eventId);
            Team teams = db.Teams.SingleOrDefault(x => x.TeamID == teamId);

            member.MemberName = email;
            member.Event = events;
            member.Team = teams;
            db.Members.Add(member);
            db.SaveChanges();

            return getMembers(teamId, eventId);
        }

        public List<Member> getMembers(int teamId, int eventId)
        {
            List<Member> member = new List<Member>();

            var members = from s in db.Members.Include("Team").Include("Event")
                          select s;

            members = members.Where(s => s.Event.EventID==eventId && s.Team.TeamID==teamId);

            return members.ToList();
        }

        public Member GetMemberName(int memberId)
        {
            Member member = db.Members.Include("Team").Include("Event").SingleOrDefault(x => x.MemberId == memberId);

            return member;

        }

        public void deleteMember(int memberId)
        {
            Member member = db.Members.SingleOrDefault(x => x.MemberId == memberId);
            db.Members.Remove(member);
            db.SaveChanges();
        }

        public List<Member> getMembersByTeam(int teamId)
        {
            List<Member> member = new List<Member>();

            var members = from s in db.Members.Include("Team").Include("Event")
                          select s;

            members = members.Where(s => s.Team.TeamID == teamId);

            return members.ToList();
        }
    }
}
