using Inlämningsuppgift1.Data.Contexts;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;

namespace Inlämningsuppgift1.Repository.Repos
{
    public class RatingRepo : IRatingRepo
    {
        private readonly ApplicationDbContext _context;

        public RatingRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }
    }
}
