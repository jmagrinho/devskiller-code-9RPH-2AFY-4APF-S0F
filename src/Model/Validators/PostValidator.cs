
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.Models;
using System;

namespace Model.Validators
{
    public class PostValidator
    {
        public static void ValidateCreatePost(CreatePostDTO postDTO)
        {
            if (postDTO.Id == null) throw new ArgumentNullException("Post Id cannot be null");
            if (postDTO.Title == null) throw new ArgumentNullException("Post Title cannot be null");
            if (postDTO.Content == null) throw new ArgumentNullException("Post Content cannot be null");
            if (postDTO.CreationDate == null) throw new ArgumentNullException("Post CreationDate cannot be null");

            if (postDTO.Title.Length > 30) throw new ArgumentException("Post Title cannot exceed maximun length of 30 characters");
            if (postDTO.Content.Length > 1200) throw new ArgumentException("Post Cpntent cannot exceed maximun length of 1200 characters");
        }

        public static void ValidateUpdatePost(UpdatePostDTO postDTO)
        {
            if (postDTO.Id == null) throw new ArgumentNullException("Post Id cannot be null");
            if (postDTO.Title == null) throw new ArgumentNullException("Post Title cannot be null");
            if (postDTO.Content == null) throw new ArgumentNullException("Post Content cannot be null");
            if (postDTO.CreationDate == null) throw new ArgumentNullException("Post CreationDate cannot be null");

            if (postDTO.Title.Length > 30) throw new ArgumentException("Post Title cannot exceed maximun length of 30 characters");
            if (postDTO.Content.Length > 1200) throw new ArgumentException("Post Cpntent cannot exceed maximun length of 1200 characters");
        }

    }
}
