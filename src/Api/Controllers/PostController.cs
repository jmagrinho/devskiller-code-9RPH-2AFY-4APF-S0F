using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Model.DTOs;
using Model.Models;
using Model.Validators;
using Repository.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public PostController(ILogger<PostController> logger, IMapper mapper, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _postRepository = postRepository;
            _commentRepository = commentRepository;

        }

        /// <summary>
        /// Returns a list with information of all the posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ReadPostDTO>> GetAll()
        {
            try
            {
                var postList = _postRepository.GetAll();

                if (postList != null)
                {
                    return Ok(_mapper.Map<IEnumerable<ReadPostDTO>>(postList));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NotFound();
        }

        /// <summary>
        /// Returns information from a single Post
        /// </summary>
        /// <param name="id">Post GUID</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public ActionResult<ReadPostDTO> Get([FromRoute] Guid id)
        {
            try
            {

                var post = _postRepository.Get(id);

                if (post != null)
                {
                    return Ok(_mapper.Map<ReadPostDTO>(post));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NotFound();

        }

        /// <summary>
        /// Creates a new Post object
        /// </summary>
        /// <param name="post">Object of schema CreatePostDTO</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ReadPostDTO> Post([FromBody] CreatePostDTO post)
        {
            try
            {
                PostValidator.ValidateCreatePost(post);
                var postToCreate = _mapper.Map<CreatePostDTO, Post>(post);

                var result = _mapper.Map<ReadPostDTO>(_postRepository.Create(postToCreate));

                return Ok(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NoContent();
        }

        /// <summary>
        /// Updates the information of a Post
        /// </summary>
        /// <param name="id">Post GUID</param>
        /// <param name="postUpdateDTO">Information of the Post to update - Object of schema UpdatePostDTO</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public ActionResult<ReadPostDTO> Put([FromRoute] Guid id, [FromBody] UpdatePostDTO postUpdateDTO)
        {
            try
            {
                PostValidator.ValidateUpdatePost(postUpdateDTO);
                var postFromRepository = _postRepository.Get(id);

                if (postFromRepository == null)
                {
                    return NotFound();
                }

                _mapper.Map(postUpdateDTO, postFromRepository);

                var result =  _mapper.Map<ReadPostDTO>(_postRepository.Update(postFromRepository));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NoContent();

        }

        /// <summary>
        /// Deletes the specified Post entry and all the Comments associated to it
        /// </summary>
        /// <param name="id">Post GUID</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _postRepository.Delete(id);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NoContent();

        }

        /// <summary>
        /// Returns a list with information of all the Comments associated to a Post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}/comments")]
        public ActionResult<IEnumerable<ReadCommentDTO>> GetComments([FromRoute] Guid id)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<ReadCommentDTO>>(_commentRepository.GetByPostId(id));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NoContent();
        }
    }
}