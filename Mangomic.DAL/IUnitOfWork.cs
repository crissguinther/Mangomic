namespace Mangomic.DAL {
    public interface IUnitOfWork : IDisposable {
        IUserRepository Users { get; }
        Task<int> Complete();
    }
}
