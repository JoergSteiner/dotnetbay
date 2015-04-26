using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.Test.Storage
{
  internal class EFMainRepositoryTests : MainRepositoryTestBase
  {

    public EFMainRepositoryTests()
    {
      var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
    }

    protected override Interfaces.IRepositoryFactory CreateFactory()
    {
      return new EFMainRepositoryFactory();
    }
  }
}