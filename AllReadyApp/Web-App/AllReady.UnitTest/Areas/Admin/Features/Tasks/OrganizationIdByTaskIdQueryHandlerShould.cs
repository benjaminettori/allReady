﻿using System.Threading.Tasks;
using AllReady.Areas.Admin.Features.Tasks;
using AllReady.Models;
using Xunit;

namespace AllReady.UnitTest.Areas.Admin.Features.Tasks
{
    public class OrganizationIdByTaskIdQueryHandlerShould : InMemoryContextTest
    {
        private readonly AllReadyTask task;
        private const int TaskId = 1;
        private const int OrganizationId = 2;

        public OrganizationIdByTaskIdQueryHandlerShould()
        {
            task = new AllReadyTask { Id = TaskId, Organization = new Organization { Id = OrganizationId } };

            Context.Tasks.Add(task);
            Context.Tasks.Add(new AllReadyTask { Id = 2 });
            Context.SaveChanges();
        }

        [Fact]
        public async Task ReturnCorrectData()
        {
            var message = new OrganizationIdByTaskIdQuery { TaskId = TaskId };

            var sut = new OrganizationIdByTaskIdQueryHandler(Context);
            var organizationId = await sut.Handle(message);

            Assert.Equal(organizationId, task.Organization.Id);
        }
    }
}