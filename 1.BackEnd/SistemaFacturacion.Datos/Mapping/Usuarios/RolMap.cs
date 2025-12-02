using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Usuarios
{
    public class RolMap  : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol")
                .HasKey(c => c.IdRol);
            builder.Property(c => c.Nombre)
                .HasMaxLength(100);
            builder.Property(c => c.Descripcion)
                .HasMaxLength(256);
        }
    }
}
