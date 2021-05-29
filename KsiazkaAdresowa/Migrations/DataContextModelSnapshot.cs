﻿// <auto-generated />
using System;
using KsiazkaAdresowa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KsiazkaAdresowa.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KsiazkaAdresowa.Data.ContactData", b =>
                {
                    b.Property<int>("ContactDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactDataId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("ContactsData");
                });

            modelBuilder.Entity("KsiazkaAdresowa.Data.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeOfAdding")
                        .HasColumnType("datetime2");

                    b.Property<int>("TypeOfAppliciant")
                        .HasColumnType("int");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("KsiazkaAdresowa.Data.TeleAddress", b =>
                {
                    b.Property<int>("TeleAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<string>("Home")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBuilding")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLocal")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Post")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeleAddressId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("TeleAddresses");
                });

            modelBuilder.Entity("KsiazkaAdresowa.Data.ContactData", b =>
                {
                    b.HasOne("KsiazkaAdresowa.Data.Person", "PersonSource")
                        .WithOne("ContactData")
                        .HasForeignKey("KsiazkaAdresowa.Data.ContactData", "PersonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PersonSource");
                });

            modelBuilder.Entity("KsiazkaAdresowa.Data.TeleAddress", b =>
                {
                    b.HasOne("KsiazkaAdresowa.Data.Person", "PersonSource")
                        .WithOne("TeleAddressData")
                        .HasForeignKey("KsiazkaAdresowa.Data.TeleAddress", "PersonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PersonSource");
                });

            modelBuilder.Entity("KsiazkaAdresowa.Data.Person", b =>
                {
                    b.Navigation("ContactData");

                    b.Navigation("TeleAddressData");
                });
#pragma warning restore 612, 618
        }
    }
}
