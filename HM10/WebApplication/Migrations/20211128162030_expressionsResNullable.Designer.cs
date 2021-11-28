﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.DB;

#nullable disable

namespace WebApplication.Migrations
{
    [DbContext(typeof(ComputedExpressionsContext))]
    [Migration("20211128162030_expressionsResNullable")]
    partial class expressionsResNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication.DB.ComputedExpression", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte>("Op")
                        .HasColumnType("tinyint");

                    b.Property<decimal?>("Res")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("V1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("V2")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ComputedExpressions");
                });
#pragma warning restore 612, 618
        }
    }
}
