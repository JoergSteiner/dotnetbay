using DotNetBay.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.EF
{
    class EFMainRepositoryFactory : IRepositoryFactory
    {

      private EFMainRepository repo;

        public IMainRepository CreateMainRepository()
        {
          repo = new EFMainRepository();
          return repo;
        }

        public void Dispose()
        {
          repo.Database.Delete();
          repo = null;
        }
    }
}
