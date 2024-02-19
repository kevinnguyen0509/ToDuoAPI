using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Repository
{
    public class AdventuresRespository : GenericRepository<Adventures>, IAdventures
    {
        private readonly ToDuoDbContext _context;
        public AdventuresRespository(ToDuoDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Exists(string address)
        {
            Adventures adventure = await _context.Adventures.FirstOrDefaultAsync(db => db.Address == address);
            if (adventure != null)
            {
                return true;
            }
            
            return false;
        }

        public async Task<IEnumerable<Adventures>> FilteredSearch(FilterDto filterDto, int limitBy)
        {
            var category = await _context.ToDuoCategories.FirstOrDefaultAsync(db => db.Name == filterDto.Category);
            var state = await _context.ToDuoStates.FirstOrDefaultAsync(db => db.Name == filterDto.State);
            var city = await _context.Adventures.FirstOrDefaultAsync(db => db.City.Trim() == filterDto.City.Trim());

            var tags = filterDto.Tags?.Split(new[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(tag => tag.Trim().Replace(" ", ""))
                                      .Where(tag => !string.IsNullOrEmpty(tag))
                                      .ToList();


            IQueryable<Adventures> query = _context.Adventures;

            if (category != null)
            {
                query = query.Where(db => db.ToDuoCategoryId == category.Id);
            }

            if (state != null)
            {
                query = query.Where(db => db.ToDuoStatesID == state.Id);
            }

            if (city != null)
            {
                query = query.Where(db => db.City == filterDto.City.Trim());
            }

            if (tags != null && tags.Count > 0)
            {
                foreach (var tag in tags)
                {
                    query = query.Where(db => db.Title.Contains(tag) || db.Description.Contains(tag) || db.Tags.Contains(tag));
                }
            }

            var adventures = await query.Take(limitBy).ToListAsync();
            return adventures;
        }

        public async Task<List<AdventureDto>> GetAdventures(int limitBy)
        {
            var adventures = await _context.Adventures
            .Include(a => a.ToDuoCategory)
            .Select(a => new AdventureDto
            {
                Id = a.Id,
                OwnerId = a.OwnerId,
                Title = a.Title,
                ImageURL = a.ImageURL,
                Description = a.Description,
                WebsiteURL = a.WebsiteURL,
                CreatedDate = a.CreatedDate,
                Address = a.Address,
                City = a.City,
                State = a.ToDuoStates.Name,
                Tags = a.Tags,
                Hours = a.Hours,
                SwipeCount = a.SwipeCount,
                CategoryName = a.ToDuoCategory.Name // Only the category name
            })
            .OrderByDescending(db => db.Id)
            .Take(limitBy)
            .ToListAsync();

            return adventures;
        }
    }
}
