﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postie.Api.Data;

namespace Postie.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210628191658_AddedUrlToBoard")]
    partial class AddedUrlToBoard
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Postie.Api.Models.Db.Board", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Postie.Api.Models.Db.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentCommentID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PostID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ParentCommentID");

                    b.HasIndex("PostID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Postie.Api.Models.Db.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BoardID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("BoardID");

                    b.HasIndex("CreatedByID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Postie.Api.Models.Db.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Postie.Api.Models.Db.Board", b =>
                {
                    b.HasOne("Postie.Api.Models.Db.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Postie.Api.Models.Db.Comment", b =>
                {
                    b.HasOne("Postie.Api.Models.Db.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("Postie.Api.Models.Db.Comment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentID");

                    b.HasOne("Postie.Api.Models.Db.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostID");

                    b.Navigation("CreatedBy");

                    b.Navigation("ParentComment");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Postie.Api.Models.Db.Post", b =>
                {
                    b.HasOne("Postie.Api.Models.Db.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardID");

                    b.HasOne("Postie.Api.Models.Db.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.Navigation("Board");

                    b.Navigation("CreatedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
