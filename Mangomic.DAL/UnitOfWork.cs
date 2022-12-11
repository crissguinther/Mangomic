using Mangomic.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangomic.DAL {
    public class UnitOfWork : IUnitOfWork {
        private readonly MangomicContext _context;

        public UnitOfWork(MangomicContext context) {
            _context = context;
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get;  }

        public Task<int> Complete() {
            return _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
