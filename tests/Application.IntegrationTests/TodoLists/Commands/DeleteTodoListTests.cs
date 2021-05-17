using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TestCA.Application.Common.Exceptions;
using TestCA.Application.TodoLists.Commands.CreateTodoList;
using TestCA.Application.TodoLists.Commands.DeleteTodoList;
using TestCA.Domain.Entities;
using static Testing;

namespace TestCA.Application.IntegrationTests.TodoLists.Commands
{
    public class DeleteTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoListId()
        {
            var command = new DeleteTodoListCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoList()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            await SendAsync(new DeleteTodoListCommand
            {
                Id = listId
            });

            var list = await FindAsync<TodoList>(listId);

            list.Should().BeNull();
        }
    }
}
