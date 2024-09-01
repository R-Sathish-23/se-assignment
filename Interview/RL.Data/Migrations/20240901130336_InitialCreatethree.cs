using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class InitialCreatethree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanUserDatas",
                table: "PlanUserDatas");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "PlanUserDatas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PlanUserDatas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanUserDatas",
                table: "PlanUserDatas",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanUserDatas",
                table: "PlanUserDatas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlanUserDatas");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "PlanUserDatas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanUserDatas",
                table: "PlanUserDatas",
                column: "PlanId");
        }
    }
}
