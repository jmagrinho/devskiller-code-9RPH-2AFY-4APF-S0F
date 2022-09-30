using System;
using System.Collections.Generic;
using System.Linq;
using Model.Models;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public Post Get(Guid id)
        {
            return _context.Posts.FirstOrDefault(x => x.Id == id);
            
        }

        public Post Create(Post post)
        {

            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Posts.Add(post);

            _context.SaveChanges();

            return post;

        }

        public Post Update(Post post)
        {
            var postToUpdate = _context.Posts.FirstOrDefault(x => x.Id == post.Id);

            if (postToUpdate != null)
            {
                postToUpdate.Title = post.Title;
                postToUpdate.Content = post.Content;
                postToUpdate.CreationDate = post.CreationDate;

                _context.SaveChanges();


            }

            return postToUpdate;
        }

        public bool Delete(Guid id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            var comments = _context.Comments.Where(x => x.PostId == id).ToList();

            if (post != null)
            {

                _context.Posts.Remove(post);


                foreach (var comment in comments)
                {
                    _context.Comments.Remove(comment);
                }

                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}