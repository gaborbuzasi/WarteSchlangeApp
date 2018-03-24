﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WarteSchlange.API.Models.CompanyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Email");

                    b.Property<int>("ImageId");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.ImagesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.LogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("ErrorLevel");

                    b.Property<DateTime>("OccuredTime");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.OpeningTimeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Close");

                    b.Property<DateTime>("Open");

                    b.HasKey("Id");

                    b.ToTable("OpeningTimes");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.QueueEntryModel", b =>
                {
                    b.Property<int>("QueueId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("EntryTime");

                    b.Property<int>("Id");

                    b.Property<string>("IdentificationCode");

                    b.Property<int>("Priority");

                    b.HasKey("QueueId", "UserId");

                    b.HasAlternateKey("Id");

                    b.ToTable("QueueEntries");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.QueueModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowMultipleEntries");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Description");

                    b.Property<int>("ImageId");

                    b.Property<string>("Location");

                    b.Property<int>("MaxLength");

                    b.Property<string>("Name");

                    b.Property<int>("OpeningTimeId");

                    b.Property<bool>("RequireSignup");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ImageId");

                    b.HasIndex("OpeningTimeId");

                    b.ToTable("Queues");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WarteSchlange.API.Models.CompanyModel", b =>
                {
                    b.HasOne("WarteSchlange.API.Models.ImagesModel", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WarteSchlange.API.Models.QueueModel", b =>
                {
                    b.HasOne("WarteSchlange.API.Models.CompanyModel", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WarteSchlange.API.Models.ImagesModel", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WarteSchlange.API.Models.OpeningTimeModel", "OpeningTime")
                        .WithMany()
                        .HasForeignKey("OpeningTimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WarteSchlange.API.Models.UserModel", b =>
                {
                    b.HasOne("WarteSchlange.API.Models.CompanyModel", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
