using SistemaFacturacion.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
            builder.ToTable("Articulo")
                .HasKey(a => a.IdArticulo);
        }

    }
}
