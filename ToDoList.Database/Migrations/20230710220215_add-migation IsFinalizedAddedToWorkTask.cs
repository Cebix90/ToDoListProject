using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Database.Migrations
{
    /// <inheritdoc />
    public partial class addmigationIsFinalizedAddedToWorkTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "WorkTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "WorkTasks");
        }
    }
}
