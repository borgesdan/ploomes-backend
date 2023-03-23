using Microsoft.EntityFrameworkCore;
using Ploomes.Application.Data.Context;
using Ploomes.Application.Data.Entities.Sql;
using System.Linq.Expressions;

namespace Ploomes.Application.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserEntity?> Get(Expression<Func<UserEntity, bool>> predicate)
            => await _context.Users.Where(predicate).FirstOrDefaultAsync();

        public async Task<UserEntity?> GetByUidAsync(string uid, bool includePerson = false)
        {
            var query = _context.Users.Where(u => u.Uid == new Guid(uid));

            if (includePerson)
                query = query.Include(u => u.Person);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserEntity?> GetByEmail(string email, bool includePerson = false)
        {
            var query = _context.Users.Where(u => u.Email == email);

            if (includePerson)
                query = query.Include(u => u.Person);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserEntity?> GetByLogin(string email, string password)
            => await _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

        public async Task Update(UserEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity> CreateAsync(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<PersonEntity> CreatePersonAsync(PersonEntity person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
    }
}