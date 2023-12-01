﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WalletAppTestTask.DbContext;

#nullable disable

namespace WalletAppTestTask.Migrations
{
    [DbContext(typeof(WalletAppDbContext))]
    [Migration("20231201231549_WalletApp_v2")]
    partial class WalletApp_v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WalletAppTestTask.Models.Bank", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.BankCard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BankId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_bank");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("uid");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("BankCard");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AuthorizedUser")
                        .HasColumnType("text")
                        .HasColumnName("authorized_user_name");

                    b.Property<long>("BankCardId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_bank_card");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completed_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("icon");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transaction_name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric")
                        .HasColumnName("sum");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("payment_type");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("DueStatus")
                        .HasColumnType("integer")
                        .HasColumnName("due_status");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.BankCard", b =>
                {
                    b.HasOne("WalletAppTestTask.Models.Bank", "Bank")
                        .WithMany("Cards")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WalletAppTestTask.Models.User", "User")
                        .WithMany("BankCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.Transaction", b =>
                {
                    b.HasOne("WalletAppTestTask.Models.BankCard", "Card")
                        .WithMany("Transactions")
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.Bank", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.BankCard", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("WalletAppTestTask.Models.User", b =>
                {
                    b.Navigation("BankCards");
                });
#pragma warning restore 612, 618
        }
    }
}
