using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;
using Inlämningsuppgift1.Services.Interfaces;

namespace Inlämningsuppgift1.Services.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepo _ratingRepo;
        private readonly IRecipeRepo _recipeRepo;
        private readonly IUserRepo _userRepo;

        public RatingService(IRatingRepo ratingRepo, IRecipeRepo recipeRepo, IUserRepo userRepo)
        {
            _ratingRepo = ratingRepo;
            _recipeRepo = recipeRepo;
            _userRepo = userRepo;

        }

        public bool AddRating(int recipeId, RatingDTO ratingDto, int currentUserId)
        {
            var recipe = _recipeRepo.GetRecipeById(recipeId);
            var currentUser = _userRepo.GetUserById(currentUserId);

            if (recipe == null || recipe.User.UserId == currentUserId)
            {
                return false;
            }
            else if (recipe.Ratings != null && recipe.Ratings.Any(r => r.User != null && r.User.UserId == currentUserId))
            {
                return false;
            }

            var rating = new Rating
            {
                RatingValue = ratingDto.RatingValue,
                Recipe = recipe,
                User = currentUser
            };

            recipe.Ratings ??= new List<Rating>();
            recipe.Ratings.Add(rating);
            _ratingRepo.AddRating(rating);
            return true;
        }
    }
}
