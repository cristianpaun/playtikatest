using Application.DTO;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products
{
    public class List
    {
        public class Query : IRequest<List<ProductDTO>> { }

        public class Handler : IRequestHandler<Query, List<ProductDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ProductDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await _context.Products.ToListAsync();
                return _mapper.Map<List<ProductDTO>>(products);
            }
        }
    }
}
