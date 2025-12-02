using SistemaFacturacion.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class MarcaMap : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("Marca")
                .HasKey(c => c.IdMarca);
            builder.Property(c => c.Nombre)
                .HasMaxLength(1000);
            builder.Property(c => c.Descripcion)
                .HasMaxLength(256);
        }
    }
}
