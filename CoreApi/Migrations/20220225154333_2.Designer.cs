﻿// <auto-generated />
using System;
using CoreApi.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreApi.Migrations
{
    [DbContext(typeof(CustomContext))]
    [Migration("20220225154333_2")]
    partial class _2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoreApi.Models.DB.Player", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("CoreApi.Models.DB.PlayerHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("RA")
                        .HasColumnType("int");

                    b.Property<int>("RO")
                        .HasColumnType("int");

                    b.Property<int>("RS")
                        .HasColumnType("int");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.Property<int?>("TribeId")
                        .HasColumnType("int");

                    b.Property<int>("VillagesCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TribeId");

                    b.ToTable("PlayerHistory");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Tribe", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.TribeHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RA")
                        .HasColumnType("int");

                    b.Property<int>("RO")
                        .HasColumnType("int");

                    b.Property<int>("RS")
                        .HasColumnType("int");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TribeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TribeId");

                    b.ToTable("TribeHistory");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Village", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("PositionX")
                        .HasColumnType("int");

                    b.Property<int>("PositionY")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Village");
                });

            modelBuilder.Entity("CoreApi.Models.DB.VillageHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("VillageId");

                    b.ToTable("VillageHistory");
                });

            modelBuilder.Entity("CoreApi.Models.DB.World", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Domain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubDomain")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("World");
                });

            modelBuilder.Entity("CoreApi.Models.DB.PlayerHistory", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Player", "Player")
                        .WithMany("History")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithMany("Players")
                        .HasForeignKey("TribeId");

                    b.Navigation("Player");

                    b.Navigation("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.TribeHistory", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithMany("History")
                        .HasForeignKey("TribeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.VillageHistory", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Player", "Player")
                        .WithMany("Villages")
                        .HasForeignKey("PlayerId");

                    b.HasOne("CoreApi.Models.DB.Village", "Village")
                        .WithMany("History")
                        .HasForeignKey("VillageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Village");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Player", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Villages");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Tribe", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Village", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}