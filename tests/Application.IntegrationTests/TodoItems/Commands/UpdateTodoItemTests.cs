using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TestCA.Application.Common.Exceptions;
using TestCA.Application.TodoItems.Commands.CreateTodoItem;
using TestCA.Application.TodoItems.Commands.UpdateTodoItem;
using TestCA.Application.TodoLists.Commands.CreateTodoList;
using TestCA.Domain.Entities;
using static Testing;

namespace TestCA.Application.IntegrationTests.TodoItems.Commands
{
    public class UpdateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            var command = new UpdateTodoItemCommand
            {
                Id = itemId,
                Title = "Updated Item Title"
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Title.Should().Be(command.Title);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
