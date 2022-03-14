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
    [Migration("20220313160441_4")]
    partial class _4
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

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "WorldId");

                    b.HasIndex("WorldId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("CoreApi.Models.DB.PlayerCurrent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId", "WorldId")
                        .IsUnique();

                    b.HasIndex("TribeId", "WorldId");

                    b.ToTable("PlayerCurrents");
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

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId", "WorldId");

                    b.HasIndex("TribeId", "WorldId");

                    b.ToTable("PlayerHistory");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Tribe", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id", "WorldId");

                    b.HasIndex("WorldId");

                    b.ToTable("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.TribeCurrent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TribeId", "WorldId")
                        .IsUnique();

                    b.ToTable("TribeCurrent");
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

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TribeId", "WorldId");

                    b.ToTable("TribeHistory");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Village", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.Property<int>("PositionX")
                        .HasColumnType("int");

                    b.Property<int>("PositionY")
                        .HasColumnType("int");

                    b.HasKey("Id", "WorldId");

                    b.HasIndex("WorldId");

                    b.ToTable("Village");
                });

            modelBuilder.Entity("CoreApi.Models.DB.VillageCurrent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("PositionX")
                        .HasColumnType("int");

                    b.Property<int>("PositionY")
                        .HasColumnType("int");

                    b.Property<int?>("TribeId")
                        .HasColumnType("int");

                    b.Property<int>("VillageId")
                        .HasColumnType("int");

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId", "WorldId");

                    b.HasIndex("TribeId", "WorldId");

                    b.HasIndex("VillageId", "WorldId")
                        .IsUnique();

                    b.ToTable("VillageCurrent");
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

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId", "WorldId");

                    b.HasIndex("VillageId", "WorldId");

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

            modelBuilder.Entity("CoreApi.Models.DB.Player", b =>
                {
                    b.HasOne("CoreApi.Models.DB.World", "World")
                        .WithMany("Players")
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("World");
                });

            modelBuilder.Entity("CoreApi.Models.DB.PlayerCurrent", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Player", "Player")
                        .WithOne("Current")
                        .HasForeignKey("CoreApi.Models.DB.PlayerCurrent", "PlayerId", "WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithMany("CurrentPlayers")
                        .HasForeignKey("TribeId", "WorldId");

                    b.Navigation("Player");

                    b.Navigation("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.PlayerHistory", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Player", "Player")
                        .WithMany("History")
                        .HasForeignKey("PlayerId", "WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithMany("Players")
                        .HasForeignKey("TribeId", "WorldId");

                    b.Navigation("Player");

                    b.Navigation("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Tribe", b =>
                {
                    b.HasOne("CoreApi.Models.DB.World", "World")
                        .WithMany("Tribes")
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("World");
                });

            modelBuilder.Entity("CoreApi.Models.DB.TribeCurrent", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithOne("Current")
                        .HasForeignKey("CoreApi.Models.DB.TribeCurrent", "TribeId", "WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.TribeHistory", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithMany("History")
                        .HasForeignKey("TribeId", "WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tribe");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Village", b =>
                {
                    b.HasOne("CoreApi.Models.DB.World", "World")
                        .WithMany("Villages")
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("World");
                });

            modelBuilder.Entity("CoreApi.Models.DB.VillageCurrent", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Player", "Player")
                        .WithMany("VillagesCurrent")
                        .HasForeignKey("PlayerId", "WorldId");

                    b.HasOne("CoreApi.Models.DB.Tribe", "Tribe")
                        .WithMany("Villages")
                        .HasForeignKey("TribeId", "WorldId");

                    b.HasOne("CoreApi.Models.DB.Village", "Village")
                        .WithOne("Current")
                        .HasForeignKey("CoreApi.Models.DB.VillageCurrent", "VillageId", "WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Tribe");

                    b.Navigation("Village");
                });

            modelBuilder.Entity("CoreApi.Models.DB.VillageHistory", b =>
                {
                    b.HasOne("CoreApi.Models.DB.Player", "Player")
                        .WithMany("Villages")
                        .HasForeignKey("PlayerId", "WorldId");

                    b.HasOne("CoreApi.Models.DB.Village", "Village")
                        .WithMany("History")
                        .HasForeignKey("VillageId", "WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Village");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Player", b =>
                {
                    b.Navigation("Current");

                    b.Navigation("History");

                    b.Navigation("Villages");

                    b.Navigation("VillagesCurrent");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Tribe", b =>
                {
                    b.Navigation("Current");

                    b.Navigation("CurrentPlayers");

                    b.Navigation("History");

                    b.Navigation("Players");

                    b.Navigation("Villages");
                });

            modelBuilder.Entity("CoreApi.Models.DB.Village", b =>
                {
                    b.Navigation("Current");

                    b.Navigation("History");
                });

            modelBuilder.Entity("CoreApi.Models.DB.World", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Tribes");

                    b.Navigation("Villages");
                });
#pragma warning restore 612, 618
        }
    }
}
