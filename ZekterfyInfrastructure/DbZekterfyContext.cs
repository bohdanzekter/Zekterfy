using System;
using System.Collections.Generic;
using ZekterfyDomain.Model;
using Microsoft.EntityFrameworkCore;

//namespace ZekterfyDomain.Model;
namespace ZekterfyInfrastructure;

public partial class DbZekterfyContext : DbContext
{
    public DbZekterfyContext()
    {
    }

    public DbZekterfyContext(DbContextOptions<DbZekterfyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<AuthorAlbum> AuthorAlbums { get; set; }

    public virtual DbSet<AuthorGenre> AuthorGenres { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Follower> Followers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Queue> Queues { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongAuthor> SongAuthors { get; set; }

    public virtual DbSet<SongGenre> SongGenres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=labdb;Username=student;Password=postgres;SSL Mode=Disable");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("albums_pkey");

            entity.ToTable("albums");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Pseudonym)
                .HasColumnType("character varying")
                .HasColumnName("pseudonym");
        });

        modelBuilder.Entity<AuthorAlbum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("author_albums");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");

            entity.HasOne(d => d.Album).WithMany()
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album");

            entity.HasOne(d => d.Author).WithMany()
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_author");
        });

        modelBuilder.Entity<AuthorGenre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("author_genres");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Author).WithMany()
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("author_id");

            entity.HasOne(d => d.Genre).WithMany()
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("genre_id");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("favorites_pkey");

            entity.ToTable("favorites");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Added)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_id");
        });

        modelBuilder.Entity<Follower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("followers_pkey");

            entity.ToTable("followers");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Author).WithMany()
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("fk_author_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Info)
                .HasColumnType("character varying")
                .HasColumnName("info");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("history_pkey");

            entity.ToTable("history");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.UserId, "history_user_id_index");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.PlayedAt).HasColumnName("played_at");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("history_user_id_fkey");
        });

        modelBuilder.Entity<Queue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("queue_pkey");

            entity.ToTable("queue");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_id");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reports_pkey");

            entity.ToTable("reports");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Reason)
                .HasMaxLength(250)
                .HasColumnName("reason");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Song).WithMany()
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("fk_song");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("songs_pkey");

            entity.ToTable("songs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.GenreId)
                .HasColumnType("integer")
                .HasColumnName("genre_id");
            entity.Property(e => e.Lenght).HasColumnName("lenght");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.NumOfStreams).HasColumnName("num_of_streams");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_album_id");
        });

        modelBuilder.Entity<SongAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("song_authors");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Author).WithMany()
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("author_id");

            entity.HasOne(d => d.Song).WithMany()
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("song_id");
        });

        modelBuilder.Entity<SongGenre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("song_genres");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Genre).WithMany()
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("genre_id");

            entity.HasOne(d => d.Song).WithMany()
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("song_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasColumnType("character varying")
                .HasColumnName("avatar_url");
            //entity.Property(e => e.FolowersCount).HasColumnName("folowers_count");
            //entity.Property(e => e.FolowsCount).HasColumnName("folows_count");
            //entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            //entity.Property(e => e.Listening).HasColumnName("listening");
            //entity.Property(e => e.Password)
            //    .HasMaxLength(15)
            //    .HasColumnName("password");
            //entity.Property(e => e.PreferedGenreId).HasColumnName("prefered_genre_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
