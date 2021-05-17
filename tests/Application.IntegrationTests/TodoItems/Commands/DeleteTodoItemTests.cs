using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TestCA.Application.Common.Exceptions;
using TestCA.Application.TodoItems.Commands.CreateTodoItem;
using TestCA.Application.TodoItems.Commands.DeleteTodoItem;
using TestCA.Application.TodoLists.Commands.CreateTodoList;
using TestCA.Domain.Entities;
using static Testing;

namespace TestCA.Application.IntegrationTests.TodoItems.Commands
{
    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var list = await FindAsync<TodoItem>(listId);

            list.Should().BeNull();
        }
    }
}
