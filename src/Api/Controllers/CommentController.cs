using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Model.DTOs;
using Model.Models;
using Repository.Interfaces;
using Repository.Repositories;

namespace Api.Controllers
{ 
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ILogger<CommentController> logger, IMapper mapper, ICommentRepository commentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Returns a list with information of all the Comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ReadCommentDTO>> GetAll()
        {
            try
            {
                var commentList = _commentRepository.GetAll();

                if (commentList != null)
                {
                    return Ok(_mapper.Map<IEnumerable<ReadCommentDTO>>(commentList));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NotFound();
        }

        /// <summary>
        /// Returns information from a single Comment
        /// </summary>
        /// <param name="id">Comment GUID</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public ActionResult<ReadCommentDTO> Get([FromRoute] Guid id)
        {
            try
            {

                var comment = _commentRepository.Get(id);

                if (comment != null)
                {
                    return Ok(_mapper.Map<ReadCommentDTO>(comment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NotFound();

        }

        /// <summary>
        /// Creates a new Comment object
        /// </summary>
        /// <param name="comment">Object of schema CreateCommentDTO</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ReadCommentDTO> Post([FromBody] CreateCommentDTO comment)
        {
            try
            {
                var commentToCreate = _mapper.Map<CreateCommentDTO, Comment>(comment);

                var result = _mapper.Map<ReadCommentDTO>(_commentRepository.Create(commentToCreate));

                return Ok(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NoContent();
        }

        /// <summary>
        /// Updates the information of a Comment
        /// </summary>
        /// <param name="id">GUID of the comment to be updated</param>
        /// <param name="commentUpdateDTO">Information of the comment to update - Object of schema UpdateCommentDTO</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public ActionResult<ReadCommentDTO> Put([FromRoute] Guid id, [FromBody] UpdateCommentDTO commentUpdateDTO)
        {
            try
            {
                var commentFromRepository = _commentRepository.Get(id);

                if (commentFromRepository == null)
                {
                    return NotFound();
                }

                _mapper.Map(commentUpdateDTO, commentFromRepository);

                var result = _mapper.Map<ReadCommentDTO>(_commentRepository.Update(commentFromRepository));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return NoContent();

        }

        /// <summary>
        /// Deletes the specified Comment entry
        /// </summary>
        /// <param name="id">Comment GUID</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _commentRepository.Delete(id);
                if(result)
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                
            }

            return NoContent();

        }
    }
}