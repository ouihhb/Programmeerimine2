using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedResult<Product>>
{
    private readonly ApplicationDbContext _context;

    public GetProductsHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        return await _context.Products
            .OrderBy(p => p.Id)
            .GetPagedAsync(pageNumber, pageSize);
    }
}