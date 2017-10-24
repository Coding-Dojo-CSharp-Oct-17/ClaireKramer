using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using weddingplanner.Models;

namespace weddingplanner.Migrations
{
    [DbContext(typeof(WeddingContext))]
    [Migration("20171024233033_SecondMigrations")]
    partial class SecondMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("weddingplanner.Models.Rsvp", b =>
                {
                    b.Property<int>("RsvpId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<int>("WeddingId");

                    b.HasKey("RsvpId");

                    b.HasIndex("UserId");

                    b.HasIndex("WeddingId");

                    b.ToTable("Rsvps");
                });

            modelBuilder.Entity("weddingplanner.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConfirmPW");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("weddingplanner.Models.Wedding", b =>
                {
                    b.Property<int>("WeddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Bride")
                        .IsRequired();

                    b.Property<string>("Groom")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.Property<DateTime>("WeddingDate");

                    b.HasKey("WeddingId");

                    b.HasIndex("UserId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("weddingplanner.Models.Rsvp", b =>
                {
                    b.HasOne("weddingplanner.Models.User", "User")
                        .WithMany("Rsvps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("weddingplanner.Models.Wedding", "Wedding")
                        .WithMany("Guests")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("weddingplanner.Models.Wedding", b =>
                {
                    b.HasOne("weddingplanner.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
