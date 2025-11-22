using HrSystem.Application.SupportTickets.Abstractions;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using HrSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystem.Infrastructure.Repositories
{
    public class SupportTicketRepository(AppDbContext dp) : ISupportTicketRepository

    {
        private readonly AppDbContext _dp = dp;

        public async Task AddAsync(SupportTicket entity, CancellationToken ct)
        {
            await _dp.SupportTickets.AddAsync(entity, ct);
            await _dp.SaveChangesAsync(ct);
        }



        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var ticket = await _dp.SupportTickets.FindAsync([id], ct)
                ?? throw new KeyNotFoundException($"Support ticket with ID {id} not found.");

            _dp.SupportTickets.Remove(ticket);
            
            await _dp.SaveChangesAsync(ct);
        }




        public async Task<SupportTicket?> GetByIdAsync(Guid id, CancellationToken ct)
        {
           return await _dp.SupportTickets
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(t => t.Id == id, ct);
        }




        public async Task<(IReadOnlyList<SupportTicket> Items, int Total)> ListAsync
            (Guid? employeeId, TicketStatus? status, string? category, int page, int pageSize, CancellationToken ct)
        {
            var entityQuery = _dp.SupportTickets
                .AsQueryable();

            if (employeeId.HasValue)
                entityQuery = entityQuery.Where(t => t.EmployeeId == employeeId.Value);


            if (status.HasValue)
                entityQuery = entityQuery.Where(t => t.Status == status.Value);


            if (!string.IsNullOrWhiteSpace(category))
                entityQuery = entityQuery.Where(t => t.Category == category);


            var total = await entityQuery.CountAsync();


            var items = await entityQuery
                .OrderByDescending(t => t.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);

        }




        public async Task UpdateAsync(SupportTicket entity, CancellationToken ct)
        {
            _dp.SupportTickets.Update(entity);
            await _dp.SaveChangesAsync(ct);
        }
    }
}
