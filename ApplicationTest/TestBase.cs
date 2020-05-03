using Application.MapProfile;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTest
{
    public class TestBase
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));
            var mapper = config.CreateMapper();
            return mapper;
        }

        public DataContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new DataContext(builder.Options);

            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
