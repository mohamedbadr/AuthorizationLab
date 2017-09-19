using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationLab.Entities;

namespace AuthorizationLab.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        static readonly List<Document> Documents = new List<Document> {
            new Document { Id = 1, Author = "Mohamed" },
            new Document { Id = 2, Author = "someoneelse" }
        };


        public IEnumerable<Document> Get()
        {
            return Documents;
        }

        public Document Get(int id)
        {
            return (Documents.FirstOrDefault(d => d.Id == id));
        }
    }
}
