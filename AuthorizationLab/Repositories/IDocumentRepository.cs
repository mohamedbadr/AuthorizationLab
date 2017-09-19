using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationLab.Entities;

namespace AuthorizationLab.Repositories
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> Get();

        Document Get(int id);
    }
}
