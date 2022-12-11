using Mangomic.Context;
using Mangomic.Domain;

namespace Mangomic.DAL {
    internal class UserRepository : Repository<User>, IUserRepository {

        public UserRepository(MangomicContext context) : base(context) {
        }

        public async Task<User> AddUser(User user) {
            var exists = Find(u => u.Email == user.Email);
            if (exists != null) throw new ArgumentException("User already exists");
            await MangomicContext.Set<User>().AddAsync(user);
            return user;
        }
        private MangomicContext MangomicContext { get { return MangomicContext;} }
    }
}
