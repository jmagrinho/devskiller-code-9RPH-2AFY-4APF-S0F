using System;
using System.Collections.Generic;
using System.Linq;
using Model.Models;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }

        public Comment Get(Guid id)
        {
            return _context.Comments.FirstOrDefault(x => x.Id == id);
        }

        public Comment Create(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            var postAssociated = _context.Posts.FirstOrDefault(x => x.Id == comment.PostId);

            if (postAssociated != null)
            {
                _context.Comments.Add(comment);

                _context.SaveChanges();

                return comment;
            }

            return null;
            
        }

        public Comment Update(Comment comment)
        {
            var commentToUpdate = _context.Comments.FirstOrDefault(x => x.Id == comment.Id);
            if (commentToUpdate != null)
            {
                commentToUpdate.PostId = comment.PostId;
                commentToUpdate.Content = comment.Content;
                commentToUpdate.Author = comment.Author;
                commentToUpdate.CreationDate = comment.CreationDate;

                _context.SaveChanges();
            }

            return commentToUpdate;
        }

        public bool Delete(Guid id)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == id);

            if (comment != null)
            {

                _context.Comments.Remove(comment);

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<Comment> GetByPostId(Guid postId)
        {
            return _context.Comments.Where(x => x.PostId == postId).ToList();
        }
    }
}