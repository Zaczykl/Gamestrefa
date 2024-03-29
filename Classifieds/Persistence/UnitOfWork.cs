﻿using Classifieds.Core;
using Classifieds.Core.Repositories;
using Classifieds.Persistence.Repositories;

namespace Classifieds.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            ClassifiedRepository = new ClassifiedRepository(context);
            UserRepository = new UserRepository(context);
            PasswordRepository = new PasswordRepository(context);
            EmailRepository = new EmailRepository(context);
        }

        public ICategoryRepository CategoryRepository { get; set; }
        public IClassifiedRepository ClassifiedRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IPasswordRepository PasswordRepository { get; set; }
        public IEmailRepository EmailRepository { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
