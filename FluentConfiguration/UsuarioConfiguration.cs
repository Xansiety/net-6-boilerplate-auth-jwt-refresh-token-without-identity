﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET6_JWT_Refresh_Token_WithOut_Identity.Entities.User;

namespace NET6_JWT_Refresh_Token_WithOut_Identity.FluentConfiguration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.Property(p => p.Id)
                .IsRequired();
        builder.Property(p => p.Nombres)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.ApellidoPaterno)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.EmailNormalized)
                .HasMaxLength(200);
        builder.Property(p => p.Password)
               .IsRequired();

        builder.HasMany(p => p.Roles).WithMany(p => p.Usuarios)
            .UsingEntity<UsuariosRoles>(
                j => j
                    .HasOne(pt => pt.Rol)
                    .WithMany(t => t.UsuariosRoles)
                    .HasForeignKey(pt => pt.RolId),
                j => j
                    .HasOne(pt => pt.Usuario)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(pt => pt.UsuarioId),
                j =>
                {
                    j.HasKey(t => new { t.UsuarioId, t.RolId });
                });

        //builder.HasMany(p => p.RefreshTokens)
        //    .WithOne(p => p.Usuario)
        //    .HasForeignKey(p => p.UsuarioId);

    }
}