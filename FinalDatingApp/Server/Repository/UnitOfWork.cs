using FinalDatingApp.Server.IRepository;
using FinalDatingApp.Server.Data;
using FinalDatingApp.Server.Models;
using FinalDatingApp.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalDatingApp.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Person> _persons;
        private IGenericRepository<Match> _matchs;
        private IGenericRepository<Media> _medias;
        private IGenericRepository<Message> _messages;
        private IGenericRepository<Preference> _preferences;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Person> Persons
            => _persons ??= new GenericRepository<Person>(_context);
        public IGenericRepository<Match> Matchs
            => _matchs ??= new GenericRepository<Match>(_context);
        public IGenericRepository<Media> Medias
            => _medias ??= new GenericRepository<Media>(_context);
        public IGenericRepository<Message> Messages
            => _messages ??= new GenericRepository<Message>(_context);
        public IGenericRepository<Preference> Preferences
            => _preferences ??= new GenericRepository<Preference>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            await _context.SaveChangesAsync();
        }
    }
}