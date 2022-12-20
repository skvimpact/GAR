using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations.Gar
{
    public partial class N2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fias");

            migrationBuilder.CreateTable(
                name: "ADDR_OBJ",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTGUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CHANGEID = table.Column<long>(type: "bigint", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TYPENAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LEVEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OPERTYPEID = table.Column<int>(type: "int", nullable: false),
                    PREVID = table.Column<long>(type: "bigint", nullable: true),
                    NEXTID = table.Column<long>(type: "bigint", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTUAL = table.Column<int>(type: "int", nullable: false),
                    ISACTIVE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDR_OBJ", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ADDR_OBJ_TYPES",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    LEVEL = table.Column<int>(type: "int", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDR_OBJ_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ADM_HIERARCHY",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTID = table.Column<long>(type: "bigint", nullable: false),
                    PARENTOBJID = table.Column<long>(type: "bigint", nullable: true),
                    CHANGEID = table.Column<long>(type: "bigint", nullable: false),
                    REGIONCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AREACODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CITYCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLACECODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLANCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STREETCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PREVID = table.Column<long>(type: "bigint", nullable: true),
                    NEXTID = table.Column<long>(type: "bigint", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTIVE = table.Column<int>(type: "int", nullable: false),
                    PATH = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADM_HIERARCHY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HOUSE_TYPES",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOUSE_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HOUSES",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTGUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CHANGEID = table.Column<long>(type: "bigint", nullable: false),
                    HOUSENUM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDNUM1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDNUM2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HOUSETYPE = table.Column<int>(type: "int", nullable: true),
                    ADDTYPE1 = table.Column<int>(type: "int", nullable: true),
                    ADDTYPE2 = table.Column<int>(type: "int", nullable: true),
                    OPERTYPEID = table.Column<int>(type: "int", nullable: false),
                    PREVID = table.Column<long>(type: "bigint", nullable: true),
                    NEXTID = table.Column<long>(type: "bigint", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTUAL = table.Column<int>(type: "int", nullable: false),
                    ISACTIVE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOUSES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MUN_HIERARCHY",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTID = table.Column<long>(type: "bigint", nullable: false),
                    PARENTOBJID = table.Column<long>(type: "bigint", nullable: true),
                    CHANGEID = table.Column<long>(type: "bigint", nullable: false),
                    OKTMO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PREVID = table.Column<long>(type: "bigint", nullable: true),
                    NEXTID = table.Column<long>(type: "bigint", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTIVE = table.Column<int>(type: "int", nullable: false),
                    PATH = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUN_HIERARCHY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OBJECT_LEVELS",
                schema: "fias",
                columns: table => new
                {
                    LEVEL = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBJECT_LEVELS", x => x.LEVEL);
                });

            migrationBuilder.CreateTable(
                name: "PARAM",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    OBJECTID = table.Column<long>(type: "bigint", nullable: false),
                    CHANGEID = table.Column<long>(type: "bigint", nullable: true),
                    CHANGEIDEND = table.Column<long>(type: "bigint", nullable: false),
                    TYPEID = table.Column<int>(type: "int", nullable: false),
                    VALUE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARAM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PARAM_TYPES",
                schema: "fias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARAM_TYPES", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADDR_OBJ",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "ADDR_OBJ_TYPES",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "ADM_HIERARCHY",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "HOUSE_TYPES",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "HOUSES",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "MUN_HIERARCHY",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "OBJECT_LEVELS",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "PARAM",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "PARAM_TYPES",
                schema: "fias");
        }
    }
}
