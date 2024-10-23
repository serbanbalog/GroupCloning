﻿// <auto-generated />
using GroupCloning;
using GroupCloning.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GroupCloning.Migrations
{
    [DbContext(typeof(GroupCloningDbContext))]
    [Migration("20241023153848_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupCloning.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<int>("IdentifierInGroup")
                        .HasColumnType("int");

                    b.Property<string>("Prop1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prop2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prop3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupNumber", "IdentifierInGroup")
                        .IsUnique();

                    b.ToTable("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
