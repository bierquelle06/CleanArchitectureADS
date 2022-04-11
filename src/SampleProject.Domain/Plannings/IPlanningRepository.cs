using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Domain.Plannings
{
    public interface IPlanningRepository
    {
        Task<List<Planning>> GetByIdsAsync(List<PlanningId> ids);

        Task<List<Planning>> GetAllAsync();

        Task AddAsync(Planning planning);
    }
}