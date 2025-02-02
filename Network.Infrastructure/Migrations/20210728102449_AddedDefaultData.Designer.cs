﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Network.Infrastructure;

namespace Network.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210728102449_AddedDefaultData")]
    partial class AddedDefaultData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Network.Core.Shared.Entities.Birthday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Birthdays");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            DateOfBirth = new DateTime(1991, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Петров Петр Петрович",
                            Note = "Не забыть поздравить хоть в этот раз",
                            Relationship = "Брат"
                        },
                        new
                        {
                            Id = 101,
                            DateOfBirth = new DateTime(1986, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Петя Александров",
                            Note = "Новая заметка",
                            Relationship = "Брат"
                        },
                        new
                        {
                            Id = 102,
                            DateOfBirth = new DateTime(1956, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Алексей Иванов Александрович",
                            Note = "Заметка 3",
                            Relationship = "Друг"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
