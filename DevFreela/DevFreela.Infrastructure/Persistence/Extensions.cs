﻿using DevFreela.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public static class Extensions {
    public static async Task<PaginationResult<T>> GetPaged<T>(
        this IQueryable<T> query,
        int currentPage,
        int pageSize
    ) where T : class {
        PaginationResult<T> result = new() {
            CurrentPage = currentPage,
            PageSize = pageSize,
            ItemsCount = await query.CountAsync()
        };

        double pageCount = (double)result.ItemsCount / pageSize;
        result.TotalPages = (int)Math.Ceiling(pageCount);

        int skip = (currentPage - 1) * pageSize;

        result.Data = await query.Skip(skip).Take(pageSize).ToListAsync();

        return result;
    }
}