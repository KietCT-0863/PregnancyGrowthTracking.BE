using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> GetTagByName(string tagName);
        Task<int> AddTagAsync(Tag tag);
    }
}
