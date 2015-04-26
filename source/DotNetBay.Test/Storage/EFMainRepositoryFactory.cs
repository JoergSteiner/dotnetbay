using DotNetBay.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.EF;

namespace DotNetBay.Test.Storage
{
    class EFMainRepositoryFactory : IRepositoryFactory
    {

      private IMainRepository repo;

        public IMainRepository CreateMainRepository()
        {
          repo = new EFMainRepository();
          return repo;
        }

        public void Dispose()
        {
          repo = null;
        }
    }
}
