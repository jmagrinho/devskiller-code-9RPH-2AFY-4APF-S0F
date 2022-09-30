
using Model.DTOs;
using System;

namespace Model.Validators
{
    public class CommentValidator
    {
        public static void ValidateCreateComment(CreateCommentDTO commentDTO)
        {
            if (commentDTO.Id == null) throw new ArgumentNullException("Comment Id cannot be null");
            if (commentDTO.Author == null) throw new ArgumentNullException("Comment Author cannot be null");
            if (commentDTO.Content == null) throw new ArgumentNullException("Comment Content cannot be null");
            if (commentDTO.CreationDate == null) throw new ArgumentNullException("Comment CreationDate cannot be null");

            if (commentDTO.Author.Length > 30) throw new ArgumentException("Comment Author cannot exceed maximun length of 30 characters");
            if (commentDTO.Content.Length > 1200) throw new ArgumentException("Comment content cannot exceed maximun length of 1200 characters");
        }

        public static void ValidateUpdateComment(UpdateCommentDTO commentDTO)
        {
            if (commentDTO.Id == null) throw new ArgumentNullException("Comment Id cannot be null");
            if (commentDTO.Author == null) throw new ArgumentNullException("Comment Author cannot be null");
            if (commentDTO.Content == null) throw new ArgumentNullException("Comment Content cannot be null");
            if (commentDTO.CreationDate == null) throw new ArgumentNullException("Comment CreationDate cannot be null");

            if (commentDTO.Author.Length > 30) throw new ArgumentException("Comment Author cannot exceed maximun length of 30 characters");
            if (commentDTO.Content.Length > 1200) throw new ArgumentException("Comment Content cannot exceed maximun length of 1200 characters");
        }

    }
}
