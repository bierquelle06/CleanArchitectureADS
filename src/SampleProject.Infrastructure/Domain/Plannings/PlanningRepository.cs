using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Plannings;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Domain.Plannings
{
    public class PlanningRepository : IPlanningRepository
    {
        private readonly OrdersContext _context;

        public PlanningRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Planning planning)
        {
            await this._context.Plannings.AddAsync(planning);
        }

        public async Task<List<Planning>> GetByIdsAsync(List<PlanningId> ids)
        {
            return await this._context.Plannings.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Planning>> GetAllAsync()
        {
            return await this._context.Plannings.ToListAsync();
        }
    }
}