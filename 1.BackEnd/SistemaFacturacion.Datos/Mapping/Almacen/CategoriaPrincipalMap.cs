using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class CategoriaPrincipalMap : IEntityTypeConfiguration<CategoriaPrincipal>
    {

        public void Configure(EntityTypeBuilder<CategoriaPrincipal> builder)
        {
            builder.ToTable("CategoriaPrincipal")
                .HasKey(c => c.IdCategoriaPrincipal);
            builder.Property(c => c.Nombre)
                .HasMaxLength(100);
            builder.Property(c => c.Descripcion)
                .HasMaxLength(256);

        }

    }
}
