using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAll();
        Post Get(Guid id);
        Post Create(Post post);
        Post Update(Post post);
        bool Delete(Guid id);
    }
}
