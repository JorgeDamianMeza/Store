﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.ShoppingCart.Persistence;

#nullable disable

namespace Store.ShoppingCart.Migrations
{
    [DbContext(typeof(CartContext))]
    partial class CartContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Store.ShoppingCart.Model.SessionCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("SessionCart");
                });

            modelBuilder.Entity("Store.ShoppingCart.Model.SessionCartDetail", b =>
                {
                    b.Property<int>("SessionCartDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SelectedProduct")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SessionCartId")
                        .HasColumnType("int");

                    b.HasKey("SessionCartDetailId");

                    b.HasIndex("SessionCartId");

                    b.ToTable("SessionCartDetail");
                });

            modelBuilder.Entity("Store.ShoppingCart.Model.SessionCartDetail", b =>
                {
                    b.HasOne("Store.ShoppingCart.Model.SessionCart", "SessionCart")
                        .WithMany("DetailsList")
                        .HasForeignKey("SessionCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SessionCart");
                });

            modelBuilder.Entity("Store.ShoppingCart.Model.SessionCart", b =>
                {
                    b.Navigation("DetailsList");
                });
#pragma warning restore 612, 618
        }
    }
}
