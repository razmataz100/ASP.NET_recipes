using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;

namespace Inlämningsuppgift1.Services.Interfaces
{
    public interface IRatingService
    {
        bool AddRating(int recipeId, RatingDTO ratingDto, int userId);
    }
}
