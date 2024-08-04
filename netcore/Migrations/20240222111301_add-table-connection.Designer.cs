﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace net.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240222111301_add-table-connection")]
    partial class addtableconnection
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_net.Models.Chat", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("idAdmin")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("idLastMessage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isGroup")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("idAdmin");

                    b.HasIndex("idLastMessage")
                        .IsUnique()
                        .HasFilter("[idLastMessage] IS NOT NULL");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("_net.Models.Connection", b =>
                {
                    b.Property<string>("idConnection")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("idUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isConnected")
                        .HasColumnType("bit");

                    b.HasKey("idConnection");

                    b.HasIndex("idUser");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("_net.Models.MemberChat", b =>
                {
                    b.Property<Guid>("idChat")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("idUser")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("idChat", "idUser");

                    b.HasIndex("idUser");

                    b.ToTable("MemberChats");
                });

            modelBuilder.Entity("_net.Models.Message", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("idChat")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("idReplyMessage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("idSender")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("idChat");

                    b.HasIndex("idReplyMessage");

                    b.HasIndex("idSender");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("_net.Models.MessageFile", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("idMessage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("idMessage");

                    b.ToTable("MessageFiles");
                });

            modelBuilder.Entity("_net.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("avartar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("_net.Models.Chat", b =>
                {
                    b.HasOne("_net.Models.User", "admin")
                        .WithMany()
                        .HasForeignKey("idAdmin");

                    b.HasOne("_net.Models.Message", "lastMessage")
                        .WithOne("messageLastChat")
                        .HasForeignKey("_net.Models.Chat", "idLastMessage");

                    b.Navigation("admin");

                    b.Navigation("lastMessage");
                });

            modelBuilder.Entity("_net.Models.Connection", b =>
                {
                    b.HasOne("_net.Models.User", "user")
                        .WithMany("connections")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("_net.Models.MemberChat", b =>
                {
                    b.HasOne("_net.Models.Chat", "chat")
                        .WithMany("memberChats")
                        .HasForeignKey("idChat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_net.Models.User", "user")
                        .WithMany("groupMembers")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chat");

                    b.Navigation("user");
                });

            modelBuilder.Entity("_net.Models.Message", b =>
                {
                    b.HasOne("_net.Models.Chat", "chat")
                        .WithMany("messages")
                        .HasForeignKey("idChat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_net.Models.Message", "replyMessage")
                        .WithMany()
                        .HasForeignKey("idReplyMessage");

                    b.HasOne("_net.Models.User", "sender")
                        .WithMany()
                        .HasForeignKey("idSender")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chat");

                    b.Navigation("replyMessage");

                    b.Navigation("sender");
                });

            modelBuilder.Entity("_net.Models.MessageFile", b =>
                {
                    b.HasOne("_net.Models.Message", "message")
                        .WithMany()
                        .HasForeignKey("idMessage")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("message");
                });

            modelBuilder.Entity("_net.Models.Chat", b =>
                {
                    b.Navigation("memberChats");

                    b.Navigation("messages");
                });

            modelBuilder.Entity("_net.Models.Message", b =>
                {
                    b.Navigation("messageLastChat")
                        .IsRequired();
                });

            modelBuilder.Entity("_net.Models.User", b =>
                {
                    b.Navigation("connections");

                    b.Navigation("groupMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
