using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll();
        Comment Get(Guid id);
        Comment Create(Comment comment);
        Comment Update(Comment comment);
        bool Delete(Guid id);
        IEnumerable<Comment> GetByPostId(Guid postId);
    }
}
