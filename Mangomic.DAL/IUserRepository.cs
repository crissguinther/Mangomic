using Mangomic.Domain;

namespace Mangomic.DAL {
    public interface IUserRepository : IRepository<User> {
        public Task<User> AddUser(User user);
    }
}
