using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Database.Migrations
{
    /// <inheritdoc />
    public partial class smallChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_WorkTasks_TaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_UserId",
                table: "WorkTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_WorkTasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "WorkTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_UserId",
                table: "WorkTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_WorkTasks_TaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_UserId",
                table: "WorkTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_WorkTasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "WorkTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_UserId",
                table: "WorkTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
