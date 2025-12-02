using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class CategoriaSecundariaMap : IEntityTypeConfiguration<CategoriaSecundaria>
    {

        public void Configure(EntityTypeBuilder<CategoriaSecundaria> builder)
        {
            builder.ToTable("CategoriaSecundaria")
                .HasKey(c => c.IdCategoriaSecundaria);
           

        }

    }
}
