using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectMatchBack.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    idAccess = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Access__FF93766616CDF460", x => x.idAccess);
                });

            migrationBuilder.CreateTable(
                name: "AnimalType",
                columns: table => new
                {
                    idAnimalType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animalTypeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AnimalTy__2F24A3993AE8DA2B", x => x.idAnimalType);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    idDepartment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.idDepartment);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    idGender = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genderName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.idGender);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    idRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__E5045C54775D45A0", x => x.idRole);
                });

            migrationBuilder.CreateTable(
                name: "Breed",
                columns: table => new
                {
                    idBreed = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    breedName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    idAnimalType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Breed__E07BCBB91BCC26AC", x => x.idBreed);
                    table.ForeignKey(
                        name: "FK__Breed__idAnimalT__35BCFE0A",
                        column: x => x.idAnimalType,
                        principalTable: "AnimalType",
                        principalColumn: "idAnimalType");
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    idCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDepartment = table.Column<int>(type: "int", nullable: true),
                    cityName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__City__814F31DE7B563306", x => x.idCity);
                    table.ForeignKey(
                        name: "FK_City_Department",
                        column: x => x.idDepartment,
                        principalTable: "Department",
                        principalColumn: "idDepartment");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRole = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    birthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    idAccess = table.Column<int>(type: "int", nullable: true),
                    idCity = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3717C98256EB3AB3", x => x.idUser);
                    table.ForeignKey(
                        name: "FK__User__idAccess__412EB0B6",
                        column: x => x.idAccess,
                        principalTable: "Access",
                        principalColumn: "idAccess");
                    table.ForeignKey(
                        name: "FK__User__idCity__4222D4EF",
                        column: x => x.idCity,
                        principalTable: "City",
                        principalColumn: "idCity");
                    table.ForeignKey(
                        name: "FK__User__idRole__4316F928",
                        column: x => x.idRole,
                        principalTable: "Role",
                        principalColumn: "idRole");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    idNotifacation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__C24D00C423C065BF", x => x.idNotifacation);
                    table.ForeignKey(
                        name: "FK__Notificat__idUse__3C69FB99",
                        column: x => x.idUser,
                        principalTable: "User",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    idPublication = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOwner = table.Column<int>(type: "int", nullable: true),
                    animalName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    idCity = table.Column<int>(type: "int", nullable: true),
                    weight = table.Column<double>(type: "float", nullable: true),
                    idGender = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: true),
                    idAnimalType = table.Column<int>(type: "int", nullable: false),
                    idBreed = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publicat__ECEE91EED6DB5C2C", x => x.idPublication);
                    table.ForeignKey(
                        name: "FK_Publication_Gender",
                        column: x => x.idGender,
                        principalTable: "Gender",
                        principalColumn: "idGender");
                    table.ForeignKey(
                        name: "FK__Publicati__idAni__3D5E1FD2",
                        column: x => x.idAnimalType,
                        principalTable: "AnimalType",
                        principalColumn: "idAnimalType");
                    table.ForeignKey(
                        name: "FK__Publicati__idBre__3E52440B",
                        column: x => x.idBreed,
                        principalTable: "Breed",
                        principalColumn: "idBreed");
                    table.ForeignKey(
                        name: "FK__Publicati__idCit__3F466844",
                        column: x => x.idCity,
                        principalTable: "City",
                        principalColumn: "idCity");
                    table.ForeignKey(
                        name: "FK__Publicati__idOwn__403A8C7D",
                        column: x => x.idOwner,
                        principalTable: "User",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    idComment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPublication = table.Column<int>(type: "int", nullable: true),
                    idUser = table.Column<int>(type: "int", nullable: true),
                    idCommentFk = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__0370297E9127B084", x => x.idComment);
                    table.ForeignKey(
                        name: "FK__Comment__idComme__36B12243",
                        column: x => x.idCommentFk,
                        principalTable: "Comment",
                        principalColumn: "idComment");
                    table.ForeignKey(
                        name: "FK__Comment__idPubli__37A5467C",
                        column: x => x.idPublication,
                        principalTable: "Publication",
                        principalColumn: "idPublication");
                    table.ForeignKey(
                        name: "FK__Comment__idUser__38996AB5",
                        column: x => x.idUser,
                        principalTable: "User",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    idImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPublication = table.Column<int>(type: "int", nullable: false),
                    DataImage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image1", x => x.idImage);
                    table.ForeignKey(
                        name: "FK_Image1_Publication",
                        column: x => x.idPublication,
                        principalTable: "Publication",
                        principalColumn: "idPublication");
                });

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    idMovement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSeller = table.Column<int>(type: "int", nullable: true),
                    idBuyer = table.Column<int>(type: "int", nullable: true),
                    idPublication = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movement__5B3BB2F5E4A1C9F0", x => x.idMovement);
                    table.ForeignKey(
                        name: "FK__Movement__idBuye__398D8EEE",
                        column: x => x.idBuyer,
                        principalTable: "User",
                        principalColumn: "idUser");
                    table.ForeignKey(
                        name: "FK__Movement__idPubl__3A81B327",
                        column: x => x.idPublication,
                        principalTable: "Publication",
                        principalColumn: "idPublication");
                    table.ForeignKey(
                        name: "FK__Movement__idSell__3B75D760",
                        column: x => x.idSeller,
                        principalTable: "User",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breed_idAnimalType",
                table: "Breed",
                column: "idAnimalType");

            migrationBuilder.CreateIndex(
                name: "IX_City_idDepartment",
                table: "City",
                column: "idDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_idCommentFk",
                table: "Comment",
                column: "idCommentFk");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_idPublication",
                table: "Comment",
                column: "idPublication");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_idUser",
                table: "Comment",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Image_idPublication",
                table: "Image",
                column: "idPublication");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_idBuyer",
                table: "Movement",
                column: "idBuyer");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_idPublication",
                table: "Movement",
                column: "idPublication");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_idSeller",
                table: "Movement",
                column: "idSeller");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_idUser",
                table: "Notification",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_idAnimalType",
                table: "Publication",
                column: "idAnimalType");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_idBreed",
                table: "Publication",
                column: "idBreed");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_idCity",
                table: "Publication",
                column: "idCity");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_idGender",
                table: "Publication",
                column: "idGender");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_idOwner",
                table: "Publication",
                column: "idOwner");

            migrationBuilder.CreateIndex(
                name: "IX_User_idAccess",
                table: "User",
                column: "idAccess");

            migrationBuilder.CreateIndex(
                name: "IX_User_idCity",
                table: "User",
                column: "idCity");

            migrationBuilder.CreateIndex(
                name: "IX_User_idRole",
                table: "User",
                column: "idRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Breed");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AnimalType");

            migrationBuilder.DropTable(
                name: "Access");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
