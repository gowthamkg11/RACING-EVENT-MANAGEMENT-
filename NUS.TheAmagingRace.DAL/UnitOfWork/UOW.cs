using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NUS.TheAmagingRace.DAL.Reporsitory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL.UnitOfWork
{
    public class UOW : IDisposable
    {
        #region Private member variables... 
        private TARDBContext _context = null;
        private GenericRepository<TARUser> _userRepository;
        private GenericRepository<Event> _eventRepository;
        private GenericRepository<PitStop> _pitstopRepository;
        private GenericRepository<Location> _locationRepository;
        private GenericRepository<Team> _teamRepository;
        private GenericRepository<IdentityRole> _roleRepository;
        public UOW()
        {
            _context = new TARDBContext();
        }
        #region Public Repository Creation properties... 
        public GenericRepository<TARUser> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<TARUser>(_context);
                return _userRepository;
            }
        }

        public GenericRepository<Event> EventRepository
        {
            get
            {
                if (this._eventRepository == null)
                    this._eventRepository = new GenericRepository<Event>(_context);
                return _eventRepository;
            }
        }
        public GenericRepository<PitStop> PitStopRepository
        {
            get
            {
                if (this._pitstopRepository == null)
                    this._pitstopRepository = new GenericRepository<PitStop>(_context);
                return _pitstopRepository;
            }
        }

        public GenericRepository<Location> LocationRepository
        {
            get
            {
                if (this._locationRepository == null)
                    this._locationRepository = new GenericRepository<Location>(_context);
                return _locationRepository;
            }
        }

        public GenericRepository<Team> TeamRepository
        {
            get
            {
                if (this._teamRepository == null)
                    this._teamRepository = new GenericRepository<Team>(_context);
                return _teamRepository;
            }
        }

        public GenericRepository<IdentityRole> RoleRepository
        {
            get
            {
                if (this._roleRepository == null) { 
                    this._roleRepository = new GenericRepository<IdentityRole>(_context);
                   
                   }
                return this._roleRepository;
            }
        }
        #endregion
        #region Save metthod
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;
            }
        }
        #endregion
        #region Implementing IDiosposable... 
        #region private dispose variable declaration...         
        private bool disposed = false;          
        #endregion 
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        { if (!this.disposed)
            { if (disposing)
                { Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose(); }
            }
            this.disposed = true;
        }
        #endregion
        #endregion



    }
}
