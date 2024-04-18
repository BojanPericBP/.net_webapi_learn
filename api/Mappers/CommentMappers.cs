using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.Comments;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment from)
        {
            return new CommentDto
            {
                Id = from.Id,
                Title = from.Title,
                Content = from.Content,
                StockId = from.StockId,
                CreatedOn = from.CreatedOn
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentDto from, int stockId)
        {
            return new Comment
            {
                Title = from.Title,
                Content = from.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentDto from)
        {
            return new Comment
            {
                Title = from.Title,
                Content = from.Content
            };
        }
    }
}