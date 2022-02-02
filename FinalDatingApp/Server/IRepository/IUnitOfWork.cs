using FinalDatingApp.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FinalDatingApp.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Person> Persons { get; }
        IGenericRepository<Match> Matchs { get; }
        IGenericRepository<Media> Medias { get; }
        IGenericRepository<Message> Messages { get; }
        IGenericRepository<Preference> Preferences { get; }
        IGenericRepository<Block> Blocks { get; }
    }
}