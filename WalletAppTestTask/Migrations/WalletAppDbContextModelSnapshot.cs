﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WalletAppTestTask.DbContext;

#nullable disable

namespace WalletAppTestTask.Migrations
{
    [DbContext(typeof(WalletAppDbContext))]
    partial class WalletAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WalletAppTestTask.DbContext.AccountContext", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.BankCardContext", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_account");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<long>("BankId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_bank");

                    b.Property<int>("DueStatus")
                        .HasColumnType("integer")
                        .HasColumnName("due_status");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("BankId");

                    b.ToTable("BankCards");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.BankContext", b =>
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

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.TransactionContext", b =>
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

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric")
                        .HasColumnName("total");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("payment_type");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.BankCardContext", b =>
                {
                    b.HasOne("WalletAppTestTask.DbContext.AccountContext", "Account")
                        .WithMany("BankCards")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WalletAppTestTask.DbContext.BankContext", "Bank")
                        .WithMany("Cards")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.TransactionContext", b =>
                {
                    b.HasOne("WalletAppTestTask.DbContext.BankCardContext", "Card")
                        .WithMany("Transactions")
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.AccountContext", b =>
                {
                    b.Navigation("BankCards");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.BankCardContext", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("WalletAppTestTask.DbContext.BankContext", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
